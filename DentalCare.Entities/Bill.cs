using System;

namespace DentalCare.Entities
{ 
    public class Bill
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        public decimal BillingAmount { get; set; }
        public int DaysPastDue { get; set; }

    }
}
