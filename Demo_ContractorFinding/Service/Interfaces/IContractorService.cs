﻿using Domain;
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
        
        public string CreateContractor(ContractorDetail contractorDetail);
        List<ContractorDisplay> GetContractorDetails();
        public string updateContractorDetails(ContractorDetail contractorDetail);

        public bool DeleteContractor(ContractorDetail contractorDetail);
    }
}
