using System;
using System.ComponentModel.DataAnnotations;
using Weigh.Helpers;
using Weigh.Localization;

namespace Weigh.Validation
{
    public class SetupInfoValidation
    {
        public static ValidationResult HeightMajorValidation(string heightMajor)
        {
            var isValid = true;

            // Perform validation logic here and set isValid to true or false.
            var HeightMajor = Convert.ToDouble(heightMajor);

            // Units == true means imperial
            if (Settings.Units)
            {
                if (HeightMajor < 1 || HeightMajor > 15) isValid = false;

                if (isValid)
                    return ValidationResult.Success;
                return new ValidationResult(
                    AppResources.HeightMajorImperialValidationErrorMessage);
            }

            if (HeightMajor < 50 || HeightMajor > 300) isValid = false;

            if (isValid)
                return ValidationResult.Success;
            return new ValidationResult(
                AppResources.HeightMajorMetricValidationErrorMessage);
        }

        public static ValidationResult HeightMinorValidation(string heightMinor)
        {
            var isValid = true;
            double HeightMinor;

            // Perform validation logic here and set isValid to true or false.
            HeightMinor = Convert.ToDouble(heightMinor);

            if (Settings.Units)
            {
                if (HeightMinor < 0 || HeightMinor > 12) isValid = false;

                if (isValid)
                    return ValidationResult.Success;
                return new ValidationResult(
                    "(0-12)");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult HeightMinorLengthValidation(string heightMinor)
        {
            var isValid = true;

            // Perform validation logic here and set isValid to true or false.

            if (Settings.Units)
            {
                if (heightMinor.Length < 0 || heightMinor.Length > 2) isValid = false;

                if (isValid)
                    return ValidationResult.Success;
                return new ValidationResult(
                    "0-2chars");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult WaistSizeValidation(string waistSize)
        {
            var WaistSize = Convert.ToDouble(waistSize);

            // Perform validation logic here and set isValid to true or false.
            if (Settings.WaistSizeEnabled)
            {
                if (waistSize.Length < 2 || waistSize.Length > 5)
                    return new ValidationResult(AppResources.WaistLengthValidationErrorMessage);
                if (WaistSize > 200 || WaistSize < 15)
                    return new ValidationResult(AppResources.WaistValueValidationErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}