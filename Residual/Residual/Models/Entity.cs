using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Residual.Models
{
    public class Entity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Adress { get; set; }
    }

}
