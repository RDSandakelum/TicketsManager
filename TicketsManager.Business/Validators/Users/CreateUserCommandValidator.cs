using FluentValidation;
using TicketsManager.Business.Actions.Users;

namespace TicketsManager.Business.Validators.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(command => command.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(50)
                .WithMessage("First name cannot exceed 50 characters.");
            RuleFor(command => command.LastName)
                .MaximumLength(50)
                .WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(command => command.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MaximumLength(100)
                .WithMessage("Email cannot exceed 100 characters.");
            RuleFor(command => command.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.");
        }
    }
}
