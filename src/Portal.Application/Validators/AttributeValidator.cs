using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class AttributeValidator : AbstractValidator<AttributeDtoCreate>
    {
        public AttributeValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name bắt buộc nhập");
        }
    }
}