using AutoMapper;
using ProductCatalogService.Core.Domain.Entities;
using ProductCatalogService.Presentation.DTOs.Product;

namespace ProductCatalogService.Presentation
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Category, CreateProductDto>().ReverseMap();
            CreateMap<Category, GetProductDetailsDto>().ReverseMap();
            CreateMap<Category, DeleteProductDto>().ReverseMap();
            //CreateMap<Category, UpdateCategoryDiscountDto>().ReverseMap();
            CreateMap<Category, UpdateProductDto>().ReverseMap();
            //CreateMap<Category, GetCategoryProductsDto>();


            //CreateMap<Category, GetCategoryProductsDto>()
            //    .AfterMap((src, dest) =>
            //    {
            //        dest.Products.ForEach(x => x.Price -= src.CategoryDiscount);
            //    });

            //CreateMap<Product, ProductDto>()
            //    .ForMember(
            //        dest => dest.Price,
            //        opt => opt.MapFrom((src, dest, member, context) => src.Price - src.Category.CategoryDiscount));


            //CreateMap<Category, GetCategoryProductsDto>()
            //    .ConvertUsing<CategoryProductsMapper>();
            //CreateMap<Category, List<ProductDto>>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            //CreateMap<Product, ProductDto>()
            //    .ForMember(dest => dest.Price, source => source.MapFrom<GetProductDiscount>())
            //    .ForMember(dest => dest.CategoryName, source => source.MapFrom(s => s.Category.CategoryName));


        }
    }
}
