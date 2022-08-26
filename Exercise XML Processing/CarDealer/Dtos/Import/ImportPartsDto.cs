using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using CarDealer.Models;

namespace CarDealer.Dtos.Import
{
    [XmlType(nameof(Part))]
    public class ImportPartsDto
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
        [XmlElement("quantity")]
        public int Quantity { get; set; }
        [XmlElement("supplierId")]
        public int SupplierId { get; set; }
    }
}
