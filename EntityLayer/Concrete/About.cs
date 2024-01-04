using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class About
    {
        [Key]

        public int AboutID { get; set; }

        public string AboutDetailsFirst { get; set; }

        public string AboutDetailsSecond { get; set; }

        public string AboutImageFirst { get; set; }
    
        public string AboutImageSecond { get; set; }

        public string AboutMapLocation { get; set; }

        public bool AboutStatus { get; set; }
    }
}

