using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Process
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        public int? EquipmentId { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual ICollection<Routing> Routings { get; set; }

        public Process()
        {
            Routings = new List<Routing>();
        }
    }
}