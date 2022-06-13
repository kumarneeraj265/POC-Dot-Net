using CRUD_PRAC.DTOs.StripePay;
using CRUD_PRAC.Models;
using CRUD_PRAC.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;

namespace CRUD_PRAC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StripePayments : ControllerBase
    {
        private IStripePayService _stripePayService;
        public StripePayments(IStripePayService stripePayService)
        {
            _stripePayService = stripePayService;

        }


        [HttpPost("create-payment-intent")]
        public async Task<ActionResult<ServiceResponse<PaymentIntentDTO>>>Create(int playerId, [FromBody] PaymentIntentCreateRequest request)
        {
            return Ok(await _stripePayService.CreatePayementIntent(playerId, request));
        }


        [HttpGet("get-payment-method")]
        public async Task<ActionResult<ServiceResponse<List<PaymentMethodModel>>>>GetPaymentMethodsByCustomerEmail(int playerId)
        {
            return Ok(await _stripePayService.GetPaymentMethodsByCustomerEmail(playerId, PaymentMethodType.Card));
        }



    }
}
