using System.Runtime.Serialization;

namespace SuperOffice.WebPanel.Enums
{
    public enum FieldRightMask
    {
        [EnumMember(Value = @"None")]
        None = 0,

        [EnumMember(Value = @"Read")]
        Read = 1,

        [EnumMember(Value = @"Write")]
        Write = 2,

        [EnumMember(Value = @"Update")]
        Update = 3,

        [EnumMember(Value = @"Unused1")]
        Unused1 = 4,

        [EnumMember(Value = @"Unused2")]
        Unused2 = 5,

        [EnumMember(Value = @"Unused3")]
        Unused3 = 6,

        [EnumMember(Value = @"Unused4")]
        Unused4 = 7,

        [EnumMember(Value = @"UIHintMandatory")]
        UIHintMandatory = 8,

        [EnumMember(Value = @"UIHintReadOnly")]
        UIHintReadOnly = 9,

        [EnumMember(Value = @"UIHints")]
        UIHints = 10,

        [EnumMember(Value = @"UndefinedValue256")]
        UndefinedValue256 = 11,

    }
}
