using System.ComponentModel;

namespace yu_gi_oh.Components.Actions
{
    public enum MonsterActions
    {
        [Description("Summon in Attack")]
        [Hand]
        SUMMON_ATTACK,
        [Description("Summon in Defense")]
        [Hand]
        SUMMON_DEFENSE,
        [Description("Summon Face Down")]
        [Hand]
        SUMMON_FACE_DOWN,
        [Description("Flip")]
        [Field]
        FLIP,
        [Description("Send to Graveyard")]
        [Hand]
        [Field]
        SEND_GRAVEYARD,
        [Description("Send to Deck")]
        [Hand]
        [Field]
        SEND_DECK
    }


    public enum SpellActions
    {
        [Description("Activate")]
        [Hand]
        [Field]
        ACTIVATE,
        [Description("Set")]
        [Hand]
        SET,
        [Description("Send to Graveyard")]
        [Hand]
        [Field]
        SEND_GRAVEYARD,
        [Description("Send to Deck")]
        [Hand]
        [Field]
        SEND_DECK
    }

    public enum TrapActions
    {
        [Description("Activate")]
        [Field]
        ACTIVATE,
        [Description("Set")]
        [Hand]
        SET,
        [Description("Send to Graveyard")]
        [Hand]
        [Field]
        SEND_GRAVEYARD,
        [Description("Send to Deck")]
        [Hand]
        [Field]
        SEND_DECK
    }
}
