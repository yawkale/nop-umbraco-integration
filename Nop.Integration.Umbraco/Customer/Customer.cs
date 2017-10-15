using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Nop.Integration.Umbraco.Customer
{
    [JsonObject(Title = "customer")]
    public class Customer
    {
        public Customer()
        {
            roles = new List<int>() { 3 };
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("role_ids")]
        public List<int> roles { get; set; }

        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        [Required]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        [Required]
        public string LastName { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }
    }
}