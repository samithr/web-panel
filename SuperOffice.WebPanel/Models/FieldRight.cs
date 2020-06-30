using Newtonsoft.Json;
using SuperOffice.WebPanel.Enums;

namespace SuperOffice.WebPanel.Models
{
    public class FieldRight
    {        /// <summary>Returns the bitflag of permissions.</summary>
        [JsonProperty("Mask", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public FieldRightMask? Mask { get; set; }

        /// <summary>Contains a string that explains why the right is not available. The reason is blank if HasAll is true.</summary>
        [JsonProperty("Reason", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }
    }
}
