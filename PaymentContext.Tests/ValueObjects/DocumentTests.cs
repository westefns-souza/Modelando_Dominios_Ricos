using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjets;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("123")]
        [DataRow("")]
        public void ShouldReturnErrorWherCNPJIsInvalid(string cnpj)
        {
            var doc = new Document(cnpj, EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("43611421000161")]
        [DataRow("17916333000168")]
        [DataRow("00325636000103")]
        public void ShouldReturnSuccessWherCNPJIsValid(string cnpj)
        {
            var doc = new Document(cnpj, EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("123")]
        [DataRow("")]
        public void ShouldReturnErrorWherCPFIsInvalid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("14171959080")]
        [DataRow("60141082038")]
        [DataRow("59976957009")]
        public void ShouldReturnSuccessWherCPFIsValid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}