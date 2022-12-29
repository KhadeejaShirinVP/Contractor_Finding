using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persistence;
using Service;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
        private readonly ContractorFindingDemoContext contractorFindingDemoContext;
        private readonly IContractorService contractorService;
        private readonly IMapper _mapper;
        private readonly Mapper mapper1; 

        //Constructor
        public ContractorController(ContractorFindingDemoContext contractorFindingDemoContext, IContractorService contractorService,IMapper mapper)
        {
            this.contractorFindingDemoContext = contractorFindingDemoContext;
            this.contractorService = contractorService;
            _mapper=mapper;
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
                    return new JsonResult(new CrudStatus() { Status = true, Message = "Successful!" });
                }
                return new JsonResult(new CrudStatus() { Status = false, Message = "Failed" });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        //RETRIVE
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //public async Task<IActionResult> GetContractorDetails()
        //{
        //    try
        //    {
        //        var user = await contractorService.GetContractorDetails();
        //        return Ok(user);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(ex.Message);
        //    }
        //}
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractorDetail"></param>
        /// <returns></returns>
        [HttpPost]


        public JsonResult UpdateContractor(ContractorDetail contractorDetail)
        {
            try
            {
                var contractor = contractorService.updateContractorDetails(contractorDetail);
                if (contractor == true && contractorDetail.License != null)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractorDetail"></param>
        /// <returns></returns>
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
