using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Persistence;
using Service;
using Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contractor_FindingDemoTest.Services
{
    [Collection("Data Base")]
    public class ContractorServiceTest
    {
        DataFixture _fixture;
        ContractorService contractorService;

        public ContractorServiceTest(DataFixture fixture)
        {
            _fixture = fixture;
            contractorService = new ContractorService(_fixture.context);
        }

        [Fact]
        public void Test_AddContractorDetails()
        {
            //Arrange
            var contractor = new ContractorDetail() { ContractorId = 5, CompanyName = "ramtraders", Gender = 2, License = "KL-45986759", Services = 1, Lattitude = 7.45, Longitude = 7.14, Pincode = 765432, PhoneNumber = 9876543322 };

            //Act
            var result = contractorService.CreateContractor(contractor);
            var expected = true;

            //Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Test_AddContractorFailcondition()
        {
            //Arrange
            var contractor = new ContractorDetail() { CompanyName = "ramtraders", Gender = 2, License = "KL-453759", Services = 1, Lattitude = 7.45, Longitude = 7.14, Pincode = 765432, PhoneNumber = 9876543322 };

            //Act
            var result = contractorService.CreateContractor(contractor);
            var expected = false;

            //Assert
            Assert.Equal(result, expected);
        }
        [Fact]
        public void Get_All_Contractor()
        {
            //Arrange

            //Act
            var result = contractorService.GetContractorDetails();

            //Assert
            var expect = _fixture.context.ContractorDetails.Count();
            var items = Assert.IsType<List<ContractorDisplay>>(result);
            Assert.Equal(expect, items.Count);
        }

        [Fact]
        public void UpdateDetails_Test_WithCorrect()
        {
            //Arrange
            var contractor = new ContractorDetail() { ContractorId = 4, CompanyName = "pavanmaniTraders", Gender = 1, License = "KA-8765437", Services = 3, Lattitude = 9.54, Longitude = 4.36, Pincode = 864357, PhoneNumber = 45678908765 };

            //Act
            var result = contractorService.updateContractorDetails(contractor);
            var expected = true;

            //Assret

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UpdateDetails_Test_With_WrongID()
        {
            //Arrange
            var contractor = new ContractorDetail() { ContractorId = 4, CompanyName = "ramtraders", Gender = 2, License = "KL-456789", Services = 1, Lattitude = 7.45, Longitude = 7.14, Pincode = 765432, PhoneNumber = 9876543322 };

            //Act
            var result = contractorService.updateContractorDetails(contractor);
            var expected = false;

            //Assert
            Assert.Equal(result, expected);

        }


        [Fact]
        public void UpdateDetails_Test_With_WrongLicense()
        {
            //Arrange
            var contractor = new ContractorDetail() { ContractorId = 5, CompanyName = "ramtraders", Gender = 2, License = "KL-489", Services = 1, Lattitude = 7.45, Longitude = 7.14, Pincode = 765432, PhoneNumber = 9876543322 };

            //Act
            var result = contractorService.updateContractorDetails(contractor);
            var excepted = false;

            //Assert
            Assert.Equal(result, excepted);

        }

        [Fact]
        public void UpdateDetails_Test_WithoutCompanyName()
        {
            //Arrange
            var contractor = new ContractorDetail() { ContractorId = 5, Gender = 2, License = "KL-456789", Services = 1, Lattitude = 7.45, Longitude = 7.14, Pincode = 765432, PhoneNumber = 9876543322 };

            //Act
            var result = contractorService.updateContractorDetails(contractor);
            var excepted = false;

            //Assert
            Assert.Equal(result, excepted);

        }

        [Fact]
        public void UpdateDetails_Test_WithoutPincode()
        {
            //Arrange
            var contractor = new ContractorDetail() { ContractorId = 3, CompanyName = "reddyconstruction", Gender = 1, License = "AP-23456789", Services = 1, Lattitude = 9.87, Longitude = 9.76, PhoneNumber = 1234567890 };

            //Act
            var result = contractorService.updateContractorDetails(contractor);
            var excepted = false;

            //Assert
            Assert.Equal(result, excepted);

        }
    }
}