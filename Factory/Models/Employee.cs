using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TeamId { get; set; }

        public Team Team { get; set; }

        public int? PositionId { get; set; }

        public virtual JobPosition Position { get; set; }
    }
}