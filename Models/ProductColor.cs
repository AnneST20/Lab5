using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab5.Models;

namespace Lab5.Models
{
    public class ProductColor
    {
        public int Id { get; set; }
        [Required]
        public int Id_Product { get; set; }
        [Required]
        public int Id_Color { get; set; }

        [Display(Name = "Цвет")]
        public virtual _Color Color { get; set; }
        [Display(Name = "Комсетическое средство")]
        public virtual Product Product { get; set; }
    }
}
