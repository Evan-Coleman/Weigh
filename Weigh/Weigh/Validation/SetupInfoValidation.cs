using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weigh.Helpers;

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
                    "Must be in the range (50-800)");
            }
        }

        public static ValidationResult AgeValidation(string age)
        {
            bool isValid = true;
            double Age;

            // Perform validation logic here and set isValid to true or false.
            Age = Convert.ToDouble(age);

            if (Age < 10 || Age > 120)
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
                    "Must be in the range (10-120)");
            }
        }

        public static ValidationResult HeightMajorValidation(string heightMajor)
        {
            bool isValid = true;
            double HeightMajor;

            // Perform validation logic here and set isValid to true or false.
            HeightMajor = Convert.ToDouble(heightMajor);

            if (AppState.Units == true)
            {
                if (HeightMajor < 1 || HeightMajor > 11)
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
                        "Must be in the range (1-300)");
                }
            }
            else
            {
                if (HeightMajor < 50 || HeightMajor > 300)
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
                        "Must be in the range (50-300)");
                }
            }

        }
        

    }
}
