﻿namespace ProjectAegis.AI.Models.Parameters
{
    using System.IO;

    using Caliburn.Micro;

    using Interfaces;

    public class FleeParameters : PropertyChangedBase, IParameter
    {
        public string Display => "Flee();";

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            
        }

        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            
        }

        #endregion
    }
}