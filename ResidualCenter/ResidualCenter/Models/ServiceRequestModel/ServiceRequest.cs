using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class ServiceRequest
    {
        public int ID { get; set; }

        [Required]
        public string EntityID { get; set; }
        [Required]
        public int ServiceTypeID { get; set; }

        [DisplayFormat(NullDisplayText = "Não Aceite")]
        public string Status { get; set; }

        public string Quantity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do pedido")]
        public DateTime RequestDate { get; set; }

        [Required]
        [ForeignKey("ResidueType")]
        [Display(Name = "Região")]
        public int ResidueTypeID { get; set; } // FK from locationModel
        [Required]
        
        [Display(Name = "Descrição")]
        public string Description { get; set; } // FK from locationModel

        public virtual Entity Entity { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual ResidueType ResidueType { get; set; }

    }
}