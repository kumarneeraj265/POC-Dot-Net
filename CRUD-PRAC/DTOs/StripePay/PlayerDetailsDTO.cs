using CRUD_PRAC.Models;
using CRUD_PRAC.Services;

namespace CRUD_PRAC.DTOs.StripePay
{
    public class PlayerDetailsDTO
    {
        public Member memberDetails { get; set; }
        public List<PaymentMethodModel> paymentMethods { get; set; }
    }
}
