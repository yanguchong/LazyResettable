using Newtonsoft.Json;

namespace LazyResettable
{
    internal sealed class Data1
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName{ get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }
    }
}
