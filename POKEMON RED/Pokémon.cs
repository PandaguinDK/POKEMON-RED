using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POKEMON_RED
{

    public class Pokémon
    {
        public PokémonNames name;
        public List<Types> type;
        public int level;
        public Genders gender;
        public Dictionary<int, List<Moves>> movesLearnset = new();
        public List<Moves> possibleMoves = new();
        public Moves[] moves = new Moves[4];
        public int[] stats = new int[6];
        public int[] statsChanges = new int[6];
        public int[] EVs = new int[6];
        public int[] IVs = new int[6];
        public Status status;
        public PokémonTypesAttribute pokemonTypes = new();
        public Dictionary<string, int> baseStats = new();

        
        public Pokémon(PokémonNames Name, int Level, Genders Gender)
        {
            this.name = Name;
            this.type = pokemonTypes.GetPokemonTyping();
            this.level = Level;
            this.gender = Gender;
            this.movesLearnset = GetMoves(Name);
            this.possibleMoves = GetPossibleMoves();

            //  Try and catch here in case there isn't 4 moves in the possibleMoves list
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    this.moves[i] = possibleMoves[i];
                }
            }

            //  Catch here sets the rest of the moves to NONE
            catch (ArgumentOutOfRangeException)
            {
                int indexOutOfRange = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (possibleMoves.Count <= i)
                    {
                        indexOutOfRange = i;
                        break;
                    }
                }
                for (int i = indexOutOfRange; i < possibleMoves.Count; i++)
                {
                    this.moves[i] = Moves.NONE;
                }
            }
            this.EVs = new int[] { 0, 0, 0, 0, 0, 0 }
