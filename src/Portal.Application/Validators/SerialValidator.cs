using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class SerialValidator : AbstractValidator<SerialDtoCreate>
    {
        public SerialValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name bắt buộc nhập");
        }
    }
}