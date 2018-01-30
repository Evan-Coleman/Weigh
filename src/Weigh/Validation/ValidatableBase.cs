using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Weigh.ViewModels;

namespace Weigh.Validation
{
    public class ValidatableBase : BindableBase, IValidatableBase
    {
        readonly Validator validator;

        public bool IsValidationEnabled
        {
            get { return validator.IsValidationEnabled; }
            set { validator.IsValidationEnabled = value; }
        }

        public Validator Errors
        {
            get { return validator; }
        }

        public bool IsDirty { get; set; }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { validator.ErrorsChanged += value; }
            remove { validator.ErrorsChanged -= value; }
        }

        public ValidatableBase()
        {
            validator = new Validator(this);
            IsDirty = false;
        }

        public ReadOnlyDictionary<string, ReadOnlyCollection<string>> GetAllErrors()
        {
            return validator.GetAllErrors();
        }

        public bool ValidateProperties()
        {
            return validator.ValidateProperties();
        }

        public void SetAllErrors(IDictionary<string, ReadOnlyCollection<string>> entityErrors)
        {
            validator.SetAllErrors(entityErrors);
        }

        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (storage != null && value != null && storage.ToString() == "" && value.ToString() != "")
            {
                IsDirty = true;
            }

            if (propertyName == "GoalWeight")
            {
                Console.WriteLine(IsDirty);
            }

            var result = base.SetProperty(ref storage, value, propertyName);

            if (result && !string.IsNullOrWhiteSpace(propertyName))
            {
                if (validator.IsValidationEnabled)
                {
                    validator.ValidateProperty(propertyName);
                }
            }
            return result;
        }
    }
}
