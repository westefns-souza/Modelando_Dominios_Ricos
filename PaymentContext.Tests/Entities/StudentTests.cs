using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjets;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var name = new Name("Bruce", "Wayne");
            var document = new Document("11111111111", EDocumentType.CPF);
            var email = new Email("westefns@outlook.com.br");
            
            var student = new Student(name, document, email);
            
            Assert.Fail();
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenHadNoActiveSubscription()
        {
            Assert.Fail();
        }
    }
}