using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Introduction.WebApiClient.RestApi
{
    public partial class ClientAccess
    {
        protected static string MediaType => "application/json";
        protected static JsonSerializerOptions DeserializerOptions => new() { PropertyNameCaseInsensitive = true };
        protected static HttpClient CreateClient(string baseAddress)
        {
            HttpClient client = new();

            if (string.IsNullOrEmpty(baseAddress) == false)
            {
                if (baseAddress.EndsWith(@"/") == false
                    && baseAddress.EndsWith(@"\") == false)
                {
                    baseAddress += "/";
                }

                client.BaseAddress = new Uri(baseAddress);
            }
            client.DefaultRequestHeaders.Accept.Clear();

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            return client;
        }

        public async Task<T[]> GetAsync<T>(string baseUri, string extUri)
        {
            using var client = CreateClient(baseUri);
            var response = await client.GetAsync($"{extUri}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var contentData = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var result = await JsonSerializer.DeserializeAsync<T[]>(contentData, DeserializerOptions).ConfigureAwait(false);

                return result == null ? Array.Empty<T>() : result;
            }
            else
            {
                string stringData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                throw new Exception(errorMessage);
            }
        }
        public async Task<T?> GetByIdAsync<T>(string baseUri, string extUri, int id)
        {
            using var client = CreateClient(baseUri);
            var response = await client.GetAsync($"{extUri}/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var contentData = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var result = await JsonSerializer.DeserializeAsync<T>(contentData, DeserializerOptions).ConfigureAwait(false);

                return result;
            }
            else
            {
                string stringData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                string errorMessage = $"{response.ReasonPhrase}: {stringData}";

                System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                throw new Exception(errorMessage);
            }
        }

        public async Task<T?> PostAsync<T>(string baseUri, string controller, T model)
        {
            using var client = CreateClient(baseUri);
            var jsonData = JsonSerializer.Serialize(model);
            var contentData = new StringContent(jsonData, Encoding.UTF8, MediaType);
            var response = await client.PostAsync($"{controller}", contentData).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var resultData = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                return await JsonSerializer.DeserializeAsync<T>(resultData, DeserializerOptions).ConfigureAwait(false);
            }
            else
            {
                string errorMessage = $"{response.ReasonPhrase}: { await response.Content.ReadAsStringAsync().ConfigureAwait(false) }";

                System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                throw new Exception(errorMessage);
            }
        }
        public async Task<T?> PutAsync<T>(string baseUri, string controller, int id, T model)
        {
            using var client = CreateClient(baseUri);
            var jsonData = JsonSerializer.Serialize(model);
            var contentData = new StringContent(jsonData, Encoding.UTF8, MediaType);
            var response = await client.PutAsync($"{controller}/{id}", contentData).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var resultData = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                return await JsonSerializer.DeserializeAsync<T>(resultData, DeserializerOptions).ConfigureAwait(false);
            }
            else
            {
                string errorMessage = $"{response.ReasonPhrase}: { await response.Content.ReadAsStringAsync().ConfigureAwait(false) }";

                System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                throw new Exception(errorMessage);
            }
        }
        public async Task DeleteAsync(string baseUri, string controller, int id)
        {
            using var client = CreateClient(baseUri);
            var response = await client.DeleteAsync($"{controller}/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode == false)
            {
                string errorMessage = $"{response.ReasonPhrase}: { await response.Content.ReadAsStringAsync().ConfigureAwait(false) }";

                System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, errorMessage);
                throw new Exception(errorMessage);
            }
        }
    }
}
