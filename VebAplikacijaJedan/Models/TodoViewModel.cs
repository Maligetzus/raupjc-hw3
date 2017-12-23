using System;
using System.ComponentModel.DataAnnotations;

namespace VebAplikacijaJedan.Models
{
    public class TodoViewModel
    {
        [Required,MinLength(1)]
        public string Text;
        [Required]
        public bool IsCompleted;
        [Required]
        public DateTime DateCreated;
        [Required]
        public DateTime DateDue;
    }
}