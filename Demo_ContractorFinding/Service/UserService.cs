using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService: IUserService
    {
        private readonly ContractorFindingDemoContext contractorFindingDemoContext;
        private readonly IEncrypt encrypt;

        public UserService(ContractorFindingDemoContext contractorFindingDemoContext, IEncrypt encrypt)
        {
            this.contractorFindingDemoContext = contractorFindingDemoContext;
            this.encrypt = encrypt;
        }

        //For Display
        public List<UserDisplay> GetUserDetails()
        {
            List<UserDisplay> user = (from u in contractorFindingDemoContext.TbUsers
                                      join ud in contractorFindingDemoContext.UserTypes on
                                      u.TypeUser equals ud.TypeId
                                      select new UserDisplay
                                      {
                                          UserId = u.UserId,
                                          TypeUser = ud.Usertype1,
                                          FirstName = u.FirstName,
                                          LastName = u.FirstName,
                                          EmailId = u.EmailId,
                                          PhoneNumber = u.PhoneNumber,
                                      }).ToList();
            return user;
        }

        public bool checkExistUser(TbUser tbUser)
        {
            var email = contractorFindingDemoContext.TbUsers.Where(e => e.EmailId == tbUser.EmailId).FirstOrDefault();
            if (email == null)
            {
                return false;
            }
            return true;
        }

        //public bool Register(Registration registration)
        //{
        //    registration.Password= encrypt.EncodePasswordToBase64(registration.Password);
        //    registration.CreatedDate = DateTime.Now;
        //    registration.UpdatedDate = null;
        //    registration.Active = true;
        //    contractorFindingDemoContext.TbUsers.Add(registration);
        //    return true;
        //}

        //for Registration
        public bool Register(Registration registration)
        {

            string encryptedPassword = encrypt.EncodePasswordToBase64(registration.Password);
            registration.CreatedDate = DateTime.Now;
            registration.UpdatedDate = null;
            registration.Active = true;
            string passwordconfirm = encrypt.EncodePasswordToBase64(registration.confirmationPassword);
            registration.Password = encryptedPassword;
            registration.confirmationPassword = passwordconfirm;
            if (registration.Password == registration.confirmationPassword)
            {
                contractorFindingDemoContext.TbUsers.Add(registration);
                contractorFindingDemoContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Login(Login login)
        {
            string checkingpassword = encrypt.EncodePasswordToBase64(login.Password);
            var myUser = contractorFindingDemoContext.TbUsers.
                FirstOrDefault(u => u.EmailId == login.EmailId
                && u.Password == checkingpassword);
            if (myUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        //for forgotpassword case

        //public bool forgotpassword(Login login)
        //{
        //    TbUser user = contractorFindingDemoContext.TbUsers.Where(a => a.EmailId == login.EmailId).SingleOrDefault();
        //    user.Password = encrypt.EncodePasswordToBase64(login.Password);
        //    user.UpdatedDate = DateTime.Now;
        //    contractorFindingDemoContext.Entry(user).State = EntityState.Modified;
        //    contractorFindingDemoContext.SaveChanges();
        //    return true;
        //}

        //for forgotpassword case
        public bool forgotpassword(Login login)
        {

            var userWithSameEmail = contractorFindingDemoContext.TbUsers.Where(m => m.EmailId == login.EmailId).SingleOrDefault();
            if (userWithSameEmail == null)
            {
                return false;
            }
            else
            {

                string encrptnewpassword = encrypt.EncodePasswordToBase64(login.Password);
                string encrptconfirmpassword = encrypt.EncodePasswordToBase64(login.confirmPassword);
                if (encrptnewpassword == encrptconfirmpassword)
                {
                    userWithSameEmail.Password = encrptconfirmpassword;
                    userWithSameEmail.UpdatedDate = DateTime.Now;
                    contractorFindingDemoContext.Entry(userWithSameEmail).State = EntityState.Modified;
                    contractorFindingDemoContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        //for delete deatils
        public bool DeleteUser(TbUser user)
        {
            contractorFindingDemoContext.TbUsers.Remove(user);
            contractorFindingDemoContext.SaveChanges();
            return true;
        }
    }
}
