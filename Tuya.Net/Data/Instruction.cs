using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Tuya Device Instruction DTO.
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the instruction description.
        /// </summary>
        [JsonProperty("desc", NullValueHandling = NullValueHandling.Ignore)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the instruction name.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the instruction type.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public InstructionType Type { get; set; }

        /// <summary>
        /// Gets or sets the instruction values.
        /// </summary>
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public dynamic? Values { get; set; }
    }

    /// <summary>
    /// Tuya Device Instruction Type Enum.
    /// </summary>
    public enum InstructionType
    {
        Boolean,
        Enum,
        Integer,
        String,
        Json,
        Raw,
        Unknown
    }
}
