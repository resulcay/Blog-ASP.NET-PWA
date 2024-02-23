using Newtonsoft.Json;

namespace Core.Areas.Admin.Models
{
    public class RoleModel
    {
        [JsonProperty("rolecount")]
        public int RoleCount { get; set; }

        [JsonProperty("rolename")]
        public string RoleName { get; set; }
    }
}
