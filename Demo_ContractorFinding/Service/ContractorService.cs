using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            var id = contractorFindingDemoContext.TbUsers.Where(u => u.UserId == contractorDetail.ContractorId).FirstOrDefault();
            var checklicense = contractorFindingDemoContext.ContractorDetails.Where(u => u.License == contractorDetail.License).FirstOrDefault();
            if (id != null && checklicense == null)
            {
                var license = contractorDetail.License.Trim();
                if (license == string.Empty)
                {
                    return false;

                }
                else
                {
                    contractorFindingDemoContext.ContractorDetails.Add(contractorDetail);
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
        public List<ContractorDisplay> GetContractorDetails()
        {
            List<ContractorDisplay> contractors = (from c in contractorFindingDemoContext.ContractorDetails
                                                   join g in contractorFindingDemoContext.TbGenders on
                                                   c.Gender equals g.GenderId
                                                   join user in contractorFindingDemoContext.TbUsers on c.ContractorId equals user.UserId
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
                                                       FirstName = user.FirstName,
                                                       LastName = user.LastName,
                                                       EmailId = user.EmailId,
                                                       PhoneNumber = c.PhoneNumber
                                                   }).ToList();
            return contractors;
        }

        //public async Task<IEnumerable<ContractorView>> GetContractorDetails()
        //{
        //    var user = await contractorFindingDemoContext.ContractorViews.OrderBy(x => x.UserId).ToListAsync();
        //    return (IEnumerable<ContractorView>)user;
        //}

        //UPDATE
        public bool updateContractorDetails(ContractorDetail contractorDetail)
        {

            var contractorobj = contractorFindingDemoContext.ContractorDetails.Where(c => c.ContractorId == contractorDetail.ContractorId).FirstOrDefault();
            var licenseobj = contractorFindingDemoContext.ContractorDetails.Where(c => c.License == contractorDetail.License).FirstOrDefault();
            var licensecon = contractorobj.License;
            if (contractorobj != null && licenseobj != null)
            {

                contractorobj.CompanyName = contractorDetail.CompanyName;
                contractorobj.Gender = contractorDetail?.Gender;
                contractorobj.Services = contractorDetail?.Services;
                contractorobj.PhoneNumber = contractorDetail?.PhoneNumber;
                contractorobj.Lattitude = contractorDetail?.Lattitude;
                contractorobj.Longitude = contractorDetail?.Longitude;
                contractorobj.Pincode = contractorDetail.Pincode;
                if (contractorDetail.CompanyName != null && contractorDetail.Pincode != 0 && contractorDetail.ContractorId == contractorobj.ContractorId && contractorDetail.License == licensecon)
                {
                    contractorFindingDemoContext.SaveChanges();
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

        ////DELETE
        //public bool DeleteContractor(ContractorDetail contractorDetail)
        //{
        //    ContractorDetail contractor = contractorFindingDemoContext.ContractorDetails.Where(x => x.License == contractorDetail.License).FirstOrDefault()!;
        //    contractorFindingDemoContext.ContractorDetails.Remove(contractor);
        //    contractorFindingDemoContext.SaveChanges();
        //    return true;
        //}

        //DELETE
        public bool DeleteContractor(ContractorDetail contractorDetail)
        {

            ContractorDetail contractor = contractorFindingDemoContext.ContractorDetails.Where(x => x.License == contractorDetail.License).FirstOrDefault()!;
            if (contractor != null)
            {
                contractorFindingDemoContext.ContractorDetails.Remove(contractor);
                contractorFindingDemoContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
