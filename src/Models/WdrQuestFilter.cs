namespace WdrFilterConverter.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    class WdrQuestFilter : WdrFilter
    {
        public override string Type => "quest";

        [JsonProperty("Rewards")]
        public List<string> Rewards { get; set; }
    }
}