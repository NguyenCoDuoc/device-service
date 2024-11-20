using FluentValidation;
using DeviceService.Application.DTOS.Supplier;

namespace DeviceService.Application.Validators
{
    public class SupplierCreateValidator : BaseValidator<SupplierDTOCreate>
    {
        public SupplierCreateValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
        }
    }
    public class SupplierUpdateValidator : BaseValidator<SupplierDTOUpdate>
    {
        public SupplierUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
        }
    }
    
}
