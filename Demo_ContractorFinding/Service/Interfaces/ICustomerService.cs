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
        bool CreateCustomer(TbCustomer tbCustomer);
        List<CustomerDisplay> GetCustomerDetails();
        bool UpdateCustomerDetails(TbCustomer tbCustomer);

        bool DeleteCustomer(TbCustomer tbCustomer);
        public List<ContractorDisplay> SearchBypincode(int pincode);
        string SendMessage(long phonenumber, string reggistration, int id);

    }
}

