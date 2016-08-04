namespace ProjectAegis.Localization.Interfaces
{
    using System.Globalization;
    using System.Collections.Generic;
    
    public interface ITranslationProvider
    {
        object Translate(string key);
        IEnumerable<CultureInfo> Languages { get; }
    }
}