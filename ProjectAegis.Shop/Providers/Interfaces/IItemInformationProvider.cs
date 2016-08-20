using System.Collections.Generic;

namespace ProjectAegis.Shop.Providers.Interfaces
{
    public interface IItemInformationProvider
    {
        void Initialize();

        string GetName(int itemId, string provider);
        string GetDescription(int itemId, string provider);

        IEnumerable<string> GetAvailableProviders();
    }
}