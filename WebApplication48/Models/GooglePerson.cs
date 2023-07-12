using Newtonsoft.Json;

namespace WebApplication48.Models
{
    public class GooglePerson
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_verified")]
        public bool EmailVerified { get; set; }
    }
}
