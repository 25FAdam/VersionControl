using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
     Test,
     TestCase("abcd1234", false),
     TestCase("irf@uni-corvinus", false),
     TestCase("irf.uni-corvinus.hu", false),
     TestCase("irf@uni-corvinus.hu", true)
 ]


        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidateEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [Test,
            TestCase("ABCD1234", false),
            TestCase("Ab1234", false),
            TestCase("Abcd1234", false),
            TestCase("abcd1234", true),
            TestCase("abcdABCD", true)]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            //ARRANGE
            var accountController = new AccountController();

            //ACT
            var actualResult = accountController.ValidatePassword(password);

            //ASSERT
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
