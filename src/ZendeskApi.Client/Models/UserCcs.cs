using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    public class UserCcs
    {
        [JsonProperty("users")]
        public List<UserCc> Users { get; set; }
    }

    public class UserCc
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
