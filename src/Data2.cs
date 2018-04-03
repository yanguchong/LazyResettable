using Newtonsoft.Json;

namespace LazyResettable
{
    internal sealed class Data2
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }


        [JsonProperty("test1")]
        public string Test1 { get; set; }

        [JsonProperty("test2")]
        public string Test2 { get; set; }

        [JsonProperty("test3")]
        public string Test3 { get; set; }

        [JsonProperty("test4")]
        public string Test4 { get; set; }
    }
}
