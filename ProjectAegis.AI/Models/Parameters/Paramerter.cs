namespace ProjectAegis.AI.Models.Parameters
{
    using Interfaces;
    using Types;

    public static class Parameter
    {
        public static IParameter Resolve(ParameterType type)
        {
            switch (type)
            {
                case ParameterType.Attack:
                    return new AttackParameters();
                case ParameterType.Skill:
                    return new SkillParameters();
                case ParameterType.Say:
                    return new SayParameters();
                case ParameterType.ResetAggro:
                    return new ResetAggroParameters();
                case ParameterType.ExecTrigger:
                    return new ExecTriggerParameters();
                case ParameterType.RemoveTrigger:
                    return new RemoveTriggerParameters();
                case ParameterType.EnableTrigger:
                    return new EnableTriggerParameters();
                case ParameterType.CreateTimer:
                    return new CreateTimerParameters();
                case ParameterType.RemoveTimer:
                    return new RemoveTimerParameters();
                case ParameterType.Flee:
                    return new FleeParameters();
                case ParameterType.BeTaunted:
                    return new BeTauntedParameters();
                case ParameterType.FadeTarget:
                    return new FadeTargetParameters();
                case ParameterType.FageAggro:
                    return new FadeAggroParameters();
                case ParameterType.Break:
                    return new BreakParameters();
                case ParameterType.ActiveSpawner:
                    return new ActiveSpawnerParameters();
                case ParameterType.SetCommmonData:
                    return new SetCommonDataParameters();
                case ParameterType.AddCommonData:
                    return new AddCommonDataParameters();
                case ParameterType.SummonMonster:
                    return new SummonMonsterParameters();
                case ParameterType.ChangePath:
                    return new ChangePathParameters();
                case ParameterType.PlayAction:
                    return new PlayActionParameters();
                case ParameterType.ReviseHistory:
                    return new ReviseHistoryParameters();
                case ParameterType.SetHistory:
                    return new SetHistoryParameters();
            }

            return new DefaultParameters();
        }
    }
}