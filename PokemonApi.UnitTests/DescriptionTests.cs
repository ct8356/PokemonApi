using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PokemonApi.Controllers;
using System.Threading.Tasks;

namespace PokemonApi.UnitTests
{
    public class DescriptionTests
    {
        private readonly Mock<IPokemonProvider> _pokemonProvider = new();
        private readonly Mock<ITranslator> _translator = new();

        [Test]
        public async Task GetPokemon_WithMultipleEnglishFlavors_ReturnsFirstEnglishFlavor()
        {
            var name = "mewtwo";
            var pokemon = new PokeApiPokemon
            {
                Name = name,
                flavor_text_entries = new FlavorTextEntry[]
                {
                    new() 
                    { 
                        flavor_text = "description 1",
                        Language = new() { name = "en" }
                    },
                    new()
                    {
                        flavor_text = "description 2",
                        Language = new() { name = "en" }
                    }
                }
            };
            
            _pokemonProvider.Setup(x => x.Get(name)).ReturnsAsync(pokemon);
            _translator.Setup(x => x.Translate("description 1")).ReturnsAsync("translated description 1");

            var controller = new PokemonController(
                new Mock<ILogger<PokemonController>>().Object,
                _pokemonProvider.Object, _translator.Object);

            var result = await controller.GetPokemon(name) as OkObjectResult;

            (result.Value as Pokemon).Description.Should().Be("translated description 1");
        }
    }
}