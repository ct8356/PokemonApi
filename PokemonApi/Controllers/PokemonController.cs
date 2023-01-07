using Microsoft.AspNetCore.Mvc;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonProvider _pokemonProvider;
        private readonly ITranslator _translator;

        public PokemonController(ILogger<PokemonController> logger,
            IPokemonProvider pokemonProvider,
            ITranslator translator)
        {
            _logger = logger;
            _pokemonProvider = pokemonProvider;
            _translator = translator;
        }

        [HttpGet]
        [Route("{name}")]
        public Pokemon GetPokemon([FromRoute] string name)
        {
            var result = _pokemonProvider.Get(name);
            var description = result.flavor_text_entries?.First(x => x.Language?.name == "en").flavor_text;
            var newDescription = _translator.Translate(description ?? string.Empty);
            return new Pokemon
            {
                Name = result.Name,
                Description = newDescription,
            };
        }
    }
}