using Newtonsoft.Json;

namespace Tuya.Net.Data
{
    /// <summary>
    /// Instruction info.
    /// </summary>
    public class InstructionInfo
    {
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string? Category { get; set; }

        [JsonProperty("functions", NullValueHandling = NullValueHandling.Ignore)]
        public IList<Instruction>? Instructions { get; set; }
    }
}
