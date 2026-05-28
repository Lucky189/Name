using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsApp.Domain.Entities
{




    public class Athlete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
