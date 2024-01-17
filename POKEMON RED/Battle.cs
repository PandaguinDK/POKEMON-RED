using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static POKEMON_RED.Battle;

namespace POKEMON_RED
{
    public class Battle
    {
        readonly int textTime = 1000;
        public List<string> textDisplayed = new();

        public MoveInfoHelper moveInfoHelper = new();

        public Pokémon trainerActivePokémon {  get; set; }
        public Pokémon opponentActivePokémon { get; set; }
        public int opponentActivePokémonNum { get; set; }

        public int faintedTrainerCount { get;set; }

        public int faintedEnemyCount { get; set; }

        public List<Pokémon> opponentPokémon { get; set; }
        public List<Pokémon> trainerPokémon { get; set; }

        public List<Types> opponentPokémonType { get; set; }
        public List<Types> trainerPokémonType { get; set; }

        public string trainerName { get; set; }
        public string opponentName { get; set; }
        

        public Battle(List<Pokémon> trainerPokémon, List<Pokémon> opponentPokémon, string trainerName, string opponentName)
        {
            this.opponentPokémon = opponentPokémon;
            this.trainerPokémon = trainerPokémon;
            this.opponentActivePokémon = opponentPokémon[1];
            this.trainerActivePokémon = trainerPokémon[0];
            this.trainerName = trainerName;
            this.opponentName = opponentName;
            this.opponentActivePokémonNum = 0;

            Text($"A battle has started between {trainerName} and {opponentName}!");
            Thread.Sleep(2000);
            BattleInterface();
            
            
        }


        public void BattleInterface()
        {
            if (trainerActivePokémon.pokémonHP <= 0) SwitchMenu();
            if (opponentActivePokémon.pokémonHP <= 0)
            {
                opponentActivePokémonNum++;
                try
                {
                    opponentActivePokémon = opponentPokémon[opponentActivePokémonNum];
                }
                //Catch here in case opponent trainer has no more Pokémon
                catch (IndexOutOfRangeException)
                {

                }
            }

            this.faintedTrainerCount = GetFaintedAmount(trainerPokémon);
            this.faintedEnemyCount = GetFaintedAmount(opponentPokémon);

            if (faintedTrainerCount == trainerPokémon.Count)
            {
                Text("All your pokemon fainted, and you were taken to the closest Pokémon Center.");
                Thread.Sleep(5000);
                return;
            }

            if (faintedEnemyCount == opponentPokémon.Count)
            {
                Text("You won the battle!");
                Thread.Sleep(5000);
                return;
            }
            Text($"What do you want to do {trainerName}?");
            Console.WriteLine("1. Attack, 2. Switch, 3. Bag, 4. Run");

            Text($"{trainerActivePokémon.name}: {trainerActivePokémon.pokémonHP}                   {opponentActivePokémon.name}: {opponentActivePokémon.pokémonHP}");
            switch (GetUserInput(4))
            {
                case 1:
                    AttackMenu(); 
                    break;
                case 2:
                    SwitchMenu();
                    break;
                case 3:
                    BagMenu();
                    break;
                case 4:
                    Run();
                    break;
            }
        }

        public void AttackMenu()
        {
            Text($"Which attack do you want to use {trainerName}?");
            Thread.Sleep(1000);
            Console.WriteLine($"1. {trainerActivePokémon.moves[0]}, 2. {trainerActivePokémon.moves[1]}, 3. {trainerActivePokémon.moves[2]}, 4. {trainerActivePokémon.moves[3]}");
            switch (GetUserInput(4))
            {
                case 1:
                    if (trainerActivePokémon.moves[0] == Moves.NONE)
                    {
                        ErrorMessage("You have no move here", 2, true);
                        AttackMenu();
                    }
                    CalculateDamage(trainerActivePokémon, opponentActivePokémon, trainerActivePokémon.moves[0]);
                    break;
                case 2:
                    if (trainerActivePokémon.moves[1] == Moves.NONE)
                    {
                        ErrorMessage("You have no move here", 2, true);
                        AttackMenu();
                    }
                    CalculateDamage(trainerActivePokémon, opponentActivePokémon, trainerActivePokémon.moves[1]);
                    break;
                case 3:
                    if (trainerActivePokémon.moves[2] == Moves.NONE)
                    {
                        ErrorMessage("You have no move here", 2, true);
                        AttackMenu();
                    }
                    CalculateDamage(trainerActivePokémon, opponentActivePokémon, trainerActivePokémon.moves[2]);
                    break;
                case 4:
                    if (trainerActivePokémon.moves[3] == Moves.NONE)
                    {
                        ErrorMessage("You have no move here", 2, true);
                        AttackMenu();
                    }
                    CalculateDamage(trainerActivePokémon, opponentActivePokémon, trainerActivePokémon.moves[3]);
                    break;
            }
            EnemyMove();
            Thread.Sleep(5000);
            Console.Clear();
            textDisplayed.Clear();
            BattleInterface();
        }

