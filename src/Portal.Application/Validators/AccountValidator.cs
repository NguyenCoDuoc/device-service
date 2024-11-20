using FluentValidation;
using DeviceService.Application.DTOS.Account;
using DeviceService.Application.DTOS.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceService.Application.Validators
{
    public class LoginValidator : BaseValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
        }
    }
    public class LogoutValidator : BaseValidator<LogoutDTO>
    {
        public LogoutValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("RefreshToken is required");
        }
    }
    public class ChangePasswordValidator : BaseValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Current password is required")
                .MinimumLength(6)
                .WithMessage("Current password must be at least 6 characters long");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("New password is required")
                .MinimumLength(6)
                .WithMessage("New password must be at least 6 characters long")
                .NotEqual(x => x.Password)
                .WithMessage("New password cannot be the same as the current password");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm password is required")
                .Equal(x => x.NewPassword)
                .WithMessage("New password and confirmation password do not match")
                .MinimumLength(6)
                .WithMessage("Confirm password must be at least 6 characters long");
        }
    }
    public class UpdateProfileValidator : BaseValidator<UpdateProfileDTO>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
        }
    }
}
