﻿using System.IO;
using ProjectAegis.AI.Helpers;
using ProjectAegis.AI.Models.Types;

namespace ProjectAegis.AI.Models
{
    using Shared.Library.Extensions;
    using Shared.Library.Interfaces;

    public class Condition : IBinaryModel
    {
        public int Id { get; set; }
        public ConditionType Type { get; set; }

        public string TypeDisplay => Type.Display(Value);
        public int ArgBytes { get; set; }
        public byte[] Value { get; set; }
        public int ConditionType { get; set; }
        public Condition ConditionLeft { get; set; }
        public int SubNodeL { get; set; }
        public Condition ConditionRight { get; set; }
        public int SubNodeR { get; set; }

        public string Display
        {
            get
            {
                if (ConditionLeft == null && ConditionRight == null)
                    return $"{TypeDisplay}";

                if (ConditionLeft == null)
                    return $"{Type}({ConditionRight?.Display})";

                return $"({ConditionLeft?.Display}{TypeDisplay}{ConditionRight?.Display})";
            }
        }

        #region Implementation of IBinaryModel

        public void ReadModel(BinaryReader reader, int version = 0, params object[] parameters)
        {
            Id = reader.ReadInt32();
            Type = (ConditionType)Id;
            ArgBytes = reader.ReadInt32();
            Value = reader.ReadBytes(ArgBytes);
            ConditionType = reader.ReadInt32();

            if (ConditionType == 1)
            {
                ConditionLeft = reader.ReadModel<Condition>();
                SubNodeL = reader.ReadInt32();
                ConditionRight = reader.ReadModel<Condition>();
                SubNodeR = reader.ReadInt32();
            }
            if (ConditionType == 2)
            {
                ConditionRight = reader.ReadModel<Condition>();
                SubNodeR = reader.ReadInt32();
            }
        }
        public void WriteModel(BinaryWriter writer, int version = 0, params object[] parameters)
        {
            writer.Write(Id);
            writer.Write(ArgBytes);
            writer.Write(Value);
            writer.Write(ConditionType);

            if (ConditionType == 1)
            {
                writer.WriteModel(ConditionLeft);
                writer.Write(SubNodeL);
                writer.WriteModel(ConditionRight);
                writer.Write(SubNodeR);
            }
            if (ConditionType == 2)
            {
                writer.WriteModel(ConditionRight);
                writer.Write(SubNodeR);
            }
        }

        #endregion
    }
}