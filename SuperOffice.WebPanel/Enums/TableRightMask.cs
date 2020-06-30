using System.Runtime.Serialization;

namespace SuperOffice.WebPanel.Models
{
    public enum TableRightMask
    {
        [EnumMember(Value = @"None")]
        None = 0,

        [EnumMember(Value = @"Select")]
        Select = 1,

        [EnumMember(Value = @"Update")]
        Update = 2,

        [EnumMember(Value = @"UR")]
        UR = 3,

        [EnumMember(Value = @"Insert")]
        Insert = 4,

        [EnumMember(Value = @"RI")]
        RI = 5,

        [EnumMember(Value = @"URI")]
        URI = 6,

        [EnumMember(Value = @"Delete")]
        Delete = 7,

        [EnumMember(Value = @"UDR")]
        UDR = 8,

        [EnumMember(Value = @"FULL")]
        FULL = 9,

        [EnumMember(Value = @"Filtering")]
        Filtering = 10,

        [EnumMember(Value = @"RF")]
        RF = 11,

        [EnumMember(Value = @"FI")]
        FI = 12,

        [EnumMember(Value = @"RestrictedUpdate")]
        RestrictedUpdate = 13,

    }
}
