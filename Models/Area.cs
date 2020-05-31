using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Lab5.Models;
using Newtonsoft.Json;

namespace Lab5.Models
{
    public class Area
    {
        public Area ()
        {
            Cosmetics = new List<Cosmetic>();
        }
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле не может быть пустым")]
        [RegularExpression(@"^[А-ЯЭЪЮ]+[а-яэюъА-ЯЮЭЪ'\s]*$")]
        [Display(Name = "Область приминения макияжа")]
        [JsonIgnore]
        public string ApplicationArea { get; set; }

        public virtual ICollection<Cosmetic> Cosmetics { get; set; }
    }
}
