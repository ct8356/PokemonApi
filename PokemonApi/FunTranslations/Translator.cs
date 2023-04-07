using System.Net.Http.Headers;
using System.Web;

namespace PokemonApi
{
    public interface ITranslator
    {
        Task<string> Translate(string input);
    }

    internal class Translator : ITranslator
    {
        public async Task<string> Translate(string input)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.funtranslations.com/");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            input = RemoveInvalidSubStrings(input);
            var queryString = HttpUtility.UrlEncode(input);
            var response = await client.GetAsync($"translate/shakespeare.json?text={queryString}");
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<Translation>().Result.Contents?.Translated ?? input;
            }
            return input;
        }

        internal string RemoveInvalidSubStrings(string input)
        {
            input = input.Replace("\n", " ");
            input = input.Replace("\f", " ");
            return input;
        }
    }
}