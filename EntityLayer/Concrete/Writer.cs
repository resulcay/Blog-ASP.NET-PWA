using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace EntityLayer.Concrete
{
    [Serializable]
    public class Writer
    {
        [Key]

        public int WriterID { get; set; }

        public string WriterUserName { get; set; }

        public string WriterNameSurname { get; set; }

        public string WriterAbout { get; set; }

        public string WriterImage { get; set; }

        public string WriterMail { get; set; }

        public virtual string WriterPassword { get; set; }

        public bool WriterStatus { get; set; }

        public int UserID { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public User User { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public List<Blog> Blogs { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<Message> WriterSender { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public virtual ICollection<Message> WriterReceiver { get; set; }
    }
}
