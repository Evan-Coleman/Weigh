using System;

namespace Weigh.Validations
{
    public class WaistSizeWithinLimitsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var weight = Convert.ToDouble(value);

            if (weight < 10)
            {
                ValidationMessage = "No waist sizes below 10";
                return false;
            }

            if (weight > 100)
            {
                ValidationMessage = "No waist sizes above 100";
                return false;
            }

            return true;
        }
    }
}
