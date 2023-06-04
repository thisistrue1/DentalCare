namespace BillingWebAPI.Model
{
    public class Bill
    {
        public int Id { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }

        public InsuranceProvider InsuranceProvider { get; set; }

    }
}
