using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProductShop.Dtos.Product
{
    [JsonObject]
    public class ExportCategoriesByProductsCountDto
    {
        [JsonProperty("category")]
        public string Name { get; set; }
        [JsonProperty("productsCount")]
        public List<int> Products { get; set; }

        [JsonProperty("averagePrice")]
        public decimal Price { get; set; }
        [JsonProperty("totalRevenue")] 
        public decimal TotalRevenue { get; set; }
    }
}
