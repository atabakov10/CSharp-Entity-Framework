using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            CarDealerContext context = new CarDealerContext();
            string xml = File.ReadAllText("../../../Datasets/cars.xml");

            string result = ImportCars(context, xml);
            Console.WriteLine(result);
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //Console.WriteLine("Database reset successfully!");
        }
        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Suppliers");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportSupplierDto[]), xmlRoot);

            using StringReader sr = new StringReader(inputXml);
            ImportSupplierDto[] supplierDtos = (ImportSupplierDto[])xmlSerializer.Deserialize(sr);

            Supplier[] suppliers = supplierDtos.Select(x => new Supplier()
            {
                Name = x.Name,
                IsImporter = x.IsImporter
            })
                .ToArray();
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Length}";
        }
        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Parts");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportPartsDto[]), xmlRoot);

            using StringReader sr = new StringReader(inputXml);
            ImportPartsDto[] partsDtos = (ImportPartsDto[])xmlSerializer.Deserialize(sr);

            ICollection<Part> parts = new List<Part>();

            foreach (ImportPartsDto pDto in partsDtos)
            {
                if (!context.Suppliers.Any(x => x.Id == pDto.SupplierId))
                {
                    continue;
                }
                Part part = new Part()
                {
                    Name = pDto.Name,
                    Price = pDto.Price,
                    Quantity = pDto.Quantity,
                    SupplierId = pDto.SupplierId
                };
                parts.Add(part);
            }
            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute("Cars");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCarsDto[]), xmlRoot);

            using StringReader sr = new StringReader(inputXml);
            ImportCarsDto[] carsDtos = (ImportCarsDto[])xmlSerializer.Deserialize(sr);
            ICollection<Car> cars = new List<Car>();
            foreach (ImportCarsDto cDto in carsDtos)
            {
                Car car = new Car()
                {
                    Make = cDto.Make,
                    Model = cDto.Model,
                    TravelledDistance = cDto.TraveledDistance
                };
                ICollection<PartCar> currCarParts = new List<PartCar>();
                foreach (int partId in cDto.Parts.Select(p=>p.Id).Distinct())
                {
                    if (!context.Parts.Any(p=> p.Id== partId))
                    {
                        continue;
                    }

                    currCarParts.Add(new PartCar()
                    {
                        Car = car,
                        PartId = partId
                    });
                }

                car.PartCars = currCarParts;
                cars.Add(car);
            }
                
            context.Cars.AddRange(cars);
            context.SaveChanges();
            return $"Successfully imported {cars.Count}";
        }

        private static T Deserialize<T>(string inputXml, string rootName)
        {
            XmlRootAttribute xmlRoot = new XmlRootAttribute(rootName);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), xmlRoot);

            using StringReader sr = new StringReader(inputXml);
            T dtos = (T)xmlSerializer.Deserialize(sr);
            return dtos;
        }
    }
}