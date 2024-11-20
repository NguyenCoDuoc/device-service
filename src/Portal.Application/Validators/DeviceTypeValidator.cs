using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class DeviceTypeValidator : AbstractValidator<DeviceTypeDtoCreate>
    {
        public DeviceTypeValidator()
        {
            RuleFor(x => x.Code).NotNull().WithMessage("Code bắt buộc nhập");
            RuleFor(x => x.Name).NotNull().WithMessage("Name bắt buộc nhập");
        }
    }
}