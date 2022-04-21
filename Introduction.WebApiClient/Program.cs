using System;

namespace Introduction.WebApiClient
{
    internal class Program
    {
        private static string BaseUri = "https://localhost:7211/api/";
        private static string TranslationBaseUri = "http://172.17.221.131:5085/api/";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Access to a RESTful-Service");
            Console.WriteLine();

            var translation = new Models.Translation
            {
                AppName = "g.gehrer",
                KeyLanguage = 2,
                Key = "How are you",
                ValueLanguage = 1,
                Value = "Wie geht es dir?",
            };

            var clientAccess = new RestApi.ClientAccess();
            var result = await clientAccess.PostAsync(TranslationBaseUri, "Translations", translation);

        }
        public static async Task<T[]> GetAsync<T>(string baseUri, string controller)
        {
            var clientAccess = new RestApi.ClientAccess();
            var models = await clientAccess.GetAsync<T>(baseUri, controller);

            return models;
        }
        public static async Task<T?> GetByIdAsync<T>(string baseUri, string controller, int id)
        {
            var clientAccess = new RestApi.ClientAccess();
            var model = await clientAccess.GetByIdAsync<T>(baseUri, controller, id);

            return model;
        }
    }
}