using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ahmatzyanov_lab2.Models
{
    [Serializable]
    public class GasStation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public long? PhoneNumber { get; set; }
        public List<Fuel> Fuels { get; set; } = new();

        public GasStation(List<Fuel> fuels, string name, string address, long phoneNumber)
        {
            this.Fuels = fuels;
            this.Name = name;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
        }

        public GasStation() { }
    }
}
