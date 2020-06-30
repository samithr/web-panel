using Newtonsoft.Json;

namespace SuperOffice.WebPanel.Models
{
    public class TableRight
    {
        /// <summary>Returns the bitflag of permissions.</summary>
        [JsonProperty("Mask", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public TableRightMask? Mask { get; set; }

        /// <summary>Contains a string that explains why the right is not available. The reason is blank if HasAll is true.</summary>
        [JsonProperty("Reason", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }
    }
}
