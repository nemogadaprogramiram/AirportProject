using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlaneProject.Models
{
    public class Flights
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string LocationFrom { get; set; }
        [Required]
        [StringLength(20)]
        public string LocationTo { get; set; }
        public DateTime TimeOfFlight { get; set; }
        public DateTime TimeOfArrival { get; set; }

        [Required]
        [StringLength(20)]
        public string TypeOfPlane { get; set; }
        [Required]
        [StringLength(20)]
        public string UniqueNumberOfPlane { get; set; }
        [Required]
        [StringLength(20)]
        public string NamePilot { get; set; }
        [Required]
        [Range(0, 100)]
        public int CapacityOfPassengers  { get; set; }
        [Required]
        [Range(0, 100)]
        public int CapacityBusinessClass { get; set; }
       
        public int Duration { get; set; }

    }
}