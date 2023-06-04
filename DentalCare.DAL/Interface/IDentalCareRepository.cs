using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

using System.Linq;

namespace DentalCare.DAL.Interface
{

    public partial interface IDentalCareRepository
    {

        DentalCareEntities DentalCareContext { get; set; }
        ISqlRepository<tblAddress> Address { get; }
        ISqlRepository<tblBill> Bill { get; }
        ISqlRepository<tblPatient> Patient { get; }
        ISqlRepository<tblBillService> BillService { get; }
        ISqlRepository<tblService> Service { get; }

        ISqlRepository<tblInsuranceProvider> InsuranceProvider { get; }
    }
}
