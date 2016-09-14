using ProjectAegis.Shared.Interfaces;

namespace ProjectAegis.AI.Models.Interfaces
{
    public interface IParameter : IBinaryModel
    {
        string Display { get; }
    }
}