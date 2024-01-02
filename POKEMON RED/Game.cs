using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace POKEMON_RED
{
    internal class Game
    {
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
        public Game()
        {
            this.userInputErrorMessageDelay = 3000;
            this.textTime = 1000;
            this.hasBeenToKanto_Route1 = false;
            this.hasBeenToProfessor_Oaks_Laboratory = false;
            this.playerLocation = Location.Players_house;
            this.availableLocations = new();
            this.availableLocations.Add(Location.Players_house);
            this.playerPokemons = new();
            this.playerPokemons.Add(new Pokémon(PokémonNames.Charmander, 5, Genders.Male));
            this.playerPokemons.Add(new Pokémon(PokémonNames.Charmeleon, 23, Genders.Male));
            this.playerPokemons.Add(new Pokémon(PokémonNames.Mewtwo, 87, Genders.Male));

            Battle battle = new Battle(playerPokemons[0], playerPokemons[1]) ;

            Console.WriteLine(battle.CalculateDamage(playerPokemons[0], playerPokemons[1], Moves.Absorb));

            Thread.Sleep(50000);

            LoadTexts("introduction");
            Text("introduction", "a");
            playerName = GetUserInput(15);
            Text($"Right! So your name is {playerName}!");
            Text("introduction", "b");
            rivalName = GetUserInput(15);
            Text($"{playerName}! Your very own Pokémon legend is about to unfold!");
            Text("A world of dreams and adventures with Pokémon awaits! Let's go!");
            Thread.Sleep(2000);
            Console.Clear();
            textDisplayed.Clear();

            Interaction();


            string userInput = string.Empty;
            while (userInput.ToLower() != "a")
            {
                Console.WriteLine($"What do you want to do {playerName}?                                                     Current Location: {playerLocation}");
                Console.WriteLine("A. Go down the stairs");
                Console.WriteLine("B. Watch TV");
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "b")
                {
                    Text("There's a movie on TV. Four boys are walking on railrod tracks.");
                    Text("I better go too.");
                    Thread.Sleep(3000);
                    Console.Clear();
                }
                else if (userInput.ToLower() != "a")
                {
                    UserInputErrorMessage(userInput);
                    Console.Clear();
                }
            }
            Text("Mother: Right. All boys leave home some day. It said so on TV");
            Text("Professor Oak, next door, is looking for you.");
            Console.Clear();
            Interaction();
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
            using (StreamReader reader = new StreamReader($"{path}{file}.txt"))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    string[] parts = line.Split(':');

                    string[] texts = parts[1].Split("/");
                    string key = parts[0];

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

        public void Interaction()
        {
            Console.WriteLine($"What do you want to do {playerName}?                                                     Current Location: {playerLocation}");
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
                    break;
                case "d":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Quitting the game...");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                    break;
                default:
                    ErrorMessage($"\'{userInput}\' is not a valid option", 3, true);
                    Interaction();
                    break;
            }
        }

        public void Text(string place, string key)
        {
            try
            {
                foreach (string text in Texts[$"{key}"])
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
                Thread.Sleep(30);
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
                playerLocation = availableLocations[userInput];
                NewLocation();
            }
            catch (FormatException)
            {
                ErrorMessage("Type one of the numbers", 3, true);
                MoveAround();
            }
        }

        public void NewLocation()
        {
            switch (playerLocation)
            {
                case Location.Players_house:
                    break;
                case Location.Kanto_Route_1:
                    if (hasBeenToKanto_Route1)
                        Interaction();
                    else
                        hasBeenToKanto_Route1 = true;
                    break;
                case Location.Professor_Oaks_Laboratory:
                    if (hasBeenToProfessor_Oaks_Laboratory)
                        Interaction();
                    else
                        hasBeenToProfessor_Oaks_Laboratory = true;
                    break;
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
