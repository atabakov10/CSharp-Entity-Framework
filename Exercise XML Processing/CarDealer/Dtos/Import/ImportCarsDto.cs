using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using CarDealer.Models;

namespace CarDealer.Dtos.Import
{
    [XmlType(nameof(Car))]
    public class ImportCarsDto
    {
        [XmlElement("make")]
        public string Make { get; set; }
        [XmlElement("model")]
        public string Model { get; set; }
        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }
        [XmlArray("parts")]
        public ImportCarsIdDto[] Parts { get; set; }
    }
}
