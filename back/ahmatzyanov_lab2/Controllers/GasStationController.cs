﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ahmatzyanov_lab2.Models;
using ahmatzyanov_lab2.Services;
using ahmatzyanov_lab2.Contexts;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ahmatzyanov_lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GasStationController : ControllerBase
    {
        GasStationService gasStationService = new GasStationService();

        [HttpGet]
        public IEnumerable<GasStation> Get()
        {
            return gasStationService.getGasStations();
        }

        [HttpGet("{id}")]
        public GasStation Get(int id)
        {
            return gasStationService.getGasStationById(id);
        }

        [HttpPost]
        [Authorize(Roles = "0")]
        public void Post( GasStation gasStation)
        {
            gasStationService.postGasStation(gasStation);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "0")]
        public void Put(int id, GasStation gasStation)
        {
            gasStationService.putGasStation(id, gasStation);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "0")]
        public void Delete(int id)
        {
            gasStationService.deleteGasStation(id);
        }

        [HttpGet("withFuels/{id}")]
        public GasStation GetWithFuels(int id)
        {
            return gasStationService.getGasStationWithFuels(id);
        }
        
        [HttpGet("fuelsOnly")]
        [Authorize]
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
        [HttpGet("fuelsOnlyWithPrice")]
        [Authorize]
        public IEnumerable<object> GetFuelsOnlyWithPrice()
        {
            using (GasStationWithFuelsContext db = new GasStationWithFuelsContext())
            {
                var q = from gs in db.GasStations
                        join f in db.Fuels on gs.Id equals f.GasStationId
                        select new { GasStationName = gs.Name, Fuel = f.Brand.ToString(), FuelPrice = f.Price };
                return q.ToList();
            }
        }
    }
}