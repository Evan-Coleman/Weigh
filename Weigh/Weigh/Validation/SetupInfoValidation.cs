using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Weigh.Helpers;

namespace Weigh.Validation
{
    public class SetupInfoValidation
    {

        public static ValidationResult HeightMajorValidation(string heightMajor)
        {
            bool isValid = true;
            double HeightMajor;

            // Perform validation logic here and set isValid to true or false.
            HeightMajor = Convert.ToDouble(heightMajor);

            // Units == true means imperial
            if (AppState.Units == true)
            {
                if (HeightMajor < 1 || HeightMajor > 15)
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
                        "Must be in the range (1-15)");
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
                        "(0-12)");
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
                if (heightMinor.Length < 0 || heightMinor.Length > 2)
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
                        "0-2chars");
                }
            }
            else
            {
                return ValidationResult.Success;
            }

        }



    }
}
