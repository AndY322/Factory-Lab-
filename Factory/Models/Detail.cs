using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Detail
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Размер")]
        public int Size { get; set; }

        [Display(Name = "Количество")]
        public int Amount { get; set; }

        [Display(Name = "Цена")]
        public decimal Cost { get; set; }

        [Display(Name = "Примечание")]
        public string Notes { get; set; }

        public virtual ICollection<Routing> Routings { get; set; }

        public Detail()
        {
            Routings = new List<Routing>();
        }
    }
}