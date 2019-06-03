using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public Specialization()
        {
            Teams = new List<Team>();
        }
    }
}