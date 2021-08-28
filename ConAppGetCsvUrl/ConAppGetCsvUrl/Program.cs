using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace ConAppGetCsvUrl
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var url = "https://localhost:44304/api/CsvTest/data.csv";
            var location = "Hyderabad";
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            //var content = await result1.Content.ReadAsStringAsync();

            //var stream = await response.Content.ReadAsStreamAsync();
            var stream = await response.Content.ReadAsStreamAsync();
            var fileInfo = new FileInfo("reports_2020-11-10 08_14_12.csv");
            using (var fileStream = fileInfo.OpenWrite())
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
