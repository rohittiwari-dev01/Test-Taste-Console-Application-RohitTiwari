// Dev: Rohit Tiwari
// Change: Updated configuration, fixed API query, added average temperature logic, and optimized HTTP calls.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }

        // Motivation for Domain Model change:
        // Updated the starting-point property and added AverageMoonTemperature to compute
        // the mean temperature across all moons belonging to this planet.
        public float AverageMoonGravity
        {
            get => AverageMoonTemperature;
        }

        public float AverageMoonTemperature
        {
            get => HasMoons() ? Moons.Average(m => m.Temperature) : 0.0f;
        }

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if (planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    if (moonDto != null)
                    {
                        Moons.Add(new Moon(moonDto));
                    }
                }
            }
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }

        public float GetAvgTemperatureOfMoons()
        {
            return AverageMoonTemperature;
        }
    }
}
