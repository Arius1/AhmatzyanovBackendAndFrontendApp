﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ahmatzyanov_lab2.DB;
using ahmatzyanov_lab2.Models;

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
            Fuel oldFuel = getFuelById(id);
            dbc.deleteFuel(oldFuel);
            dbc.createFuel(newFuel);
        }
        public void deleteFuel(int id)
        {
            Fuel fuel = getFuelById(id);
            dbc.deleteFuel(fuel);
        }
        public Fuel getWithGasStation(int id)
        {
            return dbc.selectFuelWithGasStation(id);
        }
    }
}