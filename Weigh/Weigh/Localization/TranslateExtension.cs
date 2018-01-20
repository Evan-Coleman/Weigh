using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Weigh.Localization
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "Weigh.Localization.AppResources";
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return null;
            var resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

            return resourceManager.GetString(Text, CultureInfo.CurrentCulture);
        }
    }
}