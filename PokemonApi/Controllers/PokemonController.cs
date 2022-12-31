using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{name}")]
        public Pokemon? Get([FromRoute] string name)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(
                $"https://pokeapi.co");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            var response = client.GetAsync($"/api/v2/pokemon-species/{name}").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content
                               .ReadAsAsync<PokeApiPokemon>().Result;
                return new Pokemon
                {
                    Name = result.Name,
                    Description = result.flavor_text_entries?.Single(x => x.Language?.name == "en").flavor_text,
                };
            }
            return null;
        }
    }
}