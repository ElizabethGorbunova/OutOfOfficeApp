using FluentValidation;
using OutOfOfficeApp.Entities;

namespace OutOfOfficeApp.Models.Validators
{
    public class RegisterUserDtoInValidator:AbstractValidator<RegisterUserDtoIn>
    {
        private readonly OOODbContext dbContext;
        public RegisterUserDtoInValidator(OOODbContext _dbContext) 
        {

            dbContext = _dbContext;

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.Password).Equal(x=>x.ConfirmPassword);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(x => x.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });
        }
    }
}
