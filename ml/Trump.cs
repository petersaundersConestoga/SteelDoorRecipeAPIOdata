using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Text.Json;

namespace ml
{
    public class Trump : ODataController
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Trump(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        //public IEnumerable<IEnumerable<string>>? json { get; set; }

        public async Task<IActionResult> HelloWorld()
        {
            HttpClient http = _httpClientFactory.CreateClient("Flask");
            HttpResponseMessage response = await http.GetAsync("/");

            if (response.IsSuccessStatusCode)
            {
                return (IActionResult)response.Content.ReadAsStringAsync();
            }
            return NotFound();
        }
        public async IAsyncEnumerable<IEnumerable<string>> GetNothing()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("Flask");
            HttpResponseMessage httpResponse = await httpClient.GetAsync("/trump");

            if (httpResponse.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                IEnumerable<string>? json = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(contentStream);
                yield return json;
            }
        }
    }
}
