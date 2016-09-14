namespace ProjectAegis.AI.Helpers
{
    using System;
    using System.Linq;

    using Models.Types;
    using Models.Types.Methods;
    using Models.Types.Operands;
    using Models.Types.Interfaces;

    public static class Helpers
    {
        public static byte[] Clear(this byte[] bytes)
        {
            int position = -1;
            var clearBytes = new byte[128];

            for (int i = 0; i < bytes.Length; i++)
                if (bytes[i] == 0)
                {
                    position = i;
                    break;
                }

            Array.Copy(bytes, 0, clearBytes, 0, position);
            return clearBytes;
        }

        public static string Display(this ConditionType type, byte[] args)
        {
            switch (type)
            {
                case ConditionType.IsTimerTicking:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.PublicCounter:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.Value:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.IsHpLess:
                    return $"{type}({BitConverter.ToSingle(args, 0).ToString("F2")})";
                case ConditionType.Randomize:
                    return $"{type}({BitConverter.ToSingle(args, 0).ToString("F2")})";
                case ConditionType.IsCombatStarted:
                    return $"{type}()";
                case ConditionType.IsTargetDead:
                    return $"{type}()";
                case ConditionType.Or:
                    return " || ";
                case ConditionType.And:
                    return " && ";
                case ConditionType.IsDead:
                    return $"{type}()";
                case ConditionType.Plus:
                    return " + ";
                case ConditionType.Minus:
                    return " - ";
                case ConditionType.Equals:
                    return " == ";
                case ConditionType.Gt:
                    return " > ";
                case ConditionType.GtOrEquals:
                    return " >= ";
                case ConditionType.Lt:
                    return " < ";
                case ConditionType.LtOrEquals:
                    return " > ";
                case ConditionType.IsEvent:
                    return $"{type}()";
                case ConditionType.Not:
                    return "!";
                default:
                    return "?";
            }
        }


        public static IParameterType GetConditionType(this int id)
        {
            var operands = new int[] { 5, 6, 7, 9, 10, 11, 12, 13, 14, 15 };
            var methods = new int[] { 0, 1, 2, 3, 4, 8, 16, 17, 18 };

            if (operands.Any(x => x == id))
                return new Operands() { Type = (OperandType)id };
            if (methods.Any(x => x == id))
                return new Methods() { Type = (MethodType)id };

            return null;
        }
    }
}