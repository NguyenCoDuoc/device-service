using FluentValidation;
using Portal.Application.DTOS.QCInspectionRequestDetail;

namespace Portal.Application.Validators
{
    public class QCInspectionRequestDetailValidator : BaseValidator<QCInspectionRequestDetailDTOUpdate>
    {

        public QCInspectionRequestDetailValidator()
        {
            // Kiểm tra Id không được để trống
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            // Kiểm tra LotSize lớn hơn 0
            RuleFor(x => x.LotSize)
                .GreaterThan(0).WithMessage("LotSize must be greater than 0.");

            // Kiểm tra LotQuantity lớn hơn 0
            RuleFor(x => x.LotQuantity)
                .GreaterThan(0).WithMessage("LotQuantity must be greater than 0.");

            // Kiểm tra InspectionQuantity lớn hơn 0
            RuleFor(x => x.InspectionQuantity)
                .GreaterThan(0).WithMessage("InspectionQuantity must be greater than 0.");

            // Kiểm tra QCTestType không được để trống
            RuleFor(x => x.QCTestType)
                .NotEmpty().WithMessage("QCTestType is required.");

            // Kiểm tra FromDate phải nhỏ hơn hoặc bằng ToDate
            RuleFor(x => x.FromDate)
                .LessThanOrEqualTo(x => x.ToDate).WithMessage("FromDate must be less than or equal to ToDate.");

            // Kiểm tra ToDate không được để trống
            RuleFor(x => x.ToDate)
                .NotEmpty().WithMessage("ToDate is required.");

            // Kiểm tra InspectionLocation không được để trống
            RuleFor(x => x.InspectionLocation)
                .NotEmpty().WithMessage("InspectionLocation is required.");

           
          

            // Kiểm tra InspectionDate không được để trống
            RuleFor(x => x.InspectionDate)
                .NotEmpty().WithMessage("InspectionDate is required.");

            // Kiểm tra InspectionBy không được để trống
            RuleFor(x => x.InspectionBy)
                .NotEmpty().WithMessage("InspectionBy is required.");
        }
    }

}
