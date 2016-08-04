namespace ProjectAegis.Localization.Managers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Globalization;
    using System.Collections.Generic;

    using Interfaces;

    public class TranslationManager
    {
        #region Private Members

        private static TranslationManager _translationManager;

        #endregion

        public static TranslationManager Instance => _translationManager ?? (_translationManager = new TranslationManager());

        public event EventHandler LanguageChanged;
        public ITranslationProvider TranslationProvider { get; set; }

        public CultureInfo CurrentLanguage
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (value != Thread.CurrentThread.CurrentUICulture)
                {
                    Thread.CurrentThread.CurrentUICulture = value;
                    OnLanguageChanged();
                }
            }
        }
        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                if (TranslationProvider != null)
                {
                    return TranslationProvider.Languages;
                }
                return Enumerable.Empty<CultureInfo>();
            }
        }

        private void OnLanguageChanged()
        {
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }
        public object Translate(string key)
        {
            var translatedValue = TranslationProvider?.Translate(key);

            if (translatedValue != null)
                return translatedValue;

            return $"!{key}!";
        }
    }
}