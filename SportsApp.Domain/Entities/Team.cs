using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsApp.Domain.Entities
{

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Athlete> Athletes { get; set; }
    }
}