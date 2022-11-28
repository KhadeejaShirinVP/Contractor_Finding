using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ContractorService: IContractorService
    {
        private readonly ContractorFindingDemoContext contractorFindingDemoContext;

        //Constructor
        public ContractorService(ContractorFindingDemoContext contractorFindingDemoContext)
        {
            this.contractorFindingDemoContext = contractorFindingDemoContext;
        }

        //create
        public string CreateContractor(ContractorDetail contractorDetail)
        {
            var id = contractorFindingDemoContext.TbUsers.Where(u => u.UserId == contractorDetail.ContractorId).FirstOrDefault();
            var checklicense = contractorFindingDemoContext.ContractorDetails.Where(u=>u.License == contractorDetail.License).FirstOrDefault();
            if (id != null && checklicense!=null)
            {
                contractorFindingDemoContext.ContractorDetails.Add(contractorDetail);
                contractorFindingDemoContext.SaveChanges();
                return "Successful!";
            }
            else
            {
                return "failed";
            }
        }

        //RETRIEVE
        public List<ContractorDisplay> GetContractorDetails()
        {
            List<ContractorDisplay> contractors = (from c in contractorFindingDemoContext.ContractorDetails
                                                   join g in contractorFindingDemoContext.TbGenders on
                                                   c.Gender equals g.GenderId
                                                   //from e in contractorFindingDemoContext.ContractorDetails
                                                   join h in contractorFindingDemoContext.ServiceProvidings on
                                                   c.Services equals h.ServiceId
                                                   select new ContractorDisplay
                                                   {
                                                       ContractorId = c.ContractorId,
                                                       CompanyName = c.CompanyName,
                                                       Gender = g.GenderType,
                                                       License = c.License,
                                                       Services = h.ServiceName,
                                                       Lattitude = c.Lattitude,
                                                       Longitude = c.Longitude,
                                                       Pincode = c.Pincode,
                                                       PhoneNumber = c.PhoneNumber,

                                                   }).ToList();
            return contractors;
        }

        //UPDATE

        public string updateContractorDetails(ContractorDetail contractorDetail)
        {
            //using (var context = new ContractorFindingDemoContext())
            //{
                var contractorobj = contractorFindingDemoContext.ContractorDetails.Where(c => c.ContractorId == contractorDetail.ContractorId).FirstOrDefault();
                if (contractorobj != null)
                {

                    contractorobj.CompanyName = contractorDetail.CompanyName;
                    contractorobj.Gender = contractorDetail?.Gender;
                    contractorobj.Services = contractorDetail?.Services;
                    contractorobj.PhoneNumber = contractorDetail?.PhoneNumber;
                    contractorobj.Lattitude = contractorDetail?.Lattitude;
                    contractorobj.Longitude = contractorDetail?.Longitude;
                    contractorobj.Pincode = contractorDetail.Pincode;
                    if (contractorobj.CompanyName != null && contractorobj.Pincode != null && contractorDetail.License != null)
                    {
                    contractorFindingDemoContext.SaveChanges();
                        return "sucessfully Updated!";
                    }
                    else
                    {
                        return "Updation failed";
                    }
                }
                else
                {
                    return "Updation failed";   
                }
            //}
        }

        //DELETE
        public bool DeleteContractor(ContractorDetail contractorDetail)
        {
            ContractorDetail contractor = contractorFindingDemoContext.ContractorDetails.Where(x => x.License == contractorDetail.License).FirstOrDefault()!;
            contractorFindingDemoContext.ContractorDetails.Remove(contractor);
            contractorFindingDemoContext.SaveChanges();
            return true;
        }

    }
}
