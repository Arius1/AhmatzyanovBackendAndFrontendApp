using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static ahmatzyanov_lab2.Models.FuelNames;

namespace ahmatzyanov_lab2.Models
{
    [Serializable]
    public class Fuel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Brands Brand { get; set; }
        public int Price { get; set; }
        public int Value { get; set; }

        public int GasStationId { get; set; }
        public GasStation? GasStation { get; set; }

        public Fuel(Brands brand, int price, int value, int gasStationId, GasStation? gasStation)
        {
            this.Brand = brand;
            Price = price;
            Value = value;
            GasStationId = gasStationId;
            GasStation = gasStation;
        }

        public Fuel()
        {
        }
    }
    
}
