using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory.Models
{
    public class DetailStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProcessStep { get; set; }

        public bool EndStatus { get; set; }

        public virtual ICollection<Routing> Routings { get; set; }

        public DetailStatus()
        {
            Routings = new List<Routing>();
        }
    }
}