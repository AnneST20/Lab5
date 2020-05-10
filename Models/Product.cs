using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab5.Models;

namespace Lab5.Models
{
    public class Product
    {
        public Product()
        {
            ProductColors = new List<ProductColor>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [Display(Name = "Фирма")]
        public int Id_Firm { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [RegularExpression(@"^[А-ЯЭЪЮ]+[а-яэюъА-ЯЮЭЪ'\s]*$")]
        [Display(Name = "Косметическое средство")]
        public int Id_Cosmetics { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [DataType(DataType.Currency)]
        [Display(Name = "Цена")]
        public decimal Cost { get; set; }

        [Display(Name = "Фирма")]
        public virtual Firm Firm { get; set; }
        [Display(Name = "Косметическое средство")]
        public virtual Cosmetic Cosmetic { get; set; }
        public virtual ICollection<ProductColor> ProductColors { get; set; }
    }
}
