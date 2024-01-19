using Newtonsoft.Json;

namespace Core.Areas.Admin.Models
{
    public class CategoryModel
    {
        [JsonProperty("categorycount")]
        public int CategoryCount { get; set; }

        [JsonProperty("categoryname")]
        public string CategoryName { get; set; }
    }
}
