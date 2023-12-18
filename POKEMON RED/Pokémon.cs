using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POKEMON_RED
{
    internal class Pokémon
    {
        public pokémonNames name;
        public List<types> type;
        public int level;
        public genders gender;
        public int[] stats = new int[6];
        public int[] statsChanges = new int[6];
        public int[] EVs = new int[6];
        public int[] IVs = new int[6];
        public status status;
        public PokémonTypesAttribute pokemonTypes = new PokémonTypesAttribute();
        public Dictionary<string, int> baseStats = new Dictionary<string, int>();

        
        public Pokémon(pokémonNames Name, int Level, genders Gender)
        {
            this.name = Name;
            this.type = pokemonTypes.GetPokemonTyping();
            this.level = Level;
            this.gender = Gender;
            this.EVs = new int[] { 0, 0, 0, 0, 0, 0 }
;           this.stats = getStats();

        }

        public int[] getStats()
        {

            int[] stats = new int[6];
            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 6; i++)
            {
                IVs[i] = rnd.Next(0, 31);
                stats[i] = ((2 * getBaseStats()[i] + 15) * level) / 100 + 5 + (IVs[i] / 100 * level);
            }
            return stats;
        }

        public int[] getBaseStats()
        {
            int[] baseStats = new int[6];
            switch (name)
            {
                case pokémonNames.Bulbasaur:
                    baseStats[0] = 45;
                    baseStats[1] = 49;
                    baseStats[2] = 49;
                    baseStats[3] = 65;
                    baseStats[4] = 65;
                    baseStats[5] = 45;
                    break;

                case pokémonNames.Ivysaur:
                    baseStats[0] = 60;
                    baseStats[1] = 62;
                    baseStats[2] = 63;
                    baseStats[3] = 80;
                    baseStats[4] = 80;
                    baseStats[5] = 60;
                    break;

                case pokémonNames.Venusaur:
                    baseStats[0] = 80;
                    baseStats[1] = 82;
                    baseStats[2] = 83;
                    baseStats[3] = 100;
                    baseStats[4] = 100;
                    baseStats[5] = 80;
                    break;

                case pokémonNames.Charmander:
                    baseStats[0] = 39;
                    baseStats[1] = 52;
                    baseStats[2] = 43;
                    baseStats[3] = 60;
                    baseStats[4] = 50;
                    baseStats[5] = 65;
                    break;

                case pokémonNames.Charmeleon:
                    baseStats[0] = 58;
                    baseStats[1] = 64;
                    baseStats[2] = 58;
                    baseStats[3] = 80;
                    baseStats[4] = 65;
                    baseStats[5] = 80;
                    break;

                case pokémonNames.Charizard:
                    baseStats[0] = 78;
                    baseStats[1] = 84;
                    baseStats[2] = 78;
                    baseStats[3] = 109;
                    baseStats[4] = 85;
                    baseStats[5] = 100;
                    break;

                case pokémonNames.Squirtle:
                    baseStats[0] = 44;
                    baseStats[1] = 48;
                    baseStats[2] = 65;
                    baseStats[3] = 50;
                    baseStats[4] = 64;
                    baseStats[5] = 43;
                    break;

                case pokémonNames.Wartortle:
                    baseStats[0] = 59;
                    baseStats[1] = 63;
                    baseStats[2] = 80;
                    baseStats[3] = 65;
                    baseStats[4] = 80;
                    baseStats[5] = 58;
                    break;

                case pokémonNames.Blastoise:
                    baseStats[0] = 79;
                    baseStats[1] = 83;
                    baseStats[2] = 100;
                    baseStats[3] = 85;
                    baseStats[4] = 105;
                    baseStats[5] = 78;
                    break;

                case pokémonNames.Caterpie:
                    baseStats[0] = 45;
                    baseStats[1] = 30;
                    baseStats[2] = 45;
                    baseStats[3] = 20;
                    baseStats[4] = 20;
                    baseStats[5] = 45;
                    break;

                case pokémonNames.Metapod:
                    baseStats[0] = 50;
                    baseStats[1] = 20;
                    baseStats[2] = 55;
                    baseStats[3] = 25;
                    baseStats[4] = 25;
                    baseStats[5] = 30;
                    break;

                case pokémonNames.Butterfree:
                    baseStats[0] = 60;
                    baseStats[1] = 45;
                    baseStats[2] = 50;
                    baseStats[3] = 90;
                    baseStats[4] = 80;
                    baseStats[5] = 70;
                    break;

                case pokémonNames.Weedle:
                    baseStats[0] = 40;
                    baseStats[1] = 35;
                    baseStats[2] = 30;
                    baseStats[3] = 20;
                    baseStats[4] = 20;
                    baseStats[5] = 50;
                    break;

                case pokémonNames.Kakuna:
                    baseStats[0] = 45;
                    baseStats[1] = 25;
                    baseStats[2] = 50;
                    baseStats[3] = 25;
                    baseStats[4] = 25;
                    baseStats[5] = 35;
                    break;

                case pokémonNames.Beedrill:
                    baseStats[0] = 65;
                    baseStats[1] = 90;
                    baseStats[2] = 40;
                    baseStats[3] = 45;
                    baseStats[4] = 80;
                    baseStats[5] = 75;
                    break;

                case pokémonNames.Pidgey:
                    baseStats[0] = 40;
                    baseStats[1] = 45;
                    baseStats[2] = 40;
                    baseStats[3] = 35;
                    baseStats[4] = 35;
                    baseStats[5] = 56;
                    break;

                case pokémonNames.Pidgeotto:
                    baseStats[0] = 63;
                    baseStats[1] = 60;
                    baseStats[2] = 55;
                    baseStats[3] = 50;
                    baseStats[4] = 50;
                    baseStats[5] = 71;
                    break;

                case pokémonNames.Pidgeot:
                    baseStats[0] = 83;
                    baseStats[1] = 80;
                    baseStats[2] = 75;
                    baseStats[3] = 70;
                    baseStats[4] = 70;  
                    baseStats[5] = 101;
                    break;


            }
            return baseStats;
        }
    }


    public class MoveInfoAttribute : Attribute
    {
        public types Typing { get; }
        public categories Category { get; }
        public int Power { get; }
        public int Accuracy { get; }
        public int PP { get; }


        public MoveInfoAttribute(types typing, categories category, int power, int accuracy, int pp)
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
        public List<types> Typing = new List<types>();

        public PokémonTypesAttribute()
        {

        }

        public PokémonTypesAttribute(types typing, types typing2)
        {
            this.Typing.Add(typing);
            this.Typing.Add(typing2);
        }
        public List<types> GetPokemonTyping()
        {
            return Typing;
        }
    }


    public enum pokémonNames
    {
        [PokémonTypes(types.GRASS, types.POISON)]
        Bulbasaur,
        Ivysaur,
        Venusaur,

        [PokémonTypes(types.FIRE, types.NONE)]
        Charmander,
        Charmeleon,

        [PokémonTypes(types.FIRE, types.FLYING)]
        Charizard,

        [PokémonTypes(types.WATER, types.NONE)]
        Squirtle,
        Wartortle,
        Blastoise,

        [PokémonTypes(types.BUG, types.NONE)]
        Caterpie,
        Metapod,

        [PokémonTypes(types.BUG, types.FLYING)]
        Butterfree,

        [PokémonTypes(types.BUG, types.POISON)]
        Weedle,
        Kakuna,
        Beedrill,

        [PokémonTypes(types.NORMAL, types.FLYING)]
        Pidgey,
        Pidgeotto,
        Pidgeot,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Rattata,
        Raticate,
        Spearow,
        Fearow,

        [PokémonTypes(types.POISON, types.NONE)]
        Ekans,
        Arbok,

        [PokémonTypes(types.ELECTRIC, types.NONE)]
        Pikachu,
        Raichu,

        [PokémonTypes(types.GROUND, types.NONE)]
        Sandshrew,
        Sandslash,

        [PokémonTypes(types.POISON, types.NONE)]
        Nidoran_Male,
        Nidorina,
        Nidoqueen,
        Nidoran_Female,
        Nidorino,
        Nidoking,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Clefairy,
        Clefable,

        [PokémonTypes(types.FIRE, types.NONE)]
        Vulpix,
        Ninetales,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Jigglypuff,
        Wigglytuff,

        [PokémonTypes(types.POISON, types.FLYING)]
        Zubat,
        Golbat,

        [PokémonTypes(types.GRASS, types.POISON)]
        Oddish,
        Gloom,
        Vileplume,

        [PokémonTypes(types.BUG, types.GRASS)]
        Paras,
        Parasect,

        [PokémonTypes(types.BUG, types.POISON)]
        Venonat,
        Venomoth,

        [PokémonTypes(types.GROUND, types.NONE)]
        Diglett,
        Dugtrio,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Meowth,
        Persian,

        [PokémonTypes(types.WATER, types.NONE)]
        Psyduck,
        Golduck,

        [PokémonTypes(types.FIGHTING, types.NONE)]
        Mankey,
        Primeape,

        [PokémonTypes(types.FIRE, types.NONE)]
        Growlithe,
        Arcanine,

        [PokémonTypes(types.WATER, types.NONE)]
        Poliwag,
        Poliwhirl,
        Poliwrath,

        [PokémonTypes(types.PSYCHIC, types.NONE)]
        Abra,
        Kadabra,
        Alakazam,

        [PokémonTypes(types.FIGHTING, types.NONE)]
        Machop,
        Machoke,
        Machamp,

        [PokémonTypes(types.GRASS, types.POISON)]
        Bellsprout,
        Weepinbell,
        Victreebel,

        [PokémonTypes(types.WATER, types.POISON)]
        Tentacool,
        Tentacruel,

        [PokémonTypes(types.ROCK, types.GROUND)]
        Geodude,
        Graveler,
        Golem,

        [PokémonTypes(types.FIRE, types.NONE)]
        Ponyta,
        Rapidash,

        [PokémonTypes(types.WATER, types.PSYCHIC)]
        Slowpoke,
        Slowbro,

        [PokémonTypes(types.ELECTRIC, types.NONE)]
        Magnemite,
        Magneton,

        [PokémonTypes(types.NORMAL, types.FLYING)]
        Farfetchd,
        Doduo,
        Dodrio,

        [PokémonTypes(types.WATER, types.NONE)]
        Seel,
        Dewgong,

        [PokémonTypes(types.POISON, types.NONE)]
        Grimer,
        Muk,

        [PokémonTypes(types.WATER, types.NONE)]
        Shellder,
        Cloyster,

        [PokémonTypes(types.GHOST, types.POISON)]
        Gastly,
        Haunter,
        Gengar,

        [PokémonTypes(types.ROCK, types.GROUND)]
        Onix,

        [PokémonTypes(types.PSYCHIC, types.NONE)]
        Drowzee,
        Hypno,

        [PokémonTypes(types.WATER, types.NONE)]
        Krabby,
        Kingler,

        [PokémonTypes(types.ELECTRIC, types.NONE)]
        Voltorb,
        Electrode,

        [PokémonTypes(types.GRASS, types.PSYCHIC)]
        Exeggcute,
        Exeggutor,

        [PokémonTypes(types.GROUND, types.NONE)]
        Cubone,
        Marowak,

        [PokémonTypes(types.FIGHTING, types.NONE)]
        Hitmonlee,
        Hitmonchan,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Lickitung,

        [PokémonTypes(types.POISON, types.NONE)]
        Koffing,
        Weezing,

        [PokémonTypes(types.GROUND, types.ROCK)]
        Rhyhorn,
        Rhydon,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Chansey,

        [PokémonTypes(types.GRASS, types.NONE)]
        Tangela,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Kangaskhan,

        [PokémonTypes(types.WATER, types.NONE)]
        Horsea,
        Seadra,

        [PokémonTypes(types.WATER, types.NONE)]
        Goldeen,
        Seaking,

        [PokémonTypes(types.WATER, types.NONE)]
        Staryu,
        Starmie,

        [PokémonTypes(types.PSYCHIC, types.NONE)]
        Mr_Mime,

        [PokémonTypes(types.BUG, types.FLYING)]
        Scyther,

        [PokémonTypes(types.ICE, types.PSYCHIC)]
        Jynx,

        [PokémonTypes(types.ELECTRIC, types.NONE)]
        Electabuzz,

        [PokémonTypes(types.FIRE, types.NONE)]
        Magmar,

        [PokémonTypes(types.BUG, types.NONE)]
        Pinsir,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Tauros,

        [PokémonTypes(types.WATER, types.NONE)]
        Magikarp,
        Gyarados,

        [PokémonTypes(types.WATER, types.ICE)]
        Lapras,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Ditto,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Eevee,
        Vaporeon,
        Jolteon,
        Flareon,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Porygon,

        [PokémonTypes(types.ROCK, types.WATER)]
        Omanyte,
        Omastar,

        [PokémonTypes(types.ROCK, types.WATER)]
        Kabuto,
        Kabutops,

        [PokémonTypes(types.ROCK, types.FLYING)]
        Aerodactyl,

        [PokémonTypes(types.NORMAL, types.NONE)]
        Snorlax,

        [PokémonTypes(types.ICE, types.FLYING)]
        Articuno,

        [PokémonTypes(types.FIRE, types.FLYING)]
        Moltres,

        [PokémonTypes(types.DRAGON, types.NONE)]
        Dratini,
        Dragonair,
        Dragonite,

        [PokémonTypes(types.PSYCHIC, types.NONE)]
        Mewtwo,
        Mew,
    }

    public enum status
    {
        Burned,
        Paralyzed,
        Frozen,
        Asleep,
        Poisoned,
        None
    }

    public enum genders
    {
        Male,
        Female,
        Other
    }

    public  enum categories
    {
        SPECIAL,
        PHYSICAL,
        STATUS,
    }

    public enum types
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
        [MoveInfo(types.GRASS, categories.SPECIAL , 20, 100, 25)]
        Absorb,

        [MoveInfo(types.POISON, categories.SPECIAL, 40, 100, 30)]
        Acid,

        [MoveInfo(types.POISON, categories.STATUS, -1, -1, 20)]
        Acid_Armor,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, -1, 30)]
        Agility,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, -1, 20)]
        Amnesia,

        [MoveInfo(types.ICE, categories.SPECIAL, 65, 100, 20)]
        Aurora_Beam,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 15, 85, 20)]
        Barrage,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, -1, 20)]
        Barrier,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, -1, -1, 10)]
        Bide,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 15, 85, 20)]
        Bind,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 60, 100, 25)]
        Bite,

        [MoveInfo(types.ICE, categories.SPECIAL, 110, 70, 5)]
        Blizzard,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 85, 100, 15)]
        Body_Slam,

        [MoveInfo(types.GROUND, categories.PHYSICAL, 65, 85, 20)]
        Bone_Club,

        [MoveInfo(types.GROUND, categories.PHYSICAL, 50, 90, 10)]
        Bonemerang,

        [MoveInfo(types.WATER, categories.SPECIAL, 40, 100, 30)]
        Bubble,

        [MoveInfo(types.WATER, categories.SPECIAL, 65, 100, 20)]
        Bubble_Beam,

        [MoveInfo(types.WATER, categories.SPECIAL, 35, 85, 15)]
        Clamp,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 18, 85, 15)]
        Comet_Punch,

        [MoveInfo(types.GHOST, categories.STATUS, -1, 100, 10)]
        Confuse_Ray,

        [MoveInfo(types.PSYCHIC, categories.SPECIAL , 50, 100, 25)]
        Confusion,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 100, 30)]
        Constrict,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 20)]
        Conversion,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, -1, 100, 20)]
        Counter,

        [MoveInfo(types.WATER, categories.PHYSICAL, 100, 90, 10)]
        Crabhammer,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 50, 95, 30)]
        Cut,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 40)]
        Defense_Curl,

        [MoveInfo(types.GROUND, categories.PHYSICAL, 80, 100, 10)]
        Dig,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 100, 20)]
        Disable,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 70, 100, 10)]
        Dizzy_Punch,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, 30, 100, 30)]
        Double_Kick,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 15, 85, 10)]
        Double_Slap,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 15)]
        Double_Team,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 120, 100, 15)]
        Double_Edge,

        [MoveInfo(types.DRAGON, categories.SPECIAL, -1, 100, 10)]
        Dragon_Rage,

        [MoveInfo(types.PSYCHIC, categories.SPECIAL, 100, 100, 15)]
        Dream_Eater,

        [MoveInfo(types.FLYING, categories.PHYSICAL, 80, 100, 20)]
        Drill_Peck,

        [MoveInfo(types.GROUND, categories.PHYSICAL, 100, 100, 10)]
        Earthquake,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 100, 75, 10)]
        Egg_Bomb,

        [MoveInfo(types.FIRE, categories.SPECIAL, 40, 100, 25)]
        Ember,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 250, 100, 5)]
        Explosion,

        [MoveInfo(types.FIRE, categories.SPECIAL, 110, 85, 5)]
        Fire_Blast,

        [MoveInfo(types.FIRE, categories.PHYSICAL, 75, 100, 15)]
        Fire_Punch,

        [MoveInfo(types.FIRE, categories.SPECIAL, 35, 85, 15)]
        Fire_Spin,

        [MoveInfo(types.GROUND, categories.PHYSICAL, -1, 30, 5)]
        Fissure,

        [MoveInfo(types.FIRE, categories.SPECIAL, 90, 100, 15)]
        Flamethrower,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 100, 20)]
        Flash,

        [MoveInfo(types.FLYING, categories.PHYSICAL, 90, 95, 15)]
        Fly,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 30)]
        Focus_Energy,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 15, 85, 20)]
        Fury_Attack,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 18, 80, 15)]
        Fury_Swipes,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 100, 30)]
        Glare,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 100, 40)]
        Growl,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 20)]
        Growth,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, -1, 30, 5)]
        Guillotine,

        [MoveInfo(types.FLYING, categories.SPECIAL, 40, 100, 35)]
        Gust,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 30)]
        Harden,

        [MoveInfo(types.ICE, categories.STATUS, -1, -1, 30)]
        Haze,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 70, 100, 15)]
        Headbutt,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, 130, 90, 10)]
        High_Jump_Kick,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 65, 100, 25)]
        Horn_Attack,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, -1, 30, 5)]
        Horn_Drill,

        [MoveInfo(types.WATER, categories.SPECIAL, 110, 80, 5)]
        Hydro_Pump,

        [MoveInfo(types.NORMAL, categories.SPECIAL, 150, 90, 5)]
        Hyper_Beam,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 80, 90, 15)]
        Hyper_Fang,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, 60, 20)]
        Hypnosis,

        [MoveInfo(types.ICE, categories.SPECIAL, 90, 100, 10)]
        Ice_Beam,

        [MoveInfo(types.ICE, categories.PHYSICAL, 75, 100, 15)]
        Ice_Punch,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, 100, 95, 10)]
        Jump_Kick,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, 50, 100, 25)]
        Karate_Chop,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, 80, 15)]
        Kinesis,

        [MoveInfo(types.BUG, categories.PHYSICAL, 80, 100, 10)]
        Leech_Life,

        [MoveInfo(types.GRASS, categories.STATUS, -1, 90, 10)]
        Leech_Seed,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 100, 30)]
        Leer,

        [MoveInfo(types.GHOST, categories.PHYSICAL, 30, 100, 30)]
        Lick,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, -1, 30)]
        Light_Screen,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 75, 10)]
        Lovely_Kiss,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, -1, 100, 20)]
        Low_Kick,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, -1, 40)]
        Meditate,

        [MoveInfo(types.GRASS, categories.SPECIAL, 40, 100, 15)]
        Mega_Drain,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 120, 75, 5)]
        Mega_Kick,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 80, 85, 20)]
        Mega_Punch,


        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 10)]
        Metronome,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 10)]
        Mimic,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 10)]
        Minimize,

        [MoveInfo(types.FLYING, categories.STATUS, -1, -1, 20)]
        Mirror_Move,

        [MoveInfo(types.ICE, categories.STATUS, -1, -1, 30)]
        Mist,

        [MoveInfo(types.GHOST, categories.SPECIAL, -1, 100, 15)]
        Night_Shade,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 40, 100, 20)]
        Pay_Day,

        [MoveInfo(types.FLYING, categories.PHYSICAL, 35, 100, 35)]
        Peck,

        [MoveInfo(types.GRASS, categories.SPECIAL, 120, 100, 10)]
        Petal_Dance,

        [MoveInfo(types.BUG, categories.PHYSICAL, 25, 95, 20)]
        Pin_Missile,

        [MoveInfo(types.POISON, categories.STATUS, -1, 90, 40)]
        Poison_Gas,

        [MoveInfo(types.POISON, categories.STATUS, -1, 75, 35)]
        Poison_Powder,

        [MoveInfo(types.POISON, categories.PHYSICAL, 15, 100, 35)]
        Poison_Sting,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 40, 100, 35)]
        Pound,

        [MoveInfo(types.PSYCHIC, categories.SPECIAL, 65, 100, 20)]
        Psybeam,

        [MoveInfo(types.PSYCHIC, categories.SPECIAL, 90, 100, 10)]
        Psychic,

        [MoveInfo(types.PSYCHIC, categories.SPECIAL, -1, 100, 15)]
        Psywave,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 40, 100, 30)]
        Quick_Attack,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 20, 100, 20)]
        Rage,

        [MoveInfo(types.GRASS, categories.PHYSICAL, 55, 95, 25)]
        Razor_Leaf,

        [MoveInfo(types.NORMAL, categories.SPECIAL, 80, 100, 10)]
        Razor_Wind,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 5)]
        Recover,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, -1, 20)]
        Reflect,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 5)]
        Rest,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 20)]
        Roar,

        [MoveInfo(types.ROCK, categories.PHYSICAL, 75, 90, 10)]
        Rock_Slide,

        [MoveInfo(types.ROCK, categories.PHYSICAL, 50, 90, 15)]
        Rock_Throw,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, 60, 85, 15)]
        Rolling_Kick,

        [MoveInfo(types.GROUND, categories.STATUS, -1, 100, 15)]
        Sand_Attack,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 40, 100, 35)]
        Scratch,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 85, 40)]
        Screech,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, -1, 100, 20)]
        Seismic_Toss,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 200, 100, 5)]
        Self_Destruct,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 30)]
        Sharpen,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 55, 15)]
        Sing,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 130, 100, 10)]
        Skull_Bash,

        [MoveInfo(types.FLYING, categories.PHYSICAL, 140, 90, 5)]
        Sky_Attack,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 80, 75, 20)]
        Slam,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 70, 100, 20)]
        Slash,

        [MoveInfo(types.GRASS, categories.STATUS, -1, 75, 15)]
        Sleep_Powder,

        [MoveInfo(types.POISON, categories.SPECIAL, 65, 100, 20)]
        Sludge,

        [MoveInfo(types.POISON, categories.SPECIAL, 30, 70, 20)]
        Smog,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 100, 20)]
        Smokescreen,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 5)]
        Soft_Boiled,

        [MoveInfo(types.GRASS, categories.SPECIAL, 120, 100, 10)]
        Solar_Beam,

        [MoveInfo(types.NORMAL, categories.SPECIAL, -1, 90, 20)]
        Sonic_Boom,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 20, 100, 15)]
        Spike_Cannon,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 40)]
        Splash,

        [MoveInfo(types.GRASS, categories.STATUS, -1, 100, 15)]
        Spore,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 65, 100, 20)]
        Stomp,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 80, 100, 15)]
        Strength,

        [MoveInfo(types.BUG, categories.STATUS, -1, 95, 40)]
        String_Shot,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 50, 100, -1)]
        Struggle,

        [MoveInfo(types.GRASS, categories.STATUS, -1, 75, 30)]
        Stun_Spore,

        [MoveInfo(types.FIGHTING, categories.PHYSICAL, 80, 80, 20)]
        Submission,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 10)]
        Substitute,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, -1, 90, 10)]
        Super_Fang,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 55, 20)]
        Supersonic,

        [MoveInfo(types.WATER, categories.SPECIAL, 90, 100, 15)]
        Surf,

        [MoveInfo(types.NORMAL, categories.SPECIAL, 60, -1, 20)]
        Swift,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 20)]
        Swords_Dance,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 40, 100, 35)]
        Tackle,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, 100, 30)]
        Tail_Whip,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 90, 85, 20)]
        Take_Down,

        [MoveInfo(types.PSYCHIC, categories.STATUS, -1, -1, 20)]
        Teleport,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 90, 100, 10)]
        Thrash,

        [MoveInfo(types.ELECTRIC, categories.SPECIAL, 110, 70, 10)]
        Thunder,

        [MoveInfo(types.ELECTRIC, categories.PHYSICAL, 75, 100, 15)]
        Thunder_Punch,

        [MoveInfo(types.ELECTRIC, categories.SPECIAL, 40, 100, 30)]
        Thunder_Shock,

        [MoveInfo(types.ELECTRIC, categories.STATUS, -1, 90, 20)]
        Thunder_Wave,

        [MoveInfo(types.ELECTRIC, categories.SPECIAL, 90, 100, 15)]
        Thunderbolt,

        [MoveInfo(types.POISON, categories.STATUS, -1, 90, 10)]
        Toxic,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 10)]
        Transform,

        [MoveInfo(types.NORMAL, categories.SPECIAL, 80, 100, 10)]
        Tri_Attack,

        [MoveInfo(types.BUG, categories.PHYSICAL, 25, 100, 20)]
        Twineedle,

        [MoveInfo(types.GRASS, categories.PHYSICAL, 45, 100, 25)]
        Vine_Whip,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 55, 100, 30)]
        Vise_Grip,

        [MoveInfo(types.WATER, categories.SPECIAL, 40, 100, 25)]
        Water_Gun,

        [MoveInfo(types.WATER, categories.PHYSICAL, 80, 100, 15)]
        Waterfall,

        [MoveInfo(types.NORMAL, categories.STATUS, -1, -1, 20)]
        Whirlwind,

        [MoveInfo(types.FLYING, categories.PHYSICAL, 60, 100, 35)]
        Wing_Attack,

        [MoveInfo(types.WATER, categories.STATUS, -1, -1, 40)]
        Withdraw,

        [MoveInfo(types.NORMAL, categories.PHYSICAL, 15, 90, 20)]
        Wrap,
    }
}
