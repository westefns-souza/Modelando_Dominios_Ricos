using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mooks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]

        public void ShouldReturnErrorWherDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand
            {
                FirstName = "Bruce",
                LastName  = "Wayne",
                Document  = "12125043483",
                Email = "westefns@gmail.com",
                BarCode = "12345",
                BoletoNumber = "12344" ,
                PaymentNumber  = "1223",
                PaidDate = DateTime.Now,
                ExpireDate = DateTime.Now,
                Total = 10,
                TotalPaid = 10,
                Payer = "12312",
                PayerDocument = "12125043483",
                PayerDocumentType = EDocumentType.CPF,
                PayerEmail = "12312",
                Street = "12312",
                Number = "12312",
                Neighborhood = "12312",
                City = "12312",
                State = "12312",
                Country = "12312",
                ZipCode = "123121",
            };

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}