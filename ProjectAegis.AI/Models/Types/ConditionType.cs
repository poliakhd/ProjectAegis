namespace ProjectAegis.AI.Models.Types
{
    using System.ComponentModel;

    public enum ConditionType
    {
        IsTimerTicking = 0,
        IsHpLess = 1,
        IsCombatStarted = 2,
        Randomize = 3,
        IsTargetDead = 4,
        IsDead = 8,
        PublicCounter = 16,
        Value = 17,
        IsEvent = 18,
        Unk = -1,

        [Description("!")]
        Not = 5,
        [Description("||")]
        Or = 6,
        [Description("&&")]
        And = 7,
        [Description("+")]
        Plus = 9,
        [Description("-")]
        Minus = 10,
        [Description("==")]
        Equals = 11,
        [Description(">")]
        Gt = 12,
        [Description(">=")]
        GtOrEquals = 13,
        [Description("<")]
        Lt = 14,
        [Description("<=")]
        LtOrEquals = 15,
    }
}