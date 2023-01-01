using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PokemonApi.Controllers;

namespace PokemonApi.UnitTests
{
    public class DescriptionTests
    {
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
            var pokemonProvider = new Mock<IPokemonProvider>();
            pokemonProvider.Setup(x => x.Get(name)).Returns(pokemon);

            var controller = new PokemonController(
                new Mock<ILogger<PokemonController>>().Object,
                pokemonProvider.Object);

            var result = controller.Get(name);

            result.Description.Should().Be("description 1");
        }
    }
}