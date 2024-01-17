using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POKEMON_RED
{
    class Trainer
    {
        public List<Pokémon> trainerPokemons { get; set; }
        public TrainerNames trainerName { get; set; }
        public bool isGymLeader { get; set; }
        public Trainer(List<Pokémon> TrainerPokemons, TrainerNames TrainerName)
        {
            this.trainerPokemons = TrainerPokemons;
            this.trainerName = TrainerName;
        } 
    }


    enum TrainerNames
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
