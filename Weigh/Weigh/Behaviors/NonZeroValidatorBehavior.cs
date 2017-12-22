using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Weigh.Behaviors
{
    public class NonZeroValidatorBehavior : Behavior<Entry>
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
            bool isValid = (args.NewTextValue.Length != 0) ? true : false;
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
