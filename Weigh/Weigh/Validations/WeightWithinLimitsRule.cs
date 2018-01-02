﻿using System;

namespace Weigh.Validations
{
    public class WeightWithinLimitsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var weight = Convert.ToDouble(value);

            if (weight < 50)
            {
                ValidationMessage = "No Weights below 50";
                return false;
            }

            if (weight > 800)
            {
                ValidationMessage = "No Weights above 800";
                return false;
            }

            return true;
        }
    }
}
