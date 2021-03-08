namespace WdrFilterConverter.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    class WdrLureFilter : WdrFilter
    {
        public override string Type => "lure";

        [JsonProperty("Lure_Type")]
        public List<string> LureType { get; set; }
    }
}