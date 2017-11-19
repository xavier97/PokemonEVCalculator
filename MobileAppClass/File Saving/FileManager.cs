using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace PKMNEVCalc
{
    public class FileManager
    {
        private static FileManager _instance = null; // An instance of the FileManager class
        private List<PokemonDetail> pkmnList;
        private readonly string FILENAME = "myPokemon.txt";

        // singleton
        private FileManager()
        {
            pkmnList = null;
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

        // When pkmnList (list) is null, read from json file and populate memory with list
        // Handles first time call when pkmnList is null
        // Handles if file does not exist: returns empty list in that case
        // We always read from memory instead of file when possible. We assume the "save" action updates list in memory and disk
        // This is how we'll populate the table from storage
        public List<PokemonDetail> pokemonDetailsStorage
        {
            get
            {
                // Handle first time case
                if (pkmnList == null)
                {
                    // Read from file 
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    Console.WriteLine(path); // TODO: Test
                    var pathFile = Path.Combine(path, FILENAME);

                    if (File.Exists(pathFile) == false)
                    {
                        // No file exists, make an empty list
                        pkmnList = new List<PokemonDetail>();
                    }
                    else
                    {
                        // We have a file, read it
                        using (var streamReader = new StreamReader(pathFile))
                        {
                            var myJsonFromFile = streamReader.ReadToEnd();
                            Console.WriteLine(myJsonFromFile);
                            pkmnList = JsonConvert.DeserializeObject<List<PokemonDetail>>(myJsonFromFile).OrderByDescending(x => x.nickname).ToList();
                        }
                    }
                }
                return pkmnList.OrderByDescending(x => x.nickname).ToList();

            }
        }

        // (1) Appends new Pokemon to memory, and (2) Saves entire list to to JSON file
        public void SavePokemon(PokemonDetail pokemon)
        {
            if (pokemon != null)
            {
                // add mode
                pkmnList.Add(pokemon); // add directly to the list.  Dont use the getter because it returns a COPY sorted.
            }
            String jsonData = JsonConvert.SerializeObject(pokemonDetailsStorage);
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Console.WriteLine(path);
            var pathFile = Path.Combine(path, FILENAME);
            using (var streamwriter = new StreamWriter(pathFile, false))
            {
                streamwriter.Write(jsonData);
            }
        }

        public void DeletePokemon(int pkmnToDelete)
        {
            // Remove from memory
            pkmnList.RemoveAt(pkmnToDelete);

            // Write updated list without pkmnToDelete
            String jsonData = JsonConvert.SerializeObject(pokemonDetailsStorage);
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Console.WriteLine(path);
            var pathFile = Path.Combine(path, FILENAME);
            using (var streamwriter = new StreamWriter(pathFile, false))
            {
                streamwriter.Write(jsonData);
            }
        }
    }
}