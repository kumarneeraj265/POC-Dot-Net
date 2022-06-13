using Newtonsoft.Json;

namespace CRUD_PRAC.Models
{
    public class StripePay
    {
    }

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        public string? paymentMethodId { get; set; }
        public bool isSaveCard { get; set; }
    }
}
