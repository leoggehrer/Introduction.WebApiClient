using System;

namespace Introduction.WebApiClient
{
    internal class Program
    {
        private static string BaseUri = "https://localhost:7211/api/";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Access to a RESTful-Service");
            Console.WriteLine();

        }
        public static async Task<T[]> GetAsync<T>(string baseUri, string controller)
        {
            var clientAccess = new RestApi.ClientAccess();
            var models = await clientAccess.GetAsync<T>(baseUri, controller);

            return models;
        }
        public static async Task<T?> GetByIdasync<T>(string baseUri, string controller, int id)
        {
            var clientAccess = new RestApi.ClientAccess();
            var model = await clientAccess.GetByIdAsync<T>(baseUri, controller, id);

            return model;
        }
    }
}