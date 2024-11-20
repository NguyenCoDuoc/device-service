using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class DepartmentValidator : AbstractValidator<DepartmentDtoCreate>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.Code).NotNull().WithMessage("Code bắt buộc nhập");
            RuleFor(x => x.Name).NotNull().WithMessage("Name bắt buộc nhập");
        }
    }
}