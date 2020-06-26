using ConstructionReact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace ConstructionReact.Controllers
{
    public class RoomsController : Controller
    {
        private ApplicationContext ApplicationContext { get; set; }

        public RoomsController(ApplicationContext context)
        {
            ApplicationContext = context;
        }

        //Rooms
        public JsonResult GetRoomsInConstruction(ParemetersRoom paremetersRoom)
        {
            var rooms = ApplicationContext.Rooms.AsNoTracking().Where(x => x.ConstructionId == paremetersRoom.ConstructionId).Include(x => x.Machines)
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    floor = x.Floor,
                    constructionId = x.ConstructionId,
                    haveMachine = x.Machines.Any()
                });

            return Json(rooms);
        }

        public JsonResult AddRoomInConstruction(ParemetersRoom paremetersRoom)
        {
            Room room = new Room
            {
                Name = paremetersRoom.Name,
                Floor = paremetersRoom.Floor,
                ConstructionId = paremetersRoom.ConstructionId
            };

            ApplicationContext.Rooms.Add(room);
            ApplicationContext.SaveChanges();

            return GetRoomsInConstruction(paremetersRoom);
        }

        public JsonResult DeleteRoomInConstruction(ParemetersRoom paremetersRoom)
        {
            Room room = ApplicationContext.Rooms.AsNoTracking().FirstOrDefault(x => x.Id == paremetersRoom.RoomId);
            if (room != null && room.Id == paremetersRoom.RoomId)
            {
                ApplicationContext.Rooms.Remove(room);
                ApplicationContext.SaveChanges();
            }

            return GetRoomsInConstruction(paremetersRoom);
        }

        public JsonResult EditRoomInConstruction(ParemetersRoom paremetersRoom)
        {
            Room room = ApplicationContext.Rooms.AsNoTracking().FirstOrDefault(x => x.Id == paremetersRoom.RoomId);
            if (room != null && room.Id == paremetersRoom.RoomId)
            {
                room.Name = paremetersRoom.Name;
                room.Floor = paremetersRoom.Floor;
                ApplicationContext.Rooms.Update(room);
                ApplicationContext.SaveChanges();
            }

            return GetRoomsInConstruction(paremetersRoom);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
