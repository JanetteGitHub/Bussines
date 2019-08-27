
namespace Bussines.Helpers
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using Interfaces;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty("Text")]
    public class TraslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci;
        const string ResourceId = "Bussines.Resources.Resource";
        static readonly Lazy<ResourceManager> ResMgr =
            new Lazy<ResourceManager>(() => new ResourceManager(
                ResourceId,
                typeof(TraslateExtension).GetTypeInfo().Assembly));
        public TraslateExtension()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCulturenInfo();

        }
        public string Text { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return "";
            }
            var translation = ResMgr.Value.GetString(Text, ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    string.Format(
                        "key '{0}'was not found in resources '{1}' for culture '{2}'",
                        Text, ResourceId, ci.Name), "Text");
#else
                translation = Text; //
#endif
            }
            return translation;
        }
    }
}
