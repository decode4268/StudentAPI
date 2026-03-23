using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Helper
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _age;
        public MinimumAgeAttribute(int age)
        {                                        
            _age = age;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Age is required");
            int ageData = (int)value;

            if (ageData < _age)
            {
                return new ValidationResult($"Student age must be at least {_age}");
            }
            return ValidationResult.Success;

        }
    }
}
