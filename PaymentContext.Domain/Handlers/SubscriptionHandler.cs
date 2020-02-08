using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjets;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(true, "Não foi possível Realizar sua assinatura!");
            }

            // Verificar se o documento já está cadastrado
            if(_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Esse Documento já está cadastrado existe!");

            // Verificar se o e-mail já está cadastrado
            if(_studentRepository.EmailExists(command.Email))
                AddNotification("Document", "Esse Documento já está cadastrado existe!");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address =  new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var documentPayer = new Document(command.PayerDocument, command.PayerDocumentType);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, documentPayer, address, email, command.BarCode, command.BoletoNumber);
            
            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar Validações
            AddNotifications(name, document, documentPayer, email, address, student, subscription, payment);

            //Checar Validações
            if (Invalid)
                return new CommandResult(false, "Não foi possível Realizar sua assinatura!");

            // Salvar as informações
            _studentRepository.CreateSubscriptions(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo", "Sua assinatura foi criada!");

            // Retorna informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível Realizar sua assinatura!");
            }

            // Verificar se o documento já está cadastrado
            if(_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Esse Documento já está cadastrado existe!");

            // Verificar se o e-mail já está cadastrado
            if(_studentRepository.EmailExists(command.Email))
                AddNotification("Document", "Esse Documento já está cadastrado existe!");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address =  new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var documentPayer = new Document(command.PayerDocument, command.PayerDocumentType);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer, documentPayer, address, email, command.TransactionCode);
            
            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar Validações
            AddNotifications(name, document, documentPayer, email, address, student, subscription, payment);
            
            //Checar Validações
            if (Invalid)
                return new CommandResult(false, "Não foi possível Realizar sua assinatura!");

            // Salvar as informações
            _studentRepository.CreateSubscriptions(student);

            // Enviar e-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo", "Sua assinatura foi criada!");

            // Retorna informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}