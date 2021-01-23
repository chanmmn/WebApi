using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConAppWebApiGenericGet
{
    class Program
    {
        //First run the WebApi project
        static void Main(string[] args)
        {
            string result = GetPostAsync1("http://localhost:56218/api/Values/1").GetAwaiter().GetResult();
            Console.WriteLine("{0}", result);
            Console.ReadKey();
        }

        public static async Task<string> GetPostAsync1(string url)
        {

            HttpClient client = new HttpClient();

            //var response = await client.GetAsync("http://localhost:56218/api/Values");
            var response = await client.GetAsync(url);

            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
