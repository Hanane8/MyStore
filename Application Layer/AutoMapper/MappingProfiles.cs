using Application_Layer.DTO.CartDTO;
using Application_Layer.DTO.CartItemDTO;
using Application_Layer.DTO.ClothingTypeDTO;
using Application_Layer.DTO.OrderDTO;
using Application_Layer.DTO.ProductsDto;
using Application_Layer.DTO.UserDto;
using AutoMapper;
using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.AutoMapper

{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddUserDTO, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.ClothingTypeId, opt => opt.MapFrom(src => src.ClothingTypeId));

            CreateMap<UpdateProductDTO, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ClothingType.Category.Name))
            .ForMember(dest => dest.ClothingTypeName, opt => opt.MapFrom(src => src.ClothingType.Name));

            CreateMap<AddClothingTypeDTO, ClothingType>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
            
            CreateMap<UpdateClothingTypeDTO, ClothingType>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<AddToCartDTO, CartItem>();

            CreateMap<CartItem, CartItemDto>();

            CreateMap<Cart, CartDto>();

            CreateMap<UpdateCartItemDTO, CartItem>();

            CreateMap<CheckoutDto, Order>()
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => Order.Status.Pending))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.CartItems));

            CreateMap<CartItemDto, OrderItem>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));
        }
    }
            
    
}
