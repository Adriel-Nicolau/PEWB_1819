using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models
{
    public class ClientViewModel
    {

        public class RequestService
        {
           

            [Required]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [Display(Name = "Data do Serviço")]
            public DateTime ServiceDate { get; set; }

            [Display(Name = "Descrição")]
            //vai ser um picker com as opçoes 
            public string Description { get; set; }

            [Required]
            [Display(Name = "Tipo de Residuo")]
            public int ResidueTypeID { get; set; } // FK from locationModel

            [Required]
            [Display(Name = "Serviços")]
            public int ServicesTypesID { get; set; } // FK from locationModel

            [Required]
            [Display(Name = "Local")]
            public int LocationID { get; set; } // FK from locationModel

        }
    }
}