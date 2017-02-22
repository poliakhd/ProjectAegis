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
                case ConditionType.TimeCome:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.Var:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.Constant:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.HpLess:
                    return $"{type}({BitConverter.ToSingle(args, 0).ToString("F2")})";
                case ConditionType.Random:
                    return $"{type}({BitConverter.ToSingle(args, 0).ToString("F2")})";
                case ConditionType.StartAttack:
                    return $"{type}()";
                case ConditionType.KillPlayer:
                    return $"{type}()";
                case ConditionType.Or:
                    return " || ";
                case ConditionType.And:
                    return " && ";
                case ConditionType.Died:
                    return $"{type}()";
                case ConditionType.Plus:
                    return " + ";
                case ConditionType.Minus:
                    return " - ";
                case ConditionType.Equ:
                    return " == ";
                case ConditionType.Great:
                    return " > ";
                case ConditionType.Less:
                    return " > ";
                case ConditionType.Multiply:
                    return " * ";
                case ConditionType.Divide:
                    return " / ";
                case ConditionType.BeHurt:
                    return $"{type}()";
                case ConditionType.Not:
                    return "!";
                case ConditionType.ReachEnd:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.AtHistoryStage:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.HistoryValue:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.StopFight:
                    return $"{type}()";
                case ConditionType.LocalVar:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.ReachEnd2:
                    return $"{type}({BitConverter.ToInt32(args, 0)}, {BitConverter.ToInt32(args, 3)})";
                case ConditionType.HasFilter:
                    return $"{type}({BitConverter.ToInt32(args, 0)})";
                case ConditionType.RoomIndex:
                    return $"{type}()";
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