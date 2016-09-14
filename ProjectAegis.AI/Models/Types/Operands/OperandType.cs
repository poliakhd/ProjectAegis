namespace ProjectAegis.AI.Models.Types.Operands
{
    using System.ComponentModel;

    public enum OperandType
    {
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