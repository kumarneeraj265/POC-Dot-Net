using CRUD_PRAC.DTOs.StripePay;
using CRUD_PRAC.DTOs.UserDTO;
using CRUD_PRAC.Models;

namespace CRUD_PRAC.Services
{
    public interface IStripePayService
    {
        Task<ServiceResponse<PaymentIntentDTO>> CreatePayementIntent(int playerId, PaymentIntentCreateRequest request);
        Task<CustomerModel> GetCustomerByEmail(string email, params PaymentModelInclude[] include);
        
        /// <summary>
        /// when you want to add a payment method for future payment for this particular customer.
        /// Use the return object depending depending the payment provider, e.g. for stripe use the IntentSecret as the ClientSecret
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<FuturePaymentIntentModel> PrepareForFuturePayment(string customerId);
        Task<List<PaymentMethodModel>> GetPaymentMethods(string customerId, PaymentMethodType paymentMethodType);
        Task<ServiceResponse<List<PaymentMethodModel>>> GetPaymentMethodsByCustomerEmail(int playerId, PaymentMethodType paymentMethodType);
      
    }
}
