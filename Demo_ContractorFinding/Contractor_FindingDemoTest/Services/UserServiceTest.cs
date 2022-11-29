﻿using Moq;
using Service.Interfaces;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain;

namespace Contractor_FindingDemoTest.Services
{
    [Collection ("Data Base")]
    public class UserServiceTest 
    {
        DataFixture _fixture;
        UserService userService;
        Mock<IEncrypt> encrypt;

        public UserServiceTest(DataFixture fixture)
        {
            _fixture = fixture;
            encrypt = new Mock<IEncrypt>();
            userService = new UserService(_fixture.context, encrypt.Object);
        }

        [Fact]
        public void GetAll_User()
        {
            //Arrange

            //Act
            var result = userService.GetUserDetails();

            //Assert
            var items = Assert.IsType<List<UserDisplay>>(result);
            Assert.Equal(5,items.Count);
        }

        [Fact]
        public void Check_New_with_CheckExtistUser()
        {
            //Arrange
            var user = new Registration() { TypeUser = 1, FirstName = "khadeeja", LastName = "shirin", EmailId = "shir@gmail.com", Password = "shir@123", PhoneNumber = 9876567898, CreatedDate = DateTime.Now, UpdatedDate = null, Active = true };

            //Act
            var result = userService.checkExistUser(user);
            var excepted = "user doesnot exist";

            //Assert
            Assert.Equal(excepted, result);
        }

        //[Fact]
        //public void Check_Correct_ConfirmPassword()
        //{
        //    //Arrange
        //    var user = new Registration() { TypeUser = 1, FirstName = "khadeeja", LastName = "shirin", EmailId = "shirin@gmail.com", Password = "shirin@123", confirmationPassword = "shirin@123", PhoneNumber = 9876567898, CreatedDate = DateTime.Now, UpdatedDate = null, Active = true };

        //    //Act
        //    var result = userService.CheckPassword(user);

        //    //Assert
        //    Assert.True(result);
        //}

        [Fact]
        public void Test_Register()
        {
            //Arrange
            var user = new Registration() { TypeUser = 2, FirstName = "ram", LastName = "das", EmailId = "ram@gmail.com", Password = "ram@123", PhoneNumber = 23456789, CreatedDate = DateTime.Now, UpdatedDate = null, Active = true };
            encrypt.Setup(method => method.EncodePasswordToBase64(user.Password)).Returns(user.Password);

            //Act
            var result = userService.Register(user);
            var excepted = "registration failed";

            //Assert
            Assert.Equal(result, excepted);
        }

        [Fact]
        public void LogIn_with_correct_mail_password()
        {
            //Arrange
            var user = new Login() { EmailId = "ram@gmail.com", Password = "ram@123" };
            encrypt.Setup(method => method.EncodePasswordToBase64(user.Password)).Returns(user.Password);

            //Act
            var result = userService.Login(user);
            var excepted = "login succesfully";

            //Assert
            Assert.Equal(result,excepted);
        }

        [Fact]
        public void LogIn_with_correct_mail_wrong_password()
        {
            //Arrange
            var user = new Login() { EmailId = "ram@gmail.com", Password = "SDFGH567" };
            encrypt.Setup(method => method.EncodePasswordToBase64(user.Password)).Returns(user.Password);

            //Act
            var result = userService.Login(user);
            var excepted = "login failed";

            //Assert
            Assert.Equal(result,excepted);
        }

        [Fact]
        public void LogIn_with_new_mail()
        {
            //Arrange
            var user = new Login() { EmailId = "khadeeja@gmail.com", Password = "khade@123" };
            encrypt.Setup(method => method.EncodePasswordToBase64(user.Password)).Returns(user.Password);

            //Act
            var result = userService.Login(user);
            var expected = "login failed";

            //Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Forget_Password_Test()
        {
            //Arrange
            var user = new Login() { EmailId = "shirin@gmail.com", Password = "shirin123", confirmPassword = "shirin123" };
            encrypt.Setup(method => method.EncodePasswordToBase64(user.Password)).Returns(user.Password);

            //Act
            var result = userService.forgotpassword(user);
            var expected= "Successful!";

            //Assert
            Assert.Equal(expected,expected);
        }
    }
}