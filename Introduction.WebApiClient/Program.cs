using System;

namespace Introduction.WebApiClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Access to a RESTful-Service");
            Console.WriteLine();

            await PrintGetResult();
        }
        public static async Task PrintGetResult()
        {
            var baseUri = "https://localhost:7211/api/";
            var clientAccess = new RestApi.ClientAccess();
            var models = await clientAccess.GetAsync<Models.Student>(baseUri, "Students");

            foreach (var item in models)
            {
                Console.WriteLine($"{item.LastName} {item.FirstName}");
            }
        }
    }
}