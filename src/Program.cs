namespace WdrFilterConverter
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    using Models;

    class Program
    {
        static void Main(string[] args)
        {
            var inDir = "wdr_filters";
            if (!Directory.Exists(inDir))
            {
                Console.WriteLine($"Error, inDir: {inDir} does not exist");
                return;
            }
            var outDir = "filters";
            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            var converter = new WdrFilterConverter(inDir, outDir);

            var files = Directory.GetFiles(inDir, "*.json");
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var obj = JsonConvert.DeserializeObject<dynamic>(json);
                switch (Convert.ToString(obj.Type).ToLower())
                {
                    case "pokemon":
                        var pokemon = Deserialize<WdrPokemonFilter>(json);
                        converter.ConvertPokemonFilter(file, pokemon);
                        break;
                    case "pvp":
                        var pvp = Deserialize<WdrPvpPokemonFilter>(json);
                        converter.ConvertPvpFilter(file, pvp);
                        break;
                    case "raid":
                        var raid = Deserialize<WdrRaidFilter>(json);
                        converter.ConvertRaidFilter(file, raid);
                        break;
                    case "invasion":
                        var invasion = Deserialize<WdrInvasionFilter>(json);
                        converter.ConvertInvasionFilter(file, invasion);
                        break;
                    case "lure":
                        var lure = Deserialize<WdrLureFilter>(json);
                        converter.ConvertLureFilter(file, lure);
                        break;
                    case "quest":
                        var quest = Deserialize<WdrQuestFilter>(json);
                        converter.ConvertQuestFilter(file, quest);
                        break;
                    default:
                        Console.WriteLine($"Unrecognized filter type: {obj.Type}: {obj}");
                        break;
                }
            }
        }

        static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}