using System.Text.Json;

namespace POKEMON_RED
{
    internal class Program
    {
        static void Main()
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //string filePath = Path.Combine(path, "saveFile.txt");

            ////filePath = Path.Combine("d:\\temp", "saveFile.txt");

            //using (StreamWriter sw = new StreamWriter(filePath))
            //{
            //    sw.Write("test");
            //}

            Game game = new Game();
            Pokémon pokemon = new(PokémonNames.Charmander, 5, Genders.Male);

            //var str = JsonSerializer.Serialize(pokemon);


            //var newObj = JsonSerializer.Deserialize<Pokémon>(str);



            //Console.WriteLine($" Your pokémon is: {pokemon.name} {Environment.NewLine} Gender: {pokemon.gender} {Environment.NewLine} " +
            //    $"LVL: {pokemon.level} {Environment.NewLine}  {Environment.NewLine} Moves: {Environment.NewLine} {pokemon.moves[0]}" +
            //    $"{Environment.NewLine} {pokemon.moves[1]} {Environment.NewLine} {pokemon.moves[2]}" +
            //    $"{Environment.NewLine} {pokemon.moves[3]} {Environment.NewLine} {Environment.NewLine}Stats: {Environment.NewLine}  HP: {pokemon.stats[0]} {Environment.NewLine} " +
            //    $"ATK: {pokemon.stats[1]} {Environment.NewLine} DEF: {pokemon.stats[2]} {Environment.NewLine} " +
            //    $"SPA: {pokemon.stats[3]} {Environment.NewLine} SPD: {pokemon.stats[4]} {Environment.NewLine} " +
            //    $"SPE: {pokemon.stats[5]} {Environment.NewLine} {Environment.NewLine} IVs: {Environment.NewLine}  HP: {pokemon.IVs[0]} {Environment.NewLine} " +
            //    $"ATK: {pokemon.IVs[1]} {Environment.NewLine} DEF: {pokemon.IVs[2]} {Environment.NewLine} " +
            //    $"SPA: {pokemon.IVs[3]} {Environment.NewLine} SPD: {pokemon.IVs[4]} {Environment.NewLine} " +
            //    $"SPE: {pokemon.IVs[5]} {Environment.NewLine} ");


        }
    }
}
