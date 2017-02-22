namespace ProjectAegis.AI.Models.Types
{
    using System.ComponentModel;

    public enum ConditionType
    {
        TimeCome = 0,
        HpLess = 1,
        StartAttack = 2,
        Random = 3,
        KillPlayer = 4,
        [Description("!")]
        Not = 5,
        [Description("||")]
        Or = 6,
        [Description("&&")]
        And = 7,
        Died = 8,
        [Description("+")]
        Plus = 9,
        [Description("-")]
        Minus = 10,
        [Description("*")]
        Multiply = 11,
        [Description("/")]
        Divide = 12,
        [Description(">")]
        Great = 13,
        [Description("<")]
        Less = 14,
        [Description("==")]
        Equ = 15,
        Var = 16,
        Constant = 17,
        BeHurt = 18,
        ReachEnd = 19,
        AtHistoryStage = 20,
        HistoryValue = 21,
        StopFight = 22,
        LocalVar = 23,
        ReachEnd2 = 24,
        HasFilter = 25,
        RoomIndex = 26
    }
}