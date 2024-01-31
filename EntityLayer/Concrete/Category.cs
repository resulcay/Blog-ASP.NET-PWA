using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace EntityLayer.Concrete
{
    [Serializable]
    public class Category
    {
        [Key]

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public bool CategoryStatus { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public List<Blog> Blogs { get; set; }
    }
}
