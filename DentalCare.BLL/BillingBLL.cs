using DentalCare.BLL.Interface;
using DentalCare.DAL;
using DentalCare.DAL.Interface;
using DentalCare.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.BLL
{
    public class BillingBLL: IBillingBLL
    {
        private readonly IDentalCareRepository _dentalCareRepo;
        public BillingBLL(IDentalCareRepository dentalCareRepo) 
        {
            _dentalCareRepo = dentalCareRepo;
        }

        public List<Bill> GenerateBills(string appName)
        {
            DateTime currentDateTime = DateTime.Now;
            //Generate bill records first
            var services = _dentalCareRepo.Service.FindWhere(s => s.ServiceDate.Year == currentDateTime.Year && s.ServiceDate.Month == currentDateTime.Month && s.IsActive && !s.IsDeleted).ToList();

            var patientIds = services.Select(s => s.PatientId).Distinct().ToList();

            //Generate bills by Patient
            foreach (var patientId in patientIds)
            {
                //Get distinct dates for the same patient, each service date will generate one bill
                var serviceDates = services.Select(s => s.ServiceDate.Date).Distinct().ToList();

                foreach (var serviceDate in serviceDates)
                {
                    var dayServices = services.Where(s => s.PatientId == patientId && s.ServiceDate.Date.Equals(serviceDate));
                    var bill = new tblBill
                    {
                        Amount = dayServices.Sum(s => s.Amount ?? 0),
                        PaymentDueDate = serviceDate.AddDays(90),
                        IsPaid = true,
                        IsActive = true,
                        IsDeleted = false,
                        CreatedDate = currentDateTime,
                        CreatedBy = appName
                    };

                    _dentalCareRepo.Bill.Add(bill, true);

                    foreach(var dayService in dayServices)
                    {
                        var billService = new tblBillService
                        {
                            BillId= bill.BillId,
                            ServiceId = dayService.ServiceId,
                            IsActive = true,
                            IsDeleted = false,
                            CreatedDate = currentDateTime,
                            CreatedBy = appName
                        };

                        _dentalCareRepo.BillService.Add(billService, true);
                    }          
                }
            }


            //Query unpaid bills 
            return QueryBills();
        }

        private List<Bill> QueryBills()
        {
            List<Bill> result = new List<Bill>();
            var dbBills = _dentalCareRepo.Bill.FindWhere(b => !b.IsPaid && b.IsActive && !b.IsDeleted);
            foreach (var dbBill in dbBills)
            {
                var bill = MappingBill(dbBill);
                if (bill != null)
                {
                    result.Add(bill);
                }
            }

            return result;
        }

        public Bill GetBill(int billId)
        {
            var dbBill =  _dentalCareRepo.Bill.FindWhere(b => b.BillId == billId).FirstOrDefault();
            if (dbBill == null) return null;

            return MappingBill(dbBill);
        }

        private Bill MappingBill(tblBill dbBill)
        {
            if (dbBill == null || dbBill.tblBillServices == null) return null;

            var services = (from svc in _dentalCareRepo.Service.FindWhere(s => s.IsActive && !s.IsDeleted)
                           join billsvc in _dentalCareRepo.BillService.FindWhere(bs => bs.BillId == dbBill.BillId) on svc.ServiceId equals billsvc.ServiceId
                           select svc).ToList();

            if (services.Count == 0) return null;

            var patient = services.First().tblPatient;

            Bill bill = new Bill();
            bill.Id = dbBill.BillId;
            bill.PatientName = patient.PatientName;

            if (patient.tblInsuranceProvider == null)
            {
                bill.BillingName = patient.PatientName;
                bill.BillingAddress = patient.tblAddress == null ? String.Empty : ComposeAddress(patient.tblAddress);
            }
            else
            {
                bill.BillingName = patient.tblInsuranceProvider.ProviderName;
                bill.BillingAddress = patient.tblInsuranceProvider.tblAddress == null ? String.Empty : ComposeAddress(patient.tblInsuranceProvider.tblAddress);
            }

            bill.BillingAmount = services.Sum(s => s.Amount??0);

            //Assume the payment due date has been populated when the bill is creatd in tblBill table.
            bill.DaysPastDue = CalculateDaysPastDue(dbBill.PaymentDueDate);

            return bill;
        }

        private string ComposeAddress(tblAddress address)
        {
            StringBuilder sb = new StringBuilder();
            string space = " ";
            if (!String.IsNullOrEmpty(address.AddressLine1))
            {
                sb.Append(address.AddressLine1);
            }

            AddAddressSection(sb, space, address.AddressLine2);
            AddAddressSection(sb, space, address.AddressLine3);
            AddAddressSection(sb, space, address.City);
            AddAddressSection(sb, space, address.State);
            AddAddressSection(sb, space, address.Zip);

            return sb.ToString();
        }

        private void AddAddressSection(StringBuilder sb, string space, string section)
        {
            if (!String.IsNullOrEmpty(section))
            {
                sb.Append(space);
                sb.Append(section);
            }
        }

        private int CalculateDaysPastDue(DateTime paymentDueDate)
        {
            if (paymentDueDate >= DateTime.Now) return 0;

            double days = (DateTime.Now.Date - paymentDueDate.Date).TotalDays;

            return Convert.ToInt32(days);
        }
    }
}


