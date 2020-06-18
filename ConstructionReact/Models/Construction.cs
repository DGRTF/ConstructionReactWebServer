using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionReact.Models
{
    public class Construction
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
