using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Prism.Mvvm;
using Weigh.Validation;

namespace Weigh.Models
{
    public class SetupInfo : ValidatableBase
    {
        private string _weight;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(4, MinimumLength = 2, ErrorMessage = "Minimum 2 chars, max 4 chars")]
        [CustomValidation(typeof(SetupInfoValidation), "WeightValidation")]
        public string Weight
        {
            get { return _weight; }
            set { SetProperty(ref _weight, value); }
        }
    }
}
