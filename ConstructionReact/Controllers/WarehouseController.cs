using ConstructionReact.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionReact.Controllers
{
    public class ConstructionsController : Controller
    {
        private ApplicationContext ApplicationContext { get; set; }

        public ConstructionsController(ApplicationContext context)
        {
            ApplicationContext = context;
        }

        public JsonResult GetConstructions(ParemetersRange paremetersRange)
        {
            var constructions = ApplicationContext.Constructions.AsNoTracking().Include(x => x.Rooms)
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    address = x.Address,
                    haveMachine = x.Rooms
                .SelectMany(y => y.Machines)
                .Any()
                }).Skip(paremetersRange.Skip).Take(paremetersRange.Take);

            return Json(constructions);
        }

        public JsonResult AddConstruction(ParemetersConstruction paremetersConstruction, ParemetersRange paremetersRange)
        {
            Construction construction = new Construction
            {
                Name = paremetersConstruction.Name,
                Address = paremetersConstruction.Address
            };

            ApplicationContext.Constructions.Add(construction);
            ApplicationContext.SaveChanges();

            return GetConstructions(paremetersRange);
        }

        public JsonResult DeleteConstruction(ParemetersConstruction paremetersConstruction, ParemetersRange paremetersRange)
        {
            Construction construction = ApplicationContext.Constructions.FirstOrDefault(x => x.Id == paremetersConstruction.ConstructionId);
            if (construction != null && construction.Id == paremetersConstruction.ConstructionId)
            {
                ApplicationContext.Constructions.Remove(construction);
                ApplicationContext.SaveChanges();
            }

            return GetConstructions(paremetersRange);
        }

        public JsonResult EditConstruction(ParemetersConstruction paremetersConstruction, ParemetersRange paremetersRange)
        {
            Construction construction = ApplicationContext.Constructions.FirstOrDefault(x => x.Id == paremetersConstruction.ConstructionId);
            if (construction != null && construction.Id == paremetersConstruction.ConstructionId)
            {
                construction.Name = paremetersConstruction.Name;
                construction.Address = paremetersConstruction.Address;
                ApplicationContext.Constructions.Update(construction);
                ApplicationContext.SaveChanges();
            }

            return GetConstructions(paremetersRange);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            List<int> list = new List<int>(0);
            return Json(list);
        }
    }

    public class ParemetersRange
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }

    public class ParemetersConstruction
    {
        public int ConstructionId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class ParemetersRoom
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int ConstructionId { get; set; }
    }

    public class ParemetersMachine
    {
        public int MachineId { get; set; }
        public string Name { get; set; }
        public int CreateYear { get; set; }
        public int RoomId { get; set; }
    }
}
