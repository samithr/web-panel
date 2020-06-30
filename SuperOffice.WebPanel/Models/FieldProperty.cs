using Newtonsoft.Json;

namespace SuperOffice.WebPanel.Models
{
    public class FieldProperty
    {        /// <summary>The field right</summary>
        [JsonProperty("FieldRight", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public FieldRight FieldRight { get; set; }

        /// <summary>Type of field</summary>
        [JsonProperty("FieldType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string FieldType { get; set; }

        /// <summary>Length of the field</summary>
        [JsonProperty("FieldLength", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public int? FieldLength { get; set; }
    }
}
