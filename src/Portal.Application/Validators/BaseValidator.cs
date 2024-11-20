using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using Sun.Core.Share.Helpers.Util;
using DeviceService.Application.Helpers;

namespace DeviceService.Application.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>, IValidatorInterceptor
    {
        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext; // Giữ lại hành vi mặc định
        }
        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                ValidationFailure validationFailure = result.Errors.First<ValidationFailure>();
                
                throw new Exception(validationFailure.ErrorMessage); 
            }
            return result;
        }
    }
}

