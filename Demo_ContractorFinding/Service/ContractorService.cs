using Domain;
using Domain.Models;
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
        public bool CreateContractor(ContractorDetail contractorDetail)
        {
            contractorFindingDemoContext.ContractorDetails.Add(contractorDetail);
            contractorFindingDemoContext.SaveChanges();
            return true;
        }

        //RETRIEVE
        public List<ContractorDisplay> GetContractorDetails()
        {
            List<ContractorDisplay> contractors = (from c in contractorFindingDemoContext.ContractorDetails
                                                   join g in contractorFindingDemoContext.TbGenders on
                                                   c.Gender equals g.GenderId
                                                   from e in contractorFindingDemoContext.ContractorDetails
                                                   join h in contractorFindingDemoContext.ServiceProvidings on
                                                   e.Services equals h.ServiceId
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

        public bool updateContractorDetails(ContractorDetail contractorDetail)
        {
            using (var context = new ContractorFindingDemoContext())
            {
                var contractorobj = context.ContractorDetails.Where(c => c.ContractorId == contractorDetail.ContractorId).FirstOrDefault();
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
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
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
