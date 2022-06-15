using AutoMapper;
using CRUD_PRAC.Data;
using CRUD_PRAC.DTOs.StripePay;
using CRUD_PRAC.DTOs.UserDTO;
using CRUD_PRAC.Models;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Collections;

namespace CRUD_PRAC.Services
{
    public class StripePayService : IStripePayService
    {

        private IMapper _mapper;
        private DataContext _context;

        public StripePayService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;

        }

        public Customer AddCustomer(CustomerCreateOptions options)
        {
            var service = new CustomerService();
            var custDetails = service.Create(options);
            return custDetails;

        }

        private int CalculateOrderAmount(Item[] items)
        {
            // Need to fetcht the amount from DB to create the payment intent
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            var amtToPay = 1;
            if (items.Any()) {
                if (items.Count() > 1) { 
                    amtToPay = 1500;
                }else if (items.Count() > 0)
                {
                    amtToPay = items[0].Id == "1" ? 1000 : 500;
                }
            }
            return amtToPay;
        }

        public async Task<ServiceResponse<PaymentIntentDTO>> CreatePayementIntent(int playerId, PaymentIntentCreateRequest request)
        {
            var serviceResponse = new ServiceResponse<PaymentIntentDTO>();
            var paymentIntentService = new PaymentIntentService();

            try
            {
                Customer customer = null;
                //FuturePaymentIntentModel setupIntent = new FuturePaymentIntentModel();
                var paymentIntentOptions = new PaymentIntentCreateOptions();
                var playerData = await _context.TempPlayers.Where(player => player.Id == playerId)
                    .FirstOrDefaultAsync(
                    );
                if (playerData != null)
                {
                    if (request.isSaveCard)
                    {
                        //if customer data exists in db
                        if (playerData?.StripeCustId == null)
                        {
                            //if stripe customer data not exists in db
                            var cust = new CustomerCreateOptions
                            {
                                Description = "Test Customer by neeraj",
                                Name = playerData?.Name,
                                Email = playerData?.Email,
                            };

                            customer = AddCustomer(cust);

                            //update stripe cust Id in own db records
                            playerData.StripeCustId = customer?.Id;
                            _context.TempPlayers.Attach(playerData);
                            _context.Entry(playerData).Property(x => x.StripeCustId).IsModified = true;
                            _context.SaveChanges();
                        }


                    }


                    paymentIntentOptions = new PaymentIntentCreateOptions
                    {
                        Amount = CalculateOrderAmount(request.Items),
                        Currency = "usd",
                        PaymentMethodTypes = new List<string> {
                              "card",
                             },
                    };

                    if (request.isSaveCard)
                    {
                        paymentIntentOptions.Customer = playerData?.StripeCustId;
                        paymentIntentOptions.SetupFutureUsage = "off_session";
                       
                    }
                    else if (request.paymentMethodId != null) {
                        paymentIntentOptions.PaymentMethod = request.paymentMethodId != null ? request.paymentMethodId : null;

                    }

                    var paymentIntent = paymentIntentService.Create(paymentIntentOptions);
                    serviceResponse.Data = new PaymentIntentDTO() { ClientSecret = paymentIntent.ClientSecret };
                    serviceResponse.Message = "Payment Intent created";
                    return serviceResponse;
                }
                else
                {
                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<FuturePaymentIntentModel> PrepareForFuturePayment(string customerId)
        {
            var options = new SetupIntentCreateOptions
            {
                Customer = customerId.ToString(),
                Expand = new List<string>()
                {
                    "customer"
                }
            };

            var service = new SetupIntentService();
            var intent = await service.CreateAsync(options);
            return new FuturePaymentIntentModel()
            {
                Id = intent.Id,
                IntentSecret = intent.ClientSecret,
                Customer = new CustomerModel(intent.Customer.Id)
                {
                    Email = intent.Customer.Email,
                    Name = intent.Customer.Name,
                }
            };
        }


        public async Task<ServiceResponse<List<PaymentMethodModel>>> GetPaymentMethodsByCustomerEmail(int playerId, PaymentMethodType paymentMethodType)
        {
            var serviceResponse = new ServiceResponse<List<PaymentMethodModel>>();
            List<PaymentMethodModel> paymentMethods = new List<PaymentMethodModel>();



            // just for the POC purpose we are making this query to fetch the cust id of stripe
            var playerData = await _context.TempPlayers.Where(player => player.Id == playerId)
                    .FirstOrDefaultAsync(p=> p.StripeCustId != null);

            if (playerData != null) {
                CustomerModel customer = await GetCustomerByEmail(playerData.Email);
                if (customer != null)
                {
                    var response = await this.GetPaymentMethods(customer.Id, paymentMethodType);
                    paymentMethods.AddRange(response);
                    serviceResponse.Data = paymentMethods;
                    return serviceResponse;
                }
                {
                    serviceResponse.Data = paymentMethods;
                    serviceResponse.Message = "No data found against Player Id" + playerData.Id;
                    return serviceResponse;
                }
            }
            else{
                serviceResponse.Data = paymentMethods;
                serviceResponse.Message = "No Player exists with provided details";
                return serviceResponse;
            }           
           
        }

        public async Task<List<PaymentMethodModel>> GetPaymentMethods(string customerId, PaymentMethodType paymentMethodType)
        {
            var options = new PaymentMethodListOptions
            {
                Customer = customerId,
                Type = paymentMethodType.ToString().ToLower()
            };

            var service = new PaymentMethodService();
            var paymentMethods = await service.ListAsync(options);


            List<PaymentMethodModel> result = new List<PaymentMethodModel>();
            foreach (var stripePaymentMethod in paymentMethods)
            {
                if (!Enum.TryParse(stripePaymentMethod.Type, true, out PaymentMethodType currPaymentMethodType))
                {
                    continue;
                }

                PaymentMethodModel currentPaymentMethod = new PaymentMethodModel(stripePaymentMethod.Id)
                {
                    Type = currPaymentMethodType
                };

                if (currPaymentMethodType == PaymentMethodType.Card)
                {
                    currentPaymentMethod.Card = new PaymentMethodCardModel()
                    {
                        Brand = stripePaymentMethod.Card.Brand,
                        Country = stripePaymentMethod.Card.Country,
                        ExpMonth = stripePaymentMethod.Card.ExpMonth,
                        ExpYear = stripePaymentMethod.Card.ExpYear,
                        Issuer = stripePaymentMethod.Card.Issuer,
                        Last4 = stripePaymentMethod.Card.Last4,
                        Description = stripePaymentMethod.Card.Description,
                        Fingerprint = stripePaymentMethod.Card.Fingerprint,
                        Funding = stripePaymentMethod.Card.Funding,
                        Iin = stripePaymentMethod.Card.Iin
                    };
                }

                result.Add(currentPaymentMethod);
            }
            return result;
        }


        public async Task<CustomerModel> GetCustomerByEmail(string email, params PaymentModelInclude[] includes)
        {
            var service = new CustomerService();
            var stripeCustomers = await service.ListAsync(new CustomerListOptions()
            {
                Email = email
            });

            if (!stripeCustomers.Any())
                return null;

            var stripeCustomer = stripeCustomers.Single();

            var customerModel = new CustomerModel(stripeCustomer.Id)
            {
                Email = email,
                Name = stripeCustomer.Name
            };
            if (includes.Any() && includes.Contains(PaymentModelInclude.PaymentMethods))
            {
                var paymentMethods = await this.GetPaymentMethods(stripeCustomer.Id, PaymentMethodType.Card);
                customerModel.PaymentMethods = paymentMethods;
            }

            return customerModel;
        }
     }
}
