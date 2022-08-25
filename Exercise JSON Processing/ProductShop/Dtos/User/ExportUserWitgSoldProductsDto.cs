using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProductShop.Dtos.Product
{
    [JsonObject]
    public class ExportUserWitgSoldProductsDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("soldProducts")]
        public ExportUserSoldProductsDto[] SoldProducts { get; set; }
    }
}
