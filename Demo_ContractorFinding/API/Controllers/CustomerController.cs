using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Service;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ContractorFindingDemoContext contractorFindingDemoContext;
        private readonly ICustomerService customerService;


        public CustomerController(ContractorFindingDemoContext contractorFindingDemoContext, ICustomerService customerService)
        {
            this.contractorFindingDemoContext = contractorFindingDemoContext;
            this.customerService = customerService;
        }

        //create
        [HttpPut]
        public JsonResult CreateContractor(TbCustomer tbCustomer)
        {
            try
            {
                var customer = customerService.CreateCustomer(tbCustomer);
                if (customer == true)
                {
                    return new JsonResult(new CrudStatus() { Status = true, Message = "Added Successful!" });
                }
                else
                {
                    return new JsonResult(new CrudStatus() { Status = false, Message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        //RETRIEVE
        [HttpGet]
        //public JsonResult GetCustomerDetails()
        //{
        //    try
        //    {
        //        return new JsonResult(customerService.GetCustomerDetails().ToList());
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(ex.Message);
        //    }
        //}
        public async Task<IActionResult> GetCustomerDetails()
        {
            try
            {
                var user = await customerService.GetCustomerDetails();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        //UPDATE
        [HttpPost]
        public JsonResult UpdateCustomerDetails(TbCustomer tbCustomer)
        {
            try
            {
                var contractor = customerService.UpdateCustomerDetails(tbCustomer);
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
        public JsonResult DeleteCustomer(TbCustomer tbCustomer)
        {
            try
            {
                var customer = customerService.DeleteCustomer(tbCustomer);
                if (customer == true)
                {
                    return new JsonResult(new CrudStatus() { Status = true, Message = "Deleted successfully!" });
                }
                return new JsonResult(new CrudStatus() { Status = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        //SerachingContractor
        //[HttpGet("Pincode")]
        //public JsonResult SearchBypincode(int pin)
        //{
        //    try
        //    {
        //        return new JsonResult(customerService.SearchBypincode(pin).ToList());
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(ex.Message);
        //    }
        //}

        //Sending SMS
        [HttpPost("SendingToContractor")]
        public JsonResult SendNotification(long phonenumber, string registration, int id)
        {
            try
            {

                return new JsonResult(customerService.SendMessage(phonenumber, registration, id));
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
