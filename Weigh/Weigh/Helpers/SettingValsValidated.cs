using System.ComponentModel.DataAnnotations;
using Weigh.Localization;
using Weigh.Validation;

namespace Weigh.Models
{
    public class SettingValsValidated : ValidatableBase
    {
        #region Methods

        public void InitializeFromSettings(SettingVals validatedSettings)
        {
            Age = validatedSettings.Age.ToString();
            GoalWeight = validatedSettings.GoalWeight.ToString();
            HeightMajor = validatedSettings.HeightMajor.ToString();
            HeightMinor = validatedSettings.HeightMinor.ToString();
            WaistSize = validatedSettings.WaistSize.ToString();
            Weight = validatedSettings.Weight.ToString();
        }

        #endregion

        #region Fields

        private string _age;

        [Required(ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(3, MinimumLength = 2, ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "AgeLengthValidationErrorMessage")]
        [Range(1, 150, ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "AgeValueValidationErrorMessage")]
        public string Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        private string _goalWeight;

        [Required(ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(7, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "GoalWeightLengthValidationErrorMessage")]
        [Range(0.0, 1000.0, ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "GoalWeightValueValidationErrorMessage")]
        public string GoalWeight
        {
            get => _goalWeight;
            set => SetProperty(ref _goalWeight, value);
        }

        private string _heightMajor;

        [Required(ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(5, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "HeightMajorLengthValidationErrorMessage")]
        [CustomValidation(typeof(SetupInfoValidation), "HeightMajorValidation")]
        public string HeightMajor
        {
            get => _heightMajor;
            set => SetProperty(ref _heightMajor, value);
        }

        private string _heightMinor;

        [CustomValidation(typeof(SetupInfoValidation), "HeightMinorValidation")]
        [CustomValidation(typeof(SetupInfoValidation), "HeightMinorLengthValidation")]
        public string HeightMinor
        {
            get => _heightMinor;
            set => SetProperty(ref _heightMinor, value);
        }

        private string _waistSize;

        [CustomValidation(typeof(SetupInfoValidation), "WaistSizeValidation")]
        public string WaistSize
        {
            get => _waistSize;
            set => SetProperty(ref _waistSize, value);
        }

        private string _weight;

        [Required(ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "ValidationRequiredErrorMessage")]
        [StringLength(7, MinimumLength = 1, ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "WeightLengthValidationErrorMessage")]
        [Range(0.0, 1000.0, ErrorMessageResourceType = typeof(AppResources),
            ErrorMessageResourceName = "WeightValueValidationErrorMessage")]
        public string Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        #endregion
    }
}