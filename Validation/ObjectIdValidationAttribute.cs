using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace EStoreAPI.Validation
{
    public class ObjectIdValidationAttribute : ValidationAttribute
    {
        public ObjectIdValidationAttribute() : base("Ogiltigt ObjectId.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !ObjectId.TryParse(value.ToString(), out _))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
