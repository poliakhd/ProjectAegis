namespace ProjectAegis.Localization
{
    using System;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class TranslateExtension : MarkupExtension
    {
        #region Private Members

        private string _key;

        #endregion

        [ConstructorArgument("key")]
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public TranslateExtension(string key)
        {
            _key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding("Value")
            {
                Source = new TranslationData(_key)
            };
            return binding.ProvideValue(serviceProvider);
        }
    }
}