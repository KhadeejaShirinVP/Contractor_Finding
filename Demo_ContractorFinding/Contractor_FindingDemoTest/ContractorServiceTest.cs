using Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Service;
using Service.Interfaces;

namespace Contractor_FindingDemoTest
{
    public class ContractorServiceTest
    {
        private readonly Mock<ContractorFindingDemoContext> _context ;
        private readonly UserService _user;
        private readonly Mock<IEncrypt> _encrypt;

        public ContractorServiceTest()
        {
            _encrypt= new Mock<IEncrypt>();
            _context= new Mock<ContractorFindingDemoContext>();
            _user = new UserService(_context.Object, _encrypt.Object);
        }

        [Fact]
        public void Test1()
        {
            var result = new Login()
            {
                EmailId = "khadeeja@gmail.com",
                Password = "khadeeja123*",
                confirmPassword = "khadeeja123*"
            };
            _encrypt.Setup(x => x.EncodePasswordToBase64(result.Password)).Returns(result.Password);
            var actual = _user.Login(result);
            Assert.True(actual);

        }
    }
}