using DentalCare.DAL;
using DentalCare.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.BLL.Interface
{
    public interface IBillingBLL
    {
        List<Bill> GenerateBills(string appName);

        Bill GetBill(int billId);
    }
}