;           this.stats = GetStats();

        }

        public List<Moves> GetPossibleMoves()
        {
            int count;
            List<Moves> PossibleMoves = new();
            for (int i = level; i >= 1; i--)
            {
                try
                {
                    count = movesLearnset[i].Count;
                    for (int n = 0; n < count; n++)
                    {
                        PossibleMoves.Add(movesLearnset[i][n]);
                    }
                }

                //  Catch here in case the pokemon doesn't learn a move at a level
                catch (KeyNotFoundException ex)
                {

                }
            }
            return PossibleMoves;
        }

        public int[] GetStats()
        {
            int[] stats = new int[6];
            Random rnd = new Random(DateTime.Now.Millisecond);

            //  Random IVs for HP
            IVs[0] = rnd.Next(0, 31);

            //  Formula to calculate the HP stat
            int hpBaseStat = GetBaseStats()[0];
            double EVBonus = Math.Floor(Math.Sqrt(EVs[0]) / 4);
            double middleCalculation = Math.Floor(((hpBaseStat + 7) * 2 + EVBonus) * level) / 100;
            stats[0] = (int)Math.Floor(middleCalculation) + level + 10;


            //  Formula to calculate the rest of the stats 
            for (int i = 1; i < 6; i++)
            {
                IVs[i] = rnd.Next(0, 31);
                int statBaseStat = GetBaseStats()[i];
                EVBonus = Math.Floor(Math.Sqrt(EVs[0]) / 4);
                middleCalculation = Math.Floor(((statBaseStat + 7) * 2 + EVBonus) * level) / 100;
                stats[i] = (int)Math.Floor(middleCalculation) + 5;
            }
            return stats;
        }

        public Dictionary<int, List<Moves>> GetMoves(PokémonNames Name)
        {
            Dictionary<int, List<Moves>> possibleMoves = new();
            switch (Name)
            {
                case PokémonNames.Bulbasaur:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Growl });
                    possibleMoves.Add(7, new List<Moves> { Moves.Leech_Seed });
                    possibleMoves.Add(13, new List<Moves> { Moves.Vine_Whip });
                    possibleMoves.Add(20, new List<Moves> { Moves.Poison_Powder });
                    possibleMoves.Add(27, new List<Moves> { Moves.Razor_Leaf });
                    possibleMoves.Add(34, new List<Moves> { Moves.Growth });
                    possibleMoves.Add(41, new List<Moves> { Moves.Sleep_Powder });
                    possibleMoves.Add(48, new List<Moves> { Moves.Solar_Beam });
                    break;


                case PokémonNames.Ivysaur:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Growl, Moves.Leech_Seed });
                    possibleMoves.Add(7, new List<Moves> { Moves.Leech_Seed });
                    possibleMoves.Add(13, new List<Moves> { Moves.Vine_Whip });
                    possibleMoves.Add(22, new List<Moves> { Moves.Poison_Powder });
                    possibleMoves.Add(30, new List<Moves> { Moves.Razor_Leaf });
                    possibleMoves.Add(38, new List<Moves> { Moves.Growth });
                    possibleMoves.Add(46, new List<Moves> { Moves.Sleep_Powder });
                    possibleMoves.Add(54, new List<Moves> { Moves.Solar_Beam });
                    break;


                case PokémonNames.Venusaur:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Growl, Moves.Leech_Seed, Moves.Vine_Whip });
                    possibleMoves.Add(7, new List<Moves> { Moves.Leech_Seed });
                    possibleMoves.Add(13, new List<Moves> { Moves.Vine_Whip });
                    possibleMoves.Add(22, new List<Moves> { Moves.Poison_Powder });
                    possibleMoves.Add(30, new List<Moves> { Moves.Razor_Leaf });
                    possibleMoves.Add(43, new List<Moves> { Moves.Growth });
                    possibleMoves.Add(55, new List<Moves> { Moves.Sleep_Powder });
                    possibleMoves.Add(65, new List<Moves> { Moves.Solar_Beam });
                    break;


                case PokémonNames.Charmander:
                    possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Growl });
                    possibleMoves.Add(9, new List<Moves> { Moves.Ember });
                    possibleMoves.Add(15, new List<Moves> { Moves.Leer });
                    possibleMoves.Add(22, new List<Moves> { Moves.Rage });
                    possibleMoves.Add(30, new List<Moves> { Moves.Slash });
                    possibleMoves.Add(38, new List<Moves> { Moves.Flamethrower });
                    possibleMoves.Add(46, new List<Moves> { Moves.Fire_Spin });
                    break;

                case PokémonNames.Charmeleon:
                    possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Growl, Moves.Ember });
                    possibleMoves.Add(9, new List<Moves> { Moves.Ember });
                    possibleMoves.Add(15, new List<Moves> { Moves.Leer });
                    possibleMoves.Add(24, new List<Moves> { Moves.Rage });
                    possibleMoves.Add(33, new List<Moves> { Moves.Slash });
                    possibleMoves.Add(42, new List<Moves> { Moves.Flamethrower });
                    possibleMoves.Add(56, new List<Moves> { Moves.Fire_Spin });
                    break;


                case PokémonNames.Charizard:
                    possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Growl, Moves.Ember, Moves.Leer });
                    possibleMoves.Add(9, new List<Moves> { Moves.Ember });
                    possibleMoves.Add(15, new List<Moves> { Moves.Leer });
                    possibleMoves.Add(24, new List<Moves> { Moves.Rage });
                    possibleMoves.Add(36, new List<Moves> { Moves.Slash });
                    possibleMoves.Add(46, new List<Moves> { Moves.Flamethrower });
                    possibleMoves.Add(55, new List<Moves> { Moves.Fire_Spin });
                    break;


                case PokémonNames.Squirtle:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Tail_Whip });
                    possibleMoves.Add(8, new List<Moves> { Moves.Bubble });
                    possibleMoves.Add(15, new List<Moves> { Moves.Water_Gun });
                    possibleMoves.Add(22, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(28, new List<Moves> { Moves.Withdraw });
                    possibleMoves.Add(35, new List<Moves> { Moves.Skull_Bash });
                    possibleMoves.Add(42, new List<Moves> { Moves.Hydro_Pump });
                    break;


                case PokémonNames.Wartortle:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Tail_Whip, Moves.Bubble });
                    possibleMoves.Add(8, new List<Moves> { Moves.Bubble });
                    possibleMoves.Add(15, new List<Moves> { Moves.Water_Gun });
                    possibleMoves.Add(24, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(31, new List<Moves> { Moves.Withdraw });
                    possibleMoves.Add(39, new List<Moves> { Moves.Skull_Bash });
                    possibleMoves.Add(47, new List<Moves> { Moves.Hydro_Pump });
                    break;


                case PokémonNames.Blastoise:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Tail_Whip, Moves.Bubble, Moves.Water_Gun });
                    possibleMoves.Add(8, new List<Moves> { Moves.Bubble });
                    possibleMoves.Add(15, new List<Moves> { Moves.Water_Gun });
                    possibleMoves.Add(24, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(31, new List<Moves> { Moves.Withdraw });
                    possibleMoves.Add(42, new List<Moves> { Moves.Skull_Bash });
                    possibleMoves.Add(52, new List<Moves> { Moves.Hydro_Pump });
                    break;


                case PokémonNames.Caterpie:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.String_Shot });
                    break;


                case PokémonNames.Metapod:
                    possibleMoves.Add(1, new List<Moves> { Moves.Harden });
                    break;


                case PokémonNames.Butterfree:
                    possibleMoves.Add(1, new List<Moves> { Moves.Confusion });
                    possibleMoves.Add(12, new List<Moves> { Moves.Confusion });
                    possibleMoves.Add(15, new List<Moves> { Moves.Poison_Powder });
                    possibleMoves.Add(16, new List<Moves> { Moves.Stun_Spore });
                    possibleMoves.Add(17, new List<Moves> { Moves.Sleep_Powder });
                    possibleMoves.Add(21, new List<Moves> { Moves.Supersonic });
                    possibleMoves.Add(26, new List<Moves> { Moves.Whirlwind });
                    possibleMoves.Add(32, new List<Moves> { Moves.Psybeam });
                    break;


                case PokémonNames.Weedle:
                    possibleMoves.Add(1, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(1, new List<Moves> { Moves.String_Shot });
                    break;


                case PokémonNames.Kakuna:
                    possibleMoves.Add(1, new List<Moves> { Moves.Harden });
                    break;


                case PokémonNames.Beedrill:
                    possibleMoves.Add(1, new List<Moves> { Moves.Fury_Attack });
                    possibleMoves.Add(12, new List<Moves> { Moves.Fury_Attack });
                    possibleMoves.Add(16, new List<Moves> { Moves.Focus_Energy });
                    possibleMoves.Add(20, new List<Moves> { Moves.Twineedle });
                    possibleMoves.Add(25, new List<Moves> { Moves.Rage });
                    possibleMoves.Add(30, new List<Moves> { Moves.Pin_Missile });
                    possibleMoves.Add(35, new List<Moves> { Moves.Agility });
                    break;


                case PokémonNames.Pidgey:
                    possibleMoves.Add(1, new List<Moves> { Moves.Gust });
                    possibleMoves.Add(5, new List<Moves> { Moves.Sand_Attack });
                    possibleMoves.Add(12, new List<Moves> { Moves.Quick_Attack });
                    possibleMoves.Add(19, new List<Moves> { Moves.Whirlwind });
                    possibleMoves.Add(28, new List<Moves> { Moves.Wing_Attack });
                    possibleMoves.Add(36, new List<Moves> { Moves.Agility });
                    possibleMoves.Add(44, new List<Moves> { Moves.Mirror_Move });
                    break;


                case PokémonNames.Pidgeotto:
                    possibleMoves.Add(1, new List<Moves> { Moves.Gust, Moves.Sand_Attack });
                    possibleMoves.Add(5, new List<Moves> { Moves.Sand_Attack });
                    possibleMoves.Add(12, new List<Moves> { Moves.Quick_Attack });
                    possibleMoves.Add(21, new List<Moves> { Moves.Whirlwind });
                    possibleMoves.Add(31, new List<Moves> { Moves.Wing_Attack });
                    possibleMoves.Add(40, new List<Moves> { Moves.Agility });
                    possibleMoves.Add(49, new List<Moves> { Moves.Mirror_Move });
                    break;


                case PokémonNames.Pidgeot:
                    possibleMoves.Add(1, new List<Moves> { Moves.Gust, Moves.Sand_Attack, Moves.Quick_Attack });
                    possibleMoves.Add(5, new List<Moves> { Moves.Sand_Attack });
                    possibleMoves.Add(12, new List<Moves> { Moves.Quick_Attack });
                    possibleMoves.Add(21, new List<Moves> { Moves.Whirlwind });
                    possibleMoves.Add(31, new List<Moves> { Moves.Wing_Attack });
                    possibleMoves.Add(44, new List<Moves> { Moves.Agility });
                    possibleMoves.Add(54, new List<Moves> { Moves.Mirror_Move });
                    break;


                case PokémonNames.Rattata:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Tail_Whip });
                    possibleMoves.Add(7, new List<Moves> { Moves.Quick_Attack });
                    possibleMoves.Add(14, new List<Moves> { Moves.Hyper_Fang });
                    possibleMoves.Add(23, new List<Moves> { Moves.Focus_Energy });
                    possibleMoves.Add(34, new List<Moves> { Moves.Super_Fang });
                    break;


                case PokémonNames.Raticate:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Tail_Whip, Moves.Quick_Attack });
                    possibleMoves.Add(7, new List<Moves> { Moves.Quick_Attack });
                    possibleMoves.Add(14, new List<Moves> { Moves.Hyper_Fang });
                    possibleMoves.Add(27, new List<Moves> { Moves.Focus_Energy });
                    possibleMoves.Add(41, new List<Moves> { Moves.Super_Fang });
                    break;


                case PokémonNames.Spearow:
                    possibleMoves.Add(1, new List<Moves> { Moves.Peck, Moves.Growl });
                    possibleMoves.Add(9, new List<Moves> { Moves.Leer });
                    possibleMoves.Add(15, new List<Moves> { Moves.Fury_Attack });
                    possibleMoves.Add(22, new List<Moves> { Moves.Mirror_Move });
                    possibleMoves.Add(29, new List<Moves> { Moves.Drill_Peck });
                    possibleMoves.Add(36, new List<Moves> { Moves.Agility });
                    break;


                case PokémonNames.Fearow:
                    possibleMoves.Add(1, new List<Moves> { Moves.Peck, Moves.Growl, Moves.Leer });
                    possibleMoves.Add(9, new List<Moves> { Moves.Leer });
                    possibleMoves.Add(15, new List<Moves> { Moves.Fury_Attack });
                    possibleMoves.Add(25, new List<Moves> { Moves.Mirror_Move });
                    possibleMoves.Add(34, new List<Moves> { Moves.Drill_Peck });
                    possibleMoves.Add(43, new List<Moves> { Moves.Agility });
                    break;


                case PokémonNames.Ekans:
                    possibleMoves.Add(1, new List<Moves> { Moves.Wrap, Moves.Leer });
                    possibleMoves.Add(10, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(17, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(24, new List<Moves> { Moves.Glare });
                    possibleMoves.Add(31, new List<Moves> { Moves.Screech });
                    possibleMoves.Add(38, new List<Moves> { Moves.Acid });
                    break;


                case PokémonNames.Arbok:
                    possibleMoves.Add(1, new List<Moves> { Moves.Wrap, Moves.Leer, Moves.Poison_Sting });
                    possibleMoves.Add(10, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(17, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(27, new List<Moves> { Moves.Glare });
                    possibleMoves.Add(36, new List<Moves> { Moves.Screech });
                    possibleMoves.Add(47, new List<Moves> { Moves.Acid });
                    break;


                case PokémonNames.Pikachu:
                    possibleMoves.Add(1, new List<Moves> { Moves.Thunder_Shock, Moves.Growl });
                    possibleMoves.Add(9, new List<Moves> { Moves.Thunder_Wave });
                    possibleMoves.Add(16, new List<Moves> { Moves.Quick_Attack });
                    possibleMoves.Add(33, new List<Moves> { Moves.Agility });
                    possibleMoves.Add(43, new List<Moves> { Moves.Thunder });
                    possibleMoves.Add(26, new List<Moves> { Moves.Swift });
                    break;


                case PokémonNames.Raichu:
                    possibleMoves.Add(1, new List<Moves> { Moves.Thunder_Shock, Moves.Growl, Moves.Thunder_Wave });
                    break;


                case PokémonNames.Sandshrew:
                    possibleMoves.Add(1, new List<Moves> { Moves.Scratch });
                    possibleMoves.Add(10, new List<Moves> { Moves.Sand_Attack });
                    possibleMoves.Add(17, new List<Moves> { Moves.Slash });
                    possibleMoves.Add(24, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(31, new List<Moves> { Moves.Swift });
                    possibleMoves.Add(38, new List<Moves> { Moves.Fury_Swipes });
                    break;


                case PokémonNames.Sandslash:
                    possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Sand_Attack });
                    possibleMoves.Add(10, new List<Moves> { Moves.Sand_Attack });
                    possibleMoves.Add(17, new List<Moves> { Moves.Slash });
                    possibleMoves.Add(27, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(36, new List<Moves> { Moves.Swift });
                    possibleMoves.Add(47, new List<Moves> { Moves.Fury_Swipes });
                    break;


                case PokémonNames.Nidoran_Female:
                    possibleMoves.Add(1, new List<Moves> { Moves.Growl, Moves.Tackle });
                    possibleMoves.Add(8, new List<Moves> { Moves.Scratch });
                    possibleMoves.Add(43, new List<Moves> { Moves.Double_Kick });
                    possibleMoves.Add(14, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(21, new List<Moves> { Moves.Tail_Whip });
                    possibleMoves.Add(29, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(36, new List<Moves> { Moves.Fury_Swipes });
                    break;


                case PokémonNames.Nidorina:
                    possibleMoves.Add(1, new List<Moves> { Moves.Growl, Moves.Tackle, Moves.Scratch });
                    possibleMoves.Add(8, new List<Moves> { Moves.Scratch });
                    possibleMoves.Add(50, new List<Moves> { Moves.Double_Kick });
                    possibleMoves.Add(14, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(23, new List<Moves> { Moves.Tail_Whip });
                    possibleMoves.Add(32, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(41, new List<Moves> { Moves.Fury_Swipes });
                    break;


                case PokémonNames.Nidoqueen:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Scratch, Moves.Tail_Whip, Moves.Body_Slam });
                    possibleMoves.Add(8, new List<Moves> { Moves.Scratch });
                    possibleMoves.Add(23, new List<Moves> { Moves.Body_Slam });
                    possibleMoves.Add(14, new List<Moves> { Moves.Poison_Sting });
                    break;


                case PokémonNames.Nidoran_Male
:
                    possibleMoves.Add(1, new List<Moves> { Moves.Leer, Moves.Tackle });
                    possibleMoves.Add(8, new List<Moves> { Moves.Horn_Attack });
                    possibleMoves.Add(43, new List<Moves> { Moves.Double_Kick });
                    possibleMoves.Add(14, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(21, new List<Moves> { Moves.Focus_Energy });
                    possibleMoves.Add(29, new List<Moves> { Moves.Fury_Attack });
                    possibleMoves.Add(36, new List<Moves> { Moves.Horn_Drill });

                    break;


                case PokémonNames.Nidorino:
                    possibleMoves.Add(1, new List<Moves> { Moves.Leer, Moves.Tackle, Moves.Horn_Attack });
                    possibleMoves.Add(8, new List<Moves> { Moves.Horn_Attack });
                    possibleMoves.Add(50, new List<Moves> { Moves.Double_Kick });
                    possibleMoves.Add(14, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(23, new List<Moves> { Moves.Focus_Energy });
                    possibleMoves.Add(32, new List<Moves> { Moves.Fury_Attack });
                    possibleMoves.Add(41, new List<Moves> { Moves.Horn_Drill });
                    break;


                case PokémonNames.Nidoking:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Horn_Attack, Moves.Poison_Sting, Moves.Thrash });
                    possibleMoves.Add(8, new List<Moves> { Moves.Horn_Attack });
                    possibleMoves.Add(14, new List<Moves> { Moves.Poison_Sting });
                    possibleMoves.Add(23, new List<Moves> { Moves.Thrash });
                    break;


                case PokémonNames.Clefairy:
                    possibleMoves.Add(1, new List<Moves> { Moves.Pound, Moves.Growl });
                    possibleMoves.Add(13, new List<Moves> { Moves.Sing });
                    possibleMoves.Add(18, new List<Moves> { Moves.Double_Slap });
                    possibleMoves.Add(24, new List<Moves> { Moves.Minimize });
                    possibleMoves.Add(31, new List<Moves> { Moves.Metronome });
                    possibleMoves.Add(39, new List<Moves> { Moves.Defense_Curl });
                    possibleMoves.Add(48, new List<Moves> { Moves.Light_Screen });
                    break;


                case PokémonNames.Clefable:
                    possibleMoves.Add(1, new List<Moves> { Moves.Sing, Moves.Double_Slap, Moves.Minimize, Moves.Metronome });
                    break;


                case PokémonNames.Vulpix:
                    possibleMoves.Add(1, new List<Moves> { Moves.Ember, Moves.Tail_Whip });
                    possibleMoves.Add(16, new List<Moves> { Moves.Quick_Attack });
                    possibleMoves.Add(21, new List<Moves> { Moves.Roar });
                    possibleMoves.Add(28, new List<Moves> { Moves.Confuse_Ray });
                    possibleMoves.Add(35, new List<Moves> { Moves.Flamethrower });
                    possibleMoves.Add(42, new List<Moves> { Moves.Fire_Spin });
                    break;


                case PokémonNames.Ninetales:
                    possibleMoves.Add(1, new List<Moves> { Moves.Ember, Moves.Ember, Moves.Quick_Attack, Moves.Roar });
                    break;


                case PokémonNames.Jigglypuff:
                    possibleMoves.Add(1, new List<Moves> { Moves.Sing });
                    possibleMoves.Add(9, new List<Moves> { Moves.Pound });
                    possibleMoves.Add(14, new List<Moves> { Moves.Disable });
                    possibleMoves.Add(19, new List<Moves> { Moves.Defense_Curl });
                    possibleMoves.Add(24, new List<Moves> { Moves.Double_Slap });
                    possibleMoves.Add(29, new List<Moves> { Moves.Rest });
                    possibleMoves.Add(34, new List<Moves> { Moves.Body_Slam });
                    possibleMoves.Add(39, new List<Moves> { Moves.Double_Edge });
                    break;


                case PokémonNames.Wigglytuff:
                    possibleMoves.Add(1, new List<Moves> { Moves.Sing, Moves.Disable, Moves.Defense_Curl, Moves.Double_Slap });
                    break;


                case PokémonNames.Zubat:
                    possibleMoves.Add(1, new List<Moves> { Moves.Leech_Life });
                    possibleMoves.Add(10, new List<Moves> { Moves.Supersonic });
                    possibleMoves.Add(15, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(21, new List<Moves> { Moves.Confuse_Ray });
                    possibleMoves.Add(28, new List<Moves> { Moves.Wing_Attack });
                    possibleMoves.Add(36, new List<Moves> { Moves.Haze });
                    break;


                case PokémonNames.Golbat:
                    possibleMoves.Add(1, new List<Moves> { Moves.Leech_Life, Moves.Screech, Moves.Bite });
                    possibleMoves.Add(10, new List<Moves> { Moves.Supersonic });
                    possibleMoves.Add(15, new List<Moves> { Moves.Bite });
                    possibleMoves.Add(21, new List<Moves> { Moves.Confuse_Ray });
                    possibleMoves.Add(32, new List<Moves> { Moves.Wing_Attack });
                    possibleMoves.Add(43, new List<Moves> { Moves.Haze });
                    break;


                case PokémonNames.Oddish:
                    possibleMoves.Add(1, new List<Moves> { Moves.Absorb });
                    possibleMoves.Add(15, new List<Moves> { Moves.Poison_Powder });
                    possibleMoves.Add(17, new List<Moves> { Moves.Stun_Spore });
                    possibleMoves.Add(19, new List<Moves> { Moves.Sleep_Powder });
                    possibleMoves.Add(24, new List<Moves> { Moves.Acid });
                    possibleMoves.Add(33, new List<Moves> { Moves.Petal_Dance });
                    possibleMoves.Add(46, new List<Moves> { Moves.Solar_Beam });
                    break;


                case PokémonNames.Gloom:
                    possibleMoves.Add(1, new List<Moves> { Moves.Absorb, Moves.Poison_Powder, Moves.Stun_Spore });
                    possibleMoves.Add(15, new List<Moves> { Moves.Poison_Powder });
                    possibleMoves.Add(17, new List<Moves> { Moves.Stun_Spore });
                    possibleMoves.Add(19, new List<Moves> { Moves.Sleep_Powder });
                    possibleMoves.Add(28, new List<Moves> { Moves.Acid });
                    possibleMoves.Add(38, new List<Moves> { Moves.Petal_Dance });
                    possibleMoves.Add(52, new List<Moves> { Moves.Solar_Beam });
                    break;


                case PokémonNames.Vileplume:
                    possibleMoves.Add(1, new List<Moves> { Moves.Stun_Spore, Moves.Sleep_Powder, Moves.Acid, Moves.Petal_Dance });
                    possibleMoves.Add(15, new List<Moves> { Moves.Poison_Powder });
                    possibleMoves.Add(17, new List<Moves> { Moves.Stun_Spore });
                    possibleMoves.Add(19, new List<Moves> { Moves.Sleep_Powder });
                    break;


                case PokémonNames.Paras:
                    possibleMoves.Add(1, new List<Moves> { Moves.Scratch });
                    possibleMoves.Add(13, new List<Moves> { Moves.Stun_Spore });
                    possibleMoves.Add(20, new List<Moves> { Moves.Leech_Life });
                    possibleMoves.Add(27, new List<Moves> { Moves.Spore });
                    possibleMoves.Add(34, new List<Moves> { Moves.Slash });
                    possibleMoves.Add(41, new List<Moves> { Moves.Growth });
                    break;


                case PokémonNames.Parasect:
                    possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Stun_Spore, Moves.Leech_Life });
                    possibleMoves.Add(13, new List<Moves> { Moves.Stun_Spore });
                    possibleMoves.Add(20, new List<Moves> { Moves.Leech_Life });
                    possibleMoves.Add(30, new List<Moves> { Moves.Spore });
                    possibleMoves.Add(39, new List<Moves> { Moves.Slash });
                    possibleMoves.Add(48, new List<Moves> { Moves.Growth });
                    break;


                    case PokémonNames.Venonat:
                    possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Disable });
                    possibleMoves.Add(24, new List<Moves> { Moves.Poison_Powder });
                    possibleMoves.Add(27, new List<Moves> { Moves.Leech_Life });
                    possibleMoves.Add(30, new List<Moves> { Moves.Stun_Spore });
                    possibleMoves.Add(35, new List<Moves> { Moves.Psybeam });
                    possibleMoves.Add(38, new List<Moves> { Moves.Sleep_Powder });
                    possibleMoves.Add(43, new List<Moves> { Moves.Psychic });
                    break;



                case PokémonNames.Venomoth:
                possibleMoves.Add(1, new List<Moves> { Moves.Poison_Powder, Moves.Leech_Life, Moves.Tackle, Moves.Disable });
                possibleMoves.Add(24, new List<Moves> { Moves.Poison_Powder });
                possibleMoves.Add(27, new List<Moves> { Moves.Leech_Life });
                possibleMoves.Add(30, new List<Moves> { Moves.Stun_Spore });
                possibleMoves.Add(38, new List<Moves> { Moves.Psybeam });
                possibleMoves.Add(43, new List<Moves> { Moves.Sleep_Powder });
                possibleMoves.Add(50, new List<Moves> { Moves.Psychic });
                break;


            case PokémonNames.Diglett:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch });
                possibleMoves.Add(15, new List<Moves> { Moves.Growl });
                possibleMoves.Add(19, new List<Moves> { Moves.Dig });
                possibleMoves.Add(24, new List<Moves> { Moves.Sand_Attack });
                possibleMoves.Add(31, new List<Moves> { Moves.Slash });
                possibleMoves.Add(40, new List<Moves> { Moves.Earthquake });
                break;


            case PokémonNames.Dugtrio:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Growl, Moves.Dig });
                possibleMoves.Add(15, new List<Moves> { Moves.Growl });
                possibleMoves.Add(19, new List<Moves> { Moves.Dig });
                possibleMoves.Add(24, new List<Moves> { Moves.Sand_Attack });
                possibleMoves.Add(35, new List<Moves> { Moves.Slash });
                possibleMoves.Add(47, new List<Moves> { Moves.Earthquake });
                break;


            case PokémonNames.Meowth:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Growl });
                possibleMoves.Add(12, new List<Moves> { Moves.Bite });
                possibleMoves.Add(17, new List<Moves> { Moves.Pay_Day });
                possibleMoves.Add(24, new List<Moves> { Moves.Screech });
                possibleMoves.Add(33, new List<Moves> { Moves.Fury_Swipes });
                possibleMoves.Add(44, new List<Moves> { Moves.Slash });
                break;


            case PokémonNames.Persian:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Growl, Moves.Bite, Moves.Screech });
                possibleMoves.Add(12, new List<Moves> { Moves.Bite });
                possibleMoves.Add(17, new List<Moves> { Moves.Pay_Day });
                possibleMoves.Add(24, new List<Moves> { Moves.Screech });
                possibleMoves.Add(37, new List<Moves> { Moves.Fury_Swipes });
                possibleMoves.Add(51, new List<Moves> { Moves.Slash });
                break;


            case PokémonNames.Psyduck:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch });
                possibleMoves.Add(28, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(31, new List<Moves> { Moves.Disable });
                possibleMoves.Add(36, new List<Moves> { Moves.Confusion });
                possibleMoves.Add(43, new List<Moves> { Moves.Fury_Swipes });
                possibleMoves.Add(52, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Golduck:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Tail_Whip, Moves.Disable });
                possibleMoves.Add(28, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(31, new List<Moves> { Moves.Disable });
                possibleMoves.Add(39, new List<Moves> { Moves.Confusion });
                possibleMoves.Add(48, new List<Moves> { Moves.Fury_Swipes });
                possibleMoves.Add(59, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Mankey:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Leer });
                possibleMoves.Add(15, new List<Moves> { Moves.Karate_Chop });
                possibleMoves.Add(21, new List<Moves> { Moves.Fury_Swipes });
                possibleMoves.Add(27, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(33, new List<Moves> { Moves.Seismic_Toss });
                possibleMoves.Add(39, new List<Moves> { Moves.Thrash });
                break;


            case PokémonNames.Primeape:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Leer, Moves.Fury_Swipes, Moves.Karate_Chop });
                possibleMoves.Add(15, new List<Moves> { Moves.Karate_Chop });
                possibleMoves.Add(21, new List<Moves> { Moves.Fury_Swipes });
                possibleMoves.Add(27, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(37, new List<Moves> { Moves.Seismic_Toss });
                possibleMoves.Add(46, new List<Moves> { Moves.Thrash });
                break;


            case PokémonNames.Growlithe:
                possibleMoves.Add(1, new List<Moves> { Moves.Bite, Moves.Roar });
                possibleMoves.Add(18, new List<Moves> { Moves.Ember });
                possibleMoves.Add(23, new List<Moves> { Moves.Leer });
                possibleMoves.Add(30, new List<Moves> { Moves.Take_Down });
                possibleMoves.Add(39, new List<Moves> { Moves.Agility });
                possibleMoves.Add(50, new List<Moves> { Moves.Flamethrower });
                break;


            case PokémonNames.Arcanine:
                possibleMoves.Add(1, new List<Moves> { Moves.Roar, Moves.Ember, Moves.Leer, Moves.Take_Down });
                break;


            case PokémonNames.Poliwag:
                possibleMoves.Add(1, new List<Moves> { Moves.Bubble });
                possibleMoves.Add(16, new List<Moves> { Moves.Hypnosis });
                possibleMoves.Add(19, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(25, new List<Moves> { Moves.Double_Slap });
                possibleMoves.Add(31, new List<Moves> { Moves.Body_Slam });
                possibleMoves.Add(38, new List<Moves> { Moves.Amnesia });
                possibleMoves.Add(45, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Poliwhirl:
                possibleMoves.Add(1, new List<Moves> { Moves.Bubble, Moves.Hypnosis, Moves.Water_Gun });
                possibleMoves.Add(16, new List<Moves> { Moves.Hypnosis });
                possibleMoves.Add(19, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(26, new List<Moves> { Moves.Double_Slap });
                possibleMoves.Add(33, new List<Moves> { Moves.Body_Slam });
                possibleMoves.Add(41, new List<Moves> { Moves.Amnesia });
                possibleMoves.Add(49, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Poliwrath:
                possibleMoves.Add(1, new List<Moves> { Moves.Hypnosis, Moves.Water_Gun, Moves.Double_Slap, Moves.Body_Slam });
                possibleMoves.Add(16, new List<Moves> { Moves.Hypnosis });
                possibleMoves.Add(19, new List<Moves> { Moves.Water_Gun });
                break;


            case PokémonNames.Abra:
                possibleMoves.Add(1, new List<Moves> { Moves.Teleport });
                break;


            case PokémonNames.Kadabra:
                possibleMoves.Add(1, new List<Moves> { Moves.Teleport, Moves.Confusion, Moves.Disable });
                possibleMoves.Add(16, new List<Moves> { Moves.Confusion });
                possibleMoves.Add(20, new List<Moves> { Moves.Disable });
                possibleMoves.Add(27, new List<Moves> { Moves.Psybeam });
                possibleMoves.Add(31, new List<Moves> { Moves.Recover });
                possibleMoves.Add(38, new List<Moves> { Moves.Psychic });
                possibleMoves.Add(42, new List<Moves> { Moves.Reflect });
                break;


            case PokémonNames.Alakazam:
                possibleMoves.Add(1, new List<Moves> { Moves.Teleport, Moves.Disable, Moves.Confusion });
                possibleMoves.Add(16, new List<Moves> { Moves.Confusion });
                possibleMoves.Add(20, new List<Moves> { Moves.Disable });
                possibleMoves.Add(27, new List<Moves> { Moves.Psybeam });
                possibleMoves.Add(31, new List<Moves> { Moves.Recover });
                possibleMoves.Add(38, new List<Moves> { Moves.Psychic });
                possibleMoves.Add(42, new List<Moves> { Moves.Reflect });
                break;


            case PokémonNames.Machop:
                possibleMoves.Add(1, new List<Moves> { Moves.Karate_Chop });
                possibleMoves.Add(20, new List<Moves> { Moves.Low_Kick });
                possibleMoves.Add(25, new List<Moves> { Moves.Leer });
                possibleMoves.Add(32, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(39, new List<Moves> { Moves.Seismic_Toss });
                possibleMoves.Add(46, new List<Moves> { Moves.Submission });
                break;


            case PokémonNames.Machoke:
                possibleMoves.Add(1, new List<Moves> { Moves.Karate_Chop, Moves.Low_Kick, Moves.Leer });
                possibleMoves.Add(20, new List<Moves> { Moves.Low_Kick });
                possibleMoves.Add(25, new List<Moves> { Moves.Leer });
                possibleMoves.Add(36, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(44, new List<Moves> { Moves.Seismic_Toss });
                possibleMoves.Add(52, new List<Moves> { Moves.Submission });
                break;


            case PokémonNames.Machamp:
                possibleMoves.Add(1, new List<Moves> { Moves.Karate_Chop, Moves.Low_Kick, Moves.Leer });
                possibleMoves.Add(20, new List<Moves> { Moves.Low_Kick });
                possibleMoves.Add(25, new List<Moves> { Moves.Leer });
                possibleMoves.Add(36, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(44, new List<Moves> { Moves.Seismic_Toss });
                possibleMoves.Add(52, new List<Moves> { Moves.Submission });
                break;


            case PokémonNames.Bellsprout:
                possibleMoves.Add(1, new List<Moves> { Moves.Vine_Whip, Moves.Growth });
                possibleMoves.Add(13, new List<Moves> { Moves.Wrap });
                possibleMoves.Add(15, new List<Moves> { Moves.Poison_Powder });
                possibleMoves.Add(18, new List<Moves> { Moves.Sleep_Powder });
                possibleMoves.Add(21, new List<Moves> { Moves.Stun_Spore });
                possibleMoves.Add(26, new List<Moves> { Moves.Acid });
                possibleMoves.Add(33, new List<Moves> { Moves.Razor_Leaf });
                possibleMoves.Add(42, new List<Moves> { Moves.Slam });
                break;


            case PokémonNames.Weepinbell:
                possibleMoves.Add(1, new List<Moves> { Moves.Vine_Whip, Moves.Growth, Moves.Wrap });
                possibleMoves.Add(13, new List<Moves> { Moves.Wrap });
                possibleMoves.Add(15, new List<Moves> { Moves.Poison_Powder });
                possibleMoves.Add(18, new List<Moves> { Moves.Sleep_Powder });
                possibleMoves.Add(23, new List<Moves> { Moves.Stun_Spore });
                possibleMoves.Add(29, new List<Moves> { Moves.Acid });
                possibleMoves.Add(38, new List<Moves> { Moves.Razor_Leaf });
                possibleMoves.Add(49, new List<Moves> { Moves.Slam });
                break;


            case PokémonNames.Victreebel:
                possibleMoves.Add(1, new List<Moves> { Moves.Sleep_Powder, Moves.Stun_Spore, Moves.Acid, Moves.Razor_Leaf });
                possibleMoves.Add(13, new List<Moves> { Moves.Wrap });
                possibleMoves.Add(15, new List<Moves> { Moves.Poison_Powder });
                possibleMoves.Add(18, new List<Moves> { Moves.Sleep_Powder });
                break;


            case PokémonNames.Tentacool:
                possibleMoves.Add(1, new List<Moves> { Moves.Acid });
                possibleMoves.Add(7, new List<Moves> { Moves.Supersonic });
                possibleMoves.Add(13, new List<Moves> { Moves.Wrap });
                possibleMoves.Add(18, new List<Moves> { Moves.Poison_Sting });
                possibleMoves.Add(22, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(27, new List<Moves> { Moves.Constrict });
                possibleMoves.Add(33, new List<Moves> { Moves.Barrier });
                possibleMoves.Add(40, new List<Moves> { Moves.Screech });
                possibleMoves.Add(48, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Tentacruel:
                possibleMoves.Add(1, new List<Moves> { Moves.Acid, Moves.Supersonic, Moves.Wrap });
                possibleMoves.Add(7, new List<Moves> { Moves.Supersonic });
                possibleMoves.Add(13, new List<Moves> { Moves.Wrap });
                possibleMoves.Add(18, new List<Moves> { Moves.Poison_Sting });
                possibleMoves.Add(22, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(27, new List<Moves> { Moves.Constrict });
                possibleMoves.Add(35, new List<Moves> { Moves.Barrier });
                possibleMoves.Add(43, new List<Moves> { Moves.Screech });
                possibleMoves.Add(50, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Geodude:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle });
                possibleMoves.Add(11, new List<Moves> { Moves.Defense_Curl });
                possibleMoves.Add(16, new List<Moves> { Moves.Rock_Throw });
                possibleMoves.Add(21, new List<Moves> { Moves.Self_Destruct });
                possibleMoves.Add(26, new List<Moves> { Moves.Harden });
                possibleMoves.Add(31, new List<Moves> { Moves.Earthquake });
                possibleMoves.Add(36, new List<Moves> { Moves.Explosion });
                break;


            case PokémonNames.Graveler:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Defense_Curl });
                possibleMoves.Add(11, new List<Moves> { Moves.Defense_Curl });
                possibleMoves.Add(16, new List<Moves> { Moves.Rock_Throw });
                possibleMoves.Add(21, new List<Moves> { Moves.Self_Destruct });
                possibleMoves.Add(29, new List<Moves> { Moves.Harden });
                possibleMoves.Add(36, new List<Moves> { Moves.Earthquake });
                possibleMoves.Add(43, new List<Moves> { Moves.Explosion });
                break;


            case PokémonNames.Golem:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Defense_Curl });
                possibleMoves.Add(11, new List<Moves> { Moves.Defense_Curl });
                possibleMoves.Add(16, new List<Moves> { Moves.Rock_Throw });
                possibleMoves.Add(21, new List<Moves> { Moves.Self_Destruct });
                possibleMoves.Add(29, new List<Moves> { Moves.Harden });
                possibleMoves.Add(36, new List<Moves> { Moves.Earthquake });
                possibleMoves.Add(43, new List<Moves> { Moves.Explosion });
                break;


            case PokémonNames.Ponyta:
                possibleMoves.Add(1, new List<Moves> { Moves.Ember });
                possibleMoves.Add(30, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(32, new List<Moves> { Moves.Stomp });
                possibleMoves.Add(35, new List<Moves> { Moves.Growl });
                possibleMoves.Add(39, new List<Moves> { Moves.Fire_Spin });
                possibleMoves.Add(43, new List<Moves> { Moves.Take_Down });
                possibleMoves.Add(48, new List<Moves> { Moves.Agility });
                break;


            case PokémonNames.Rapidash:
                possibleMoves.Add(1, new List<Moves> { Moves.Ember, Moves.Tail_Whip, Moves.Stomp, Moves.Growl });
                possibleMoves.Add(30, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(32, new List<Moves> { Moves.Stomp });
                possibleMoves.Add(35, new List<Moves> { Moves.Growl });
                possibleMoves.Add(39, new List<Moves> { Moves.Fire_Spin });
                possibleMoves.Add(47, new List<Moves> { Moves.Take_Down });
                possibleMoves.Add(55, new List<Moves> { Moves.Agility });
                break;


            case PokémonNames.Slowpoke:
                possibleMoves.Add(1, new List<Moves> { Moves.Confusion });
                possibleMoves.Add(18, new List<Moves> { Moves.Disable });
                possibleMoves.Add(22, new List<Moves> { Moves.Headbutt });
                possibleMoves.Add(27, new List<Moves> { Moves.Growl });
                possibleMoves.Add(33, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(40, new List<Moves> { Moves.Amnesia });
                possibleMoves.Add(48, new List<Moves> { Moves.Psychic });
                break;


            case PokémonNames.Slowbro:
                possibleMoves.Add(1, new List<Moves> { Moves.Confusion, Moves.Disable, Moves.Headbutt });
                possibleMoves.Add(18, new List<Moves> { Moves.Disable });
                possibleMoves.Add(22, new List<Moves> { Moves.Headbutt });
                possibleMoves.Add(27, new List<Moves> { Moves.Growl });
                possibleMoves.Add(33, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(37, new List<Moves> { Moves.Withdraw });
                possibleMoves.Add(44, new List<Moves> { Moves.Amnesia });
                possibleMoves.Add(55, new List<Moves> { Moves.Psychic });
                break;


            case PokémonNames.Magnemite:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle });
                possibleMoves.Add(21, new List<Moves> { Moves.Sonic_Boom });
                possibleMoves.Add(25, new List<Moves> { Moves.Thunder_Shock });
                possibleMoves.Add(29, new List<Moves> { Moves.Supersonic });
                possibleMoves.Add(35, new List<Moves> { Moves.Thunder_Wave });
                possibleMoves.Add(41, new List<Moves> { Moves.Swift });
                possibleMoves.Add(47, new List<Moves> { Moves.Screech });
                break;


            case PokémonNames.Magneton:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Sonic_Boom, Moves.Thunder_Shock });
                possibleMoves.Add(21, new List<Moves> { Moves.Sonic_Boom });
                possibleMoves.Add(25, new List<Moves> { Moves.Thunder_Shock });
                possibleMoves.Add(29, new List<Moves> { Moves.Supersonic });
                possibleMoves.Add(38, new List<Moves> { Moves.Thunder_Wave });
                possibleMoves.Add(46, new List<Moves> { Moves.Swift });
                possibleMoves.Add(54, new List<Moves> { Moves.Screech });
                break;


            case PokémonNames.Farfetchd:
                possibleMoves.Add(1, new List<Moves> { Moves.Peck, Moves.Sand_Attack });
                possibleMoves.Add(7, new List<Moves> { Moves.Leer });
                possibleMoves.Add(15, new List<Moves> { Moves.Fury_Attack });
                possibleMoves.Add(23, new List<Moves> { Moves.Swords_Dance });
                possibleMoves.Add(31, new List<Moves> { Moves.Agility });
                possibleMoves.Add(39, new List<Moves> { Moves.Slash });
                break;


            case PokémonNames.Doduo:
                possibleMoves.Add(1, new List<Moves> { Moves.Peck });
                possibleMoves.Add(20, new List<Moves> { Moves.Growl });
                possibleMoves.Add(24, new List<Moves> { Moves.Fury_Attack });
                possibleMoves.Add(30, new List<Moves> { Moves.Drill_Peck });
                possibleMoves.Add(36, new List<Moves> { Moves.Rage });
                possibleMoves.Add(40, new List<Moves> { Moves.Tri_Attack });
                possibleMoves.Add(44, new List<Moves> { Moves.Agility });
                break;


            case PokémonNames.Dodrio:
                possibleMoves.Add(1, new List<Moves> { Moves.Peck, Moves.Growl, Moves.Fury_Attack });
                possibleMoves.Add(20, new List<Moves> { Moves.Growl });
                possibleMoves.Add(24, new List<Moves> { Moves.Fury_Attack });
                possibleMoves.Add(30, new List<Moves> { Moves.Drill_Peck });
                possibleMoves.Add(39, new List<Moves> { Moves.Rage });
                possibleMoves.Add(45, new List<Moves> { Moves.Tri_Attack });
                possibleMoves.Add(51, new List<Moves> { Moves.Agility });
                break;


            case PokémonNames.Seel:
                possibleMoves.Add(1, new List<Moves> { Moves.Headbutt });
                possibleMoves.Add(30, new List<Moves> { Moves.Growl });
                possibleMoves.Add(35, new List<Moves> { Moves.Aurora_Beam });
                possibleMoves.Add(40, new List<Moves> { Moves.Rest });
                possibleMoves.Add(45, new List<Moves> { Moves.Take_Down });
                possibleMoves.Add(50, new List<Moves> { Moves.Ice_Beam });
                break;


            case PokémonNames.Dewgong:
                possibleMoves.Add(1, new List<Moves> { Moves.Headbutt, Moves.Growl, Moves.Aurora_Beam });
                possibleMoves.Add(30, new List<Moves> { Moves.Growl });
                possibleMoves.Add(35, new List<Moves> { Moves.Aurora_Beam });
                possibleMoves.Add(44, new List<Moves> { Moves.Rest });
                possibleMoves.Add(50, new List<Moves> { Moves.Take_Down });
                possibleMoves.Add(56, new List<Moves> { Moves.Ice_Beam });
                break;


            case PokémonNames.Grimer:
                possibleMoves.Add(1, new List<Moves> { Moves.Pound, Moves.Disable });
                possibleMoves.Add(30, new List<Moves> { Moves.Poison_Gas });
                possibleMoves.Add(33, new List<Moves> { Moves.Minimize });
                possibleMoves.Add(37, new List<Moves> { Moves.Sludge });
                possibleMoves.Add(42, new List<Moves> { Moves.Harden });
                possibleMoves.Add(48, new List<Moves> { Moves.Screech });
                possibleMoves.Add(55, new List<Moves> { Moves.Acid_Armor });
                break;


            case PokémonNames.Muk:
                possibleMoves.Add(1, new List<Moves> { Moves.Pound, Moves.Disable, Moves.Poison_Gas });
                possibleMoves.Add(30, new List<Moves> { Moves.Poison_Gas });
                possibleMoves.Add(33, new List<Moves> { Moves.Minimize });
                possibleMoves.Add(37, new List<Moves> { Moves.Sludge });
                possibleMoves.Add(45, new List<Moves> { Moves.Harden });
                possibleMoves.Add(53, new List<Moves> { Moves.Screech });
                possibleMoves.Add(60, new List<Moves> { Moves.Acid_Armor });
                break;


            case PokémonNames.Shellder:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Withdraw });
                possibleMoves.Add(18, new List<Moves> { Moves.Supersonic });
                possibleMoves.Add(23, new List<Moves> { Moves.Clamp });
                possibleMoves.Add(30, new List<Moves> { Moves.Aurora_Beam });
                possibleMoves.Add(39, new List<Moves> { Moves.Leer });
                possibleMoves.Add(50, new List<Moves> { Moves.Ice_Beam });
                break;


            case PokémonNames.Cloyster:
                possibleMoves.Add(1, new List<Moves> { Moves.Withdraw, Moves.Supersonic, Moves.Clamp, Moves.Aurora_Beam });
                possibleMoves.Add(50, new List<Moves> { Moves.Spike_Cannon });
                break;


            case PokémonNames.Gastly:
                possibleMoves.Add(1, new List<Moves> { Moves.Lick, Moves.Confuse_Ray, Moves.Night_Shade });
                possibleMoves.Add(27, new List<Moves> { Moves.Hypnosis });
                possibleMoves.Add(35, new List<Moves> { Moves.Dream_Eater });
                break;


            case PokémonNames.Haunter:
                possibleMoves.Add(1, new List<Moves> { Moves.Lick, Moves.Confuse_Ray, Moves.Night_Shade });
                possibleMoves.Add(29, new List<Moves> { Moves.Hypnosis });
                possibleMoves.Add(38, new List<Moves> { Moves.Dream_Eater });
                break;


            case PokémonNames.Gengar:
                possibleMoves.Add(1, new List<Moves> { Moves.Lick, Moves.Confuse_Ray, Moves.Night_Shade });
                possibleMoves.Add(29, new List<Moves> { Moves.Hypnosis });
                possibleMoves.Add(38, new List<Moves> { Moves.Dream_Eater });
                break;


            case PokémonNames.Onix:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Screech });
                possibleMoves.Add(15, new List<Moves> { Moves.Bind });
                possibleMoves.Add(19, new List<Moves> { Moves.Rock_Throw });
                possibleMoves.Add(25, new List<Moves> { Moves.Rage });
                possibleMoves.Add(33, new List<Moves> { Moves.Slam });
                possibleMoves.Add(43, new List<Moves> { Moves.Harden });
                break;


            case PokémonNames.Drowzee:
                possibleMoves.Add(1, new List<Moves> { Moves.Pound, Moves.Hypnosis });
                possibleMoves.Add(12, new List<Moves> { Moves.Disable });
                possibleMoves.Add(17, new List<Moves> { Moves.Confusion });
                possibleMoves.Add(24, new List<Moves> { Moves.Headbutt });
                possibleMoves.Add(29, new List<Moves> { Moves.Poison_Gas });
                possibleMoves.Add(32, new List<Moves> { Moves.Psychic });
                possibleMoves.Add(37, new List<Moves> { Moves.Meditate });
                break;


            case PokémonNames.Hypno:
                possibleMoves.Add(1, new List<Moves> { Moves.Pound, Moves.Hypnosis, Moves.Disable, Moves.Confusion });
                possibleMoves.Add(12, new List<Moves> { Moves.Disable });
                possibleMoves.Add(17, new List<Moves> { Moves.Confusion });
                possibleMoves.Add(24, new List<Moves> { Moves.Headbutt });
                possibleMoves.Add(33, new List<Moves> { Moves.Poison_Gas });
                possibleMoves.Add(37, new List<Moves> { Moves.Psychic });
                possibleMoves.Add(43, new List<Moves> { Moves.Meditate });
                break;


            case PokémonNames.Krabby:
                possibleMoves.Add(1, new List<Moves> { Moves.Bubble, Moves.Leer });
                possibleMoves.Add(20, new List<Moves> { Moves.Vise_Grip });
                possibleMoves.Add(25, new List<Moves> { Moves.Guillotine });
                possibleMoves.Add(30, new List<Moves> { Moves.Stomp });
                possibleMoves.Add(35, new List<Moves> { Moves.Crabhammer });
                possibleMoves.Add(40, new List<Moves> { Moves.Harden });
                break;


            case PokémonNames.Kingler:
                possibleMoves.Add(1, new List<Moves> { Moves.Bubble, Moves.Leer, Moves.Vise_Grip });
                possibleMoves.Add(20, new List<Moves> { Moves.Vise_Grip });
                possibleMoves.Add(25, new List<Moves> { Moves.Guillotine });
                possibleMoves.Add(34, new List<Moves> { Moves.Stomp });
                possibleMoves.Add(42, new List<Moves> { Moves.Crabhammer });
                possibleMoves.Add(49, new List<Moves> { Moves.Harden });
                break;


            case PokémonNames.Voltorb:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Screech });
                possibleMoves.Add(17, new List<Moves> { Moves.Sonic_Boom });
                possibleMoves.Add(22, new List<Moves> { Moves.Self_Destruct });
                possibleMoves.Add(29, new List<Moves> { Moves.Light_Screen });
                possibleMoves.Add(36, new List<Moves> { Moves.Swift });
                possibleMoves.Add(43, new List<Moves> { Moves.Explosion });
                break;


            case PokémonNames.Electrode:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Screech, Moves.Sonic_Boom });
                possibleMoves.Add(17, new List<Moves> { Moves.Sonic_Boom });
                possibleMoves.Add(22, new List<Moves> { Moves.Self_Destruct });
                possibleMoves.Add(29, new List<Moves> { Moves.Light_Screen });
                possibleMoves.Add(40, new List<Moves> { Moves.Swift });
                possibleMoves.Add(50, new List<Moves> { Moves.Explosion });
                break;


            case PokémonNames.Exeggcute:
                possibleMoves.Add(1, new List<Moves> { Moves.Barrage, Moves.Hypnosis });
                possibleMoves.Add(25, new List<Moves> { Moves.Reflect });
                possibleMoves.Add(28, new List<Moves> { Moves.Leech_Seed });
                possibleMoves.Add(32, new List<Moves> { Moves.Stun_Spore });
                possibleMoves.Add(37, new List<Moves> { Moves.Poison_Powder });
                possibleMoves.Add(42, new List<Moves> { Moves.Solar_Beam });
                possibleMoves.Add(48, new List<Moves> { Moves.Sleep_Powder });
                break;


            case PokémonNames.Exeggutor:
                possibleMoves.Add(1, new List<Moves> { Moves.Barrage, Moves.Hypnosis });
                possibleMoves.Add(28, new List<Moves> { Moves.Stomp });
                break;


            case PokémonNames.Cubone:
                possibleMoves.Add(1, new List<Moves> { Moves.Growl,  Moves.Bone_Club });
                possibleMoves.Add(25, new List<Moves> { Moves.Leer });
                possibleMoves.Add(31, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(38, new List<Moves> { Moves.Thrash });
                possibleMoves.Add(43, new List<Moves> { Moves.Bonemerang });
                possibleMoves.Add(46, new List<Moves> { Moves.Rage });
                break;


            case PokémonNames.Marowak:
                possibleMoves.Add(1, new List<Moves> { Moves.Focus_Energy, Moves.Leer, Moves.Growl, Moves.Bone_Club });
                possibleMoves.Add(25, new List<Moves> { Moves.Leer });
                possibleMoves.Add(33, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(41, new List<Moves> { Moves.Thrash });
                possibleMoves.Add(48, new List<Moves> { Moves.Bonemerang });
                possibleMoves.Add(55, new List<Moves> { Moves.Rage });
                break;


            case PokémonNames.Hitmonlee:
                possibleMoves.Add(1, new List<Moves> { Moves.Double_Kick, Moves.Meditate });
                possibleMoves.Add(33, new List<Moves> { Moves.Rolling_Kick });
                possibleMoves.Add(38, new List<Moves> { Moves.Jump_Kick });
                possibleMoves.Add(43, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(48, new List<Moves> { Moves.High_Jump_Kick });
                possibleMoves.Add(53, new List<Moves> { Moves.Mega_Kick });
                break;


            case PokémonNames.Hitmonchan:
                possibleMoves.Add(1, new List<Moves> { Moves.Comet_Punch, Moves.Agility });
                possibleMoves.Add(33, new List<Moves> { Moves.Fire_Punch });
                possibleMoves.Add(38, new List<Moves> { Moves.Ice_Punch });
                possibleMoves.Add(43, new List<Moves> { Moves.Thunder_Punch });
                possibleMoves.Add(48, new List<Moves> { Moves.Mega_Punch });
                possibleMoves.Add(53, new List<Moves> { Moves.Counter });
                break;


            case PokémonNames.Lickitung:
                possibleMoves.Add(1, new List<Moves> { Moves.Wrap, Moves.Supersonic });
                possibleMoves.Add(7, new List<Moves> { Moves.Stomp });
                possibleMoves.Add(15, new List<Moves> { Moves.Disable });
                possibleMoves.Add(23, new List<Moves> { Moves.Defense_Curl });
                possibleMoves.Add(31, new List<Moves> { Moves.Slam });
                possibleMoves.Add(39, new List<Moves> { Moves.Screech });
                break;


            case PokémonNames.Koffing:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Smog });
                possibleMoves.Add(32, new List<Moves> { Moves.Sludge });
                possibleMoves.Add(37, new List<Moves> { Moves.Smokescreen });
                possibleMoves.Add(40, new List<Moves> { Moves.Self_Destruct });
                possibleMoves.Add(45, new List<Moves> { Moves.Haze });
                possibleMoves.Add(48, new List<Moves> { Moves.Explosion });
                break;


            case PokémonNames.Weezing:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Smog, Moves.Sludge });
                possibleMoves.Add(32, new List<Moves> { Moves.Sludge });
                possibleMoves.Add(39, new List<Moves> { Moves.Smokescreen });
                possibleMoves.Add(43, new List<Moves> { Moves.Self_Destruct });
                possibleMoves.Add(49, new List<Moves> { Moves.Haze });
                possibleMoves.Add(53, new List<Moves> { Moves.Explosion });
                break;


            case PokémonNames.Rhyhorn:
                possibleMoves.Add(1, new List<Moves> { Moves.Horn_Attack });
                possibleMoves.Add(30, new List<Moves> { Moves.Stomp });
                possibleMoves.Add(35, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(40, new List<Moves> { Moves.Fury_Attack });
                possibleMoves.Add(45, new List<Moves> { Moves.Horn_Drill });
                possibleMoves.Add(50, new List<Moves> { Moves.Leer });
                possibleMoves.Add(55, new List<Moves> { Moves.Take_Down });
                break;


            case PokémonNames.Rhydon:
                possibleMoves.Add(1, new List<Moves> { Moves.Horn_Attack, Moves.Stomp, Moves.Tail_Whip, Moves.Fury_Attack });
                possibleMoves.Add(30, new List<Moves> { Moves.Stomp });
                possibleMoves.Add(35, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(40, new List<Moves> { Moves.Fury_Attack });
                possibleMoves.Add(48, new List<Moves> { Moves.Horn_Drill });
                possibleMoves.Add(55, new List<Moves> { Moves.Leer });
                possibleMoves.Add(64, new List<Moves> { Moves.Take_Down });
                break;


            case PokémonNames.Chansey:
                possibleMoves.Add(1, new List<Moves> { Moves.Pound, Moves.Double_Slap });
                possibleMoves.Add(24, new List<Moves> { Moves.Sing });
                possibleMoves.Add(30, new List<Moves> { Moves.Growl });
                possibleMoves.Add(38, new List<Moves> { Moves.Minimize });
                possibleMoves.Add(44, new List<Moves> { Moves.Defense_Curl });
                possibleMoves.Add(48, new List<Moves> { Moves.Light_Screen });
                possibleMoves.Add(54, new List<Moves> { Moves.Double_Edge });
                break;


            case PokémonNames.Tangela:
                possibleMoves.Add(1, new List<Moves> { Moves.Constrict, Moves.Bind });
                possibleMoves.Add(29, new List<Moves> { Moves.Absorb });
                possibleMoves.Add(32, new List<Moves> { Moves.Poison_Powder });
                possibleMoves.Add(36, new List<Moves> { Moves.Stun_Spore });
                possibleMoves.Add(39, new List<Moves> { Moves.Sleep_Powder });
                possibleMoves.Add(45, new List<Moves> { Moves.Slam });
                possibleMoves.Add(49, new List<Moves> { Moves.Growth });
                break;


            case PokémonNames.Kangaskhan:
                possibleMoves.Add(1, new List<Moves> { Moves.Comet_Punch, Moves.Rage });
                possibleMoves.Add(26, new List<Moves> { Moves.Bite });
                possibleMoves.Add(31, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(36, new List<Moves> { Moves.Mega_Punch });
                possibleMoves.Add(41, new List<Moves> { Moves.Leer });
                possibleMoves.Add(46, new List<Moves> { Moves.Dizzy_Punch });
                break;


            case PokémonNames.Horsea:
                possibleMoves.Add(1, new List<Moves> { Moves.Bubble });
                possibleMoves.Add(19, new List<Moves> { Moves.Smokescreen });
                possibleMoves.Add(24, new List<Moves> { Moves.Leer });
                possibleMoves.Add(30, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(37, new List<Moves> { Moves.Agility });
                possibleMoves.Add(45, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Seadra:
                possibleMoves.Add(1, new List<Moves> { Moves.Bubble, Moves.Smokescreen });
                possibleMoves.Add(19, new List<Moves> { Moves.Smokescreen });
                possibleMoves.Add(24, new List<Moves> { Moves.Leer });
                possibleMoves.Add(30, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(41, new List<Moves> { Moves.Agility });
                possibleMoves.Add(52, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Goldeen:
                possibleMoves.Add(1, new List<Moves> { Moves.Peck, Moves.Tail_Whip });
                possibleMoves.Add(19, new List<Moves> { Moves.Supersonic });
                possibleMoves.Add(24, new List<Moves> { Moves.Horn_Attack });
                possibleMoves.Add(30, new List<Moves> { Moves.Fury_Attack });
                possibleMoves.Add(37, new List<Moves> { Moves.Waterfall });
                possibleMoves.Add(45, new List<Moves> { Moves.Horn_Drill });
                possibleMoves.Add(54, new List<Moves> { Moves.Agility });
                break;


            case PokémonNames.Seaking:
                possibleMoves.Add(1, new List<Moves> { Moves.Peck, Moves.Tail_Whip, Moves.Supersonic });
                possibleMoves.Add(19, new List<Moves> { Moves.Supersonic });
                possibleMoves.Add(24, new List<Moves> { Moves.Horn_Attack });
                possibleMoves.Add(30, new List<Moves> { Moves.Fury_Attack });
                possibleMoves.Add(39, new List<Moves> { Moves.Waterfall });
                possibleMoves.Add(48, new List<Moves> { Moves.Horn_Drill });
                possibleMoves.Add(54, new List<Moves> { Moves.Agility });
                break;


            case PokémonNames.Staryu:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle });
                possibleMoves.Add(17, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(22, new List<Moves> { Moves.Harden });
                possibleMoves.Add(27, new List<Moves> { Moves.Recover });
                possibleMoves.Add(32, new List<Moves> { Moves.Swift });
                possibleMoves.Add(37, new List<Moves> { Moves.Minimize });
                possibleMoves.Add(42, new List<Moves> { Moves.Light_Screen });
                possibleMoves.Add(47, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Starmie:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Water_Gun, Moves.Harden });
                break;


            case PokémonNames.Mr_Mime:
                possibleMoves.Add(1, new List<Moves> { Moves.Confusion, Moves.Barrier });
                possibleMoves.Add(15, new List<Moves> { Moves.Confusion });
                possibleMoves.Add(23, new List<Moves> { Moves.Light_Screen });
                possibleMoves.Add(31, new List<Moves> { Moves.Double_Slap });
                possibleMoves.Add(39, new List<Moves> { Moves.Meditate });
                possibleMoves.Add(47, new List<Moves> { Moves.Substitute });
                break;


            case PokémonNames.Scyther:
                possibleMoves.Add(1, new List<Moves> { Moves.Quick_Attack });
                possibleMoves.Add(17, new List<Moves> { Moves.Leer });
                possibleMoves.Add(20, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(24, new List<Moves> { Moves.Double_Team });
                possibleMoves.Add(29, new List<Moves> { Moves.Slash });
                possibleMoves.Add(35, new List<Moves> { Moves.Swords_Dance });
                possibleMoves.Add(42, new List<Moves> { Moves.Agility });
                break;


            case PokémonNames.Jynx:
                possibleMoves.Add(1, new List<Moves> { Moves.Pound, Moves.Lovely_Kiss });
                possibleMoves.Add(18, new List<Moves> { Moves.Lick });
                possibleMoves.Add(23, new List<Moves> { Moves.Double_Slap });
                possibleMoves.Add(31, new List<Moves> { Moves.Ice_Punch });
                possibleMoves.Add(39, new List<Moves> { Moves.Body_Slam });
                possibleMoves.Add(47, new List<Moves> { Moves.Thrash });
                possibleMoves.Add(58, new List<Moves> { Moves.Blizzard });
                break;


            case PokémonNames.Electabuzz:
                possibleMoves.Add(1, new List<Moves> { Moves.Quick_Attack, Moves.Leer });
                possibleMoves.Add(34, new List<Moves> { Moves.Thunder_Shock });
                possibleMoves.Add(37, new List<Moves> { Moves.Screech });
                possibleMoves.Add(42, new List<Moves> { Moves.Thunder_Punch });
                possibleMoves.Add(49, new List<Moves> { Moves.Light_Screen });
                possibleMoves.Add(54, new List<Moves> { Moves.Thunder });
                break;


            case PokémonNames.Magmar:
                possibleMoves.Add(1, new List<Moves> { Moves.Ember });
                possibleMoves.Add(36, new List<Moves> { Moves.Leer });
                possibleMoves.Add(39, new List<Moves> { Moves.Confuse_Ray });
                possibleMoves.Add(43, new List<Moves> { Moves.Fire_Punch });
                possibleMoves.Add(48, new List<Moves> { Moves.Smokescreen });
                possibleMoves.Add(52, new List<Moves> { Moves.Smog });
                possibleMoves.Add(55, new List<Moves> { Moves.Flamethrower });
                break;


            case PokémonNames.Pinsir:
                possibleMoves.Add(1, new List<Moves> { Moves.Vise_Grip });
                possibleMoves.Add(25, new List<Moves> { Moves.Seismic_Toss });
                possibleMoves.Add(30, new List<Moves> { Moves.Guillotine });
                possibleMoves.Add(36, new List<Moves> { Moves.Focus_Energy });
                possibleMoves.Add(43, new List<Moves> { Moves.Harden });
                possibleMoves.Add(49, new List<Moves> { Moves.Slash });
                possibleMoves.Add(54, new List<Moves> { Moves.Swords_Dance });
                break;


            case PokémonNames.Tauros:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle });
                possibleMoves.Add(21, new List<Moves> { Moves.Stomp });
                possibleMoves.Add(28, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(35, new List<Moves> { Moves.Leer });
                possibleMoves.Add(44, new List<Moves> { Moves.Rage });
                possibleMoves.Add(51, new List<Moves> { Moves.Take_Down });
                break;


            case PokémonNames.Magikarp:
                possibleMoves.Add(1, new List<Moves> { Moves.Splash });
                possibleMoves.Add(15, new List<Moves> { Moves.Tackle });
                break;


            case PokémonNames.Gyarados:
                possibleMoves.Add(1, new List<Moves> { Moves.Bite, Moves.Dragon_Rage, Moves.Leer, Moves.Hydro_Pump });
                possibleMoves.Add(20, new List<Moves> { Moves.Bite });
                possibleMoves.Add(25, new List<Moves> { Moves.Dragon_Rage });
                possibleMoves.Add(32, new List<Moves> { Moves.Leer });
                possibleMoves.Add(41, new List<Moves> { Moves.Hydro_Pump });
                possibleMoves.Add(52, new List<Moves> { Moves.Hyper_Beam });
                break;


            case PokémonNames.Lapras:
                possibleMoves.Add(1, new List<Moves> { Moves.Water_Gun, Moves.Growl });
                possibleMoves.Add(16, new List<Moves> { Moves.Sing });
                possibleMoves.Add(20, new List<Moves> { Moves.Mist });
                possibleMoves.Add(25, new List<Moves> { Moves.Body_Slam });
                possibleMoves.Add(31, new List<Moves> { Moves.Confuse_Ray });
                possibleMoves.Add(38, new List<Moves> { Moves.Ice_Beam });
                possibleMoves.Add(46, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Ditto:
                possibleMoves.Add(1, new List<Moves> { Moves.Transform });
                break;


            case PokémonNames.Eevee:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Sand_Attack });
                possibleMoves.Add(31, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(27, new List<Moves> { Moves.Quick_Attack });
                possibleMoves.Add(37, new List<Moves> { Moves.Bite });
                possibleMoves.Add(45, new List<Moves> { Moves.Take_Down });
                break;


            case PokémonNames.Vaporeon:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Quick_Attack, Moves.Water_Gun, Moves.Sand_Attack });
                possibleMoves.Add(37, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(31, new List<Moves> { Moves.Water_Gun });
                possibleMoves.Add(27, new List<Moves> { Moves.Quick_Attack });
                possibleMoves.Add(40, new List<Moves> { Moves.Bite });
                possibleMoves.Add(44, new List<Moves> { Moves.Haze });
                possibleMoves.Add(42, new List<Moves> { Moves.Acid_Armor });
                possibleMoves.Add(54, new List<Moves> { Moves.Hydro_Pump });
                possibleMoves.Add(48, new List<Moves> { Moves.Mist });
                break;


            case PokémonNames.Jolteon:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Quick_Attack, Moves.Thunder_Shock, Moves.Sand_Attack });
                possibleMoves.Add(37, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(31, new List<Moves> { Moves.Thunder_Shock });
                possibleMoves.Add(27, new List<Moves> { Moves.Quick_Attack });
                possibleMoves.Add(42, new List<Moves> { Moves.Double_Kick });
                possibleMoves.Add(48, new List<Moves> { Moves.Pin_Missile });
                possibleMoves.Add(40, new List<Moves> { Moves.Thunder_Wave });
                possibleMoves.Add(44, new List<Moves> { Moves.Agility });
                possibleMoves.Add(54, new List<Moves> { Moves.Thunder });
                break;


            case PokémonNames.Flareon:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Quick_Attack, Moves.Ember, Moves.Sand_Attack });
                possibleMoves.Add(37, new List<Moves> { Moves.Tail_Whip });
                possibleMoves.Add(31, new List<Moves> { Moves.Ember });
                possibleMoves.Add(27, new List<Moves> { Moves.Quick_Attack });
                possibleMoves.Add(40, new List<Moves> { Moves.Bite });
                possibleMoves.Add(44, new List<Moves> { Moves.Fire_Spin });
                possibleMoves.Add(42, new List<Moves> { Moves.Leer });
                possibleMoves.Add(48, new List<Moves> { Moves.Rage });
                possibleMoves.Add(54, new List<Moves> { Moves.Flamethrower });
                break;


            case PokémonNames.Porygon:
                possibleMoves.Add(1, new List<Moves> { Moves.Tackle, Moves.Sharpen, Moves.Conversion });
                possibleMoves.Add(23, new List<Moves> { Moves.Psybeam });
                possibleMoves.Add(28, new List<Moves> { Moves.Recover });
                possibleMoves.Add(35, new List<Moves> { Moves.Agility });
                possibleMoves.Add(42, new List<Moves> { Moves.Tri_Attack });
                break;


            case PokémonNames.Omanyte:
                possibleMoves.Add(1, new List<Moves> { Moves.Water_Gun, Moves.Withdraw });
                possibleMoves.Add(34, new List<Moves> { Moves.Horn_Attack });
                possibleMoves.Add(39, new List<Moves> { Moves.Leer });
                possibleMoves.Add(46, new List<Moves> { Moves.Spike_Cannon });
                possibleMoves.Add(53, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Omastar:
                possibleMoves.Add(1, new List<Moves> { Moves.Water_Gun, Moves.Withdraw, Moves.Horn_Attack });
                possibleMoves.Add(34, new List<Moves> { Moves.Horn_Attack });
                possibleMoves.Add(39, new List<Moves> { Moves.Leer });
                possibleMoves.Add(44, new List<Moves> { Moves.Spike_Cannon });
                possibleMoves.Add(49, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Kabuto:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Harden });
                possibleMoves.Add(34, new List<Moves> { Moves.Absorb });
                possibleMoves.Add(39, new List<Moves> { Moves.Slash });
                possibleMoves.Add(44, new List<Moves> { Moves.Leer });
                possibleMoves.Add(49, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Kabutops:
                possibleMoves.Add(1, new List<Moves> { Moves.Scratch, Moves.Harden, Moves.Absorb });
                possibleMoves.Add(34, new List<Moves> { Moves.Absorb });
                possibleMoves.Add(39, new List<Moves> { Moves.Slash });
                possibleMoves.Add(46, new List<Moves> { Moves.Leer });
                possibleMoves.Add(53, new List<Moves> { Moves.Hydro_Pump });
                break;


            case PokémonNames.Aerodactyl:
                possibleMoves.Add(1, new List<Moves> { Moves.Wing_Attack, Moves.Agility });
                possibleMoves.Add(33, new List<Moves> { Moves.Supersonic });
                possibleMoves.Add(38, new List<Moves> { Moves.Bite });
                possibleMoves.Add(45, new List<Moves> { Moves.Take_Down });
                possibleMoves.Add(54, new List<Moves> { Moves.Hyper_Beam });
                break;


            case PokémonNames.Snorlax:
                possibleMoves.Add(1, new List<Moves> { Moves.Headbutt, Moves.Amnesia, Moves.Rest });
                possibleMoves.Add(35, new List<Moves> { Moves.Body_Slam });
                possibleMoves.Add(41, new List<Moves> { Moves.Harden });
                possibleMoves.Add(48, new List<Moves> { Moves.Double_Edge });
                possibleMoves.Add(56, new List<Moves> { Moves.Hyper_Beam });
                break;


            case PokémonNames.Articuno:
                possibleMoves.Add(1, new List<Moves> { Moves.Peck, Moves.Ice_Beam });
                possibleMoves.Add(51, new List<Moves> { Moves.Blizzard });
                possibleMoves.Add(55, new List<Moves> { Moves.Agility });
                possibleMoves.Add(60, new List<Moves> { Moves.Mist });
                break;


            case PokémonNames.Zapdos:
                possibleMoves.Add(1, new List<Moves> { Moves.Thunder_Shock, Moves.Drill_Peck });
                possibleMoves.Add(51, new List<Moves> { Moves.Thunder });
                possibleMoves.Add(55, new List<Moves> { Moves.Agility });
                possibleMoves.Add(60, new List<Moves> { Moves.Light_Screen });
                break;


            case PokémonNames.Moltres:
                possibleMoves.Add(1, new List<Moves> { Moves.Peck, Moves.Peck });
                possibleMoves.Add(51, new List<Moves> { Moves.Leer });
                possibleMoves.Add(55, new List<Moves> { Moves.Agility });
                possibleMoves.Add(60, new List<Moves> { Moves.Sky_Attack });
                break;


            case PokémonNames.Dratini:
                possibleMoves.Add(1, new List<Moves> { Moves.Wrap, Moves.Leer });
                possibleMoves.Add(10, new List<Moves> { Moves.Thunder_Wave });
                possibleMoves.Add(20, new List<Moves> { Moves.Agility });
                possibleMoves.Add(30, new List<Moves> { Moves.Slam });
                possibleMoves.Add(40, new List<Moves> { Moves.Dragon_Rage });
                possibleMoves.Add(50, new List<Moves> { Moves.Hyper_Beam });
                break;


            case PokémonNames.Dragonair:
                possibleMoves.Add(1, new List<Moves> { Moves.Wrap, Moves.Leer, Moves.Thunder_Wave });
                possibleMoves.Add(10, new List<Moves> { Moves.Thunder_Wave });
                possibleMoves.Add(20, new List<Moves> { Moves.Agility });
                possibleMoves.Add(35, new List<Moves> { Moves.Slam });
                possibleMoves.Add(45, new List<Moves> { Moves.Dragon_Rage });
                possibleMoves.Add(55, new List<Moves> { Moves.Hyper_Beam });
                break;


            case PokémonNames.Dragonite:
                possibleMoves.Add(1, new List<Moves> { Moves.Wrap, Moves.Leer, Moves.Thunder_Wave, Moves.Agility });
                possibleMoves.Add(10, new List<Moves> { Moves.Thunder_Wave });
                possibleMoves.Add(20, new List<Moves> { Moves.Agility });
                possibleMoves.Add(35, new List<Moves> { Moves.Slam });
                possibleMoves.Add(45, new List<Moves> { Moves.Dragon_Rage });
                possibleMoves.Add(60, new List<Moves> { Moves.Hyper_Beam });
                break;


            case PokémonNames.Mewtwo:
                possibleMoves.Add(1, new List<Moves> { Moves.Confusion, Moves.Disable, Moves.Swift, Moves.Psychic });
                possibleMoves.Add(63, new List<Moves> { Moves.Barrier });
                possibleMoves.Add(66, new List<Moves> { Moves.Psychic });
                possibleMoves.Add(70, new List<Moves> { Moves.Recover });
                possibleMoves.Add(75, new List<Moves> { Moves.Mist });
                possibleMoves.Add(81, new List<Moves> { Moves.Amnesia });
                break;


            case PokémonNames.Mew:
                possibleMoves.Add(1, new List<Moves> { Moves.Pound });
                possibleMoves.Add(10, new List<Moves> { Moves.Transform });
                possibleMoves.Add(20, new List<Moves> { Moves.Mega_Punch });
                possibleMoves.Add(30, new List<Moves> { Moves.Metronome });
                possibleMoves.Add(40, new List<Moves> { Moves.Psychic });
                break;
            }
            return possibleMoves;
        }

        public int GetExperienceYield()
        {
            int experienceYield = 0;
            switch (name)
            {
                case PokémonNames.Bulbasaur:
                    experienceYield = 64;
                    break;

                case PokémonNames.Ivysaur:
                    experienceYield = 141;
                    break;

                case PokémonNames.Venusaur:
                    experienceYield = 208;
                    break;

                case PokémonNames.Charmander:
                    experienceYield = 65;
                    break;

                case PokémonNames.Charmeleon:
                    experienceYield = 142;
                    break;

                case PokémonNames.Charizard:
                    experienceYield = 209;
                    break;

                case PokémonNames.Squirtle:
                    experienceYield = 66;
                    break;

                case PokémonNames.Wartortle:
                    experienceYield = 143;
                    break;

                case PokémonNames.Blastoise:
                    experienceYield = 210;
                    break;

                case PokémonNames.Caterpie:
                    experienceYield = 53;
                    break;

                case PokémonNames.Metapod:
                    experienceYield = 72;
                    break;

                case PokémonNames.Butterfree:
                    experienceYield = 160;
                    break;

                case PokémonNames.Weedle:
                    experienceYield = 52;
                    break;

                case PokémonNames.Kakuna:
                    experienceYield = 71;
                    break;

                case PokémonNames.Beedrill:
                    experienceYield = 159;
                    break;

                case PokémonNames.Pidgey:
                    experienceYield = 55;
                    break;

                case PokémonNames.Pidgeotto:
                    experienceYield = 113;
                    break;

                case PokémonNames.Pidgeot:
                    experienceYield = 172;
                    break;

                case PokémonNames.Rattata:
                    experienceYield = 57;
                    break;

                case PokémonNames.Raticate:
                    experienceYield = 116;
                    break;

                case PokémonNames.Spearow:
                    experienceYield = 58;
                    break;

                case PokémonNames.Fearow:
                    experienceYield = 162;
                    break;

                case PokémonNames.Ekans:
                    experienceYield = 62;
                    break;

                case PokémonNames.Arbok:
                    experienceYield = 147;
                    break;

                case PokémonNames.Pikachu:
                    experienceYield = 82;
                    break;

                case PokémonNames.Raichu:
                    experienceYield = 122;
                    break;

                case PokémonNames.Sandshrew:
                    experienceYield = 93;
                    break;

                case PokémonNames.Sandslash:
                    experienceYield = 163;
                    break;

                case PokémonNames.Nidoran_Female:
                    experienceYield = 59;
                    break;

                case PokémonNames.Nidorina:
                    experienceYield = 117;
                    break;

                case PokémonNames.Nidoqueen:
                    experienceYield = 194;
                    break;

                case PokémonNames.Nidoran_Male:
                    experienceYield = 60;
                    break;

                case PokémonNames.Nidorino:
                    experienceYield = 118;
                    break;

                case PokémonNames.Nidoking:
                    experienceYield = 195;
                    break;

                case PokémonNames.Clefairy:
                    experienceYield = 68;
                    break;

                case PokémonNames.Clefable:
                    experienceYield = 129;
                    break;

                case PokémonNames.Vulpix:
                    experienceYield = 63;
                    break;

                case PokémonNames.Ninetales:
                    experienceYield = 178;
                    break;

                case PokémonNames.Jigglypuff:
                    experienceYield = 76;
                    break;

                case PokémonNames.Wigglytuff:
                    experienceYield = 109;
                    break;

                case PokémonNames.Zubat:
                    experienceYield = 54;
                    break;

                case PokémonNames.Golbat:
                    experienceYield = 171;
                    break;

                case PokémonNames.Oddish:
                    experienceYield = 78;
                    break;

                case PokémonNames.Gloom:
                    experienceYield = 132;
                    break;

                case PokémonNames.Vileplume:
                    experienceYield = 184;
                    break;

                case PokémonNames.Paras:
                    experienceYield = 70;
                    break;

                case PokémonNames.Parasect:
                    experienceYield = 128;
                    break;

                case PokémonNames.Venonat:
                    experienceYield = 75;
                    break;

                case PokémonNames.Venomoth:
                    experienceYield = 138;
                    break;

                case PokémonNames.Diglett:
                    experienceYield = 81;
                    break;

                case PokémonNames.Dugtrio:
                    experienceYield = 153;
                    break;

                case PokémonNames.Meowth:
                    experienceYield = 69;
                    break;

                case PokémonNames.Persian:
                    experienceYield = 148;
                    break;

                case PokémonNames.Psyduck:
                    experienceYield = 80;
                    break;

                case PokémonNames.Golduck:
                    experienceYield = 174;
                    break;

                case PokémonNames.Mankey:
                    experienceYield = 74;
                    break;

                case PokémonNames.Primeape:
                    experienceYield = 149;
                    break;

                case PokémonNames.Growlithe:
                    experienceYield = 91;
                    break;

                case PokémonNames.Arcanine:
                    experienceYield = 213;
                    break;

                case PokémonNames.Poliwag:
                    experienceYield = 77;
                    break;

                case PokémonNames.Poliwhirl:
                    experienceYield = 131;
                    break;

                case PokémonNames.Poliwrath:
                    experienceYield = 185;
                    break;

                case PokémonNames.Abra:
                    experienceYield = 73;
                    break;

                case PokémonNames.Kadabra:
                    experienceYield = 145;
                    break;

                case PokémonNames.Alakazam:
                    experienceYield = 186;
                    break;

                case PokémonNames.Machop:
                    experienceYield = 88;
                    break;

                case PokémonNames.Machoke:
                    experienceYield = 146;
                    break;

                case PokémonNames.Machamp:
                    experienceYield = 193;
                    break;

                case PokémonNames.Bellsprout:
                    experienceYield = 84;
                    break;

                case PokémonNames.Weepinbell:
                    experienceYield = 151;
                    break;

                case PokémonNames.Victreebel:
                    experienceYield = 191;
                    break;

                case PokémonNames.Tentacool:
                    experienceYield = 105;
                    break;

                case PokémonNames.Tentacruel:
                    experienceYield = 205;
                    break;

                case PokémonNames.Geodude:
                    experienceYield = 86;
                    break;

                case PokémonNames.Graveler:
                    experienceYield = 134;
                    break;

                case PokémonNames.Golem:
                    experienceYield = 177;
                    break;

                case PokémonNames.Ponyta:
                    experienceYield = 152;
                    break;

                case PokémonNames.Rapidash:
                    experienceYield = 192;
                    break;

                case PokémonNames.Slowpoke:
                    experienceYield = 99;
                    break;

                case PokémonNames.Slowbro:
                    experienceYield = 164;
                    break;

                case PokémonNames.Magnemite:
                    experienceYield = 89;
                    break;

                case PokémonNames.Magneton:
                    experienceYield = 161;
                    break;

                case PokémonNames.Farfetchd:
        experienceYield = 94;
                    break;

                case PokémonNames.Doduo:
                    experienceYield = 96;
                    break;

                case PokémonNames.Dodrio:
                    experienceYield = 158;
                    break;

                case PokémonNames.Seel:
                    experienceYield = 100;
                    break;

                case PokémonNames.Dewgong:
                    experienceYield = 176;
                    break;

                case PokémonNames.Grimer:
                    experienceYield = 90;
                    break;

                case PokémonNames.Muk:
                    experienceYield = 157;
                    break;

                case PokémonNames.Shellder:
                    experienceYield = 97;
                    break;

                case PokémonNames.Cloyster:
                    experienceYield = 203;
                    break;

                case PokémonNames.Gastly:
                    experienceYield = 95;
                    break;

                case PokémonNames.Haunter:
                    experienceYield = 126;
                    break;

                case PokémonNames.Gengar:
                    experienceYield = 190;
                    break;

                case PokémonNames.Onix:
                    experienceYield = 108;
                    break;

                case PokémonNames.Drowzee:
                    experienceYield = 102;
                    break;

                case PokémonNames.Hypno:
                    experienceYield = 165;
                    break;

                case PokémonNames.Krabby:
                    experienceYield = 115;
                    break;

                case PokémonNames.Kingler:
                    experienceYield = 206;
                    break;

                case PokémonNames.Voltorb:
                    experienceYield = 103;
                    break;

                case PokémonNames.Electrode:
                    experienceYield = 150;
                    break;

                case PokémonNames.Exeggcute:
                    experienceYield = 98;
                    break;

                case PokémonNames.Exeggutor:
                    experienceYield = 212;
                    break;

                case PokémonNames.Cubone:
                    experienceYield = 87;
                    break;

                case PokémonNames.Marowak:
                    experienceYield = 124;
                    break;

                case PokémonNames.Hitmonlee:
                    experienceYield = 139;
                    break;

                case PokémonNames.Hitmonchan:
                    experienceYield = 140;
                    break;

                case PokémonNames.Lickitung:
                    experienceYield = 127;
                    break;

                case PokémonNames.Koffing:
                    experienceYield = 114;
                    break;

                case PokémonNames.Weezing:
                    experienceYield = 173;
                    break;

                case PokémonNames.Rhyhorn:
                    experienceYield = 135;
                    break;

                case PokémonNames.Rhydon:
                    experienceYield = 204;
                    break;

                case PokémonNames.Chansey:
                    experienceYield = 255;
                    break;

                case PokémonNames.Tangela:
                    experienceYield = 166;
                    break;

                case PokémonNames.Kangaskhan:
                    experienceYield = 175;
                    break;

                case PokémonNames.Horsea:
                    experienceYield = 83;
                    break;

                case PokémonNames.Seadra:
                    experienceYield = 155;
                    break;

                case PokémonNames.Goldeen:
                    experienceYield = 111;
                    break;

                case PokémonNames.Seaking:
                    experienceYield = 170;
                    break;

                case PokémonNames.Staryu:
                    experienceYield = 106;
                    break;

                case PokémonNames.Starmie:
                    experienceYield = 207;
                    break;

                case PokémonNames.Mr_Mime:
                    experienceYield = 136;
                    break;

                case PokémonNames.Scyther:
                    experienceYield = 187;
                    break;

                case PokémonNames.Jynx:
                    experienceYield = 137;
                    break;

                case PokémonNames.Electabuzz:
                    experienceYield = 156;
                    break;

                case PokémonNames.Magmar:
                    experienceYield = 167;
                    break;

                case PokémonNames.Pinsir:
                    experienceYield = 200;
                    break;

                case PokémonNames.Tauros:
                    experienceYield = 211;
                    break;

                case PokémonNames.Magikarp:
                    experienceYield = 20;
                    break;

                case PokémonNames.Gyarados:
                    experienceYield = 214;
                    break;

                case PokémonNames.Lapras:
                    experienceYield = 219;
                    break;

                case PokémonNames.Ditto:
                    experienceYield = 61;
                    break;

                case PokémonNames.Eevee:
                    experienceYield = 92;
                    break;

                case PokémonNames.Vaporeon:
                    experienceYield = 196;
                    break;

                case PokémonNames.Jolteon:
                    experienceYield = 197;
                    break;

                case PokémonNames.Flareon:
                    experienceYield = 198;
                    break;

                case PokémonNames.Porygon:
                    experienceYield = 130;
                    break;

                case PokémonNames.Omanyte:
                    experienceYield = 120;
                    break;

                case PokémonNames.Omastar:
                    experienceYield = 199;
                    break;

                case PokémonNames.Kabuto:
                    experienceYield = 119;
                    break;

                case PokémonNames.Kabutops:
                    experienceYield = 201;
                    break;

                case PokémonNames.Aerodactyl:
                    experienceYield = 202;
                    break;

                case PokémonNames.Snorlax:
                    experienceYield = 154;
                    break;

                case PokémonNames.Articuno:
                    experienceYield = 215;
                    break;

                case PokémonNames.Zapdos:
                    experienceYield = 216;
                    break;

                case PokémonNames.Moltres:
                    experienceYield = 217;
                    break;

                case PokémonNames.Dratini:
                    experienceYield = 67;
                    break;

                case PokémonNames.Dragonair:
                    experienceYield = 144;
                    break;

                case PokémonNames.Dragonite:
                    experienceYield = 218;
                    break;

                case PokémonNames.Mewtwo:
                    experienceYield = 220;
                    break;

                case PokémonNames.Mew:
                    experienceYield = 64;
                    break;
            }
            return experienceYield;
        }

        public ExperienceGroup GetExperienceGroup()
        {
            ExperienceGroup experienceGroup = new();
            switch (name)
            {
                case PokémonNames.Bulbasaur:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Ivysaur:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Venusaur:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Charmander:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Charmeleon:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Charizard:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Squirtle:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Wartortle:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Blastoise:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Caterpie:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Metapod:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Butterfree:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Weedle:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Kakuna:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Beedrill:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Pidgey:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Pidgeotto:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Pidgeot:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Rattata:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Raticate:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Spearow:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Fearow:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Ekans:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Arbok:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Pikachu:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Raichu:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Sandshrew:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Sandslash:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Nidoran_Female:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Nidorina:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Nidoqueen:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Nidoran_Male
:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Nidorino:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Nidoking:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Clefairy:
                    experienceGroup = ExperienceGroup.Fast;
                    break;
                case PokémonNames.Clefable:
                    experienceGroup = ExperienceGroup.Fast;
                    break;
                case PokémonNames.Vulpix:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Ninetales:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Jigglypuff:
                    experienceGroup = ExperienceGroup.Fast;
                    break;
                case PokémonNames.Wigglytuff:
                    experienceGroup = ExperienceGroup.Fast;
                    break;
                case PokémonNames.Zubat:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Golbat:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Oddish:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Gloom:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Vileplume:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Paras:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Parasect:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Venonat:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Venomoth:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Diglett:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Dugtrio:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Meowth:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Persian:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Psyduck:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Golduck:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Mankey:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Primeape:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Growlithe:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Arcanine:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Poliwag:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Poliwhirl:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Poliwrath:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Abra:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Kadabra:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Alakazam:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Machop:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Machoke:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Machamp:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Bellsprout:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Weepinbell:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Victreebel:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Tentacool:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Tentacruel:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Geodude:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Graveler:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Golem:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Ponyta:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Rapidash:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Slowpoke:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Slowbro:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Magnemite:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Magneton:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Farfetchd:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Doduo:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Dodrio:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Seel:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Dewgong:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Grimer:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Muk:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Shellder:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Cloyster:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Gastly:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Haunter:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Gengar:
                    experienceGroup = ExperienceGroup.Medium_Slow;
                    break;
                case PokémonNames.Onix:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Drowzee:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Hypno:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Krabby:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Kingler:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Voltorb:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Electrode:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Exeggcute:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Exeggutor:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Cubone:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Marowak:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Hitmonlee:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Hitmonchan:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Lickitung:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Koffing:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Weezing:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Rhyhorn:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Rhydon:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Chansey:
                    experienceGroup = ExperienceGroup.Fast;
                    break;
                case PokémonNames.Tangela:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Kangaskhan:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Horsea:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Seadra:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Goldeen:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Seaking:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Staryu:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Starmie:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Mr_Mime:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Scyther:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Jynx:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Electabuzz:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Magmar:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Pinsir:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Tauros:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Magikarp:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Gyarados:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Lapras:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Ditto:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Eevee:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Vaporeon:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Jolteon:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Flareon:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Porygon:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Omanyte:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Omastar:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Kabuto:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Kabutops:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
                case PokémonNames.Aerodactyl:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Snorlax:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Articuno:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Zapdos:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Moltres:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Dratini:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Dragonair:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Dragonite:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Mewtwo:
                    experienceGroup = ExperienceGroup.Slow;
                    break;
                case PokémonNames.Mew:
                    experienceGroup = ExperienceGroup.Medium_Fast;
                    break;
            }
            return experienceGroup;
        }

        public int[] GetBaseStats()
        {
            int[] baseStats = new int[6];
            switch (name)
            {
                case PokémonNames.Bulbasaur:
                    baseStats[0] = 45;
                    baseStats[1] = 49;
                    baseStats[2] = 49;
                    baseStats[3] = 65;
                    baseStats[4] = 65;
                    baseStats[5] = 45;
                    break;

                case PokémonNames.Ivysaur:
                    baseStats[0] = 60;
                    baseStats[1] = 62;
                    baseStats[2] = 63;
                    baseStats[3] = 80;
                    baseStats[4] = 80;
                    baseStats[5] = 60;
                    break;

                case PokémonNames.Venusaur:
                    baseStats[0] = 80;
                    baseStats[1] = 82;
                    baseStats[2] = 83;
                    baseStats[3] = 100;
                    baseStats[4] = 100;
                    baseStats[5] = 80;
                    break;

                case PokémonNames.Charmander:
                    baseStats[0] = 39;
                    baseStats[1] = 52;
                    baseStats[2] = 43;
                    baseStats[3] = 60;
                    baseStats[4] = 50;
                    baseStats[5] = 65;
                    break;

                case PokémonNames.Charmeleon:
                    baseStats[0] = 58;
                    baseStats[1] = 64;
                    baseStats[2] = 58;
                    baseStats[3] = 80;
                    baseStats[4] = 65;
                    baseStats[5] = 80;
                    break;

                case PokémonNames.Charizard:
                    baseStats[0] = 78;
                    baseStats[1] = 84;
                    baseStats[2] = 78;
                    baseStats[3] = 109;
                    baseStats[4] = 85;
                    baseStats[5] = 100;
                    break;

                case PokémonNames.Squirtle:
                    baseStats[0] = 44;
                    baseStats[1] = 48;
                    baseStats[2] = 65;
                    baseStats[3] = 50;
                    baseStats[4] = 64;
                    baseStats[5] = 43;
                    break;

                case PokémonNames.Wartortle:
                    baseStats[0] = 59;
                    baseStats[1] = 63;
                    baseStats[2] = 80;
                    baseStats[3] = 65;
                    baseStats[4] = 80;
                    baseStats[5] = 58;
                    break;

                case PokémonNames.Blastoise:
                    baseStats[0] = 79;
                    baseStats[1] = 83;
                    baseStats[2] = 100;
                    baseStats[3] = 85;
                    baseStats[4] = 105;
                    baseStats[5] = 78;
                    break;

                case PokémonNames.Caterpie:
                    baseStats[0] = 45;
                    baseStats[1] = 30;
                    baseStats[2] = 45;
                    baseStats[3] = 20;
                    baseStats[4] = 20;
                    baseStats[5] = 45;
                    break;

                case PokémonNames.Metapod:
                    baseStats[0] = 50;
                    baseStats[1] = 20;
                    baseStats[2] = 55;
                    baseStats[3] = 25;
                    baseStats[4] = 25;
                    baseStats[5] = 30;
                    break;

                case PokémonNames.Butterfree:
                    baseStats[0] = 60;
                    baseStats[1] = 45;
                    baseStats[2] = 50;
                    baseStats[3] = 90;
                    baseStats[4] = 80;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Weedle:
                    baseStats[0] = 40;
                    baseStats[1] = 35;
                    baseStats[2] = 30;
                    baseStats[3] = 20;
                    baseStats[4] = 20;
                    baseStats[5] = 50;
                    break;

                case PokémonNames.Kakuna:
                    baseStats[0] = 45;
                    baseStats[1] = 25;
                    baseStats[2] = 50;
                    baseStats[3] = 25;
                    baseStats[4] = 25;
                    baseStats[5] = 35;
                    break;

                case PokémonNames.Beedrill:
                    baseStats[0] = 65;
                    baseStats[1] = 90;
                    baseStats[2] = 40;
                    baseStats[3] = 45;
                    baseStats[4] = 80;
                    baseStats[5] = 75;
                    break;

                case PokémonNames.Pidgey:
                    baseStats[0] = 40;
                    baseStats[1] = 45;
                    baseStats[2] = 40;
                    baseStats[3] = 35;
                    baseStats[4] = 35;
                    baseStats[5] = 56;
                    break;

                case PokémonNames.Pidgeotto:
                    baseStats[0] = 63;
                    baseStats[1] = 60;
                    baseStats[2] = 55;
                    baseStats[3] = 50;
                    baseStats[4] = 50;
                    baseStats[5] = 71;
                    break;

                case PokémonNames.Pidgeot:
                    baseStats[0] = 83;
                    baseStats[1] = 80;
                    baseStats[2] = 75;
                    baseStats[3] = 70;
                    baseStats[4] = 70;
                    baseStats[5] = 101;
                    break;

                case PokémonNames.Rattata:
                    baseStats[0] = 30;
                    baseStats[1] = 56;
                    baseStats[2] = 35;
                    baseStats[3] = 25;
                    baseStats[4] = 36;
                    baseStats[5] = 72;
                    break;

                case PokémonNames.Raticate:
                    baseStats[0] = 55;
                    baseStats[1] = 81;
                    baseStats[2] = 60;
                    baseStats[3] = 50;
                    baseStats[4] = 70;
                    baseStats[5] = 97;
                    break;

                case PokémonNames.Spearow:
                    baseStats[0] = 40;
                    baseStats[1] = 60;
                    baseStats[2] = 30;
                    baseStats[3] = 31;
                    baseStats[4] = 31;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Fearow:
                    baseStats[0] = 65;
                    baseStats[1] = 90;
                    baseStats[2] = 65;
                    baseStats[3] = 61;
                    baseStats[4] = 61;
                    baseStats[5] = 100;
                    break;

                case PokémonNames.Ekans:
                    baseStats[0] = 35;
                    baseStats[1] = 60;
                    baseStats[2] = 44;
                    baseStats[3] = 40;
                    baseStats[4] = 54;
                    baseStats[5] = 55;
                    break;

                case PokémonNames.Arbok:
                    baseStats[0] = 60;
                    baseStats[1] = 95;
                    baseStats[2] = 69;
                    baseStats[3] = 65;
                    baseStats[4] = 79;
                    baseStats[5] = 80;
                    break;

                case PokémonNames.Pikachu:
                    baseStats[0] = 35;
                    baseStats[1] = 55;
                    baseStats[2] = 40;
                    baseStats[3] = 50;
                    baseStats[4] = 50;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Raichu:
                    baseStats[0] = 60;
                    baseStats[1] = 90;
                    baseStats[2] = 55;
                    baseStats[3] = 90;
                    baseStats[4] = 80;
                    baseStats[5] = 110;
                    break;

                case PokémonNames.Sandshrew:
                    baseStats[0] = 50;
                    baseStats[1] = 75;
                    baseStats[2] = 85;
                    baseStats[3] = 20;
                    baseStats[4] = 30;
                    baseStats[5] = 40;
                    break;

                case PokémonNames.Sandslash:
                    baseStats[0] = 75;
                    baseStats[1] = 100;
                    baseStats[2] = 110;
                    baseStats[3] = 45;
                    baseStats[4] = 55;
                    baseStats[5] = 65;
                    break;

                case PokémonNames.Nidoran_Female:
                    baseStats[0] = 55;
                    baseStats[1] = 47;
                    baseStats[2] = 52;
                    baseStats[3] = 40;
                    baseStats[4] = 40;
                    baseStats[5] = 41;
                    break;

                case PokémonNames.Nidorina:
                    baseStats[0] = 70;
                    baseStats[1] = 62;
                    baseStats[2] = 67;
                    baseStats[3] = 55;
                    baseStats[4] = 55;
                    baseStats[5] = 56;
                    break;

                case PokémonNames.Nidoqueen:
                    baseStats[0] = 90;
                    baseStats[1] = 92;
                    baseStats[2] = 87;
                    baseStats[3] = 75;
                    baseStats[4] = 85;
                    baseStats[5] = 76;
                    break;

                case PokémonNames.Nidoran_Male:
                    baseStats[0] = 46;
                    baseStats[1] = 57;
                    baseStats[2] = 40;
                    baseStats[3] = 40;
                    baseStats[4] = 40;
                    baseStats[5] = 50;
                    break;

                case PokémonNames.Nidorino:
                    baseStats[0] = 61;
                    baseStats[1] = 72;
                    baseStats[2] = 57;
                    baseStats[3] = 55;
                    baseStats[4] = 55;
                    baseStats[5] = 65;
                    break;


                case PokémonNames.Nidoking:
                    baseStats[0] = 81;
                    baseStats[1] = 102;
                    baseStats[2] = 77;
                    baseStats[3] = 85;
                    baseStats[4] = 75;
                    baseStats[5] = 85;
                    break;


                case PokémonNames.Clefairy:
                    baseStats[0] = 70;
                    baseStats[1] = 45;
                    baseStats[2] = 48;
                    baseStats[3] = 60;
                    baseStats[4] = 65;
                    baseStats[5] = 35;
                    break;


                case PokémonNames.Clefable:
                    baseStats[0] = 95;
                    baseStats[1] = 70;
                    baseStats[2] = 73;
                    baseStats[3] = 95;
                    baseStats[4] = 90;
                    baseStats[5] = 60;
                    break;


                case PokémonNames.Vulpix:
                    baseStats[0] = 38;
                    baseStats[1] = 41;
                    baseStats[2] = 40;
                    baseStats[3] = 50;
                    baseStats[4] = 65;
                    baseStats[5] = 65;
                    break;


                case PokémonNames.Ninetales:
                    baseStats[0] = 73;
                    baseStats[1] = 76;
                    baseStats[2] = 75;
                    baseStats[3] = 81;
                    baseStats[4] = 100;
                    baseStats[5] = 100;
                    break;


                case PokémonNames.Jigglypuff:
                    baseStats[0] = 115;
                    baseStats[1] = 45;
                    baseStats[2] = 20;
                    baseStats[3] = 45;
                    baseStats[4] = 25;
                    baseStats[5] = 20;
                    break;


                case PokémonNames.Wigglytuff:
                    baseStats[0] = 140;
                    baseStats[1] = 70;
                    baseStats[2] = 45;
                    baseStats[3] = 85;
                    baseStats[4] = 50;
                    baseStats[5] = 45;
                    break;


                case PokémonNames.Zubat:
                    baseStats[0] = 40;
                    baseStats[1] = 45;
                    baseStats[2] = 35;
                    baseStats[3] = 30;
                    baseStats[4] = 40;
                    baseStats[5] = 55;
                    break;


                case PokémonNames.Golbat:
                    baseStats[0] = 75;
                    baseStats[1] = 80;
                    baseStats[2] = 70;
                    baseStats[3] = 65;
                    baseStats[4] = 75;
                    baseStats[5] = 90;
                    break;


                case PokémonNames.Oddish:
                    baseStats[0] = 45;
                    baseStats[1] = 50;
                    baseStats[2] = 55;
                    baseStats[3] = 75;
                    baseStats[4] = 65;
                    baseStats[5] = 30;
                    break;


                case PokémonNames.Gloom:
                    baseStats[0] = 60;
                    baseStats[1] = 65;
                    baseStats[2] = 70;
                    baseStats[3] = 85;
                    baseStats[4] = 75;
                    baseStats[5] = 40;
                    break;


                case PokémonNames.Vileplume:
                    baseStats[0] = 75;
                    baseStats[1] = 80;
                    baseStats[2] = 85;
                    baseStats[3] = 110;
                    baseStats[4] = 90;
                    baseStats[5] = 50;
                    break;


                case PokémonNames.Paras:
                    baseStats[0] = 35;
                    baseStats[1] = 70;
                    baseStats[2] = 55;
                    baseStats[3] = 45;
                    baseStats[4] = 55;
                    baseStats[5] = 25;
                    break;

                case PokémonNames.Parasect:
                    baseStats[0] = 60;
                    baseStats[1] = 95;
                    baseStats[2] = 80;
                    baseStats[3] = 60;
                    baseStats[4] = 80;
                    baseStats[5] = 30;
                    break;


                case PokémonNames.Venonat:
                    baseStats[0] = 60;
                    baseStats[1] = 55;
                    baseStats[2] = 50;
                    baseStats[3] = 40;
                    baseStats[4] = 55;
                    baseStats[5] = 45;
                    break;


                case PokémonNames.Venomoth:
                    baseStats[0] = 70;
                    baseStats[1] = 65;
                    baseStats[2] = 60;
                    baseStats[3] = 90;
                    baseStats[4] = 75;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Diglett:
                    baseStats[0] = 10;
                    baseStats[1] = 55;
                    baseStats[2] = 25;
                    baseStats[3] = 35;
                    baseStats[4] = 45;
                    baseStats[5] = 95;
                    break;

                case PokémonNames.Dugtrio:
                    baseStats[0] = 35;
                    baseStats[1] = 100;
                    baseStats[2] = 50;
                    baseStats[3] = 50;
                    baseStats[4] = 70;
                    baseStats[5] = 120;
                    break;

                case PokémonNames.Meowth:
                    baseStats[0] = 40;
                    baseStats[1] = 45;
                    baseStats[2] = 35;
                    baseStats[3] = 40;
                    baseStats[4] = 40;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Persian:
                    baseStats[0] = 65;
                    baseStats[1] = 70;
                    baseStats[2] = 60;
                    baseStats[3] = 65;
                    baseStats[4] = 65;
                    baseStats[5] = 115;
                    break;

                case PokémonNames.Psyduck:
                    baseStats[0] = 50;
                    baseStats[1] = 52;
                    baseStats[2] = 48;
                    baseStats[3] = 65;
                    baseStats[4] = 50;
                    baseStats[5] = 55;
                    break;

                case PokémonNames.Golduck:
                    baseStats[0] = 80;
                    baseStats[1] = 82;
                    baseStats[2] = 78;
                    baseStats[3] = 95;
                    baseStats[4] = 80;
                    baseStats[5] = 85;
                    break;

                case PokémonNames.Mankey:
                    baseStats[0] = 40;
                    baseStats[1] = 80;
                    baseStats[2] = 35;
                    baseStats[3] = 35;
                    baseStats[4] = 45;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Primeape:
                    baseStats[0] = 65;
                    baseStats[1] = 105;
                    baseStats[2] = 60;
                    baseStats[3] = 60;
                    baseStats[4] = 70;
                    baseStats[5] = 95;
                    break;

                case PokémonNames.Growlithe:
                    baseStats[0] = 55;
                    baseStats[1] = 70;
                    baseStats[2] = 45;
                    baseStats[3] = 70;
                    baseStats[4] = 50;
                    baseStats[5] = 60;
                    break;

                case PokémonNames.Arcanine:
                    baseStats[0] = 90;
                    baseStats[1] = 110;
                    baseStats[2] = 80;
                    baseStats[3] = 100;
                    baseStats[4] = 80;
                    baseStats[5] = 95;
                    break;

                case PokémonNames.Poliwag:
                    baseStats[0] = 40;
                    baseStats[1] = 50;
                    baseStats[2] = 40;
                    baseStats[3] = 40;
                    baseStats[4] = 40;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Poliwhirl:
                    baseStats[0] = 65;
                    baseStats[1] = 65;
                    baseStats[2] = 65;
                    baseStats[3] = 50;
                    baseStats[4] = 50;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Poliwrath:
                    baseStats[0] = 90;
                    baseStats[1] = 95;
                    baseStats[2] = 95;
                    baseStats[3] = 70;
                    baseStats[4] = 90;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Abra:
                    baseStats[0] = 25;
                    baseStats[1] = 20;
                    baseStats[2] = 15;
                    baseStats[3] = 105;
                    baseStats[4] = 55;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Kadabra:
                    baseStats[0] = 40;
                    baseStats[1] = 35;
                    baseStats[2] = 30;
                    baseStats[3] = 120;
                    baseStats[4] = 70;
                    baseStats[5] = 105;
                    break;

                case PokémonNames.Alakazam:
                    baseStats[0] = 55;
                    baseStats[1] = 50;
                    baseStats[2] = 45;
                    baseStats[3] = 135;
                    baseStats[4] = 95;
                    baseStats[5] = 120;
                    break;

                case PokémonNames.Machop:
                    baseStats[0] = 70;
                    baseStats[1] = 80;
                    baseStats[2] = 50;
                    baseStats[3] = 35;
                    baseStats[4] = 35;
                    baseStats[5] = 35;
                    break;

                case PokémonNames.Machoke:
                    baseStats[0] = 80;
                    baseStats[1] = 100;
                    baseStats[2] = 70;
                    baseStats[3] = 50;
                    baseStats[4] = 60;
                    baseStats[5] = 45;
                    break;

                case PokémonNames.Machamp:
                    baseStats[0] = 90;
                    baseStats[1] = 130;
                    baseStats[2] = 80;
                    baseStats[3] = 65;
                    baseStats[4] = 85;
                    baseStats[5] = 55;
                    break;

                case PokémonNames.Bellsprout:
                    baseStats[0] = 50;
                    baseStats[1] = 75;
                    baseStats[2] = 35;
                    baseStats[3] = 70;
                    baseStats[4] = 30;
                    baseStats[5] = 40;
                    break;

                case PokémonNames.Weepinbell:
                    baseStats[0] = 65;
                    baseStats[1] = 90;
                    baseStats[2] = 50;
                    baseStats[3] = 85;
                    baseStats[4] = 45;
                    baseStats[5] = 55;
                    break;

                case PokémonNames.Victreebel:
                    baseStats[0] = 80;
                    baseStats[1] = 105;
                    baseStats[2] = 65;
                    baseStats[3] = 100;
                    baseStats[4] = 70;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Tentacool:
                    baseStats[0] = 40;
                    baseStats[1] = 40;
                    baseStats[2] = 35;
                    baseStats[3] = 50;
                    baseStats[4] = 100;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Tentacruel:
                    baseStats[0] = 80;
                    baseStats[1] = 70;
                    baseStats[2] = 65;
                    baseStats[3] = 80;
                    baseStats[4] = 120;
                    baseStats[5] = 100;
                    break;

                case PokémonNames.Geodude:
                    baseStats[0] = 40;
                    baseStats[1] = 80;
                    baseStats[2] = 100;
                    baseStats[3] = 30;
                    baseStats[4] = 30;
                    baseStats[5] = 20;
                    break;

                case PokémonNames.Graveler:
                    baseStats[0] = 55;
                    baseStats[1] = 95;
                    baseStats[2] = 115;
                    baseStats[3] = 45;
                    baseStats[4] = 45;
                    baseStats[5] = 35;
                    break;

                case PokémonNames.Golem:
                    baseStats[0] = 80;
                    baseStats[1] = 120;
                    baseStats[2] = 130;
                    baseStats[3] = 55;
                    baseStats[4] = 65;
                    baseStats[5] = 45;
                    break;

                case PokémonNames.Ponyta:
                    baseStats[0] = 50;
                    baseStats[1] = 85;
                    baseStats[2] = 55;
                    baseStats[3] = 65;
                    baseStats[4] = 65;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Rapidash:
                    baseStats[0] = 65;
                    baseStats[1] = 100;
                    baseStats[2] = 70;
                    baseStats[3] = 80;
                    baseStats[4] = 80;
                    baseStats[5] = 105;
                    break;

                case PokémonNames.Slowpoke:
                    baseStats[0] = 90;
                    baseStats[1] = 65;
                    baseStats[2] = 65;
                    baseStats[3] = 40;
                    baseStats[4] = 40;
                    baseStats[5] = 15;
                    break;

                case PokémonNames.Slowbro:
                    baseStats[0] = 95;
                    baseStats[1] = 75;
                    baseStats[2] = 110;
                    baseStats[3] = 110;
                    baseStats[4] = 80;
                    baseStats[5] = 30;
                    break;

                case PokémonNames.Magnemite:
                    baseStats[0] = 25;
                    baseStats[1] = 35;
                    baseStats[2] = 70;
                    baseStats[3] = 95;
                    baseStats[4] = 55;
                    baseStats[5] = 45;
                    break;

                case PokémonNames.Magneton:
                    baseStats[0] = 50;
                    baseStats[1] = 60;
                    baseStats[2] = 95;
                    baseStats[3] = 120;
                    baseStats[4] = 70;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Farfetchd:
                    baseStats[0] = 52;
                    baseStats[1] = 90;
                    baseStats[2] = 55;
                    baseStats[3] = 58;
                    baseStats[4] = 62;
                    baseStats[5] = 60;
                    break;

                case PokémonNames.Doduo:
                    baseStats[0] = 35;
                    baseStats[1] = 85;
                    baseStats[2] = 45;
                    baseStats[3] = 35;
                    baseStats[4] = 35;
                    baseStats[5] = 75;
                    break;

                case PokémonNames.Dodrio:
                    baseStats[0] = 60;
                    baseStats[1] = 110;
                    baseStats[2] = 70;
                    baseStats[3] = 60;
                    baseStats[4] = 60;
                    baseStats[5] = 110;
                    break;

                case PokémonNames.Seel:
                    baseStats[0] = 65;
                    baseStats[1] = 45;
                    baseStats[2] = 55;
                    baseStats[3] = 45;
                    baseStats[4] = 70;
                    baseStats[5] = 45;
                    break;

                case PokémonNames.Dewgong:
                    baseStats[0] = 90;
                    baseStats[1] = 70;
                    baseStats[2] = 80;
                    baseStats[3] = 70;
                    baseStats[4] = 95;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Grimer:
                    baseStats[0] = 80;
                    baseStats[1] = 80;
                    baseStats[2] = 50;
                    baseStats[3] = 40;
                    baseStats[4] = 50;
                    baseStats[5] = 25;
                    break;

                case PokémonNames.Muk:
                    baseStats[0] = 105;
                    baseStats[1] = 105;
                    baseStats[2] = 75;
                    baseStats[3] = 65;
                    baseStats[4] = 100;
                    baseStats[5] = 50;
                    break;

                case PokémonNames.Shellder:
                    baseStats[0] = 30;
                    baseStats[1] = 65;
                    baseStats[2] = 100;
                    baseStats[3] = 45;
                    baseStats[4] = 25;
                    baseStats[5] = 40;
                    break;

                case PokémonNames.Cloyster:
                    baseStats[0] = 50;
                    baseStats[1] = 95;
                    baseStats[2] = 180;
                    baseStats[3] = 85;
                    baseStats[4] = 45;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Gastly:
                    baseStats[0] = 30;
                    baseStats[1] = 35;
                    baseStats[2] = 30;
                    baseStats[3] = 100;
                    baseStats[4] = 35;
                    baseStats[5] = 80;
                    break;

                case PokémonNames.Haunter:
                    baseStats[0] = 45;
                    baseStats[1] = 50;
                    baseStats[2] = 45;
                    baseStats[3] = 115;
                    baseStats[4] = 55;
                    baseStats[5] = 95;
                    break;

                case PokémonNames.Gengar:
                    baseStats[0] = 60;
                    baseStats[1] = 65;
                    baseStats[2] = 60;
                    baseStats[3] = 130;
                    baseStats[4] = 75;
                    baseStats[5] = 110;
                    break;

                case PokémonNames.Onix:
                    baseStats[0] = 35;
                    baseStats[1] = 45;
                    baseStats[2] = 160;
                    baseStats[3] = 30;
                    baseStats[4] = 45;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Drowzee:
                    baseStats[0] = 60;
                    baseStats[1] = 48;
                    baseStats[2] = 45;
                    baseStats[3] = 43;
                    baseStats[4] = 90;
                    baseStats[5] = 42;
                    break;

                case PokémonNames.Hypno:
                    baseStats[0] = 85;
                    baseStats[1] = 73;
                    baseStats[2] = 70;
                    baseStats[3] = 73;
                    baseStats[4] = 115;
                    baseStats[5] = 67;
                    break;

                case PokémonNames.Krabby:
                    baseStats[0] = 30;
                    baseStats[1] = 105;
                    baseStats[2] = 90;
                    baseStats[3] = 25;
                    baseStats[4] = 25;
                    baseStats[5] = 50;
                    break;

                case PokémonNames.Kingler:
                    baseStats[0] = 55;
                    baseStats[1] = 130;
                    baseStats[2] = 115;
                    baseStats[3] = 50;
                    baseStats[4] = 50;
                    baseStats[5] = 75;
                    break;

                case PokémonNames.Voltorb:
                    baseStats[0] = 40;
                    baseStats[1] = 30;
                    baseStats[2] = 50;
                    baseStats[3] = 55;
                    baseStats[4] = 55;
                    baseStats[5] = 100;
                    break;

                case PokémonNames.Electrode:
                    baseStats[0] = 60;
                    baseStats[1] = 50;
                    baseStats[2] = 70;
                    baseStats[3] = 80;
                    baseStats[4] = 80;
                    baseStats[5] = 150;
                    break;

                case PokémonNames.Exeggcute:
                    baseStats[0] = 60;
                    baseStats[1] = 40;
                    baseStats[2] = 80;
                    baseStats[3] = 60;
                    baseStats[4] = 45;
                    baseStats[5] = 40;
                    break;

                case PokémonNames.Exeggutor:
                    baseStats[0] = 95;
                    baseStats[1] = 95;
                    baseStats[2] = 85;
                    baseStats[3] = 125;
                    baseStats[4] = 75;
                    baseStats[5] = 55;
                    break;

                case PokémonNames.Cubone:
                    baseStats[0] = 50;
                    baseStats[1] = 50;
                    baseStats[2] = 95;
                    baseStats[3] = 40;
                    baseStats[4] = 50;
                    baseStats[5] = 35;
                    break;

                case PokémonNames.Marowak:
                    baseStats[0] = 60;
                    baseStats[1] = 80;
                    baseStats[2] = 110;
                    baseStats[3] = 50;
                    baseStats[4] = 80;
                    baseStats[5] = 45;
                    break;

                case PokémonNames.Hitmonlee:
                    baseStats[0] = 50;
                    baseStats[1] = 120;
                    baseStats[2] = 53;
                    baseStats[3] = 35;
                    baseStats[4] = 110;
                    baseStats[5] = 87;
                    break;

                case PokémonNames.Hitmonchan:
                    baseStats[0] = 50;
                    baseStats[1] = 105;
                    baseStats[2] = 79;
                    baseStats[3] = 35;
                    baseStats[4] = 110;
                    baseStats[5] = 76;
                    break;

                case PokémonNames.Lickitung:
                    baseStats[0] = 90;
                    baseStats[1] = 55;
                    baseStats[2] = 75;
                    baseStats[3] = 60;
                    baseStats[4] = 75;
                    baseStats[5] = 30;
                    break;

                case PokémonNames.Koffing:
                    baseStats[0] = 40;
                    baseStats[1] = 65;
                    baseStats[2] = 95;
                    baseStats[3] = 60;
                    baseStats[4] = 45;
                    baseStats[5] = 35;
                    break;

                case PokémonNames.Weezing:
                    baseStats[0] = 65;
                    baseStats[1] = 90;
                    baseStats[2] = 120;
                    baseStats[3] = 85;
                    baseStats[4] = 79;
                    baseStats[5] = 60;
                    break;

                case PokémonNames.Rhyhorn:
                    baseStats[0] = 80;
                    baseStats[1] = 85;
                    baseStats[2] = 95;
                    baseStats[3] = 30;
                    baseStats[4] = 30;
                    baseStats[5] = 25;
                    break;

                case PokémonNames.Rhydon:
                    baseStats[0] = 105;
                    baseStats[1] = 130;
                    baseStats[2] = 120;
                    baseStats[3] = 45;
                    baseStats[4] = 45;
                    baseStats[5] = 40;
                    break;

                case PokémonNames.Chansey:
                    baseStats[0] = 250;
                    baseStats[1] = 5;
                    baseStats[2] = 5;
                    baseStats[3] = 35;
                    baseStats[4] = 105;
                    baseStats[5] = 50;
                    break;

                case PokémonNames.Tangela:
                    baseStats[0] = 65;
                    baseStats[1] = 55;
                    baseStats[2] = 115;
                    baseStats[3] = 100;
                    baseStats[4] = 40;
                    baseStats[5] = 60;
                    break;

                case PokémonNames.Kangaskhan:
                    baseStats[0] = 105;
                    baseStats[1] = 95;
                    baseStats[2] = 80;
                    baseStats[3] = 40;
                    baseStats[4] = 80;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Horsea:
                    baseStats[0] = 30;
                    baseStats[1] = 40;
                    baseStats[2] = 70;
                    baseStats[3] = 70;
                    baseStats[4] = 25;
                    baseStats[5] = 60;
                    break;

                case PokémonNames.Seadra:
                    baseStats[0] = 55;
                    baseStats[1] = 65;
                    baseStats[2] = 95;
                    baseStats[3] = 95;
                    baseStats[4] = 45;
                    baseStats[5] = 85;
                    break;

                case PokémonNames.Goldeen:
                    baseStats[0] = 45;
                    baseStats[1] = 67;
                    baseStats[2] = 60;
                    baseStats[3] = 35;
                    baseStats[4] = 50;
                    baseStats[5] = 63;
                    break;

                case PokémonNames.Seaking:
                    baseStats[0] = 80;
                    baseStats[1] = 92;
                    baseStats[2] = 65;
                    baseStats[3] = 65;
                    baseStats[4] = 80;
                    baseStats[5] = 68;
                    break;

                case PokémonNames.Staryu:
                    baseStats[0] = 30;
                    baseStats[1] = 45;
                    baseStats[2] = 55;
                    baseStats[3] = 70;
                    baseStats[4] = 55;
                    baseStats[5] = 85;
                    break;

                case PokémonNames.Starmie:
                    baseStats[0] = 60;
                    baseStats[1] = 75;
                    baseStats[2] = 85;
                    baseStats[3] = 100;
                    baseStats[4] = 85;
                    baseStats[5] = 115;
                    break;

                case PokémonNames.Mr_Mime:
                    baseStats[0] = 40;
                    baseStats[1] = 45;
                    baseStats[2] = 65;
                    baseStats[3] = 100;
                    baseStats[4] = 120;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Scyther:
                    baseStats[0] = 70;
                    baseStats[1] = 110;
                    baseStats[2] = 80;
                    baseStats[3] = 55;
                    baseStats[4] = 80;
                    baseStats[5] = 105;
                    break;

                case PokémonNames.Jynx:
                    baseStats[0] = 65;
                    baseStats[1] = 50;
                    baseStats[2] = 35;
                    baseStats[3] = 115;
                    baseStats[4] = 95;
                    baseStats[5] = 95;
                    break;

                case PokémonNames.Electabuzz:
                    baseStats[0] = 65;
                    baseStats[1] = 83;
                    baseStats[2] = 57;
                    baseStats[3] = 95;
                    baseStats[4] = 85;
                    baseStats[5] = 105;
                    break;

                case PokémonNames.Magmar:
                    baseStats[0] = 65;
                    baseStats[1] = 95;
                    baseStats[2] = 57;
                    baseStats[3] = 100;
                    baseStats[4] = 85;
                    baseStats[5] = 93;
                    break;

                case PokémonNames.Pinsir:
                    baseStats[0] = 65;
                    baseStats[1] = 125;
                    baseStats[2] = 100;
                    baseStats[3] = 55;
                    baseStats[4] = 70;
                    baseStats[5] = 85;
                    break;

                case PokémonNames.Tauros:
                    baseStats[0] = 75;
                    baseStats[1] = 100;
                    baseStats[2] = 95;
                    baseStats[3] = 40;
                    baseStats[4] = 70;
                    baseStats[5] = 110;
                    break;

                case PokémonNames.Magikarp:
                    baseStats[0] = 20;
                    baseStats[1] = 10;
                    baseStats[2] = 55;
                    baseStats[3] = 15;
                    baseStats[4] = 20;
                    baseStats[5] = 80;
                    break;

                case PokémonNames.Gyarados:
                    baseStats[0] = 95;
                    baseStats[1] = 125;
                    baseStats[2] = 79;
                    baseStats[3] = 60;
                    baseStats[4] = 100;
                    baseStats[5] = 81;
                    break;

                case PokémonNames.Lapras:
                    baseStats[0] = 130;
                    baseStats[1] = 85;
                    baseStats[2] = 80;
                    baseStats[3] = 85;
                    baseStats[4] = 95;
                    baseStats[5] = 60;
                    break;

                case PokémonNames.Ditto:
                    baseStats[0] = 48;
                    baseStats[1] = 48;
                    baseStats[2] = 48;
                    baseStats[3] = 48;
                    baseStats[4] = 48;
                    baseStats[5] = 48;
                    break;

                case PokémonNames.Eevee:
                    baseStats[0] = 55;
                    baseStats[1] = 55;
                    baseStats[2] = 50;
                    baseStats[3] = 45;
                    baseStats[4] = 65;
                    baseStats[5] = 55;
                    break;

                case PokémonNames.Vaporeon:
                    baseStats[0] = 130;
                    baseStats[1] = 65;
                    baseStats[2] = 60;
                    baseStats[3] = 110;
                    baseStats[4] = 95;
                    baseStats[5] = 65;
                    break;

                case PokémonNames.Jolteon:
                    baseStats[0] = 65;
                    baseStats[1] = 65;
                    baseStats[2] = 60;
                    baseStats[3] = 110;
                    baseStats[4] = 95;
                    baseStats[5] = 130;
                    break;

                case PokémonNames.Flareon:
                    baseStats[0] = 65;
                    baseStats[1] = 130;
                    baseStats[2] = 60;
                    baseStats[3] = 95;
                    baseStats[4] = 110;
                    baseStats[5] = 65;
                    break;

                case PokémonNames.Porygon:
                    baseStats[0] = 65;
                    baseStats[1] = 60;
                    baseStats[2] = 70;
                    baseStats[3] = 85;
                    baseStats[4] = 75;
                    baseStats[5] = 40;
                    break;

                case PokémonNames.Omanyte:
                    baseStats[0] = 35;
                    baseStats[1] = 40;
                    baseStats[2] = 100;
                    baseStats[3] = 90;
                    baseStats[4] = 55;
                    baseStats[5] = 35;
                    break;

                case PokémonNames.Omastar:
                    baseStats[0] = 70;
                    baseStats[1] = 60;
                    baseStats[2] = 125;
                    baseStats[3] = 115;
                    baseStats[4] = 70;
                    baseStats[5] = 55;
                    break;

                case PokémonNames.Kabuto:
                    baseStats[0] = 30;
                    baseStats[1] = 80;
                    baseStats[2] = 90;
                    baseStats[3] = 55;
                    baseStats[4] = 45;
                    baseStats[5] = 55;
                    break;

                case PokémonNames.Kabutops:
                    baseStats[0] = 60;
                    baseStats[1] = 115;
                    baseStats[2] = 105;
                    baseStats[3] = 65;
                    baseStats[4] = 70;
                    baseStats[5] = 80;
                    break;

                case PokémonNames.Aerodactyl:
                    baseStats[0] = 80;
                    baseStats[1] = 105;
                    baseStats[2] = 65;
                    baseStats[3] = 60;
                    baseStats[4] = 75;
                    baseStats[5] = 130;
                    break;

                case PokémonNames.Snorlax:
                    baseStats[0] = 160;
                    baseStats[1] = 110;
                    baseStats[2] = 65;
                    baseStats[3] = 65;
                    baseStats[4] = 110;
                    baseStats[5] = 30;
                    break;

                case PokémonNames.Articuno:
                    baseStats[0] = 90;
                    baseStats[1] = 85;
                    baseStats[2] = 100;
                    baseStats[3] = 96;
                    baseStats[4] = 125;
                    baseStats[5] = 85;
                    break;

                case PokémonNames.Zapdos:
                    baseStats[0] = 90;
                    baseStats[1] = 90;
                    baseStats[2] = 85;
                    baseStats[3] = 125;
                    baseStats[4] = 90;
                    baseStats[5] = 100;
                    break;

                case PokémonNames.Moltres:
                    baseStats[0] = 90;
                    baseStats[1] = 100;
                    baseStats[2] = 90;
                    baseStats[3] = 125;
                    baseStats[4] = 85;
                    baseStats[5] = 90;
                    break;

                case PokémonNames.Dratini:
                    baseStats[0] = 41;
                    baseStats[1] = 64;
                    baseStats[2] = 45;
                    baseStats[3] = 50;
                    baseStats[4] = 50;
                    baseStats[5] = 50;
                    break;

                case PokémonNames.Dragonair:
                    baseStats[0] = 61;
                    baseStats[1] = 84;
                    baseStats[2] = 65;
                    baseStats[3] = 70;
                    baseStats[4] = 70;
                    baseStats[5] = 70;
                    break;

                case PokémonNames.Dragonite:
                    baseStats[0] = 91;
                    baseStats[1] = 134;
                    baseStats[2] = 95;
                    baseStats[3] = 100;
                    baseStats[4] = 100;
                    baseStats[5] = 80;
                    break;

                case PokémonNames.Mewtwo:
                    baseStats[0] = 106;
                    baseStats[1] = 110;
                    baseStats[2] = 90;
                    baseStats[3] = 154;
                    baseStats[4] = 90;
                    baseStats[5] = 130;
                    break;

                case PokémonNames.Mew:
                    baseStats[0] = 100;
                    baseStats[1] = 100;
                    baseStats[2] = 100;
                    baseStats[3] = 100;
                    baseStats[4] = 100;
                    baseStats[5] = 100;
                    break;
            }
            return baseStats;
        }
    }

    public class MoveInfoAttribute : Attribute
    {
        public Types Typing { get; }
        public Categories Category { get; }
        public int Power { get; }
        public int Accuracy { get; }
        public int PP { get; }


        public MoveInfoAttribute(Types typing, Categories category, int power, int accuracy, int pp)
        {
            this.Typing = typing;
            this.Category = category;
            this.Power = power;
            this.Accuracy = accuracy;
            this.PP = pp;
        }
    }

    public class PokémonTypesAttribute : Attribute
    {
        public List<Types> Typing = new();

        public PokémonTypesAttribute()
        {

        }

        public PokémonTypesAttribute(Types typing, Types typing2)
        {
            this.Typing.Add(typing);
            this.Typing.Add(typing2);
        }
        public List<Types> GetPokemonTyping()
        {
            return Typing;
        }
    }

    public enum ExperienceGroup
    {
        Medium_Fast,
        Slightly_Fast,
        Slightly_Slow,
        Medium_Slow,
        Fast,
        Slow,
    }

    public enum PokémonNames
    {
        [PokémonTypes(Types.GRASS, Types.POISON)]
        Bulbasaur,
        Ivysaur,
        Venusaur,

        [PokémonTypes(Types.FIRE, Types.NONE)]
        Charmander,
        Charmeleon,

        [PokémonTypes(Types.FIRE, Types.FLYING)]
        Charizard,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Squirtle,
        Wartortle,
        Blastoise,

        [PokémonTypes(Types.BUG, Types.NONE)]
        Caterpie,
        Metapod,

        [PokémonTypes(Types.BUG, Types.FLYING)]
        Butterfree,

        [PokémonTypes(Types.BUG, Types.POISON)]
        Weedle,
        Kakuna,
        Beedrill,

        [PokémonTypes(Types.NORMAL, Types.FLYING)]
        Pidgey,
        Pidgeotto,
        Pidgeot,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Rattata,
        Raticate,
        Spearow,
        Fearow,

        [PokémonTypes(Types.POISON, Types.NONE)]
        Ekans,
        Arbok,

        [PokémonTypes(Types.ELECTRIC, Types.NONE)]
        Pikachu,
        Raichu,

        [PokémonTypes(Types.GROUND, Types.NONE)]
        Sandshrew,
        Sandslash,

        [PokémonTypes(Types.POISON, Types.NONE)]
        Nidoran_Male,
        Nidorina,
        Nidoqueen,
        Nidoran_Female,
        Nidorino,
        Nidoking,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Clefairy,
        Clefable,

        [PokémonTypes(Types.FIRE, Types.NONE)]
        Vulpix,
        Ninetales,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Jigglypuff,
        Wigglytuff,

        [PokémonTypes(Types.POISON, Types.FLYING)]
        Zubat,
        Golbat,

        [PokémonTypes(Types.GRASS, Types.POISON)]
        Oddish,
        Gloom,
        Vileplume,

        [PokémonTypes(Types.BUG, Types.GRASS)]
        Paras,
        Parasect,

        [PokémonTypes(Types.BUG, Types.POISON)]
        Venonat,
        Venomoth,

        [PokémonTypes(Types.GROUND, Types.NONE)]
        Diglett,
        Dugtrio,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Meowth,
        Persian,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Psyduck,
        Golduck,

        [PokémonTypes(Types.FIGHTING, Types.NONE)]
        Mankey,
        Primeape,

        [PokémonTypes(Types.FIRE, Types.NONE)]
        Growlithe,
        Arcanine,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Poliwag,
        Poliwhirl,
        Poliwrath,

        [PokémonTypes(Types.PSYCHIC, Types.NONE)]
        Abra,
        Kadabra,
        Alakazam,

        [PokémonTypes(Types.FIGHTING, Types.NONE)]
        Machop,
        Machoke,
        Machamp,

        [PokémonTypes(Types.GRASS, Types.POISON)]
        Bellsprout,
        Weepinbell,
        Victreebel,

        [PokémonTypes(Types.WATER, Types.POISON)]
        Tentacool,
        Tentacruel,

        [PokémonTypes(Types.ROCK, Types.GROUND)]
        Geodude,
        Graveler,
        Golem,

        [PokémonTypes(Types.FIRE, Types.NONE)]
        Ponyta,
        Rapidash,

        [PokémonTypes(Types.WATER, Types.PSYCHIC)]
        Slowpoke,
        Slowbro,

        [PokémonTypes(Types.ELECTRIC, Types.NONE)]
        Magnemite,
        Magneton,

        [PokémonTypes(Types.NORMAL, Types.FLYING)]
        Farfetchd,
        Doduo,
        Dodrio,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Seel,
        Dewgong,

        [PokémonTypes(Types.POISON, Types.NONE)]
        Grimer,
        Muk,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Shellder,
        Cloyster,

        [PokémonTypes(Types.GHOST, Types.POISON)]
        Gastly,
        Haunter,
        Gengar,

        [PokémonTypes(Types.ROCK, Types.GROUND)]
        Onix,

        [PokémonTypes(Types.PSYCHIC, Types.NONE)]
        Drowzee,
        Hypno,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Krabby,
        Kingler,

        [PokémonTypes(Types.ELECTRIC, Types.NONE)]
        Voltorb,
        Electrode,

        [PokémonTypes(Types.GRASS, Types.PSYCHIC)]
        Exeggcute,
        Exeggutor,

        [PokémonTypes(Types.GROUND, Types.NONE)]
        Cubone,
        Marowak,

        [PokémonTypes(Types.FIGHTING, Types.NONE)]
        Hitmonlee,
        Hitmonchan,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Lickitung,

        [PokémonTypes(Types.POISON, Types.NONE)]
        Koffing,
        Weezing,

        [PokémonTypes(Types.GROUND, Types.ROCK)]
        Rhyhorn,
        Rhydon,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Chansey,

        [PokémonTypes(Types.GRASS, Types.NONE)]
        Tangela,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Kangaskhan,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Horsea,
        Seadra,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Goldeen,
        Seaking,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Staryu,
        Starmie,

        [PokémonTypes(Types.PSYCHIC, Types.NONE)]
        Mr_Mime,

        [PokémonTypes(Types.BUG, Types.FLYING)]
        Scyther,

        [PokémonTypes(Types.ICE, Types.PSYCHIC)]
        Jynx,

        [PokémonTypes(Types.ELECTRIC, Types.NONE)]
        Electabuzz,

        [PokémonTypes(Types.FIRE, Types.NONE)]
        Magmar,

        [PokémonTypes(Types.BUG, Types.NONE)]
        Pinsir,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Tauros,

        [PokémonTypes(Types.WATER, Types.NONE)]
        Magikarp,
        Gyarados,

        [PokémonTypes(Types.WATER, Types.ICE)]
        Lapras,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Ditto,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Eevee,
        Vaporeon,
        Jolteon,
        Flareon,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Porygon,

        [PokémonTypes(Types.ROCK, Types.WATER)]
        Omanyte,
        Omastar,

        [PokémonTypes(Types.ROCK, Types.WATER)]
        Kabuto,
        Kabutops,

        [PokémonTypes(Types.ROCK, Types.FLYING)]
        Aerodactyl,

        [PokémonTypes(Types.NORMAL, Types.NONE)]
        Snorlax,

        [PokémonTypes(Types.ICE, Types.FLYING)]
        Articuno,

        [PokémonTypes(Types.ELECTRIC, Types.FLYING)]
        Zapdos,

        [PokémonTypes(Types.FIRE, Types.FLYING)]
        Moltres,

        [PokémonTypes(Types.DRAGON, Types.NONE)]
        Dratini,
        Dragonair,
        Dragonite,

        [PokémonTypes(Types.PSYCHIC, Types.NONE)]
        Mewtwo,
        Mew,
    }

    public enum Status
    {
        Burned,
        Paralyzed,
        Frozen,
        Asleep,
        Poisoned,
        None
    }

    public enum Genders
    {
        Male,
        Female,
        Other
    }

    public  enum Categories
    {
        SPECIAL,
        PHYSICAL,
        STATUS
    }

    public enum Types
    {
        NORMAL,
        FIRE,
        WATER,
        ELECTRIC,
        GRASS,
        ICE,
        FIGHTING,
        POISON,
        GROUND,
        FLYING,
        PSYCHIC,
        BUG,
        ROCK,
        GHOST,
        DRAGON,
        NONE
    }

    public enum Moves
    {
        NONE,
        [MoveInfo(Types.GRASS, Categories.SPECIAL , 20, 100, 25)]
        Absorb,

        [MoveInfo(Types.POISON, Categories.SPECIAL, 40, 100, 30)]
        Acid,

        [MoveInfo(Types.POISON, Categories.STATUS, -1, -1, 20)]
        Acid_Armor,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, -1, 30)]
        Agility,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, -1, 20)]
        Amnesia,

        [MoveInfo(Types.ICE, Categories.SPECIAL, 65, 100, 20)]
        Aurora_Beam,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 15, 85, 20)]
        Barrage,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, -1, 20)]
        Barrier,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, -1, -1, 10)]
        Bide,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 15, 85, 20)]
        Bind,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 60, 100, 25)]
        Bite,

        [MoveInfo(Types.ICE, Categories.SPECIAL, 110, 70, 5)]
        Blizzard,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 85, 100, 15)]
        Body_Slam,

        [MoveInfo(Types.GROUND, Categories.PHYSICAL, 65, 85, 20)]
        Bone_Club,

        [MoveInfo(Types.GROUND, Categories.PHYSICAL, 50, 90, 10)]
        Bonemerang,

        [MoveInfo(Types.WATER, Categories.SPECIAL, 40, 100, 30)]
        Bubble,

        [MoveInfo(Types.WATER, Categories.SPECIAL, 65, 100, 20)]
        Bubble_Beam,

        [MoveInfo(Types.WATER, Categories.SPECIAL, 35, 85, 15)]
        Clamp,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 18, 85, 15)]
        Comet_Punch,

        [MoveInfo(Types.GHOST, Categories.STATUS, -1, 100, 10)]
        Confuse_Ray,

        [MoveInfo(Types.PSYCHIC, Categories.SPECIAL , 50, 100, 25)]
        Confusion,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 100, 30)]
        Constrict,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 20)]
        Conversion,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, -1, 100, 20)]
        Counter,

        [MoveInfo(Types.WATER, Categories.PHYSICAL, 100, 90, 10)]
        Crabhammer,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 50, 95, 30)]
        Cut,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 40)]
        Defense_Curl,

        [MoveInfo(Types.GROUND, Categories.PHYSICAL, 80, 100, 10)]
        Dig,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 100, 20)]
        Disable,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 70, 100, 10)]
        Dizzy_Punch,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, 30, 100, 30)]
        Double_Kick,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 15, 85, 10)]
        Double_Slap,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 15)]
        Double_Team,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 120, 100, 15)]
        Double_Edge,

        [MoveInfo(Types.DRAGON, Categories.SPECIAL, -1, 100, 10)]
        Dragon_Rage,

        [MoveInfo(Types.PSYCHIC, Categories.SPECIAL, 100, 100, 15)]
        Dream_Eater,

        [MoveInfo(Types.FLYING, Categories.PHYSICAL, 80, 100, 20)]
        Drill_Peck,

        [MoveInfo(Types.GROUND, Categories.PHYSICAL, 100, 100, 10)]
        Earthquake,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 100, 75, 10)]
        Egg_Bomb,

        [MoveInfo(Types.FIRE, Categories.SPECIAL, 40, 100, 25)]
        Ember,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 250, 100, 5)]
        Explosion,

        [MoveInfo(Types.FIRE, Categories.SPECIAL, 110, 85, 5)]
        Fire_Blast,

        [MoveInfo(Types.FIRE, Categories.PHYSICAL, 75, 100, 15)]
        Fire_Punch,

        [MoveInfo(Types.FIRE, Categories.SPECIAL, 35, 85, 15)]
        Fire_Spin,

        [MoveInfo(Types.GROUND, Categories.PHYSICAL, -1, 30, 5)]
        Fissure,

        [MoveInfo(Types.FIRE, Categories.SPECIAL, 90, 100, 15)]
        Flamethrower,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 100, 20)]
        Flash,

        [MoveInfo(Types.FLYING, Categories.PHYSICAL, 90, 95, 15)]
        Fly,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 30)]
        Focus_Energy,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 15, 85, 20)]
        Fury_Attack,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 18, 80, 15)]
        Fury_Swipes,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 100, 30)]
        Glare,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 100, 40)]
        Growl,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 20)]
        Growth,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, -1, 30, 5)]
        Guillotine,

        [MoveInfo(Types.FLYING, Categories.SPECIAL, 40, 100, 35)]
        Gust,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 30)]
        Harden,

        [MoveInfo(Types.ICE, Categories.STATUS, -1, -1, 30)]
        Haze,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 70, 100, 15)]
        Headbutt,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, 130, 90, 10)]
        High_Jump_Kick,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 65, 100, 25)]
        Horn_Attack,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, -1, 30, 5)]
        Horn_Drill,

        [MoveInfo(Types.WATER, Categories.SPECIAL, 110, 80, 5)]
        Hydro_Pump,

        [MoveInfo(Types.NORMAL, Categories.SPECIAL, 150, 90, 5)]
        Hyper_Beam,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 80, 90, 15)]
        Hyper_Fang,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, 60, 20)]
        Hypnosis,

        [MoveInfo(Types.ICE, Categories.SPECIAL, 90, 100, 10)]
        Ice_Beam,

        [MoveInfo(Types.ICE, Categories.PHYSICAL, 75, 100, 15)]
        Ice_Punch,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, 100, 95, 10)]
        Jump_Kick,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, 50, 100, 25)]
        Karate_Chop,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, 80, 15)]
        Kinesis,

        [MoveInfo(Types.BUG, Categories.PHYSICAL, 80, 100, 10)]
        Leech_Life,

        [MoveInfo(Types.GRASS, Categories.STATUS, -1, 90, 10)]
        Leech_Seed,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 100, 30)]
        Leer,

        [MoveInfo(Types.GHOST, Categories.PHYSICAL, 30, 100, 30)]
        Lick,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, -1, 30)]
        Light_Screen,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 75, 10)]
        Lovely_Kiss,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, -1, 100, 20)]
        Low_Kick,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, -1, 40)]
        Meditate,

        [MoveInfo(Types.GRASS, Categories.SPECIAL, 40, 100, 15)]
        Mega_Drain,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 120, 75, 5)]
        Mega_Kick,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 80, 85, 20)]
        Mega_Punch,


        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 10)]
        Metronome,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 10)]
        Mimic,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 10)]
        Minimize,

        [MoveInfo(Types.FLYING, Categories.STATUS, -1, -1, 20)]
        Mirror_Move,

        [MoveInfo(Types.ICE, Categories.STATUS, -1, -1, 30)]
        Mist,

        [MoveInfo(Types.GHOST, Categories.SPECIAL, -1, 100, 15)]
        Night_Shade,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 40, 100, 20)]
        Pay_Day,

        [MoveInfo(Types.FLYING, Categories.PHYSICAL, 35, 100, 35)]
        Peck,

        [MoveInfo(Types.GRASS, Categories.SPECIAL, 120, 100, 10)]
        Petal_Dance,

        [MoveInfo(Types.BUG, Categories.PHYSICAL, 25, 95, 20)]
        Pin_Missile,

        [MoveInfo(Types.POISON, Categories.STATUS, -1, 90, 40)]
        Poison_Gas,

        [MoveInfo(Types.POISON, Categories.STATUS, -1, 75, 35)]
        Poison_Powder,

        [MoveInfo(Types.POISON, Categories.PHYSICAL, 15, 100, 35)]
        Poison_Sting,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 40, 100, 35)]
        Pound,

        [MoveInfo(Types.PSYCHIC, Categories.SPECIAL, 65, 100, 20)]
        Psybeam,

        [MoveInfo(Types.PSYCHIC, Categories.SPECIAL, 90, 100, 10)]
        Psychic,

        [MoveInfo(Types.PSYCHIC, Categories.SPECIAL, -1, 100, 15)]
        Psywave,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 40, 100, 30)]
        Quick_Attack,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 20, 100, 20)]
        Rage,

        [MoveInfo(Types.GRASS, Categories.PHYSICAL, 55, 95, 25)]
        Razor_Leaf,

        [MoveInfo(Types.NORMAL, Categories.SPECIAL, 80, 100, 10)]
        Razor_Wind,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 5)]
        Recover,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, -1, 20)]
        Reflect,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 5)]
        Rest,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 20)]
        Roar,

        [MoveInfo(Types.ROCK, Categories.PHYSICAL, 75, 90, 10)]
        Rock_Slide,

        [MoveInfo(Types.ROCK, Categories.PHYSICAL, 50, 90, 15)]
        Rock_Throw,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, 60, 85, 15)]
        Rolling_Kick,

        [MoveInfo(Types.GROUND, Categories.STATUS, -1, 100, 15)]
        Sand_Attack,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 40, 100, 35)]
        Scratch,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 85, 40)]
        Screech,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, -1, 100, 20)]
        Seismic_Toss,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 200, 100, 5)]
        Self_Destruct,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 30)]
        Sharpen,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 55, 15)]
        Sing,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 130, 100, 10)]
        Skull_Bash,

        [MoveInfo(Types.FLYING, Categories.PHYSICAL, 140, 90, 5)]
        Sky_Attack,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 80, 75, 20)]
        Slam,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 70, 100, 20)]
        Slash,

        [MoveInfo(Types.GRASS, Categories.STATUS, -1, 75, 15)]
        Sleep_Powder,

        [MoveInfo(Types.POISON, Categories.SPECIAL, 65, 100, 20)]
        Sludge,

        [MoveInfo(Types.POISON, Categories.SPECIAL, 30, 70, 20)]
        Smog,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 100, 20)]
        Smokescreen,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 5)]
        Soft_Boiled,

        [MoveInfo(Types.GRASS, Categories.SPECIAL, 120, 100, 10)]
        Solar_Beam,

        [MoveInfo(Types.NORMAL, Categories.SPECIAL, -1, 90, 20)]
        Sonic_Boom,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 20, 100, 15)]
        Spike_Cannon,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 40)]
        Splash,

        [MoveInfo(Types.GRASS, Categories.STATUS, -1, 100, 15)]
        Spore,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 65, 100, 20)]
        Stomp,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 80, 100, 15)]
        Strength,

        [MoveInfo(Types.BUG, Categories.STATUS, -1, 95, 40)]
        String_Shot,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 50, 100, -1)]
        Struggle,

        [MoveInfo(Types.GRASS, Categories.STATUS, -1, 75, 30)]
        Stun_Spore,

        [MoveInfo(Types.FIGHTING, Categories.PHYSICAL, 80, 80, 20)]
        Submission,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 10)]
        Substitute,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, -1, 90, 10)]
        Super_Fang,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 55, 20)]
        Supersonic,

        [MoveInfo(Types.WATER, Categories.SPECIAL, 90, 100, 15)]
        Surf,

        [MoveInfo(Types.NORMAL, Categories.SPECIAL, 60, -1, 20)]
        Swift,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 20)]
        Swords_Dance,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 40, 100, 35)]
        Tackle,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, 100, 30)]
        Tail_Whip,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 90, 85, 20)]
        Take_Down,

        [MoveInfo(Types.PSYCHIC, Categories.STATUS, -1, -1, 20)]
        Teleport,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 90, 100, 10)]
        Thrash,

        [MoveInfo(Types.ELECTRIC, Categories.SPECIAL, 110, 70, 10)]
        Thunder,

        [MoveInfo(Types.ELECTRIC, Categories.PHYSICAL, 75, 100, 15)]
        Thunder_Punch,

        [MoveInfo(Types.ELECTRIC, Categories.SPECIAL, 40, 100, 30)]
        Thunder_Shock,

        [MoveInfo(Types.ELECTRIC, Categories.STATUS, -1, 90, 20)]
        Thunder_Wave,

        [MoveInfo(Types.ELECTRIC, Categories.SPECIAL, 90, 100, 15)]
        Thunderbolt,

        [MoveInfo(Types.POISON, Categories.STATUS, -1, 90, 10)]
        Toxic,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 10)]
        Transform,

        [MoveInfo(Types.NORMAL, Categories.SPECIAL, 80, 100, 10)]
        Tri_Attack,

        [MoveInfo(Types.BUG, Categories.PHYSICAL, 25, 100, 20)]
        Twineedle,

        [MoveInfo(Types.GRASS, Categories.PHYSICAL, 45, 100, 25)]
        Vine_Whip,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 55, 100, 30)]
        Vise_Grip,

        [MoveInfo(Types.WATER, Categories.SPECIAL, 40, 100, 25)]
        Water_Gun,

        [MoveInfo(Types.WATER, Categories.PHYSICAL, 80, 100, 15)]
        Waterfall,

        [MoveInfo(Types.NORMAL, Categories.STATUS, -1, -1, 20)]
        Whirlwind,

        [MoveInfo(Types.FLYING, Categories.PHYSICAL, 60, 100, 35)]
        Wing_Attack,

        [MoveInfo(Types.WATER, Categories.STATUS, -1, -1, 40)]
        Withdraw,

        [MoveInfo(Types.NORMAL, Categories.PHYSICAL, 15, 90, 20)]
        Wrap,
    }
}
