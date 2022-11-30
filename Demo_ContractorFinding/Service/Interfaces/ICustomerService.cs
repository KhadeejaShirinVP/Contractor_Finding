using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        string CreateCustomer(TbCustomer tbCustomer);
        List<CustomerDisplay> GetCustomerDetails();
        string UpdateCustomerDetails(TbCustomer tbCustomer);

        bool DeleteCustomer(TbCustomer tbCustomer);
    }
}
