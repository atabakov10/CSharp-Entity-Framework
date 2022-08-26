using System.Linq;
using AutoMapper;
using ProductShop.Dtos.Category;
using ProductShop.Dtos.CategoryProduct;
using ProductShop.Dtos.Input;
using ProductShop.Dtos.Product;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<UserInputDto, User>();
            this.CreateMap<ImportProductDto, Product>();
            this.CreateMap<CategoryInputDto, Category>();
            this.CreateMap<CategoryProductDto, CategoryProduct>();
            this.CreateMap<Product, ExportProductsInRangeDto>()
                .ForMember(d => d.SellerFullName, mo => mo
                    .MapFrom(s => $"{s.Seller.FirstName} {s.Seller.LastName}"));
            //Inner Dto
            this.CreateMap<Product, ExportUserSoldProductsDto>()
                .ForMember(x => x.BuyerFirstName,
                    mo => mo.MapFrom(s => s.Buyer.FirstName))
                .ForMember(x => x.BuyerLastName,
                    mo => mo.MapFrom(s => s.Buyer.LastName));
            //Outer Dto
            this.CreateMap<User, ExportUserWitgSoldProductsDto>()
                .ForMember(x => x.SoldProducts,
                    mo => mo.MapFrom(s =>
                        s.ProductsSold.Where(x => x.BuyerId.HasValue)));
            this.CreateMap<Category, ExportCategoriesByProductsCountDto>()
                .ForMember(x => x.Products, mo =>
                    mo.MapFrom(s => s.CategoryProducts.Count));
        }
    }
}
