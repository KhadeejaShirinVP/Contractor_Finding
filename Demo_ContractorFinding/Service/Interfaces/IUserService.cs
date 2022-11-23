using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        List<UserDisplay> GetUserDetails();
        bool checkExistUser(TbUser tbUser);
        bool Register(Registration registration);

        bool Login(Login login);

        bool forgotpassword(Login login);
        bool DeleteUser(TbUser user);
    }
}
