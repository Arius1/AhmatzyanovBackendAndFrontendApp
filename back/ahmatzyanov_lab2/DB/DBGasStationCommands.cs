using ahmatzyanov_lab2.Contexts;
using ahmatzyanov_lab2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
    }
}
