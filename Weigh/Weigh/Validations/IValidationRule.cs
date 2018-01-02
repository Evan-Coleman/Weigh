using System;
using System.Collections.Generic;
using System.Text;

namespace Weigh.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }
}
