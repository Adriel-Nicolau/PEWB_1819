using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class Review
    {
        public int ID { get; set; }

        [Required]
        [ForeignKey("Entity")]
        public int EntityID { get; set; }

        [Required]
        [ForeignKey("ServiceRequest")]
        public int ServiceRequestID { get; set; }

        public string Content { get; set; }
        public DateTime CreationDate { get; set; }

        [Range(0, 5, ErrorMessage = "Pontuação tem de ser entre 0 e 5")]
        public int Rating { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual ServiceRequest ServiceRequest { get; set; }

    }
}