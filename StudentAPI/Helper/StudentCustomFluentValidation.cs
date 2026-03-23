using FluentValidation;
using StudentAPI.Model;

namespace StudentAPI.Helper
{
    public class StudentCustomFluentValidation  : AbstractValidator<Student>
    {
        public StudentCustomFluentValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(4);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Age).NotEmpty().LessThanOrEqualTo(18);
        }
    }
}
