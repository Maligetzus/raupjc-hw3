using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VebAplikacijaJedan.Models
{
    public class AddViewModel
    {
        [Required, MinLength(1)]
        public string _text;
        [Required]
        public DateTime _dateDue;
    }
}
