using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POKEMON_RED
{
    public class Battle
    {
        private readonly Pokémon opponentPokémon;
        

        public Battle(Pokémon trainerPokémon, Pokémon opponentPokémon)
        {
            this.opponentPokémon = opponentPokémon;
        }

        //public bool Attack(Moves moveToUseOnOpponent)
        //{
        //    var IsDead = opponentPokémon.Attack(moveToUseOnOpponent);

        //    int damage = (())

        //    return IsDead;
        //}

        public double CalculateDamage(Pokémon hittingPokemon, Pokémon defendingPokemon, Moves move)
        {
            MoveInfoHelper moveInfoHelper = new();

            MoveInfoAttribute moveInfo = moveInfoHelper.GetMoveInfoAttribute(Moves.Double_Slap);

            Random rnd = new Random(DateTime.Now.Microsecond);

            int movePower = moveInfo.Power;
            Types moveTyping = moveInfo.Typing;
            Categories moveCategory = moveInfo.Category;
            int moveAccuracy = moveInfo.Accuracy;
            int movePP = moveInfo.PP;
            double random = rnd.Next(217, 255) / 255;

            double AttackDefenseRatio = GetAttackDefenseRatio(hittingPokemon, defendingPokemon, moveCategory);
            double STAB = GetSTAB(hittingPokemon, moveTyping);

            int critical = 1;
            double damage = ((((((2 * hittingPokemon.level * critical / 5) + 2) * movePower * AttackDefenseRatio) / 50) + 2) * STAB * GetSuperEffectiveness(defendingPokemon.type[0], moveTyping) * GetSuperEffectiveness(defendingPokemon.type[1], moveTyping) * random);
            return damage;
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

        public double GetSuperEffectiveness(Types defendingPokemonType, Types hittingMoveType)
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
    }
}
