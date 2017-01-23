namespace ProjectAegis.AI.Models.Types.Methods
{
    public enum MethodType
    {
        IsTimerTicking = 0,
        IsHpLess = 1,
        IsCombatStarted = 2,
        Randomize = 3,
        IsTargetDead = 4,
        IsDead = 8,
        PulbicCounter = 16,
        Value = 17,
        IsEvent = 18,
        Unk = -1
    }

    public enum Test
    {
        c_time_come = 0x0,
        c_hp_less = 0x1,
        c_start_attack = 0x2,
        c_random = 0x3,
        c_kill_player = 0x4,
        c_not = 0x5,
        c_or = 0x6,
        c_and = 0x7,
        c_died = 0x8,
        c_plus = 0x9,
        c_minus = 0xA,
        c_multiply = 0xB,
        c_divide = 0xC,
        c_great = 0xD,
        c_less = 0xE,
        c_equ = 0xF,
        c_var = 0x10,
        c_constant = 0x11,
        c_be_hurt = 0x12,
        c_reach_end = 0x13,
        c_at_history_stage = 0x14,
        c_history_value = 0x15,
        c_stop_fight = 0x16,
        c_local_var = 0x17,
        c_reach_end_2 = 0x18,
        c_has_filter = 0x19,
        c_room_index = 0x1A,
        c_num = 0x1B,
    }
}