using System.ComponentModel.DataAnnotations;

namespace University.Web.Validations
{
    public class CheckStreetNrAttribute : ValidationAttribute
    {
        private readonly int max;

        public CheckStreetNrAttribute(int max)
        {
            this.max = max;
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
