using BusinessLayer.Concrete;
using Core.Areas.Admin.Models;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Areas.Admin.ViewComponents.Statistic
{
    public class StatisticTopBar : ViewComponent
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());
        readonly MessageManager message2Manager = new(new EfMessageRepository());
        readonly CommentManager commentManager = new(new EfCommentRepository());

        public IViewComponentResult Invoke()
        {
            // TODO: will impact performance if there are many.
            int blogCount = blogManager.GetEntities().Count;
            int messageCount = message2Manager.GetEntities().Count;
            int commentCount = commentManager.GetEntities().Count;


            ViewBag.blogCount = blogCount;
            ViewBag.messageCount = messageCount;
            ViewBag.commentCount = commentCount;

            // XML format
            string weather = GetWeatherByXmlFormat();
            ViewBag.weather = weather;

            //JSON format
            //WeatherModel weather = GetWeatherByJsonFormat().Result;
            //if (weather != null)
            //{
            //    ViewBag.weather = (int)weather.Current.Temperature_2m;
            //}
            //else
            //{
            //    ViewBag.weather = 0;
            //}

            return View();
        }

        static private async Task<WeatherModel> GetWeatherByJsonFormat()
        {
            // Yalova, TR
            // double latitude = 40.656624;
            // double longitude = 29.283731;

            string apiUrl = "https://api.open-meteo.com/v1/forecast?latitude=40.656624&longitude=29.283731&current=temperature_2m";
            using var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<WeatherModel>(responseData);
                }
            }
            catch (HttpRequestException)
            {
                return null;
            }

            return null;
        }

        ///<summary>
        ///Returns temperature in celcius
        ///</summary>
        static private string GetWeatherByXmlFormat()
        {
            // Yalova, TR
            // double latitude = 40.656624;
            // double longitude = 29.283731;

            string apiKey = "e9462439c0ed29e0d999e78203b9cd90";
            string apiUrl = "https://api.openweathermap.org/data/2.5/weather?lat=40.656624&lon=29.283731&mode=xml&appid=" + apiKey;

            using var httpClient = new HttpClient();
            XDocument xml = XDocument.Load(apiUrl);
            string response = xml.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            double celcius = double.Parse(response, System.Globalization.CultureInfo.InvariantCulture) - 273.15;

            return ((int)celcius).ToString();
        }
    }
}