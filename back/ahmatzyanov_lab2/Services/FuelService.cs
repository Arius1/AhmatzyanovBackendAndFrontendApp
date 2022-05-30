using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ahmatzyanov_lab2.DB;
using ahmatzyanov_lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace ahmatzyanov_lab2.Services
{
    public class FuelService
    {
        private DBFuelCommands dbc = new DBFuelCommands();
        public List<Fuel> getFuels()
        {
            return dbc.selectAllFuels();
        }
        public Fuel getFuelById(int id)
        {
            return dbc.selectFuelById(id);
        }
        public void postFuel(Fuel fuel)
        {
            dbc.createFuel(fuel);
        }
        public void putFuel(int id, Fuel newFuel)
        {
            dbc.editFuel(id, newFuel);
        }
        public void deleteFuel(int id)
        {
            Fuel fuel = getFuelById(id);
            dbc.deleteFuel(fuel);
        }
        public List<Fuel> getWithGasStation(int id)
        {
            return dbc.selectFuelWithGasStation(id);
        }
        public IEnumerable<object> getWithGasStationBranded(int id)
        {
            return dbc.selectFuelWithGasStationBranded(id);
        }
    }
}
