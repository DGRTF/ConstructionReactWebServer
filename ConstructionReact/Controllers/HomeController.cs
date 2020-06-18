using ConstructionReact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace ConstructionReact.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly ILogger<WarehouseController> _logger;
        private ApplicationContext applicationContext { get; set; }

        public WarehouseController(ILogger<WarehouseController> logger, ApplicationContext context)
        {
            _logger = logger;
            applicationContext = context;
        }

        public JsonResult GetConstructions(int skip = 0, int take = 10)
        {
            var constructions = applicationContext.Constructions.AsNoTracking().Skip(skip).Take(take);
            return Json(constructions);
        }

        public JsonResult AddConstruction(string name, string address, int skip = 0, int take = 10)
        {
            Construction construction = new Construction
            {
                Name = name,
                Address = address
            };

            applicationContext.Constructions.Add(construction);
            applicationContext.SaveChanges();
            var constructions = applicationContext.Constructions.AsNoTracking().Skip(skip).Take(take);
            return Json(constructions);
        }

        public JsonResult DeleteConstruction(int id, int skip = 0, int take = 10)
        {
            Construction construction = applicationContext.Constructions.FirstOrDefault(x => x.Id == id);
            if (construction != null && construction.Id == id)
            {
                applicationContext.Constructions.Remove(construction);
                applicationContext.SaveChanges();
            }

            var constructions = applicationContext.Constructions.AsNoTracking().Skip(skip).Take(take);
            return Json(constructions);
        }

        public JsonResult EditConstruction(int id, string name, string address, int skip = 0, int take = 10)
        {
            Construction construction = applicationContext.Constructions.FirstOrDefault(x => x.Id == id);
            if (construction != null && construction.Id == id)
            {
                construction.Name = name;
                construction.Address = address;
                applicationContext.Constructions.Update(construction);
                applicationContext.SaveChanges();
            }

            var constructions = applicationContext.Constructions.AsNoTracking().Skip(skip).Take(take);
            return Json(constructions);
        }



        public JsonResult GetMachines(int skip = 0, int take = 10)
        {
            var machines = applicationContext.Machines.AsNoTracking().Skip(skip).Take(take);
            return Json(machines);
        }

        public JsonResult AddMachines(string name, int createYear, int skip = 0, int take = 10)
        {
            Machine machine = new Machine
            {
                Name = name,
                CreateYear = createYear
            };

            applicationContext.Machines.Add(machine);
            applicationContext.SaveChanges();
            var machines = applicationContext.Machines.AsNoTracking().Skip(skip).Take(take);
            return Json(machines);
        }

        public JsonResult DeleteMachines(int id, int skip = 0, int take = 10)
        {
            Machine machine = applicationContext.Machines.FirstOrDefault(x => x.Id == id);
            if (machine != null && machine.Id == id)
            {
                applicationContext.Machines.Remove(machine);
                applicationContext.SaveChanges();
            }

            var machines = applicationContext.Machines.AsNoTracking().Skip(skip).Take(take);
            return Json(machines);
        }

        public JsonResult EditMachines(int id, string name, int createYear, int skip = 0, int take = 10)
        {
            Machine machine = applicationContext.Machines.FirstOrDefault(x => x.Id == id);
            if (machine != null && machine.Id == id)
            {
                machine.Name = name;
                machine.CreateYear = createYear;
                applicationContext.Machines.Update(machine);
                applicationContext.SaveChanges();
            }

            var machines = applicationContext.Machines.AsNoTracking().Skip(skip).Take(take);
            return Json(machines);
        }

        //public JsonResult GetMachineInConstruction(int id, int skip = 0, int take = 10)
        //{

        //}

        public JsonResult GetRoomsInConstruction(int id)
        {
            var rooms = applicationContext.Rooms.AsNoTracking().Where(x => x.ConstructionId == id);
            return Json(rooms);
        }

        public JsonResult AddRoomInConstruction(int id,string name,int floor)
        {
            Room room = new Room
            {
                Name=name,
                Floor=floor,
                ConstructionId=id
            };

            applicationContext.Rooms.Add(room);
            applicationContext.SaveChanges();
            var rooms = applicationContext.Rooms.AsNoTracking().Where(x => x.ConstructionId == id);
            return Json(rooms);
        }

        public JsonResult GetMachineInRoom(int id, int skip = 0, int take = 10)
        {
            var machines = applicationContext.Machines.AsNoTracking().Where(x => x.RoomId == id).Skip(skip).Take(take);
            return Json(machines);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
