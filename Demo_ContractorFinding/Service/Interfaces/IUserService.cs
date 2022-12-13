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
        //public List<UserDisplay> GetUserDetails();
        Task<IEnumerable<Userview>> GetUserDetails();
        public bool checkExistUser(TbUser tbUser);
        public bool Register(Registration registration);

        public bool Login(TbUser login);

        public bool forgotpassword(Registration login);
        public bool DeleteUser(TbUser user);
        public List<Userview> GetUsers();
    }
}