        public void SwitchMenu()
        {
            int i = 1;
            List<int> possibleIndex = new();
            Text("Which Pokémon do you want to switch to?");
            for (int n = 0; n <= trainerPokémon.Count-1; n++)
            {
                if (trainerPokémon[n].pokémonHP <= 0)
                {
                    possibleIndex.Add(n);
                    Console.WriteLine($"{i}. {trainerPokémon[n].name}");
                    i++;
                }
                if (n == trainerPokémon.Count -1)
                {
                    Console.WriteLine($"{i+1}. Go back");
                    possibleIndex.Add(n);
                }
            }
            int userInput = GetUserInput(i) - 1;
            if (userInput == possibleIndex.Count - 1) AttackMenu();
            trainerActivePokémon = trainerPokémon[possibleIndex[userInput]];

        }

        public void BagMenu()
        {

        }

        public void Run()
        {

        }

        public int GetFaintedAmount(List<Pokémon> pokemons)
        {
            int faintedAmount = 0;
            foreach (Pokémon pokemon in pokemons)
            {
                if (pokemon.pokémonHP <= 0) faintedAmount++;
            }
            return faintedAmount;
        }

        public void EnemyMove()
        {
            int opponentTrainerPokemonHP = opponentActivePokémon.pokémonHP;
            foreach (Moves move in opponentActivePokémon.moves)
            {
                if (GetSuperEffectiveness(opponentActivePokémon.type[0], moveInfoHelper.GetMoveInfoAttribute(move).Typing) == Effectiveness.SUPER_EFFECTIVE && GetSuperEffectiveness(opponentActivePokémon.type[1], moveInfoHelper.GetMoveInfoAttribute(move).Typing) == Effectiveness.SUPER_EFFECTIVE)
                {
                    CalculateDamage(opponentActivePokémon, trainerActivePokémon, move);
                    Console.WriteLine($"{opponentActivePokémon.name} used {move} and dealt {opponentTrainerPokemonHP - trainerActivePokémon.pokémonHP} damage!");
                    return;
                }
                else if (GetSuperEffectiveness(opponentActivePokémon.type[1], moveInfoHelper.GetMoveInfoAttribute(move).Typing) == Effectiveness.SUPER_EFFECTIVE || GetSuperEffectiveness(opponentActivePokémon.type[1], moveInfoHelper.GetMoveInfoAttribute(move).Typing) == Effectiveness.SUPER_EFFECTIVE)
                {
                    CalculateDamage(opponentActivePokémon, trainerActivePokémon, move);
                    Console.WriteLine($"{opponentActivePokémon.name} used {move} and dealt {opponentTrainerPokemonHP - trainerActivePokémon.pokémonHP} damage!");
                    return;
                }
            }
            CalculateDamage(opponentActivePokémon, trainerActivePokémon, opponentActivePokémon.moves[0]);
            Console.WriteLine($"{opponentActivePokémon.name} used {opponentActivePokémon.moves[0]} and dealt {opponentTrainerPokemonHP - trainerActivePokémon.pokémonHP} damage!");
            return;
        }

