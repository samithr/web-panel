using System.Runtime.Serialization;

namespace SuperOffice.WebPanel.Enums
{
    public enum WebPanelEntityUrlEncoding
    {
        [EnumMember(Value = @"Unknown")]
        Unknown = 0,

        [EnumMember(Value = @"None")]
        None = 1,

        [EnumMember(Value = @"ANSI")]
        ANSI = 2,

        [EnumMember(Value = @"Unicode")]
        Unicode = 3,

    }
}
