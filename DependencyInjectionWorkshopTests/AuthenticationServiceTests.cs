using DependencyInjectionWorkshop.Models;
using NUnit.Framework;

namespace DependencyInjectionWorkshopTests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        [Test]
        public void is_valid()
        {
            var authenticationService = new AuthenticationService();
            
            bool isValid = authenticationService.isVerify("Amber", "abc", "123");
            Assert.IsTrue(isValid);
        }
    }
}