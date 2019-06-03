using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory.Models
{
    public class Team
    {
        public int Id{ get; set; }

        public int Number { get; set; }

        public int? SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Routing> Routings { get; set; }

        public Team()
        {
            Employees = new List<Employee>();
            Routings = new List<Routing>();
        }
    }
}