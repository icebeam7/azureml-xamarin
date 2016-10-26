using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace PrediccionAutos.Clases
{
    public static class PrediccionAutos
    {
        public async static Task<string> Predecir(string make, string body, string wheel, string engine, string horsepower, string peak, string highway)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, Autos>() {
                        {
                            "input1",
                            new Autos()
                            {
                                ColumnNames = new string[] {"make", "body-style", "wheel-base", "engine-size", "horsepower", "peak-rpm", "highway-mpg", "price"},
                                Values = new string[,] { { make, body, wheel, engine, horsepower, peak, highway, "0" }}
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>() { }
                };

                const string apiKey = "gT5ZLOvVRT3iDFmFHJO18cv7NAOb/94KaJ8wrgVL0m2VUwtxT/xSIB3TvJGcGmETYuzL3DTzEQVDHQo8Z+7awQ==";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/5ae4b529b6b24cffb9e96e24edfb5c60/services/328e354da7644102a4e3adbd9a30abe5/execute?api-version=2.0&details=true");

                //HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                string json = JsonConvert.SerializeObject(scoreRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(result);
                    var precio = JObject.Parse(result)["Results"]["output1"]["value"]["Values"][0][8];
                    return precio.ToString();
                }
                else
                {
                    //Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    //Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseContent);
                    return "0";
                }
            }
        }
    }
}