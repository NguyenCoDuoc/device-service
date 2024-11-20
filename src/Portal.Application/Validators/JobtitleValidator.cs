using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class JobtitleValidator : AbstractValidator<JobtitleDtoCreate>
    {
        public JobtitleValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name bắt buộc nhập");
        }
    }
}