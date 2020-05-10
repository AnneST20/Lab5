using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab5.Models;

namespace Lab5.Models
{
    public class _Color
    {
        public _Color()
        {
            ProductColors = new List<ProductColor>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [RegularExpression(@"^[А-ЯЭЪЮ]+[а-яэюъА-ЯЮЭЪ'\s]*$")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [Display(Name = "Html код")]
        [RegularExpression(@"[#]+[a-zA-z0-9'\s]*$")]
        public string HtmlCode { get; set; }

        public virtual ICollection<ProductColor> ProductColors {get; set;}
    }
}
