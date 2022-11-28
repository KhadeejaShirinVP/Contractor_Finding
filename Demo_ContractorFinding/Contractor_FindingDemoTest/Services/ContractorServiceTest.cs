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
    [CollectionDefinition("Data Base")]
    public class DataBasecollection : ICollectionFixture<DataFixture>
    {

    }
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
        //public ContractorServiceTest(DbContextOptions options) : base()
        //{
        //}

        [Fact]
        public void Test_AddContractorDetails()
        {
            //Arrange
            var contractor = new ContractorDetail() { CompanyName = "vinnyConstruction", Gender = 1, License = "AP-24567789", Services = 3, Lattitude = 3.45, Longitude = 5.34, Pincode = 676563, PhoneNumber = 1234567890 ,ContractorId=3};

            //Act
            var result = contractorService.CreateContractor(contractor);
            var expected= "Successful!";

            //Assert
            Assert.Equal(result,expected);
        }
        [Fact]
        public void Test_AddContractorFailcondition()
        {
            //Arrange
            var contractor = new ContractorDetail() { CompanyName = "vinnyConstruction", Gender = 1, License = "AP-24567789", Services = 3, Lattitude = 3.45, Longitude = 5.34, Pincode = 676563, PhoneNumber = 1234567890, ContractorId = 1 };

            //Act
            var result = contractorService.CreateContractor(contractor);
            var expected = "failed";

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
            var contractor = new ContractorDetail() { ContractorId = 3, CompanyName = "vinReddyConstruction", Gender = 1, License = "AP-24567789", Services = 2, Lattitude = 9.87, Longitude = 9.76, Pincode = 879654, PhoneNumber = 1234567890 };

            //Act
            var result=contractorService.updateContractorDetails(contractor);
            var expected= "sucessfully Updated!";

            //Assret

            Assert.Equal(expected, result);
        }

        [Fact]
        public void UpdateDetails_Test_With_Wrong()
        {
            //Arrange
            var contractor = new ContractorDetail() { ContractorId = 3, Gender = 1, License = "AP-24567789", Services = 2, Lattitude = 9.87, Longitude = 9.76, Pincode = 879654, PhoneNumber = 1234567890 };

            //Act
            var result = contractorService.updateContractorDetails(contractor);
            var expected= "Updation failed";

            //Assert
            Assert.Equal(result, expected);

        }
    }
}