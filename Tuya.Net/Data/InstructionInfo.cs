using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Instruction info.
    /// </summary>
    public class InstructionInfo
    {
        /// <summary>
        /// Gets or sets the device category.
        /// </summary>
        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the device instruction list.
        /// </summary>
        [JsonProperty("functions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Instruction>? Instructions { get; set; }
    }
}
