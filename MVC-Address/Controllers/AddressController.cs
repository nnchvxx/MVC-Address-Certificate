using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using MVC_Address.Models;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC_Address.Controllers
{
    public class AddressController : Controller
    {
        private static string token;
        private const string searchUrl = "https://epc.opendatacommunities.org/api/v1/display/search";
        private const string recommendationUrl = "https://epc.opendatacommunities.org/api/v1/domestic/recommendations";
        private const string certificateUrl = "https://epc.opendatacommunities.org/api/v1/display/certificate";

        private readonly ILogger<AddressController> _logger;
        private readonly IHttpClientFactory httpFactory;

        public AddressController(ILogger<AddressController> logger, IHttpClientFactory httpFactory)
        {
            _logger = logger;
            this.httpFactory = httpFactory;
        }

        public IActionResult InputToken()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InputToken(string inputToken)
        {
            token = inputToken;
            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Info(string postcode, string from)
        {
            try
            {
                var fromValue = int.Parse(from) < 0 ? 0.ToString() : from;
                ViewBag.PageFrom = fromValue;
                var query = new Dictionary<string, string>()
                {
                    ["from"] = fromValue,
                    ["size"] = "25"
                };
                using (var httpClient = httpFactory.CreateClient())
                {
                    var url = "";
                    if (postcode == null)
                    {
                        url = searchUrl;
                    }
                    else
                    {
                        query.Add("postcode", postcode);
                    }
                    url = QueryHelpers.AddQueryString(searchUrl, query);

                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "text/csv");
                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {token}");

                        var response = await httpClient.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(result))
                        {
                            return View(new List<CertificateModel>());
                        }
                        System.IO.File.WriteAllText("./Files/result.csv", result);

                        var dt = ConvertCSVtoDataTable("./Files/result.csv");
                        var jsonString = JsonConvert.SerializeObject(dt);
                        var final = jsonString.Replace("\\\"", "");
                        System.IO.File.WriteAllText("./Files/result.json", final);
                        var model = JsonConvert.DeserializeObject<List<CertificateModel>>(final,
                        new JsonSerializerSettings()
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        return View(model);
                    }
                }
            }
            catch (Exception e)
            {

                _logger.LogDebug(e.Message);
                return View("Error", new ErrorViewModel());
            }
        }

        public async Task<IActionResult> Recommendation(string lmkKey)
        {
            try
            {
                using (var httpClient = httpFactory.CreateClient())
                {
                    if (lmkKey == null)
                    {
                        throw new ArgumentException();
                    }
                    var url = "";
                    url = recommendationUrl + ($"/{lmkKey}");
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "text/csv");
                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {token}");

                        var response = await httpClient.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        System.IO.File.WriteAllText("./Files/resultRecommendation.csv", result);

                        var dt = ConvertCSVtoDataTable("./Files/resultRecommendation.csv");
                        var jsonString = JsonConvert.SerializeObject(dt);
                        var final = jsonString.Replace("\\\"", "");
                        System.IO.File.WriteAllText("./Files/resultRecommendation.json", final);
                        var model = JsonConvert.DeserializeObject<List<RecommendationModel>>(final,
                        new JsonSerializerSettings()
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        return View(model);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                return View("Error", new ErrorViewModel());
            }
        }

        public async Task<IActionResult> Details(string lmkKey)
        {
            try
            {
                using (var httpClient = httpFactory.CreateClient())
                {
                    var url = certificateUrl + $"/{lmkKey}";

                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "text/csv");
                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {token}");

                        var response = await httpClient.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var result = await response.Content.ReadAsStringAsync();
                        System.IO.File.WriteAllText("./Files/certificate.csv", result);

                        var dt = ConvertCSVtoDataTable("./Files/certificate.csv");
                        var jsonString = JsonConvert.SerializeObject(dt);
                        var final = jsonString.Replace("\\\"", "");
                        var model = JsonConvert.DeserializeObject<List<CertificateModel>>(final,
                        new JsonSerializerSettings()
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                        foreach (var item in model)
                        {
                            item.HasRecommendation = await HasRecommendation(item.LmkKey);
                        }
                        return View(model.FirstOrDefault());
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                return View("Error", new ErrorViewModel());
            }
        }

        private async Task<bool> HasRecommendation(string lmkKey)
        {
            using (var httpClient = httpFactory.CreateClient())
            {
                var url = recommendationUrl + ($"/{lmkKey}");
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    request.Headers.TryAddWithoutValidation("Accept", "text/csv");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {token}");

                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        private static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}

