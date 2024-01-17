using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POKEMON_RED
{
    public abstract class Trainer
    {
        public List<Pokémon> trainerPokemons { get; set; }
        public TrainerNames trainerName { get; set; }
        public string strTrainerName { get; set; }
        public Trainer(List<Pokémon> TrainerPokemons, TrainerNames TrainerName)
        {
            this.trainerPokemons = TrainerPokemons;
            this.trainerName = TrainerName;
        }
        public Trainer(List<Pokémon> TrainerPokemons, string TrainerName)
        {
            this.trainerPokemons = TrainerPokemons;
            this.strTrainerName = TrainerName;
        }
        public abstract void GreetPlayer();
        public abstract void ChallengePlayer();
        public abstract void CongratulatePlayer();
        public abstract void BeatPlayer();

    }

    public class Rival : Trainer
    {
        public Rival(List<Pokémon> rivalPokemons, string rivalName) : base(rivalPokemons, rivalName) { }
        public override void GreetPlayer()
        {
            Console.WriteLine("My Pokémon looks stronger than yours!");
        }
        public override void ChallengePlayer()
        {
            Console.WriteLine("Let's battle!");
        }
        public override void CongratulatePlayer()
        {
            Console.WriteLine("I guess I will have to train harder");
        }
        public override void BeatPlayer()
        {
            Console.WriteLine("Train harder, and maybe you can beat me");
        }
    }

    public enum Badges
    {
        NONE,
        BOULDER_BADGE,
        CASCADE_BADGE,
        THUNDER_BADGE,
        RAINBOW_BADGE,
        SOUL_BADGE,
        MARSH_BADGE,
        VOLCANO_BADGE,
        EARTH_BADGE
    }

    public enum TrainerNames
    {
        YOUNGSTER,
        BUG_CATCHER,
        LASS,
        SAILOR,
        JR_TRAINER_MALE,
        JR_TRAINER_FEMALE,
        POKéMANIAC,
        SUPER_NERD,
        HIKER,
        BIKER,
        BURGLAR,
        ENGINEER,
        JUGGLER,
        FISHERMAN,
        SWIMMER,
        CUE_BALL,
        GAMBLER,
        BEAUTY,
        PSYCHIC,
        ROCKER,
        TAMER,
        BIRD_KEEPER,
        BLACKBELT,
        RIVAL1,
        PROF_OAK,
        CHIEF,
        SCIENTIST,
        GIOVANNI,
        ROCKET,
        COOLTRAINER,
        COOL_TRAINER,
        BRUNO,
        BROCK,
        MISTY,
        LT_SURGE,
        ERIKA,
        KOGA,
        BLAINE,
        SABRINA,
        GENTLEMAN,
        RIVAL2,
        RIVAL3,
        LORELEI,
        CHANNELER,
        AGATHA,
        LANCE
    }
}
