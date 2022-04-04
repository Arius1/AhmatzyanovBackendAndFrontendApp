using ahmatzyanov_lab2.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ahmatzyanov_lab2.Models;

namespace ahmatzyanov_lab2.Services
{
    public class GasStationService
    {
        private DBGasStationCommands dbc = new DBGasStationCommands();

        public List<GasStation> getGasStations()
        {
            return dbc.selectAllGasStations();
        }
        public GasStation getGasStationById(int id)
        {
            return dbc.selectGasStationById(id);
        }
        public void postGasStation(GasStation gasStation)
        {
            dbc.createGasStation(gasStation);
        }
        public void putGasStation(int id, GasStation newGasStation)
        {
            GasStation oldGasStation = getGasStationById(id);
            dbc.deleteGasStation(oldGasStation);
            dbc.createGasStation(newGasStation);
        }
        public void deleteGasStation(int id)
        {
            GasStation gasStation = getGasStationById(id);
            dbc.deleteGasStation(gasStation);
        }

        public GasStation getGasStationWithFuels(int id)
        {
            return dbc.selectGasStationWithFuels(id);
        }
    }
}
