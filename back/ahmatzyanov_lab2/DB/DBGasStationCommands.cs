using ahmatzyanov_lab2.Contexts;
using ahmatzyanov_lab2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static ahmatzyanov_lab2.Models.FuelNames;

namespace ahmatzyanov_lab2.DB
{
    public class DBGasStationCommands
    {
        public List<GasStation> selectAllGasStations()
        {
            using(GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                return db.GasStations.ToList();
            }
        }
        public GasStation selectGasStationById(int id)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                return db.GasStations.Find(id);
            }
        }
        public void createGasStation(GasStation gasStation)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                db.GasStations.Add(gasStation);
                db.SaveChanges();
            }
        }
        public void deleteGasStation(GasStation gasStation)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                db.GasStations.Remove(gasStation);
                db.SaveChanges();
            }
        }
        public GasStation selectGasStationWithFuels(int id)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                return db.GasStations.Include(i => i.Fuels).FirstOrDefault(gs => gs.Id == id);
            }
        }
        public IEnumerable<object> GetFuelsOnly()
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                var q = from gs in db.GasStations
                        join f in db.Fuels on gs.Id equals f.GasStationId
                        select new { GasStationName = gs.Name, Fuel = f.Brand.ToString() };
                return q.ToList();
            }
        }
        public IEnumerable<object> GetFuelsOnlyWithPrice()
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                var q = from gs in db.GasStations
                        join f in db.Fuels on gs.Id equals f.GasStationId
                        select new { GasStationName = gs.Name, Fuel = f.Brand.ToString() };
                return q.ToList();
            }
        }
        public IEnumerable<object> GetGasStationWithBrand(Brands brand)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                var q = from gs in db.GasStations
                        join f in db.Fuels.Where(x => x.Brand == brand && x.Value > 0) on gs.Id equals f.GasStationId
                        select new { GasStationName = gs.Name, GasStationAddress = gs.Address, FuelValue = f.Value };
                
                return q.ToList();
            }
        }
    }
}
