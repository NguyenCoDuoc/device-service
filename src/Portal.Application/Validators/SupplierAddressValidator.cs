using FluentValidation;
using DeviceService.Application.DTOS.SupplierAddress;


namespace DeviceService.Application.Validators
{
    public class SupplierAddressCreateValidator : BaseValidator<SupplierAddressDTOCreate>
    {
        public SupplierAddressCreateValidator()
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        }
    }
	public class SupplierAddressUpdateValidator : BaseValidator<SupplierAddressDTOUpdate>
	{
		public SupplierAddressUpdateValidator()
		{
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required")
                .MaximumLength(10).WithMessage("Type cannot exceed 10 characters");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
        }
	}

}
