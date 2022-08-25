using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProductShop.Dtos.CategoryProduct
{
    [JsonObject]
    public class CategoryProductDto
    {
        [JsonProperty(nameof(CategoryId))]
        public int CategoryId { get; set; }
        [JsonProperty(nameof(ProductId))]
        public int ProductId { get; set; }
    }
}
