using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Residual.Models
{
    public class Service
    {
        public int ID { get; set; }
        public int IDserviceType { get; set; }
        public int IDentityRequesting { get; set; }
        public int IDentityReceiving { get; set; }
    }
}