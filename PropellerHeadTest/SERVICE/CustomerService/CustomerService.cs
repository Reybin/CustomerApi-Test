using SERVICE.Base;
using DATA.Context;
using DATA.Entities;
using SERVICE.CustomerService.Contracts;
using Microsoft.EntityFrameworkCore;
using DATA.ViewModels;
using AutoMapper;

namespace SERVICE.CustomerService
{
    public class CustomerService : BaseService<Customer, CustomerViewModel>, ICustomerService
    {
        public CustomerService(ApiContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<Customer?> GetById(int id) => await _context.Customers.Include(c => c.Notes).FirstOrDefaultAsync(e => e.Id == id);        
    }
}
