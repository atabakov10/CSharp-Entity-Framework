using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using ProductShop.Common;

namespace ProductShop.Dtos.Input
{
    [JsonObject]
    public class UserInputDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        [Required]
        [MinLength(GlobalConstants.UserLastNameMinLength)]
        public string LastName { get; set; }
        [JsonProperty("age")]
        public int? Age { get; set; }
    }
}
