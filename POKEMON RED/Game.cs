using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace POKEMON_RED
{
    public class Game
    {
        public int dialogueStage { get; set; }
        public bool hasStarterPokemon { get; set; }
        public int userInputErrorMessageDelay { get; set; }
        public int textTime {  get; set; }
        public bool hasBeenToKanto_Route1 { get; set; }
        public bool hasBeenToProfessor_Oaks_Laboratory { get; set; }

        public Dictionary<string, List<string>> Texts = new();

        public Location playerLocation { get; set; }
        List<string> textDisplayed = new();
        public List<Location> availableLocations { get; set; }
        public List<Pokémon> playerPokemons { get; set; }
        public string playerName { get; set; }
        public string rivalName { get; set; }
        public List<Pokémon> rivalPokemons { get; set; }
        public Game()
        {

            this.dialogueStage = 1;
            this.hasStarterPokemon = false;
            this.userInputErrorMessageDelay = 3000;
            this.textTime = 1000;
            this.hasBeenToKanto_Route1 = false;
            this.hasBeenToProfessor_Oaks_Laboratory = false;
            this.playerLocation = Location.Players_house;
            this.availableLocations = new();
            this.availableLocations.Add(Location.Players_house);
            this.playerPokemons = new();
            this.rivalPokemons = new();

            SaveGame();

            Dialogue();
        }

        public string GetUserInput(int maxLength)
        {
            string userInput = Console.ReadLine();
            while (userInput.Length > maxLength || userInput.Length < 1)
            {
                ErrorMessage("Choose a shorter name", 0, false);
                userInput = Console.ReadLine();
            }
            return userInput;
        }

        public void LoadTexts(string file)
        {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\textfiles\\";
                using (StreamReader reader = new($"{path}{file}.txt"))
                {
                    string line = reader.ReadLine();

                    while (line != null)
                    {
                        string[] parts = line.Split(';');

                        string[] texts = parts[1].Split("/");
                        string key = $"{file} {parts[0]}";

                        List<string> messages = new();

                        foreach (string text in texts)
                        {
                            messages.Add(text);
                        }

                        Texts.Add(key, messages);

                        line = reader.ReadLine();
                    }
                }
        }

        public void Dialogue()
        {
            switch(dialogueStage)
            {
                case 1:
                    LoadTexts("introduction");
                    Text("introduction", "a");
                    playerName = GetUserInput(15);
                    Text($"Right! So your name is {playerName}!");
                    Text("introduction", "b");
                    rivalName = GetUserInput(15);
                    Text($"{playerName}! Your very own Pokémon legend is about to unfold!");
                    Text("A world of dreams and adventures with Pokémon awaits! Let's go!");
                    break;
                case 2:
                    LoadTexts("playersHouse");
                    Text("playersHouse", "a");
                    availableLocations.Add(Location.Kanto_Route_1);
                    availableLocations.Remove(Location.Players_house);
                    break;

                case 3:
                    LoadTexts("route1");
                    Text("route1", "a");
                    Thread.Sleep(2000);
                    Console.Clear();
                    Text("route1", "b");
                    Thread.Sleep(2000);
                    Console.Clear();
                    playerLocation = Location.Professor_Oaks_Laboratory;
                    dialogueStage++;
                    Dialogue();
                    break;
                case 4:
                    if (hasStarterPokemon == false)
                    {
                        Text($"{rivalName}: Gramps! I'm fed up with waiting!");
                        Text($"Oak: {rivalName}? Let me think... Oh, that's right, I told you to come!");
                        Text("Just wait!");
                        Text($"Here, {playerName}! There are 3 POKEMON here! Haha! They are inside the");
                        Text("POKE BALLS. When I was young, I was a serious POKEMON trainer.");
                        Text("In my old age, I have only 3 left, but you can have one! Choose!");
                        Text($"{rivalName}: Hey! Gramps! What about me?");
                        Text($"Oak: Be patient! {rivalName}, you can have one too!");
                        Text($"Now, {playerName}, which POKEMON do you want?");
                        Text("1. Bulbasaur      2. Charmander       3. Squirtle");
                        string userInput = GetUserInput(1);
                        while (!hasStarterPokemon)
                        {
                            switch (userInput)
                            {
                                case "1":
                                    playerPokemons.Add(new Pokémon(PokémonNames.Bulbasaur, 5, Genders.Male));
                                    rivalPokemons.Add(new Pokémon(PokémonNames.Charmander, 5, Genders.Male));
                                    hasStarterPokemon = true;
                                    break;
                                case "2":
                                    playerPokemons.Add(new Pokémon(PokémonNames.Charmander, 5, Genders.Male));
                                    rivalPokemons.Add(new Pokémon(PokémonNames.Squirtle, 5, Genders.Male));
                                    hasStarterPokemon = true;
                                    break;
                                case "3":
                                    playerPokemons.Add(new Pokémon(PokémonNames.Squirtle, 5, Genders.Male));
                                    rivalPokemons.Add(new Pokémon(PokémonNames.Bulbasaur, 5, Genders.Male));
                                    hasStarterPokemon = true;
                                    break;
                                default:
                                    UserInputErrorMessage(userInput);
                                    Console.Clear();
                                    Text("1. Bulbasaur      2. Charmander       3. Squirtle");
                                    userInput = GetUserInput(1);
                                    break;
                            }
                        }
                        Rival rival = new(rivalPokemons, rivalName);
                        LoadTexts("oakResearchLab");
                        Text("oakResearchLab", "a");
                        Thread.Sleep(2000);
                        Console.Clear();
                        Text("oakResearchLab", "b");
                        Battle btl = new Battle(this.playerPokemons, this.rivalPokemons, playerName, rivalName);
                        Text("oakResearchLab", "c");
                    }
                    break;
            }
            Thread.Sleep(3000);
            Console.Clear();
            textDisplayed.Clear();
            dialogueStage++;
            Interaction();
        }

        public void Interaction()
        {
            Console.WriteLine($"What do you want to do {playerName}?                                          Current Location: {playerLocation}");
            Console.WriteLine("A. Move Around");
            Console.WriteLine("B. Look at your Pokémon");
            Console.WriteLine("C. Save the game");
            Console.WriteLine("D. Quit the game");
            var userInput = Console.ReadLine(); ;
            switch (userInput.ToLower())
            {
                case "a":
                    Console.Clear();
                    MoveAround();
                    break;
                case "b":
                    Console.Clear();
                    LookAtPokémon();
                    break;
                case "c":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Text("Saving the game...");
                    Thread.Sleep(3000);
                    SaveGame();
                    Console.WriteLine("Game saved.");
                    Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    Interaction();
                    break;
                case "d":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Closing the game...");
                    Thread.Sleep(3000);
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                    break;
                default:
                    ErrorMessage($"\'{userInput}\' is not a valid option", 3, true);
                    Interaction();
                    break;
            }
        }

        public void SaveGame()
        {
            //Gets path to the games folder and goes into saveState.txt
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\SaveStates\\saveState.txt";

            StreamWriter writer = new StreamWriter(path);

            //Serializes this object, so you can load the game again
            string str = JsonSerializer.Serialize(this);

            //Writes the serialized object in the text file
            writer.WriteLine(str);

            writer.Close();
        }

        public void Text(string place, string key)
        {
            try
            {
                foreach (string text in Texts[$"{place} {key}"])
                {
                    Text(text);
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("error");
            }
        }

        public void Text(string text)
        {
            //Console.WriteLine(text);
            //return;
            // Bool here makes sure it doesn't write the entire text twice.
            bool displayPreviousText = false;
            string message = null;
            foreach (char c in text)
            {
                if (displayPreviousText)
                {
                    foreach (string previousText in textDisplayed)
                    {
                        Console.WriteLine(previousText);
                    }
                }
                displayPreviousText = true;
                message += c;
                Console.WriteLine(message);
                Thread.Sleep(40);
                Console.Clear();
            }
            textDisplayed.Add(text);
            foreach (string previousText in textDisplayed)
            {
                Console.WriteLine(previousText);
            }
            Thread.Sleep(textTime);
        }

        public void UserInputErrorMessage(string userInput)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\'{userInput}\' is not a valid option.");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(userInputErrorMessageDelay * 1000);
            Console.Clear();
        }

        public void ErrorMessage(string text, int delay, bool Clear)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(delay*1000);
            if (Clear) Console.Clear();
        }

        public void MoveAround()
        {
            Console.WriteLine("Where do you want to move to?");
            for (int i = 0; i < availableLocations.Count; i++)
            {
                Console.WriteLine($"{i+1}. {availableLocations[i]}");
            }

            try
            {
                int userInput = Convert.ToInt32(Console.ReadLine());
                playerLocation = availableLocations[userInput-1];
                Console.Clear();
                Dialogue();
            }
            catch (FormatException)
            {
                ErrorMessage("Type one of the numbers", 3, true);
                MoveAround();
            }
        }

        public void LookAtPokémon()
        {
            Console.WriteLine("Your current pokémon are:");
            try
            {
                foreach (Pokémon pokemon in playerPokemons)
                {
                    Console.WriteLine($"Name: {pokemon.name} {Environment.NewLine}Gender: {pokemon.gender} {Environment.NewLine}" +
                    $"LVL: {pokemon.level}");
                }
                Console.WriteLine($"{Environment.NewLine}What do you want to do now?");
                Console.WriteLine("A. Inspect pokémon");
                Console.WriteLine("B. Go back");
                var userInput = Console.ReadLine();
                switch (userInput.ToLower())
                {
                    case "a":
                        break;
                    case "b":
                        Interaction();
                        break;
                    default:
                        UserInputErrorMessage(userInput);
                        LookAtPokémon();
                        break;
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("None");
                Console.ReadLine();
            }
        }
    }
}
