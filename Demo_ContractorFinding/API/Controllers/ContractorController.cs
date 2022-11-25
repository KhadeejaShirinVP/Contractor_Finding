﻿using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly ContractorFindingDemoContext contractorFindingDemoContext;
        private readonly IContractorService contractorService;

        //Constructor
        public ContractorController(ContractorFindingDemoContext contractorFindingDemoContext, IContractorService contractorService)
        {
            this.contractorFindingDemoContext = contractorFindingDemoContext;
            this.contractorService = contractorService;
        }

        //create
        [HttpPut]
        public JsonResult CreateContractor(ContractorDetail contractorDetail)
        {
            try
            {
                var contractor = contractorService.CreateContractor(contractorDetail);
                if (contractor == true)
                {
                    return new JsonResult(new CrudStatus() { Status = true, Message = "Added Successful!" });
                }
                return new JsonResult(new CrudStatus() { Status = false, Message = "Failed" });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        //RETRIVE
        [HttpGet]
        public JsonResult GetContractorDetails()
        {
            try
            {
                return new JsonResult(contractorService.GetContractorDetails().ToList());
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        //UPDATE
        [HttpPost]

        public JsonResult UpdateContractor(ContractorDetail contractorDetail)
        {
            try
            {
                var contractor = contractorService.updateContractorDetails(contractorDetail);
                if (contractor == true)
                {
                    return new JsonResult(new CrudStatus() { Status = true, Message = "Successfully Updated" });
                }
                else
                    return new JsonResult(new CrudStatus() { Status = false, Message = "Updation Failed" });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        //DELETE
        [HttpDelete]
        public JsonResult DeleteContractor(ContractorDetail contractorDetail)
        {
            try
            {
                var contractor = contractorService.DeleteContractor(contractorDetail);
                if (contractor == true)
                {
                    return new JsonResult(new CrudStatus() { Status = true, Message = "Deleted successful!" });
                }
                return new JsonResult(new CrudStatus() { Status = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }
    }
}