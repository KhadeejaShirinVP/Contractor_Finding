using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IContractorService
    {

        bool CreateContractor(ContractorDetail contractorDetail);
        //List<ContractorDisplay> GetContractorDetails();
        Task<IEnumerable<ContractorView>> GetContractorDetails();
         bool updateContractorDetails(ContractorDetail contractorDetail);

         bool DeleteContractor(ContractorDetail contractorDetail);

    }
}
