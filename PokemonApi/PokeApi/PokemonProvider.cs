using System.Net.Http.Headers;

namespace PokemonApi
{
    public interface IPokemonProvider
    {
        Task<PokeApiPokemon> Get(string name);
    }

    internal class PokemonProvider : IPokemonProvider
    {
        public async Task<PokeApiPokemon> Get(string name)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://pokeapi.co/");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync($"api/v2/pokemon-species/{name}").Result;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<PokeApiPokemon>();
            }
            var error = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException("Call to pokeapi was not successful. It returned the message: " + error);
        }
    }
}