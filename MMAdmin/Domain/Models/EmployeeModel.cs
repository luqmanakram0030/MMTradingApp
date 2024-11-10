using System;
using System.ComponentModel.DataAnnotations;

namespace MMAdmin.Domain.Models
{
    public class EmployeeModel
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [StrongPassword(ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        
        public Location Location { get; set; }

        public bool IsPresent { get; set; }
    }

    public class StrongPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required", new[] { validationContext.MemberName });
            }

            var hasUpperCase = new Regex(@"[A-Z]+");
            var hasLowerCase = new Regex(@"[a-z]+");
            var hasDigits = new Regex(@"[0-9]+");
            var hasSpecialChars = new Regex(@"[\W]+");

            if (!hasUpperCase.IsMatch(password))
            {
                return new ValidationResult("Password must contain at least one uppercase letter.", new[] { validationContext.MemberName });
            }
            if (!hasLowerCase.IsMatch(password))
            {
                return new ValidationResult("Password must contain at least one lowercase letter.", new[] { validationContext.MemberName });
            }
            if (!hasDigits.IsMatch(password))
            {
                return new ValidationResult("Password must contain at least one digit.", new[] { validationContext.MemberName });
            }
            if (!hasSpecialChars.IsMatch(password))
            {
                return new ValidationResult("Password must contain at least one special character.", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}