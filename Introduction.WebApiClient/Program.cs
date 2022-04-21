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
                Key = "How are you 4713",
                ValueLanguage = 1,
                Value = "Wie geht es dir 4713?",
            };

            //var clientAccess = new RestApi.ClientAccess();
            //var result = await clientAccess.PostAsync(TranslationBaseUri, "Translations", translation);
            //await PrintTranslationsAsync();
            //await PrintTranslationByIdAsync(1);

            var model = await CreateTranslationAsync(translation);

            if (model != null)
            {
                await PrintTranslationByIdAsync(model.Id);
            }
            if (model != null)
            {
                model.AppName+= "Update";

                model = await UpdateTranslationAsync(model);
                if (model != null)
                {
                    await PrintTranslationByIdAsync(model.Id);
                    await DeleteTranslationAsync(model.Id);
                }
            }

        }

        public static async Task<Models.Translation?> CreateTranslationAsync(Models.Translation translation)
        {
            var clientAccess = new RestApi.ClientAccess();
            var model = await clientAccess.PostAsync<Models.Translation>(TranslationBaseUri, "translations", translation);

            return model;
        }
        public static async Task<Models.Translation?> UpdateTranslationAsync(Models.Translation translation)
        {
            var clientAccess = new RestApi.ClientAccess();
            var model = await clientAccess.PutAsync<Models.Translation>(TranslationBaseUri, "translations", translation.Id, translation);

            return model;
        }
        public static async Task DeleteTranslationAsync(int id)
        {
            var clientAccess = new RestApi.ClientAccess();
            
            await clientAccess.DeleteAsync(TranslationBaseUri, "translations", id);
        }

        public static async Task PrintTranslationsAsync()
        {
            var models = await GetAsync<Models.Translation>(TranslationBaseUri, "translations");

            foreach (var item in models)
            {
                Console.WriteLine($"{item}");
            }
        }
        public static async Task PrintTranslationByIdAsync(int id)
        {
            var model = await GetByIdAsync<Models.Translation>(TranslationBaseUri, "translations", id);

            if (model != null)
            {
                Console.WriteLine($"{model}");
            }
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