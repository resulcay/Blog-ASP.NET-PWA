using Newtonsoft.Json;

namespace Core.Areas.Admin.Models
{
    public class ChartCategory
    {
        [JsonProperty("categorycount")]
        public int CategoryCount { get; set; }

        [JsonProperty("categoryname")]
        public string CategoryName { get; set; }
    }
}
