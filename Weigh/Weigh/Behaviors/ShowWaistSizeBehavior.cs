using Weigh.Helpers;
using Xamarin.Forms;

namespace Weigh.Behaviors
{
    public class ShowWaistSizeBehavior : Behavior<Label>
    {
        protected override void OnAttachedTo(Label bindable)
        {
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Label bindable)
        {
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var label = (Label) sender;

            if (Settings.WaistSizeEnabled == false)
                label.IsVisible = false;
            else
                label.IsVisible = true;
        }
    }
}