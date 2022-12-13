using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Repository;
using Service.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ContractorFindingDemoContext contractorFindingDemoContext;
        private readonly IUserService userService;
        private readonly IMapper _mapper;

        //constructor
        public UserController(ContractorFindingDemoContext contractorFindingDemoContext, IUserService userService, IMapper mapper)
        {
            this.contractorFindingDemoContext = contractorFindingDemoContext;
            this.userService = userService;
            _mapper = mapper;
        }

        //for get user details
        // GET: api/<ContractorController>
        [HttpGet]
        //public JsonResult Getuserdetails()
        //{
        //    try
        //    {
        //        return new JsonResult(userService.GetUserDetails().ToList());
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(ex.Message);
        //    }
        //

        public async Task<IActionResult> GetUserDetails()
        {
            var user = await userService.GetUserDetails();
            var userdto = _mapper.Map<IEnumerable<Userview>>(user);
            return Ok(userdto);
        }

        [HttpGet]
        [MapToApiVersion("2")]
        [Route("V2")]
        public ActionResult<List<UserDisplayV2>> GetUsers2()
        {
            var users = userService.GetUsers().Select(a => _mapper.Map<UserDisplayV2>(a));
            return Ok(users);
        }

        [HttpGet]
        [MapToApiVersion("3")]
        [Route("V3")]
        public JsonResult GetUsers3()
        {
            var user = new MapperConfiguration(cfg => cfg.CreateProjection<TbUser, UserDisplayV2>()
            .ForMember(dto => dto.Usertype, conf =>
             conf.MapFrom(ol => ol.TypeUserNavigation.Usertype1)));
            return new JsonResult(contractorFindingDemoContext.TbUsers.ProjectTo<UserDisplayV2>(user).ToList());
        }

        [HttpGet]
        [MapToApiVersion("4")]
        [Route("V4")]
        public List<UserDisplayV2> GetUsers4()
        {
            List<UserDisplayV2>users=AutoMapper<Userview,UserDisplayV2>.Maplist(userService.GetUsers());
            return users;

        }

        //for user registration
        // POST api/<ContractorController>
        [HttpPut]
        public JsonResult RegisterUser(Registration registration)
        {

            try
            {
                var userexist = userService.checkExistUser(registration);

                if (userexist == true)
                {
                    var details = userService.Register(registration);
                    if (details == true)
                    {
                        return new JsonResult(new CrudStatus() { Status = true, Message = "Registration Successful!" });
                    }
                    else
                    {
                        return new JsonResult(new CrudStatus() { Status = false, Message = "registration failed" });
                    }
                }
                else
                {
                    return new JsonResult(new CrudStatus() { Status = false, Message = "Mail ID is already existing" });
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }

        //for user login 
        [HttpPost("login")]
        public JsonResult LoginUser(TbUser login)
        {
            try
            {
                var details = userService.Login(login);
                if (details != null)
                {
                    return new JsonResult(new CrudStatus() { Status = true, Message = "Login Successfull!" });
                }
                return new JsonResult(new CrudStatus() { Status = false, Message = "LoginFailed" });

            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }


        //for forgot password
        [HttpPost("forgotpassword")]
        public JsonResult ForgotPassword(Registration login)
        {
            try
            {
                var details = userService.forgotpassword(login);
                if (details == true)
                {
                    return new JsonResult(new CrudStatus() { Status = true, Message = "Password Updated" });
                }
                return new JsonResult(new CrudStatus() { Status = false, Message = "Not Updated" });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        // for DELETE 
        [HttpDelete]
        public JsonResult Delete(TbUser user)
        {
            try
            {
                var details = userService.DeleteUser(user);
                if (details == true)
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
 