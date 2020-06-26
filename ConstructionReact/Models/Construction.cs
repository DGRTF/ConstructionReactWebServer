using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ConstructionReact.Models
{
    public class Construction
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        [JsonIgnore]
        public List<Room> Rooms { get; set; }
    }
}
