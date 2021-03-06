﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab5.Models;

namespace Lab5.Models
{
    public class Firm
    {
        public Firm()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        [Required]
        public int Id_Country { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Страна")]
        public virtual Country Country { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
