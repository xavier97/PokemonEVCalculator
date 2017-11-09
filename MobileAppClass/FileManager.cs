using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace MobileAppClass
{
    public class FileManager
    {
        private static FileManager _instance = null; // An instance of the FileManager class
        List<PokemonDetail> pkmnDetails;
        private readonly string FILENAME = "myPokemon.txt";

        // singleton
        private FileManager()
        {
            pkmnDetails = null;
        }

        public static FileManager getInstance
        {
            get
            {
                // Create the single used instance if none is made
                if (_instance == null)
                {
                    _instance = new FileManager();
                }
                return _instance;
            }
        }

        // When pkmnDetails (list) is null, read from file and populate memory with list
        // Handles first time call when _gameDetails is null
        // Handles if file does not exist: returns empty list in that case
        // We always read from memory instead of file when possible. We assume the "save" action updates list in memory and disk
        public List<PokemonDetail> pokemonDetails
        {
            get
            {
                if (pkmnDetails == null)
                {
                    // Handle first time case

                    // Read from file 
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    Console.WriteLine(path); // TODO: Test
                    var pathFile = Path.Combine(path, FILENAME);

                    if (File.Exists(pathFile) == false)
                    {
                        // no file exists, make an empty list
                        pkmnDetails = new List<PokemonDetail>();
                    }

                    else
                    {
                        // we have a file, read it
                        using (var streamReader = new StreamReader(pathFile))
                        {
                            var myJsonFromFile = streamReader.ReadToEnd();
                            pkmnDetails = JsonConvert.DeserializeObject<List<PokemonDetail>>(myJsonFromFile).OrderByDescending(x => x.nickname).ToList();
                        }
                    }
                }
                return pkmnDetails.OrderByDescending(x => x.nickname).ToList();

            }
        }

        // (1) Appends new Pokemon to memory, and (2) Saves entire list to to JSON file
        public void SavePokemon(PokemonDetail pokemon)
        {
            // Appends to list in memory
            if (pokemon != null)
            {
                // add mode
                pkmnDetails.Add(pokemon); // Add directly to the list
            }

            // Setting to always include the .Net type name when serializing
            var mySettings = new JsonSerializerSettings();
            mySettings.TypeNameHandling = TypeNameHandling.All;
            var mySerializer = JsonSerializer.Create(mySettings);

            // Convert object to Json
            string jsonData = JsonConvert.SerializeObject(pokemon);

            // Get file path where saving Pokemon
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Console.WriteLine(path); // TODO : Test
            var pathFile = Path.Combine(path, FILENAME);

            // Write json object to Pokemon storage file
            using (var streamwriter = new StreamWriter(pathFile, false))
            {
                streamwriter.Write(jsonData);
            }
        }


        /*void GenerateSimulatedDataFile()
        {
            myPeeps = new List<Person>();
            myPeeps.Add(new Person("aa", "AA", "34"));
            myPeeps.Add(new Person("bb", "BB", "456456"));
            myPeeps.Add(new Person("cc", "CC", "3t5g5g"));
            myPeeps.Add(new Person("dd", "DD", "4545"));
            myPeeps.Add(new Person("ee", "EE", "45455"));

            var mySettings = new JsonSerializerSettings();
            mySettings.TypeNameHandling = TypeNameHandling.All;

            var mySerializer = JsonSerializer.Create(mySettings);
        }*/

    }
}