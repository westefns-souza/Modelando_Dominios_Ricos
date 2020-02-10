using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mooks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscriptions(Student student)
        {
            
        }
        
        public bool DocumentExists(string document)
        {
            if (document.Equals("121.250.434-83"))
                return true;
            
            return false;
        }

        public bool EmailExists(string email)
        {
            if (email.Equals("westefns@outlook.com.br"))
                return true;
            
            return false;
        }
    }
}