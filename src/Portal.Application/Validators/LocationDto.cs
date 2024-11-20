using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class LocationValidator : AbstractValidator<LocationDtoCreate>
    {
        public LocationValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name bắt buộc nhập");
        }
    }
}