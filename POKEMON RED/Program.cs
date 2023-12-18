namespace POKEMON_RED
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pokémon charmander = new Pokémon(pokémonNames.Charmander, 5, genders.Male);
            Console.WriteLine($" Your pokémon is:{charmander.name} {Environment.NewLine} Gender: {charmander.gender} {Environment.NewLine} " +
                $"LVL: {charmander.level} {Environment.NewLine}  {Environment.NewLine} Stats: {Environment.NewLine}  HP: {charmander.stats[0]} {Environment.NewLine} " +
                $"ATK: {charmander.stats[1]} {Environment.NewLine} DEF: {charmander.stats[2]} {Environment.NewLine} " +
                $"SPA: {charmander.stats[3]} {Environment.NewLine} SPD: {charmander.stats[4]} {Environment.NewLine} " +
                $"SPE: {charmander.stats[5]} {Environment.NewLine} {Environment.NewLine} IVs: {Environment.NewLine} HP: {charmander.IVs[0]} {Environment.NewLine} " +
                $"ATK: {charmander.IVs[1]} {Environment.NewLine} DEF: {charmander.IVs[2]} {Environment.NewLine} " +
                $"SPA: {charmander.IVs[3]} {Environment.NewLine} SPD: {charmander.IVs[4]} {Environment.NewLine} " +
                $"SPE: {charmander.IVs[5]} {Environment.NewLine} ");


        }
    }
}
