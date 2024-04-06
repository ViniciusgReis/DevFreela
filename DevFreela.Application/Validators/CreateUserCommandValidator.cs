using DevFreela.Application.Commands.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("E-mail Invalido!");

            RuleFor(p => p.Password)
                .Must(ValidPassword)
                .WithMessage("Senha deve conter no mínimo 8 caracteres, 1 maíusculo, 1 minúsculo e 1 especial");

            RuleFor(p => p.FullName)
                .NotNull()
                .NotEmpty()
                .WithMessage("O nome deve ser preenchido!");
        } 

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

            return regex.IsMatch(password);
        }
    }
}
