using AutoMapper;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Application.DTOs.Authentication;
using OrderManagementSystem.Application.Features.Customers.Models;
using OrderManagementSystem.Application.Features.Invoices.Models;
using OrderManagementSystem.Application.Features.Orders.Models;
using OrderManagementSystem.Application.Features.Products.Models;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Entities.Identity;

namespace OrderManagementSystem.Application.Profiles
{
    public partial class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Customers
            CreateMap<Customer, CreateCustomer>().ReverseMap();
            CreateMap<Customer, GetOrdersListWithCustomerId>().ReverseMap();

            // Invoices
            CreateMap<Invoice, GetInvoiceList>().ReverseMap();
            CreateMap<Invoice, GetInvoiceDetails>().ReverseMap();

            // Orders
            CreateMap<Order, CreateOrder>().ReverseMap();
            CreateMap<Order, UpdateOrder>().ReverseMap();
            CreateMap<Order, GetOrderList>().ReverseMap();
            CreateMap<Order, GetOrderDetails>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();

            //OrderItems
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            // Products
            CreateMap<Product, CreateProduct>().ReverseMap();
            CreateMap<Product, UpdateProduct>().ReverseMap();
            CreateMap<Product, GetProductList>().ReverseMap();
            CreateMap<Product, GetProductDetails>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();

            //Users
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
        }
    }
}
