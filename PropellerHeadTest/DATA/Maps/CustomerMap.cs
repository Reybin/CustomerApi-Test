using AutoMapper;
using DATA.Entities;
using DATA.ViewModels;


namespace DATA.Maps
{
    public class CustomerMap : Profile
    {
        public CustomerMap()
        {
            CreateMap<Customer, CustomerViewModel>()
                .ConstructUsing(c => new CustomerViewModel());

            CreateMap<CustomerViewModel, Customer>()
                .ConstructUsing(c => new Customer());
        }
    }
}
