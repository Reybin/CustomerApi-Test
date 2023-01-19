using DATA.Entities;
using DATA.ViewModels;
using SERVICE.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.CustomerService.Contracts
{
    public interface ICustomerService : IBaseContract<Customer, CustomerViewModel>
    {

    }
}
