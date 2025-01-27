using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjets
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres!")
                .HasMinLen(LastName, 3, "Name.FirstName", "Sobrenome deve conter pelo menos 3 caracteres!")
                .HasMaxLen(FirstName, 40, "Name.FirstName", "Nome deve até pelo menos 40 caracteres!")
            );
        }

        public string FirstName { get; private set; }   
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}