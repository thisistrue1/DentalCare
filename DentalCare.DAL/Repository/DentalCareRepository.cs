using DentalCare.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

using System.Linq;

namespace DentalCare.DAL.Repository
{
    public partial class DentalCareRepository : IDentalCareRepository
    {
        #region Private Variables

        readonly ObjectContext context;
        const string ConnectionStringName = "DentalCareEntities";

        SqlRepository<tblAddress> address = null;
        SqlRepository<tblBill> bill = null;
        SqlRepository<tblBillService> billService = null;
        SqlRepository<tblService> service = null;
        SqlRepository<tblPatient> patient = null;
        SqlRepository<tblInsuranceProvider> insuranceProvider = null;
        #endregion

        #region Constructor 
        public DentalCareEntities DentalCareContext { get; set; }

        public DentalCareRepository()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings[ConnectionStringName]
                    .ConnectionString;


            DentalCareContext = new DentalCareEntities();
            context = ((IObjectContextAdapter)DentalCareContext).ObjectContext;
            context.ContextOptions.LazyLoadingEnabled = true;
        }

        public DentalCareRepository(string connectionString)
        {
            context = new ObjectContext(connectionString);
            context.ContextOptions.LazyLoadingEnabled = true;

            // hack to force the EntityFramework.SqlServer.dll to be copied when another project references this one
            var forceDllCopy = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DentalCareRepository(System.Data.Entity.DbContext cont)
        {
            context = ((IObjectContextAdapter)cont).ObjectContext;
            context.ContextOptions.LazyLoadingEnabled = true;
        }
        #endregion

        #region ISqlRepositories

 
        public ISqlRepository<tblAddress> Address
        {
            get
            {
                if (address == null)
                {
                    address = new SqlRepository<tblAddress>(context);
                }
                return address;
            }
        }

        public ISqlRepository<tblBill> Bill
        {
            get
            {
                if (bill == null)
                {
                    bill = new SqlRepository<tblBill>(context);
                }
                return bill;
            }
        }

        public ISqlRepository<tblBillService> BillService
        {
            get
            {
                if (billService == null)
                {
                    billService = new SqlRepository<tblBillService>(context);
                }
                return billService;
            }
        }

        public ISqlRepository<tblService> Service
        {
            get
            {
                if (service == null)
                {
                    service = new SqlRepository<tblService>(context);
                }
                return service;
            }
        }

        public ISqlRepository<tblPatient> Patient
        {
            get
            {
                if (patient == null)
                {
                    patient = new SqlRepository<tblPatient>(context);
                }
                return patient;
            }
        }

        public ISqlRepository<tblInsuranceProvider> InsuranceProvider
        {
            get
            {
                if (insuranceProvider == null)
                {
                    insuranceProvider = new SqlRepository<tblInsuranceProvider>(context);
                }
                return insuranceProvider;
            }
        }
        #endregion

    }
}
