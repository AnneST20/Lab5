using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab5.Models;

namespace Lab5.Models
{
    public class Country
    {
        public Country()
        {
            Firms = new List<Firm>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [RegularExpression(@"^[А-ЯЭЪЮ]+[а-яэюъА-ЯЮЭЪ'\s]*$")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public ICollection<Firm> Firms { get; set; }
    }
}
