using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab5.Models;

namespace Lab5.Models
{
    public class Cosmetic
    {
        public Cosmetic()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        [Required]
        public int Id_Area { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [RegularExpression(@"^[А-ЯЭЪЮ]+[а-яэюъА-ЯЮЭЪ'\s]*$")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Область приминения макияжа")]
        public virtual Area Area { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
