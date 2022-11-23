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
        
        public bool CreateContractor(ContractorDetail contractorDetail);
        List<ContractorDisplay> GetContractorDetails();
        public bool updateContractorDetails(ContractorDetail contractorDetail);

        public bool DeleteContractor(ContractorDetail contractorDetail);
    }
}
