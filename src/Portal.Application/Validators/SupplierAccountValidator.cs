using FluentValidation;
using DeviceService.Application.DTOS.SupplierAccount;

namespace DeviceService.Application.Validators
{
    public class SupplierAccountCreateValidator : BaseValidator<SupplierAccountDTOCreate>
    {
        public SupplierAccountCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.BankCode).NotEmpty().WithMessage("BankCode is required");
            RuleFor(x => x.BankNumber).NotEmpty().WithMessage("BankNumber is required");
            RuleFor(x => x.BankName).NotEmpty().WithMessage("BankName is required");
            RuleFor(x => x.BankAddress).NotEmpty().WithMessage("BankAddress is required");
        }
    }
	public class SupplierAccountUpdateValidator : BaseValidator<SupplierAccountDTOUpdate>
	{
		public SupplierAccountUpdateValidator()
		{
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.BankCode).NotEmpty().WithMessage("BankCode is required");
            RuleFor(x => x.BankNumber).NotEmpty().WithMessage("BankNumber is required");
            RuleFor(x => x.BankName).NotEmpty().WithMessage("BankName is required");
            RuleFor(x => x.BankAddress).NotEmpty().WithMessage("BankAddress is required");
        }
	}

}
