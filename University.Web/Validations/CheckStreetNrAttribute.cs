using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace University.Web.Validations
{
    public class CheckStreetNrAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int max;

        public CheckStreetNrAttribute(int max)
        {
            this.max = max;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-streetnr", "Felmedellande från servern");
            context.Attributes.Add("data-val-streetnr-max", $"{max}");
        }

        public override bool IsValid(object? value)
        {
            if(value is string input)
            {
                var num = input.Split().Last();
                return int.TryParse(num, out int res) && res <= max;
            }
            return false;
        }
    }
}
