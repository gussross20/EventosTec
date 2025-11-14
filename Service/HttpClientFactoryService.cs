using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Service
{
    public class HttpClientFactoryService : IHttpClientFactoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public string Metodo { get; set; } = null!;
        public Dictionary<string, object> Parametros { get; set; } = null!;
        public TimeSpan? Timeout { get; set; }

        public async Task<TResult> InvocarGetAsync<TResult>()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = Timeout ?? TimeSpan.FromSeconds(30); // Valor por defecto
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var urlExec = ConstruirUrl();

            using var response = await httpClient.GetAsync(urlExec);
            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(contentString)!;
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<byte[]> InvocarGetAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = Timeout ?? TimeSpan.FromSeconds(30); // Valor por defecto
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var urlExec = ConstruirUrl();

            using var response = await httpClient.GetAsync(urlExec);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return Convert.FromBase64String(responseBody.Replace("\"", string.Empty));
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<TResult> InvocarPostAsJsonAsync<TResult>()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = Timeout ?? TimeSpan.FromSeconds(30); // Valor por defecto
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await httpClient.PostAsJsonAsync(Metodo, Parametros);
            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResult>(contentString)!;
            }

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<byte[]> InvocarPostAsJsonAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = Timeout ?? TimeSpan.FromSeconds(30); // Valor por defecto
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await httpClient.PostAsJsonAsync(Metodo, Parametros);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return Convert.FromBase64String(responseBody.Replace("\"", string.Empty));
            }

            throw new Exception(response.StatusCode.ToString());
        }

        private string ConstruirUrl()
        {
            if (Parametros == null)
                return Metodo;

            var queryString = "?";
            foreach (var p in Parametros)
            {
                queryString += $"{p.Key}={p.Value}&";
            }

            return Metodo + queryString.TrimEnd('&');
        }
    }
}