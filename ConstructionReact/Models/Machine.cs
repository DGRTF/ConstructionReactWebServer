using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConstructionReact.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CreateYear { get; set; }

        public int RoomId { get; set; } 

        [JsonIgnore]
        public Room Room { get; set; }
    }
}
