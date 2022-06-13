namespace CRUD_PRAC.DTOs.StripePay
{
    public class StripeCustomerDTO
    {
        public int id { get; set; }
        //public string objectname { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }

    }
}
