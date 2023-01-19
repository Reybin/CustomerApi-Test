using DATA.Entities;
using DATA.ViewModels;
using SERVICE.CustomerService.Contracts;

namespace API.Controllers
{
    public class CustomerController : BaseController<ICustomerService, Customer, CustomerViewModel>
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService service) : base(service)
        {
            _customerService = service;
        }
    }
}
