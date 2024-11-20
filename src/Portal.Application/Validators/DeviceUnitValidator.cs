using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class DeviceUnitValidator : AbstractValidator<DeviceUnitDtoCreate>
    {
        public DeviceUnitValidator()
        {
            RuleFor(x => x.Code).NotNull().WithMessage("Code bắt buộc nhập");
            RuleFor(x => x.Name).NotNull().WithMessage("Name bắt buộc nhập");
        }
    }
}