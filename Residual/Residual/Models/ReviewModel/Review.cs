using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Residual.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int IDentityReviwer { get; set; }
        public int IDentityReviwed { get; set; }
        public string content { get; set; }
        public DateTime CreationDate { get; set; }
        public int Rating { get; set; }
        public bool Seen { get; set; }
    }
}