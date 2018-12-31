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
            [Display(Name = "Morada")]
            public string Adress { get; set; }

            [Required]
            [Display(Name = "Serviços")]
            public int ServicesTypesID { get; set; } // FK from locationModel

            [Required]
            [Display(Name = "Local")]
            public int LocationID { get; set; } // FK from locationModel

        }

        

        public class CreateReview
        {   
            [Display(Name = "Conteudo")]
            //vai ser um picker com as opçoes 
            public string Content { get; set; }

           
            [Required]
            [Display(Name = "Pontuação")]
            [Range(0,5, ErrorMessage = "Pontuação tem de ser entre 0 e 5")]
            public int Rating { get; set; } // FK from locationModel

            [Required]
          
            public int ServiceRequestID { get; set; } // FK from locationModel

            [Required]
       
            public int EntityID { get; set; } // FK from locationModel
        }
    }
}