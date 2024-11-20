using StockApp.Domain.Validation;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace StockApp.Domain.Entities
{
    public class Supplier
    {
        #region atributos
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        #endregion

        public Supplier(string name, string contactEmail, string phoneNumber)
        {
            ValidateDomain(name, contactEmail, phoneNumber);
        }

        public Supplier(int id, string name, string contactEmail, string phoneNumber) 
        {
            DomainExceptionValidation.When(id < 0, "Update Invalid Id value");
            Id = id;
            ValidateDomain(name, contactEmail, phoneNumber);
        }

        private void ValidateDomain(string name, string contactEmail, string phoneNumber)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name, name is required.");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(phoneNumber),
                "Invalid phoneNumber, phoneNumber is required.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(contactEmail),
                "Invalid contactEmail, contactEmail is required.");

            DomainExceptionValidation.When(contactEmail.Length < 16,
                "Invalid contactEmail, too short, minimum 16 characters.");

            DomainExceptionValidation.When(phoneNumber.Length < 11,
                "Invalid phoneNumber, too short, minimum 11 characters.");

            Name = name;
            ContactEmail = contactEmail;
            PhoneNumber = phoneNumber;
        }
    }
}
