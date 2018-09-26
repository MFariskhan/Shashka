using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Shaska.Models;
using Shaska.Dto;
namespace Shaska.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<ProductDto, Product>();
            Mapper.CreateMap<ProductCategory, ProductCategoryDto>();

            Mapper.CreateMap<Cart, CartDto>();
            Mapper.CreateMap<CartDto, Cart>();
            Mapper.CreateMap<Product, ProductDto>();

            Mapper.CreateMap<Order, OrderDto>();
            Mapper.CreateMap<OrderDto, Order>();
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserDto>();
            
            

        }
    }
}