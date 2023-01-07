using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PokemonApi.Controllers;

namespace PokemonApi.UnitTests
{
    public class DescriptionTests
    {
        private readonly Mock<IPokemonProvider> _pokemonProvider = new();
        private readonly Mock<ITranslator> _translator = new();

        [Test]
        public void GetPokemon_WithMultipleEnglishFlavors_ReturnsFirstEnglishFlavor()
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
            
            _pokemonProvider.Setup(x => x.Get(name)).Returns(pokemon);
            _translator.Setup(x => x.Translate("description 1")).Returns("translated description 1");

            var controller = new PokemonController(
                new Mock<ILogger<PokemonController>>().Object,
                _pokemonProvider.Object, _translator.Object);

            var result = controller.GetPokemon(name);

            result.Description.Should().Be("translated description 1");
        }
    }
}