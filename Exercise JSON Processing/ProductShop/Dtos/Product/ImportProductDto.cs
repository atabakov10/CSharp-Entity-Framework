﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;
using ProductShop.Common;

namespace ProductShop.Dtos.Product
{
    [JsonObject]
    public class ImportProductDto
    {
        [JsonProperty(nameof(Name))]
        [Required]
        [MinLength(GlobalConstants.ProductNameMinLength)]
        public string Name { get; set; }
        [JsonProperty(nameof(Price))]
        public decimal Price { get; set; }
        [JsonProperty(nameof(SellerId))]
        public int SellerId { get; set; }
        [JsonProperty(nameof(BuyerId))]
        public int? BuyerId { get; set; }
    }
}
