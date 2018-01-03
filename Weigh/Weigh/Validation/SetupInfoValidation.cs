using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Weigh.Validation
{
    public class SetupInfoValidation
    {
        public static ValidationResult WeightValidation(string weight)
        {
            bool isValid = true;
            double Weight;

            // Perform validation logic here and set isValid to true or false.
            Weight = Convert.ToDouble(weight);

            if (Weight < 50 || Weight > 800)
            {
                isValid = false;
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(
                    "Weight must be in the range (50-800)");
            }
        }


    }
}
