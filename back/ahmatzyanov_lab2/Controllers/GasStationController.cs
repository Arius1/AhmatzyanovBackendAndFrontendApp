using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ahmatzyanov_lab2.Models;
using ahmatzyanov_lab2.Services;
using ahmatzyanov_lab2.Contexts;
using Microsoft.AspNetCore.Authorization;
using static ahmatzyanov_lab2.Models.FuelNames;

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

        [HttpPost("auth/post/")]
        [Authorize(Roles = "0")]
        public void Post( GasStation gasStation)
        {
            gasStationService.postGasStation(gasStation);
        }

        [HttpPut("auth/edit/{id}")]
        [Authorize(Roles = "0")]
        public void Put(int id, GasStation gasStation)
        {
            gasStationService.putGasStation(id, gasStation);
        }

        [HttpDelete("auth/delete/{id}")]
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
            return gasStationService.GetFuelsOnly();
        }
        [HttpGet("fuelsOnlyWithPrice")]
        [Authorize]
        public IEnumerable<object> GetFuelsOnlyWithPrice()
        {
            return gasStationService.GetFuelsOnlyWithPrice();
        }

        [HttpGet("fuelsWithPrice/{brand}")]
        public IEnumerable<object> GetGasStationWithBrand(Brands brand)
        {
            return gasStationService.GetGasStationWithBrand(brand);
        }
    }
}
