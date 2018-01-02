using Prism.Events;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using Weigh.ViewModels;

namespace Weigh.Validations
{
    public class ValidatableObject<T> : ViewModelBase, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;

        public List<IValidationRule<T>> Validations => _validations;


        private List<string> _errors;
        public List<string> Errors
        {
            get { return _errors; }
            set { SetProperty(ref _errors, value); }
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private bool _isValid;
        public bool IsValid
        {
            get { return _isValid; }
            set { SetProperty(ref _isValid, value); }
        }

        public ValidatableObject(INavigationService navigationService, IEventAggregator ea)
            : base(navigationService)
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
