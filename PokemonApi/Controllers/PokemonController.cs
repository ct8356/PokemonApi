using Microsoft.AspNetCore.Mvc;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonProvider _pokemonProvider;

        public PokemonController(ILogger<PokemonController> logger,
            IPokemonProvider pokemonProvider)
        {
            _logger = logger;
            _pokemonProvider = pokemonProvider;
        }

        [HttpGet]
        [Route("{name}")]
        public Pokemon Get([FromRoute] string name)
        {
            var result = _pokemonProvider.Get(name);
            return new Pokemon
            {
                Name = result.Name,
                Description = result.flavor_text_entries?.First(x => x.Language?.name == "en").flavor_text,
            };
        }
    }
}