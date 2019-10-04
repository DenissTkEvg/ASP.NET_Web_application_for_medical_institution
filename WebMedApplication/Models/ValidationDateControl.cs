namespace WebMedApplication.Models
{
    using WebMedApplication.Controllers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class ValidationDateControl 
    {
       //[Display(Name="С")]
       [DataType(DataType.Date)]
       public DateTime DOB { get; set; }

        //[Display(Name = "по")]
        [DataType(DataType.Date)]
        public DateTime DOB2 { get; set; }

        public Guid PrepID { get; set; }


    }
}