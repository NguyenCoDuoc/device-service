using FluentValidation;
using DeviceService.Application.DTOS.Users;

namespace DeviceService.Application.Validators
{
    public class UsersCreateValidator : BaseValidator<UsersDTOCreate>
    {
        public UsersCreateValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
        }
    }
    public class UsersUpdateValidator : BaseValidator<UsersDTOUpdate>
    {
        public UsersUpdateValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
        }
    }
    public class UsersRequestValidator : BaseValidator<UsersRequestDTO>
    {
        public UsersRequestValidator()
        {
            RuleFor(x => x.RequestId).NotEmpty().WithMessage("RequestId is required");
            RuleFor(x => x.RequestNumber).NotEmpty().WithMessage("RequestNumber is required");
            RuleFor(x => x.CreateBy).NotEmpty().WithMessage("CreateBy is required");
            RuleFor(x => x.RequestDate).NotEmpty().WithMessage("RequestDate is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.AdminUser).NotEmpty().WithMessage("AdminUser is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Full Name is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .Must(emailList =>
                {    
                    // Kiểm tra nếu có dấu chấm phẩy trong chuỗi email
                    if (emailList.Contains(";"))
                    {
                        return false; // Trả về false nếu chuỗi chứa dấu chấm phẩy
                    }
                    // Tách các email bằng dấu phẩy
                    var emails = emailList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    // Duyệt qua từng email và kiểm tra định dạng của nó
                    foreach (var email in emails)
                    {
                        if (!IsValidEmail(email.Trim()))
                        {
                            return false; // Nếu bất kỳ email nào không hợp lệ, trả về false
                        }
                    }
                    return true; // Tất cả email đều hợp lệ
                }).WithMessage("Invalid email format");
            RuleFor(x => x.FollowEmail)
                .Must(emailList =>
                {
                    // Nếu không có email hoặc trường rỗng thì bỏ qua kiểm tra
                    if (string.IsNullOrEmpty(emailList))
                    {
                        return true; // Không bắt buộc, cho phép bỏ qua nếu không nhập email
                    }
                    // Kiểm tra nếu có dấu chấm phẩy trong chuỗi email
                    if (emailList.Contains(";"))
                    {
                        return false; // Trả về false nếu chuỗi chứa dấu chấm phẩy
                    }
                    // Tách các email bằng dấu phẩy
                    var emails = emailList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    // Duyệt qua từng email và kiểm tra định dạng của nó
                    foreach (var email in emails)
                    {
                        if (!IsValidEmail(email.Trim()))
                        {
                            return false; // Nếu bất kỳ email nào không hợp lệ, trả về false
                        }
                    }
                    return true; // Tất cả email đều hợp lệ
                }).WithMessage("Invalid email format");
            RuleFor(x => x.AdminPassword)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.cmp_wwn).NotEmpty().WithMessage("cmp_wwn is required");
        }
        // Hàm helper để kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false; // Nếu email không hợp lệ, bắt ngoại lệ và trả về false
            }
        }
    }
}
