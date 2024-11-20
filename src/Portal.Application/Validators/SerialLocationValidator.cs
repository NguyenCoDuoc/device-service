using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class SerialLocationValidator : AbstractValidator<SerialLocationDtoCreate>
    {
        public SerialLocationValidator()
        {
            RuleFor(x => x.Description).NotNull().WithMessage("Description bắt buộc nhập");
        }
    }
}