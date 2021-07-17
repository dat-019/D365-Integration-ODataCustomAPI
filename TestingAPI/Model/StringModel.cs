using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIConsumer
{

    public class StringModel
    {
        [Required(ErrorMessage = "{0} is required!")]
        public string Data { get; set; }
    }
}