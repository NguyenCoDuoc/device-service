using FluentValidation;
using DeviceService.Application.DTOS;

namespace DeviceService.Application.Validators
{
    public class SerialValidator : AbstractValidator<SerialDtoCreate>
    {
        public SerialValidator()
        {
            RuleFor(x => x.WarrantyPeriod).NotNull().WithMessage("Số tháng bảo hành bắt buộc");
            RuleFor(x => x.PurchaseDate).NotNull().WithMessage("Ngày mua bắt buộc");
            RuleFor(x => x.LocationId).NotNull().WithMessage("Vị trí bắt buộc");
        }
    }
}