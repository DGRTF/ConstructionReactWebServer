using ConstructionReact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionReact.Controllers
{
    public class MachinesController: Controller
    {
        private ApplicationContext ApplicationContext { get; set; }

        public MachinesController(ApplicationContext context)
        {
            ApplicationContext = context;
        }

        public JsonResult GetMachinesInRoom(ParemetersMachine paremetersMachine, ParemetersRange paremetersRange)
        {
            var machines = ApplicationContext.Machines.AsNoTracking().Where(x => x.RoomId == paremetersMachine.RoomId).Skip(paremetersRange.Skip).Take(paremetersRange.Take);
            return Json(machines);
        }

        public JsonResult AddMachineInRoom(ParemetersMachine paremetersMachine, ParemetersRange paremetersRange)
        {
            Machine machine = new Machine
            {
                Name = paremetersMachine.Name,
                CreateYear = paremetersMachine.CreateYear,
                RoomId = paremetersMachine.RoomId
            };

            ApplicationContext.Machines.Add(machine);
            ApplicationContext.SaveChanges();

            return GetMachinesInRoom(paremetersMachine, paremetersRange);
        }

        public JsonResult DeleteMachineInRoom(ParemetersMachine paremetersMachine, ParemetersRange paremetersRange)
        {
            Machine machine = ApplicationContext.Machines.FirstOrDefault(x => x.Id == paremetersMachine.MachineId);
            if (machine != null && machine.Id == paremetersMachine.MachineId)
            {
                ApplicationContext.Machines.Remove(machine);
                ApplicationContext.SaveChanges();
            }

            return GetMachinesInRoom(paremetersMachine, paremetersRange);
        }

        public JsonResult EditMachineInRoom(ParemetersMachine paremetersMachine, ParemetersRange paremetersRange)
        {
            Machine machine = ApplicationContext.Machines.FirstOrDefault(x => x.Id == paremetersMachine.MachineId);
            if (machine != null && machine.Id == paremetersMachine.MachineId)
            {
                machine.Name = paremetersMachine.Name;
                machine.CreateYear = paremetersMachine.CreateYear;
                ApplicationContext.Machines.Update(machine);
                ApplicationContext.SaveChanges();
            }

            return GetMachinesInRoom(paremetersMachine, paremetersRange);
        }

        public JsonResult GetMachinesInConstruction(int constructionId, ParemetersRange paremetersRange)
        {
            var rooms = ApplicationContext.Rooms.AsNoTracking().Where(x => x.ConstructionId == constructionId).Include(x => x.Machines).ToList();
            List<Machine> machines = new List<Machine>();
            foreach (var room in rooms)
            {
                machines = machines.Concat(room.Machines).ToList();
            }

            var machinesShort = machines.Skip(paremetersRange.Skip).Take(paremetersRange.Take).AsQueryable();
            return Json(machinesShort);
        }

        public JsonResult EditMachinesInConstruction(int constructionId, ParemetersMachine paremetersMachine, ParemetersRange paremetersRange)
        {
            Machine machine = ApplicationContext.Machines.FirstOrDefault(x => x.Id == paremetersMachine.MachineId);
            if (machine != null && machine.Id == paremetersMachine.MachineId)
            {
                machine.Name = paremetersMachine.Name;
                machine.CreateYear = paremetersMachine.CreateYear;
                ApplicationContext.Machines.Update(machine);
                ApplicationContext.SaveChanges();
            }

            return GetMachinesInConstruction(constructionId, paremetersRange);
        }

        public JsonResult DeleteMachineInConstruction(int constructionId, ParemetersMachine paremetersMachine, ParemetersRange paremetersRange)
        {
            Machine machine = ApplicationContext.Machines.FirstOrDefault(x => x.Id == paremetersMachine.MachineId);
            if (machine != null && machine.Id == paremetersMachine.MachineId)
            {
                ApplicationContext.Machines.Remove(machine);
                ApplicationContext.SaveChanges();
            }

            return GetMachinesInConstruction(constructionId, paremetersRange);
        }


    }
}
