using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Routing
    {
        public int Id { get; set; }

        public int? DetailId { get; set; }

        [Display(Name = "Деталь")]
        public virtual Detail Detail { get; set; }

        public int? ProcessId { get; set; }

        [Display(Name = "Процесс")]
        public virtual Process Process { get; set; }

        public int? TeamId { get; set; }

        [Display(Name = "Команда")]
        public virtual Team Team { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartProduction { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PutOnStorage { get; set; }

        public string Notes { get; set; }

        public int? StatusId { get; set; }

        public virtual DetailStatus Status { get; set; }
    }
}