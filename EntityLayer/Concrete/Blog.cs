using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace EntityLayer.Concrete
{
    [Serializable]
    public class Blog
    {
        [Key]
        public int BlogID { get; set; }

        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }

        public string BlogThumbnailImage { get; set; }

        public string BlogImage { get; set; }

        public DateTime BlogCreatedAt { get; set; }

        public bool BlogStatus { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public int WriterID { get; set; }

        public Writer Writer { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public List<Comment> Comments { get; set; }
    }
}
