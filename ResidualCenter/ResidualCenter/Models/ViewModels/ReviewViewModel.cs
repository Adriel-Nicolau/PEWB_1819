using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResidualCenter.Models.ViewModels
{
    public class ReviewViewModel
    {
        public class CreateReview
        {
            [Display(Name = "Conteudo")]
            //vai ser um picker com as opçoes 
            public string Content { get; set; }


            [Required]
            [Display(Name = "Pontuação")]
            [Range(0, 5, ErrorMessage = "Pontuação tem de ser entre 0 e 5")]
            public int Rating { get; set; } // FK from locationModel

            [Required]

            public int ServiceRequestID { get; set; } // FK from locationModel

            [Required]

            public int EntityID { get; set; } // FK from locationModel
        }
        public class ListReview
        {
            [Display(Name = "Review")]
            public string Content { get; set; }

            [Display(Name = "Cliente")]
            public string EntityName { get; set; } // FK from locationModel

            [Required]
            [Display(Name = "Pontuação")]
            public int Rating { get; set; } // FK from locationModel
        }
    }
}