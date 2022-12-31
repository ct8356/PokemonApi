namespace PokemonApi
{
    public class FlavourTextEntry
    {
        public string? flavor_text { get; set; }

        public Language? Language { get; set; }

        public override string? ToString()
        {
            return Language?.name;
        }
    }
}