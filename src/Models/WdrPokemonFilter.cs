namespace WdrFilterConverter.Models
{
    using Newtonsoft.Json;

    class WdrPokemonFilter : WdrFilter
    {
        public override string Type => "pokemon";

        [JsonProperty("Post_Without_IV")]
        public bool PostWithoutIV { get; set; }

        [JsonProperty("min_iv")]
        public ushort MinimumIV { get; set; }

        [JsonProperty("max_iv")]
        public ushort MaximumIV { get; set; }

        [JsonProperty("min_level")]
        public ushort MinimumLevel { get; set; }

        [JsonProperty("max_level")]
        public ushort MaximumLevel { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        // pokemon per key
    }
}