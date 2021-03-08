namespace WdrFilterConverter.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    class WdrRaidFilter : WdrFilter
    {
        [JsonProperty("Boss_Levels")]
        public List<ushort> BossLevels { get; set; }

        [JsonProperty("Egg_Levels")]
        public List<ushort> EggLevels { get; set; }

        [JsonProperty("Ex_Eligible_Only")]
        public bool ExEligibleOnly { get; set; }
    }
}