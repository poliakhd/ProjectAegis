namespace ProjectAegis.AI.Models.Interfaces
{
    using Shared.Library.Interfaces;

    public interface IParameter : IBinaryModel
    {
        string Display { get; }
    }
}