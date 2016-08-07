namespace ProjectAegis.Shared.Extensions
{
    using Interfaces;

    using System.IO;

    public static class BinaryExtensions
    {
        public static TModel ReadModel<TModel>(this BinaryReader reader, int version = 0)
            where TModel : IBinaryModel, new()
        {
            var model = new TModel();
            model.ReadModel(reader, version);
            return model;
        }
        public static void WriteModel<TModel>(this BinaryWriter writer, TModel model, int version = 0)
            where TModel : IBinaryModel
        {
            model.WriteModel(writer, version);
        }

        public static TModel ReadModelWithParameters<TModel>(this BinaryReader reader,  int version = 0, params object[] prameters)
            where TModel : IBinaryModel, new()
        {
            var model = new TModel();
            model.ReadModel(reader, version, prameters);
            return model;
        }
        public static void WriteModelWithParameters<TModel>(this BinaryWriter writer, TModel model, int version = 0, params object[] prameters)
            where TModel : IBinaryModel
        {
            model.WriteModel(writer, version, prameters);
        }
    }
}