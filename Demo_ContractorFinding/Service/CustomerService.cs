using Domain;
using Domain.Models;
using Microsoft.Identity.Client;
using Persistence;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ContractorFindingDemoContext contractorFindingDemoContext;

        public CustomerService(ContractorFindingDemoContext contractorFindingDemoContext)
        {
            this.contractorFindingDemoContext = contractorFindingDemoContext;
        }

        //create
        public string CreateCustomer(TbCustomer tbCustomer)
        {
            var registrationID = contractorFindingDemoContext.TbCustomers.Where(r => r.RegistrationNo == tbCustomer.RegistrationNo).FirstOrDefault();
            if (registrationID == null && tbCustomer.LandSqft != null)
            {
                var ID = tbCustomer.RegistrationNo.Trim();
                if (ID == string.Empty)
                {
                    return null;
                }
                else
                {
                    contractorFindingDemoContext.TbCustomers.Add(tbCustomer);
                    contractorFindingDemoContext.SaveChanges();
                    return "Successfully Added";
                }
            }
            else
            {
                return null;
            }


        }

        //RETRIEVE
        public List<CustomerDisplay> GetCustomerDetails()
        {
            List<CustomerDisplay> customers = (from c in contractorFindingDemoContext.TbCustomers
                                               join b in contractorFindingDemoContext.TbBuildings on
                                               c.BuildingType equals b.Id
                                               select new CustomerDisplay
                                               {
                                                   LandSqft = c.LandSqft,
                                                   RegistrationNo = c.RegistrationNo,
                                                   BuildingType = b.Building,
                                                   Lattitude = c.Lattitude,
                                                   Longitude = c.Longitude,
                                                   Pincode = c.Pincode,

                                               }).ToList();
            return customers;
        }

        //UPDATE
        public string UpdateCustomerDetails(TbCustomer tbCustomer)
        {
            using (var context = new ContractorFindingDemoContext())
            {
                var customer = context.TbCustomers.Where(x => x.RegistrationNo == tbCustomer.RegistrationNo).FirstOrDefault();
                if (customer != null)
                {
                    customer.LandSqft = tbCustomer.LandSqft;
                    customer.BuildingType = tbCustomer.BuildingType;
                    customer.Lattitude = tbCustomer.Lattitude;
                    customer.Longitude = tbCustomer.Longitude;
                    customer.Pincode = tbCustomer.Pincode;
                    if (customer.LandSqft != null && customer.RegistrationNo != null && customer.Pincode != null)
                    {
                        context.SaveChanges();
                        return "sucessfully Updated!";
                    }
                    return null;
                }
                else
                {
                    return null;
                }

            }         
        }

        //DELETE
        public bool DeleteCustomer(TbCustomer tbCustomer)
        {
            TbCustomer customer = contractorFindingDemoContext.TbCustomers.Where(c => c.RegistrationNo == tbCustomer.RegistrationNo).FirstOrDefault()!;
            contractorFindingDemoContext.TbCustomers.Remove(customer);
            contractorFindingDemoContext.SaveChanges();
            return true;
        }
    }
}
