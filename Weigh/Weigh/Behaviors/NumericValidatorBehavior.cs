using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Weigh.Behaviors
{
    public class NumericValidatorBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryChanged;
            base.OnAttachedTo(bindable);
            // Perform setup
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged += OnEntryChanged;
            base.OnDetachingFrom(bindable);
            // Perform clean up
        }

        private void OnEntryChanged (object sender, TextChangedEventArgs args)
        {
            double result;
            bool isValid = double.TryParse(args.NewTextValue, out result);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
