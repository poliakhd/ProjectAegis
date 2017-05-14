namespace ProjectAegis.AI.Extensions
{
    using System.IO;

    using Models.Interfaces;
    using Models.Parameters;
    using Models.TargetParameters;
    using Models.Types;
    using Shared.Library.Extensions;

    public static class BinaryExtensions
    {
        public static IParameter ReadParameters(this BinaryReader reader, ParameterType parameterType, int version = 0)
        {
            switch (parameterType)
            {
                case ParameterType.Attack:
                    return reader.ReadModel<AttackParameters>();
                case ParameterType.Skill:
                    return reader.ReadModel<SkillParameters>();
                case ParameterType.Say:
                    return reader.ReadModel<SayParameters>(version);
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
                case ParameterType.DeliverFactionPvpPoints:
                    return reader.ReadModel<DeliverFactionPvpPointsParameters>();
                case ParameterType.CalcVar:
                    return reader.ReadModel<CalcVarParameters>();
                case ParameterType.SummonMonster2:
                    return reader.ReadModel<SummonMonster2Parameters>();
                case ParameterType.ChangePath2:
                    return reader.ReadModel<ChangePath2Parameters>();
                case ParameterType.Skill2:
                    return reader.ReadModel<Skill2Parameters>();
                case ParameterType.ActiveSpawner2:
                    return reader.ReadModel<ActiveSpawner2Parameters>();
                case ParameterType.DeliverTask:
                    return reader.ReadModel<DeliverTaskParameters>();
                case ParameterType.SummonMine:
                    return reader.ReadModel<SummonMineParameters>();
                case ParameterType.SummonNpc:
                    return reader.ReadModel<SummonNpcParameters>();
                case ParameterType.DeliverRandomTaskInRegion:
                    return reader.ReadModel<DeliverRandomTaskInRegionParameters>();
                case ParameterType.DeliverRandomTaskInHateList:
                    return reader.ReadModel<DeliverRandomTaskInHateListParameters>();
                case ParameterType.ClearTowerTaskInRegion:
                    return reader.ReadModel<ClearTowerTaskInRegionParameters>();
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

        public static void WriteParameters(this BinaryWriter writer, IParameter parameter, ParameterType parameterType, int version = 0)
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
                    writer.WriteModel(parameter as SayParameters, version);
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
                case ParameterType.DeliverFactionPvpPoints:
                    writer.WriteModel(parameter as DeliverFactionPvpPointsParameters);
                    break;
                case ParameterType.CalcVar:
                    writer.WriteModel(parameter as CalcVarParameters);
                    break;
                case ParameterType.SummonMonster2:
                    writer.WriteModel(parameter as SummonMonster2Parameters);
                    break;
                case ParameterType.ChangePath2:
                    writer.WriteModel(parameter as ChangePath2Parameters);
                    break;
                case ParameterType.Skill2:
                    writer.WriteModel(parameter as Skill2Parameters);
                    break;
                case ParameterType.ActiveSpawner2:
                    writer.WriteModel(parameter as ActiveSpawner2Parameters);
                    break;
                case ParameterType.DeliverTask:
                    writer.WriteModel(parameter as DeliverTaskParameters);
                    break;
                case ParameterType.SummonMine:
                    writer.WriteModel(parameter as SummonMineParameters);
                    break;
                case ParameterType.SummonNpc:
                    writer.WriteModel(parameter as SummonNpcParameters);
                    break;
                case ParameterType.DeliverRandomTaskInRegion:
                    writer.WriteModel(parameter as DeliverRandomTaskInRegionParameters);
                    break;
                case ParameterType.DeliverRandomTaskInHateList:
                    writer.WriteModel(parameter as DeliverRandomTaskInHateListParameters);
                    break;
                case ParameterType.ClearTowerTaskInRegion:
                    writer.WriteModel(parameter as ClearTowerTaskInRegionParameters);
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