using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Dtos.Category;
using ProductShop.Dtos.CategoryProduct;
using ProductShop.Dtos.Input;
using ProductShop.Dtos.Product;
using ProductShop.Models;

namespace ProductShop
{
    
    public class StartUp
    {
        private static string filePath;
        
        public static void Main(string[] args)
        {
            Mapper.Initialize(cfg => cfg.AddProfile(typeof(ProductShopProfile)));
            var dbContext = new ProductShopContext();
            InitializeOutputFilePath("users-sold-products.json");
            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();
            //Console.WriteLine("Database copy was created!");

           // var usersJsonAsString = File.ReadAllText("../../../Datasets/products-in-range.json");
            string json = GetSoldProducts(dbContext);
            File.WriteAllText(filePath, json);
        }
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            UserInputDto[] userDtos = JsonConvert.DeserializeObject <UserInputDto[]>(inputJson);
            ICollection<User> users = new List<User>();

            foreach (UserInputDto uDto in userDtos)
            {
                User user = Mapper.Map<User>(uDto);
                users.Add(user);
            }
            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported { users.Count}";
        }
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            ImportProductDto[] productDto = JsonConvert.DeserializeObject<ImportProductDto[]>(inputJson);
            ICollection<Product> validProducts = new List<Product>();
            foreach (ImportProductDto pDto in productDto)
            {
                Product product = Mapper.Map<Product>(pDto);
                validProducts.Add(product);
            }
            context.Products.AddRange(validProducts);
            context.SaveChanges();
           return $"Successfully imported {validProducts.Count}";
        }
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            CategoryInputDto[] categoryDto = JsonConvert.DeserializeObject<CategoryInputDto[]>(inputJson);
            ICollection<Category> validCategories = new List<Category>();
            foreach (CategoryInputDto cDto in categoryDto)
            {
                if (cDto.Name ==null)
                {
                    continue;
                }
                Category category = Mapper.Map<Category>(cDto);
                validCategories.Add(category);
            }
            context.Categories.AddRange(validCategories);
            context.SaveChanges();
            return $"Successfully imported {validCategories.Count}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            CategoryProductDto[] categoryDto = JsonConvert.DeserializeObject<CategoryProductDto[]>(inputJson);
            ICollection<CategoryProduct> validCategoryProducts = new List<CategoryProduct>();
            foreach (CategoryProductDto cpDto in categoryDto)
            {
                CategoryProduct product = Mapper.Map<CategoryProduct>(cpDto);
                validCategoryProducts.Add(product);
            }
            context.CategoryProducts.AddRange(validCategoryProducts);
            context.SaveChanges();
            return $"Successfully imported {validCategoryProducts.Count}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p=> p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p=>p.Price)
                .ProjectTo<ExportProductsInRangeDto>()
                .ToArray();
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            return json;
        }
        public static string GetSoldProducts(ProductShopContext context)
        {
            ExportUserSoldProductsDto[] users = context.Users
                .Where(x => x.ProductsSold.Any(p => p.BuyerId.HasValue))
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ProjectTo<ExportUserSoldProductsDto>()
                .ToArray();
                 string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            return json;

        }
        private static void InitializeOutputFilePath(string fileName)
        {
            filePath =
                Path.Combine(Directory.GetCurrentDirectory(), "../../../Results/", fileName);
        }
    }
}