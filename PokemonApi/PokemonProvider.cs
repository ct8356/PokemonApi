using System.Net.Http.Headers;

namespace PokemonApi
{
    public interface IPokemonProvider
    {
        PokeApiPokemon Get(string name);
    }

    internal class PokemonProvider
    {
        internal PokeApiPokemon Get(string name)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(
                $"https://pokeapi.co");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync($"/api/v2/pokemon-species/{name}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<PokeApiPokemon>().Result;
            }
            return new PokeApiPokemon();
        }
    }
}