using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("email_ccs")]
    public class EmailCcs
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user_email")]
        public string UserEmail { get; set; }

        [JsonProperty("action")]
        public UserEmailAction Action { get; set; }
    }
}
