namespace ProjectAegis.AI.Extensions
{
    using System.IO;

    using Models.Interfaces;
    using Models.Parameters;
    using Models.TargetParameters;
    using Models.Types;
    using Shared.Extensions;

    public static class BinaryExtensions
    {
        public static IParameter ReadParameters(this BinaryReader reader, ParameterType parameterType)
        {
            switch (parameterType)
            {
                case ParameterType.Attack:
                    return reader.ReadModel<AttackParameters>();
                case ParameterType.Skill:
                    return reader.ReadModel<SkillParameters>();
                case ParameterType.Say:
                    return reader.ReadModel<SayParameters>();
                case ParameterType.ResetAggro:
                    return reader.ReadModel<ResetAggroParameters>();
                case ParameterType.ExecTrigger:
                    return reader.ReadModel<ExecTriggerParameters>();
                case ParameterType.RemoveTrigger:
                    return reader.ReadModel<RemoveTriggerParameters>();
                case ParameterType.EnableTrigger:
                    return reader.ReadModel<EnableTriggerParameters>();
                case ParameterType.CreateTimer:
                    return reader.ReadModel<CreateTimerParameters>();
                case ParameterType.RemoveTimer:
                    return reader.ReadModel<RemoveTimerParameters>();
                case ParameterType.Flee:
                    return reader.ReadModel<FleeParameters>();
                case ParameterType.BeTaunted:
                    return reader.ReadModel<BeTauntedParameters>();
                case ParameterType.FadeTarget:
                    return reader.ReadModel<FadeTargetParameters>();
                case ParameterType.FageAggro:
                    return reader.ReadModel<FadeAggroParameters>();
                case ParameterType.Break:
                    return reader.ReadModel<BreakParameters>();
                case ParameterType.ActiveSpawner:
                    return reader.ReadModel<ActiveSpawnerParameters>();
                case ParameterType.SetCommmonData:
                    return reader.ReadModel<SetCommonDataParameters>();
                case ParameterType.AddCommonData:
                    return reader.ReadModel<AddCommonDataParameters>();
                case ParameterType.SummonMonster:
                    return reader.ReadModel<SummonMonsterParameters>();
                case ParameterType.ChangePath:
                    return reader.ReadModel<ChangePathParameters>();
                case ParameterType.PlayAction:
                    return reader.ReadModel<PlayActionParameters>();
                case ParameterType.ReviseHistory:
                    return reader.ReadModel<ReviseHistoryParameters>();
                case ParameterType.SetHistory:
                    return reader.ReadModel<SetHistoryParameters>();
                default:
                    return new DefaultParameters();
            }
        }
        public static IParameter ReadTargetParameters(this BinaryReader reader, TargetParameterType targetParameterType)
        {
            switch (targetParameterType)
            {
                case TargetParameterType.ClassCombo:
                    return reader.ReadModel<ClassComboParameters>();
                default:
                    return reader.ReadModel<DefaultParameters>();
            }
        }

        public static void WriteParameters(this BinaryWriter writer, IParameter parameter, ParameterType parameterType)
        {
            switch (parameterType)
            {
                case ParameterType.Attack:
                    writer.WriteModel(parameter as AttackParameters);
                    break;
                case ParameterType.Skill:
                    writer.WriteModel(parameter as SkillParameters);
                    break;
                case ParameterType.Say:
                    writer.WriteModel(parameter as SayParameters);
                    break;
                case ParameterType.ResetAggro:
                    writer.WriteModel(parameter as ResetAggroParameters);
                    break;
                case ParameterType.ExecTrigger:
                    writer.WriteModel(parameter as ExecTriggerParameters);
                    break;
                case ParameterType.RemoveTrigger:
                    writer.WriteModel(parameter as RemoveTriggerParameters);
                    break;
                case ParameterType.EnableTrigger:
                    writer.WriteModel(parameter as EnableTriggerParameters);
                    break;
                case ParameterType.CreateTimer:
                    writer.WriteModel(parameter as CreateTimerParameters);
                    break;
                case ParameterType.RemoveTimer:
                    writer.WriteModel(parameter as RemoveTimerParameters);
                    break;
                case ParameterType.Flee:
                    writer.WriteModel(parameter as FleeParameters);
                    break;
                case ParameterType.BeTaunted:
                    writer.WriteModel(parameter as BeTauntedParameters);
                    break;
                case ParameterType.FadeTarget:
                    writer.WriteModel(parameter as FadeTargetParameters);
                    break;
                case ParameterType.FageAggro:
                    writer.WriteModel(parameter as FadeAggroParameters);
                    break;
                case ParameterType.Break:
                    writer.WriteModel(parameter as BreakParameters);
                    break;
                case ParameterType.ActiveSpawner:
                    writer.WriteModel(parameter as ActiveSpawnerParameters);
                    break;
                case ParameterType.SetCommmonData:
                    writer.WriteModel(parameter as SetCommonDataParameters);
                    break;
                case ParameterType.AddCommonData:
                    writer.WriteModel(parameter as AddCommonDataParameters);
                    break;
                case ParameterType.SummonMonster:
                    writer.WriteModel(parameter as SummonMonsterParameters);
                    break;
                case ParameterType.ChangePath:
                    writer.WriteModel(parameter as ChangePathParameters);
                    break;
                case ParameterType.PlayAction:
                    writer.WriteModel(parameter as PlayActionParameters);
                    break;
                case ParameterType.ReviseHistory:
                    writer.WriteModel(parameter as ReviseHistoryParameters);
                    break;
                case ParameterType.SetHistory:
                    writer.WriteModel(parameter as SetHistoryParameters);
                    break;
            }
        }
        public static void WriteTargetParameters(this BinaryWriter writer, IParameter parameter, TargetParameterType targetParameterType)
        {
            switch (targetParameterType)
            {
                case TargetParameterType.ClassCombo:
                    writer.WriteModel(parameter as ClassComboParameters);
                    break;
            }
        }
    }
}