        public void CalculateDamage(Pokémon hittingPokemon, Pokémon defendingPokemon, Moves move)
        {
            MoveInfoAttribute moveInfo = moveInfoHelper.GetMoveInfoAttribute(move);
            Random rnd = new Random(DateTime.Now.Microsecond);

            int movePower = moveInfo.Power;
            Types moveTyping = moveInfo.Typing;
            Categories moveCategory = moveInfo.Category;
            int moveAccuracy = moveInfo.Accuracy;
            int movePP = moveInfo.PP;
            double random = Convert.ToDouble(rnd.Next(217, 255)) / Convert.ToDouble(255);

            double AttackDefenseRatio = GetAttackDefenseRatio(hittingPokemon, defendingPokemon, moveCategory);

            double Type1 = GetEffectivenessMultiplier(GetSuperEffectiveness(defendingPokemon.type[0], moveTyping));
            double Type2 = GetEffectivenessMultiplier(GetSuperEffectiveness(defendingPokemon.type[1], moveTyping));

            double STAB = GetSTAB(hittingPokemon, moveTyping);

            int critical = 1;
            double damage = (((((2 * hittingPokemon.level * critical) / 5 + 2) * movePower * AttackDefenseRatio) / 50 + 2) * STAB * Type1 * Type2 * random);
            if (rnd.Next(0, 100) >= moveAccuracy)
            {
                Console.WriteLine($"{hittingPokemon.name} missed!");
                return;
            }
            if (movePP > 0) defendingPokemon.pokémonHP -= (int)damage;
            else if (move == Moves.Struggle) defendingPokemon.pokémonHP -= (int)damage;
            else if (movePP < 1) CalculateDamage(hittingPokemon, defendingPokemon, Moves.Struggle);
            movePP -= 1;
        }

        public double GetEffectivenessMultiplier(Effectiveness effectiveness)
        {
            if (effectiveness == Effectiveness.SUPER_EFFECTIVE)
            {
                return 1.5;
            }
            else if (effectiveness == Effectiveness.NEUTRAL)
            {
                return 1;
            }
            else if (effectiveness == Effectiveness.RESISTS)
            {
                return 0.5;
            }
            else if (effectiveness == Effectiveness.IMMUNE)
            {
                return 0;
            }
            return -1;
        }

        public double GetSTAB(Pokémon pokemon, Types moveType)
        {
            foreach (Types type in pokemon.type)
            {
                if (type == moveType)
                    return 1.5;
            }
            return 1;
        }
        public double GetAttackDefenseRatio(Pokémon hittingPokemon, Pokémon defendingPokemon, Categories moveCategory)
        {
            double hittingPokemonAtk = hittingPokemon.stats[1];
            double hittingPokemonSpa = hittingPokemon.stats[3];

            double defendingPokemonDef = defendingPokemon.stats[2];
            double defendingPokemonSpd = defendingPokemon.stats[4];

            if (moveCategory == Categories.SPECIAL)
            {
                return hittingPokemonSpa / defendingPokemonSpd;
            }
            else if (moveCategory == Categories.PHYSICAL)
            {
                return hittingPokemonAtk / defendingPokemonDef;
            }
            return 0;
        }

        public enum Effectiveness
        {
            SUPER_EFFECTIVE,
            RESISTS,
            IMMUNE,
            NEUTRAL,
            UNASSIGNED

        }

