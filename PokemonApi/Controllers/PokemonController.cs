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
        public async Task<IActionResult> GetPokemon([FromRoute] string name)
        {
            try
            {
                var result = await _pokemonProvider.Get(name);
                var description = result.flavor_text_entries?.First(x => x.Language?.name == "en").flavor_text;
                var newDescription = await _translator.Translate(description ?? string.Empty);
                return Ok(new Pokemon
                {
                    Name = result.Name,
                    Description = newDescription,
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}