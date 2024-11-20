using FluentValidation;
using DeviceService.Application.DTOS.SupplierContact;

namespace DeviceService.Application.Validators
{
    public class SupplierContactCreateValidator : BaseValidator<SupplierContactDTOCreate>
    {
        public SupplierContactCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
    }
	public class SupplierContactUpdateValidator : BaseValidator<SupplierContactDTOUpdate>
	{
		public SupplierContactUpdateValidator()
		{
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        }
	}

}