        public Effectiveness GetSuperEffectiveness(Types defendingPokemonType, Types hittingMoveType)
        {
            Effectiveness effectiveness = Effectiveness.UNASSIGNED;
            switch (hittingMoveType)
            {
                case Types.NORMAL:
                    if (defendingPokemonType == Types.GHOST)
                    {
                        effectiveness = Effectiveness.IMMUNE;
                    }
                    else if (defendingPokemonType == Types.ROCK)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.FIRE:
                    if (defendingPokemonType == Types.FIRE ||  defendingPokemonType == Types.WATER || defendingPokemonType == Types.ROCK || defendingPokemonType == Types.DRAGON)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.GRASS || defendingPokemonType == Types.ICE || defendingPokemonType == Types.BUG)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.WATER:
                    if (defendingPokemonType == Types.WATER || defendingPokemonType == Types.GRASS || defendingPokemonType == Types.DRAGON)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.FIRE || defendingPokemonType == Types.GROUND || defendingPokemonType == Types.ROCK)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.ELECTRIC:
                    if (defendingPokemonType == Types.GROUND)
                    {
                        effectiveness = Effectiveness.IMMUNE;
                    }
                    else if (defendingPokemonType == Types.ELECTRIC || defendingPokemonType == Types.GRASS || defendingPokemonType == Types.DRAGON)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.WATER || defendingPokemonType == Types.FLYING)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.GRASS:
                    if (defendingPokemonType == Types.FIRE || defendingPokemonType == Types.GRASS || defendingPokemonType == Types.POISON || defendingPokemonType == Types.FLYING || defendingPokemonType == Types.BUG || defendingPokemonType == Types.DRAGON)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.WATER || defendingPokemonType == Types.GROUND || defendingPokemonType == Types.ROCK)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.ICE:
                    if (defendingPokemonType == Types.WATER || defendingPokemonType == Types.ICE)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.GRASS || defendingPokemonType == Types.GROUND || defendingPokemonType == Types.DRAGON)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.FIGHTING:
                    if (defendingPokemonType == Types.GHOST)
                    {
                        effectiveness = Effectiveness.IMMUNE;
                    }
                    else if (defendingPokemonType == Types.POISON || defendingPokemonType == Types.FLYING || defendingPokemonType == Types.PSYCHIC || defendingPokemonType == Types.BUG)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.NORMAL || defendingPokemonType == Types.ICE || defendingPokemonType == Types.ROCK)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.POISON:
                    if (defendingPokemonType == Types.POISON || defendingPokemonType == Types.GROUND || defendingPokemonType == Types.ROCK || defendingPokemonType == Types.GHOST)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.GRASS || defendingPokemonType == Types.BUG)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.GROUND:
                    if (defendingPokemonType == Types.FLYING)
                    {
                        effectiveness = Effectiveness.IMMUNE;
                    }
                    else if (defendingPokemonType == Types.GRASS || defendingPokemonType == Types.BUG)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.FIRE || defendingPokemonType == Types.ELECTRIC || defendingPokemonType == Types.POISON || defendingPokemonType == Types.ROCK)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.FLYING:
                    if (defendingPokemonType == Types.ELECTRIC || defendingPokemonType == Types.ROCK)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.GRASS || defendingPokemonType == Types.FIGHTING || defendingPokemonType == Types.BUG)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.PSYCHIC:
                    if (defendingPokemonType == Types.PSYCHIC)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.FIGHTING || defendingPokemonType == Types.POISON)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.BUG:
                    if (defendingPokemonType == Types.FIRE || defendingPokemonType == Types.FIGHTING || defendingPokemonType == Types.FLYING || defendingPokemonType == Types.GHOST)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.GRASS || defendingPokemonType == Types.POISON || defendingPokemonType == Types.PSYCHIC)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.ROCK:
                    if (defendingPokemonType == Types.FIGHTING || defendingPokemonType == Types.GROUND)
                    {
                        effectiveness = Effectiveness.RESISTS;
                    }
                    else if (defendingPokemonType == Types.FIRE || defendingPokemonType == Types.ICE || defendingPokemonType == Types.FLYING || defendingPokemonType == Types.BUG)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    break;
                case Types.GHOST:
                    if (defendingPokemonType == Types.GHOST)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
                case Types.DRAGON:
                    if (defendingPokemonType == Types.DRAGON)
                    {
                        effectiveness = Effectiveness.SUPER_EFFECTIVE;
                    }
                    else
                    {
                        effectiveness = Effectiveness.NEUTRAL;
                    }
                    break;
            }
            return effectiveness;
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
                Thread.Sleep(1);
                Console.Clear();
            }
            textDisplayed.Add(text);
            foreach (string previousText in textDisplayed)
            {
                Console.WriteLine(previousText);
            }
            Thread.Sleep(textTime);
        }
        public int GetUserInput(int maxLength)
        {
            int userInput = 0;
            try
            {
                userInput = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                ErrorMessage("Type a number, ", 0, false);
                GetUserInput(maxLength);
            }
            if (userInput >= maxLength || userInput < 1)
            {
                ErrorMessage("Choose a smaller number", 0, false);
                GetUserInput(maxLength);
            }
            return userInput;
        }
        public void ErrorMessage(string text, int delay, bool clear)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(delay * 1000);
            if (clear) Console.Clear();
        }
    }
}
