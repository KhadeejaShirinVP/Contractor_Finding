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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ContractorFindingDemoContext contractorFindingDemoContext;
        private readonly SendMessage sms;
        private readonly ContractorService _contractorservice;

        public CustomerService(ContractorFindingDemoContext contractorFindingDemoContext)
        {
            this.contractorFindingDemoContext = contractorFindingDemoContext;
            sms = new SendMessage();
            _contractorservice = new ContractorService(contractorFindingDemoContext);
        }

        //create
        public bool CreateCustomer(TbCustomer tbCustomer)
        {
            var registrationID = contractorFindingDemoContext.TbCustomers.Where(r => r.RegistrationNo == tbCustomer.RegistrationNo).FirstOrDefault();
            if (registrationID == null && tbCustomer.LandSqft != null)
            {
                var ID = tbCustomer.RegistrationNo.Trim();
                if (ID == string.Empty)
                {
                    return false;
                }
                else
                {
                    contractorFindingDemoContext.TbCustomers.Add(tbCustomer);
                    contractorFindingDemoContext.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
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
        public bool UpdateCustomerDetails(TbCustomer tbCustomer)
        {
            {
                var customer = contractorFindingDemoContext.TbCustomers.Where(x => x.RegistrationNo == tbCustomer.RegistrationNo).FirstOrDefault();
                if (customer != null)
                {
                    customer.LandSqft = tbCustomer.LandSqft;
                    customer.BuildingType = tbCustomer.BuildingType;
                    customer.Lattitude = tbCustomer.Lattitude;
                    customer.Longitude = tbCustomer.Longitude;
                    customer.Pincode = tbCustomer.Pincode;
                    customer.CustomerId = tbCustomer.CustomerId;
                    if (customer.LandSqft != null && customer.LandSqft != 0 && customer.RegistrationNo != null && customer.Pincode != null)
                    {
                        contractorFindingDemoContext.SaveChanges();
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }

            }
        }

        //DELETE
        public bool DeleteCustomer(TbCustomer tbCustomer)
        {
            TbCustomer customer = contractorFindingDemoContext.TbCustomers.Where(c => c.RegistrationNo == tbCustomer.RegistrationNo).FirstOrDefault()!;
            if (customer != null)
            {
                contractorFindingDemoContext.TbCustomers.Remove(customer);
                contractorFindingDemoContext.SaveChanges();
                return true;
            }
            return false;
        }


        //send message
        public string SendMessage(long phonenumber,string reggistration,int id)
        {
            var phone = contractorFindingDemoContext.ContractorDetails.Where(c => c.PhoneNumber == phonenumber).FirstOrDefault();
            var registrationid = contractorFindingDemoContext.TbCustomers.Where(c => c.RegistrationNo == reggistration).FirstOrDefault();
            var customer = contractorFindingDemoContext.TbUsers.Where(a => a.UserId == id ).FirstOrDefault();
            var custid = contractorFindingDemoContext.TbCustomers.Where(a=>a.CustomerId==id).FirstOrDefault();
            if(phone != null && registrationid != null && custid!=null && customer != null)
            {
                string message2= customer.FirstName + " " + customer.LastName + " \n phone number : " + customer.PhoneNumber + " \n Emailid:  " + customer.EmailId + "\n Land squarefeet: " + registrationid.LandSqft + "\n Pincode:  " + registrationid.Pincode;            
                sms.SendMessageToContractor(message2, phonenumber);           
                return "Message sended";
            }
            else
            {
                return "failed";
            }
        }
        //search
        public List<ContractorDisplay> SearchBypincode(int pincode)
        {
            return  _contractorservice.GetContractorDetails().Where(x => x.Pincode == pincode).ToList();
        }

    }

}
