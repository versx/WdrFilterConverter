namespace WdrFilterConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    using Models;

    class WdrFilterConverter
    {
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
        };

        public string InDir { get; }

        public string OutDir { get; }

        public IReadOnlyDictionary<ushort, string> Pokemon { get; }

        public WdrFilterConverter(string inDir, string outDir)
        {
            InDir = inDir;
            OutDir = outDir;

            var path = "pokemon.json";
            var data = File.ReadAllText(path);
            Pokemon = JsonConvert.DeserializeObject<Dictionary<ushort, string>>(data);
        }

        #region Converters

        public void ConvertPokemonFilter(string name, WdrPokemonFilter filter)
        {
            var ignoreKeys = new string[]
            {
                "type",
                "post_without_iv",
                "min_iv",
                "max_iv",
                "min_level",
                "max_level",
                "min_cp",
                "max_cp",
                "gender",
                "size",
            };
            var pokemonNames = filter.Keys.Where(x => !ignoreKeys.Contains(x.ToLower())).ToList();
            //pokemonNames = pokemonNames.Select(x => x.Replace("'", null)).ToList();
            pokemonNames = pokemonNames.Where(x => filter[x].ToString() == "True").ToList();
            var pokemonFilter = new
            {
                pokemon = new
                {
                    enabled = true,
                    pokemon = ConvertPokemonNamesToIds(pokemonNames),
                    type = "Include",
                    ignoreMissing = !filter.ContainsKey("Post_Without_IV") || bool.Parse(filter["Post_Without_IV"].ToString()),
                    min_iv = filter["min_iv"],
                    max_iv = filter["max_iv"],
                    min_lvl = filter["min_level"],
                    max_lvl = filter["max_level"],
                    min_cp = filter["min_cp"],
                    max_cp = filter["max_cp"],
                    gender = filter.ContainsKey("gender")
                    ? string.Compare(filter["gender"].ToString(), "All", true) == 0
                        ? "*"
                        : filter["gender"] // TODO: Convert to m/f
                    : "*",
                    /*
                    size = filter.ContainsKey("size")
                    ? string.Compare(filter["size"].ToString(), "All", true) == 0
                        ? "All"
                        : filter["size"] // TODO: Convert to sizes
                    : "All",
                    */
                }
            };
            var json = JsonConvert.SerializeObject(pokemonFilter, _jsonSettings);
            File.WriteAllText(Path.Combine(OutDir, Path.GetFileName(name)), json);
        }

        public void ConvertPvpFilter(string name, WdrPvpPokemonFilter filter)
        {
            var ignoreKeys = new string[]
            {
                "type",
                "post_without_iv",
                "min_iv",
                "max_iv",
                "min_level",
                "max_level",
                "gender",
                "size",
                "min_cp",
                "max_cp",
                "min_cp_range",
                "max_cp_range",
                "min_pvp_rank",
                "min_pvp_percent",
                "league",
            };
            var pokemonNames = filter.Keys.Where(x => !ignoreKeys.Contains(x.ToLower())).ToList();
            //pokemonNames = pokemonNames.Select(x => x.Replace("'", null)).ToList();
            pokemonNames = pokemonNames.Where(x => filter[x].ToString() == "True").ToList();
            var pokemonFilter = new
            {
                pokemon = new
                {
                    enabled = true,
                    pokemon = ConvertPokemonNamesToIds(pokemonNames),
                    type = "Include",
                    ignoreMissing = !filter.ContainsKey("Post_Without_IV") || bool.Parse(filter["Post_Without_IV"].ToString()),
                    min_iv = filter["min_iv"],
                    max_iv = filter["max_iv"],
                    min_lvl = filter["min_level"],
                    max_lvl = filter["max_level"],
                    min_cp = filter["min_cp_range"],
                    max_cp = filter["max_cp_range"],
                    min_rank = filter["min_pvp_rank"],
                    great_league = filter["league"].ToString() == "great",
                    ultra_league = filter["league"].ToString() == "ultra",
                    gender = filter.ContainsKey("gender")
                    ? string.Compare(filter["gender"].ToString(), "All", true) == 0
                        ? "*"
                        : filter["gender"] // TODO: Convert to m/f
                    : "*",
                    /*
                    size = filter.ContainsKey("size")
                    ? string.Compare(filter["size"].ToString(), "All", true) == 0
                        ? "All"
                        : filter["size"] // TODO: Convert to sizes
                    : "All",
                    */
                }
            };
            var json = JsonConvert.SerializeObject(pokemonFilter, _jsonSettings);
            File.WriteAllText(Path.Combine(OutDir, Path.GetFileName(name)), json);
        }

        public void ConvertRaidFilter(string name, WdrRaidFilter filter)
        {
        }

        public void ConvertInvasionFilter(string name, WdrInvasionFilter filter)
        {
            var invasionFilter = new
            {
                pokestops = new
                {
                    enabled = true,
                    lured = false,
                    invasions = true,
                }
            };
            var json = JsonConvert.SerializeObject(invasionFilter, _jsonSettings);
            File.WriteAllText(Path.Combine(OutDir, Path.GetFileName(name)), json);
        }

        public void ConvertLureFilter(string name, WdrLureFilter filter)
        {
            var lureFilter = new
            {
                pokestops = new
                {
                    enabled = true,
                    lured = true,
                    invasions = false,
                    lure_types = filter["Lure_Type"],
                }
            };
            var json = JsonConvert.SerializeObject(lureFilter, _jsonSettings);
            File.WriteAllText(Path.Combine(OutDir, Path.GetFileName(name)), json);
        }

        public void ConvertQuestFilter(string name, WdrQuestFilter filter)
        {
            var questFilter = new
            {
                quests = new
                {
                    enabled = true,
                    rewards = filter["Rewards"],
                    isShiny = false,
                }
            };
            var json = JsonConvert.SerializeObject(questFilter, _jsonSettings);
            File.WriteAllText(Path.Combine(OutDir, Path.GetFileName(name)), json);
        }

        #endregion

        private List<ushort> ConvertPokemonNamesToIds(List<string> pokemonNames)
        {
            var list = new List<ushort>();
            foreach (var pokemonName in pokemonNames)
            {
                var id = Pokemon.FirstOrDefault(x => string.Compare(x.Value, pokemonName, true) == 0).Key;
                if (id == 0)
                {
                    Console.WriteLine($"Failed to lookup ID for pokemon {pokemonName}");
                    continue;
                }
                list.Add(id);
            }
            return list;
        }
    }
}