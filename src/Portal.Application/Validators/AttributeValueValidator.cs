using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class AttributeValueValidator : AbstractValidator<AttributeValueDtoCreate>
    {
        public AttributeValueValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name bắt buộc nhập");
        }
    }
}