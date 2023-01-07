using System.Net.Http.Headers;

namespace PokemonApi
{
    public interface ITranslator
    {
        string Translate(string input);
    }

    internal class Translator : ITranslator
    {
        public string Translate(string input)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.funtranslations.com/");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            input = RemoveInvalidSubStrings(input);
            var queryString = Uri.EscapeDataString(input); 
            //NOTE I think plus works fine,
            //it was POST that was breaking it!
            var response = client.GetAsync($"translate/shakespeare.json?text={queryString}")
                .Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<string>().Result;
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