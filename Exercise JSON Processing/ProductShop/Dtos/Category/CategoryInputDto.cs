using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace ProductShop.Dtos.Category
{
    [JsonObject]
    public class CategoryInputDto
    {
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }
    }
}
