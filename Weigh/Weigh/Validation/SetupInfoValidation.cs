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

        public static ValidationResult HeightMinorValidation(string heightMinor)
        {
            bool isValid = true;
            double HeightMinor;

            // Perform validation logic here and set isValid to true or false.
            HeightMinor = Convert.ToDouble(heightMinor);

            if (AppState.Units == true)
            {
                if (HeightMinor < 0 || HeightMinor > 12)
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
                        "Must be in the range (0-12)");
                }
            }
            else
            {
                return ValidationResult.Success;
            }

        }

        public static ValidationResult HeightMinorLengthValidation(string heightMinor)
        {
            bool isValid = true;

            // Perform validation logic here and set isValid to true or false.

            if (AppState.Units == true)
            {
                if (heightMinor.Length < 1 || heightMinor.Length > 2)
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
                        "Minimum 1 chars, max 2 chars");
                }
            }
            else
            {
                return ValidationResult.Success;
            }

        }



    }
}
