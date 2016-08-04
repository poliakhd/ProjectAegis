namespace ProjectAegis.Localization.Providers
{
    using System.Resources;
    using System.Reflection;
    using System.Globalization;
    using System.Collections.Generic;

    using Interfaces;

    public class ResxTranslationProvider : ITranslationProvider
    {
        #region Private Members

        private readonly ResourceManager _resourceManager;

        #endregion

        public ResxTranslationProvider(string baseName, Assembly assembly)
        {
            _resourceManager = new ResourceManager(baseName, assembly);
        }

        #region Implementation of ITranslationProvider

        public object Translate(string key)
        {
            return _resourceManager.GetString(key);
        }

        public IEnumerable<CultureInfo> Languages
        {
            get
            {
                yield return new CultureInfo("en");
            }
        }

        #endregion
    }
}