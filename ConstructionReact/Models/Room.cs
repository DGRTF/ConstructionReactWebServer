using System.Collections.Generic;

namespace ConstructionReact.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Floor { get; set; }

        public List<Machine> Machines { get; set; }

        public int ConstructionId { get; set; }      // внешний ключ
        public Construction Construction { get; set; }
    }
}
