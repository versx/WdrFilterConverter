namespace WdrFilterConverter.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    class WdrPvpPokemonFilter : WdrPokemonFilter
    {
        [JsonProperty("min_cp")]
        public ushort MinimumCP { get; set; }

        [JsonProperty("max_cp")]
        public ushort MaximumCP { get; set; }

        [JsonProperty("min_cp_range")]
        public ushort MinimumCpRange { get; set; }

        [JsonProperty("max_cp_range")]
        public ushort MaximumCpRange { get; set; }

        [JsonProperty("league")]
        public string League { get; set; }

        // pokemon per key
    }
}