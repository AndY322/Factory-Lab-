using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory.Models
{
    public class JobPosition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public JobPosition()
        {
            Employees = new List<Employee>();
        }
    }
}