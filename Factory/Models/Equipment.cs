using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Display(Name = "Дата начала эксплуатации")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата выпуска")]
        public DateTime Release { get; set; }

        [Display(Name = "Примечание")]
        public string Description { get; set; }

        public virtual IEnumerable<Process> Processes { get; set; }

        public Equipment()
        {
            Processes = new List<Process>();
        }

        [Display(Name = "Наименование")]
        public string Name { get; set; }
    }
}