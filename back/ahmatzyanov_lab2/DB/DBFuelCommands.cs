using ahmatzyanov_lab2.Contexts;
using ahmatzyanov_lab2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ahmatzyanov_lab2.DB
{
    public class DBFuelCommands
    {
        public List<Fuel> selectAllFuels()
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                return db.Fuels.ToList();
            }
        }
        public Fuel selectFuelById(int id)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                return db.Fuels.Find(id);
            }
        }
        public void createFuel(Fuel fuel)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                db.Fuels.Add(fuel);
                db.SaveChanges();
            }
        }
        public void deleteFuel(Fuel fuel)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                db.Fuels.Remove(fuel);
                db.SaveChanges();
            }
        }
        public List<Fuel> selectFuelWithGasStation(int id)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                return db.Fuels.Where(f => f.GasStationId == id).ToList();
            }
        }
        public IEnumerable<object> selectFuelWithGasStationBranded(int id)
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                var q = from f in db.Fuels.Where(x => x.GasStationId == id)
                        select new { id = f.Id, brand = f.Brand.ToString(), price = f.Price, value = f.Value, gasStationId = f.GasStationId };

                return q.ToList();
            }
        }
    }
}
