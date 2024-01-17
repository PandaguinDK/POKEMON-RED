using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POKEMON_RED
{
    abstract class GymLeader
    {
        Badges GiveBadge(TrainerNames trainer)
        {
            switch (trainer)
            {
                case TrainerNames.BROCK:
                    return Badges.BOULDER_BADGE;
                case TrainerNames.MISTY:
                    return Badges.CASCADE_BADGE;
                case TrainerNames.LT_SURGE:
                    return Badges.THUNDER_BADGE;
                case TrainerNames.ERIKA:
                    return Badges.RAINBOW_BADGE;
                case TrainerNames.KOGA:
                    return Badges.SOUL_BADGE;
                case TrainerNames.SABRINA:
                    return Badges.MARSH_BADGE;
                case TrainerNames.BLAINE:
                    return Badges.VOLCANO_BADGE;
                case TrainerNames.GIOVANNI:
                    return Badges.EARTH_BADGE;
                default:
                    return Badges.NONE;
            }
        }
    }
    enum Badges
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
}
