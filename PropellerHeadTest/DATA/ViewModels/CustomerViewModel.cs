using DATA.Entities;
using DATA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.ViewModels
{
    public class CustomerViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public CustomerStatus Status { get; set; }
    }
}
