// Dev: Rohit Tiwari
// Change: Updated configuration, fixed API query, added average temperature logic, and optimized HTTP calls.

using System;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Moon
    {
        public string Id { get; set; }
        public float MassValue { get; set; }
        public float MassExponent { get; set; }

        // Motivation for Domain Model change:
        // Added Temperature property to represent the moon's average temperature in the domain model.
        public float Temperature { get; set; }

        public Moon(MoonDto moonDto)
        {
            if (moonDto == null) return;
            Id = moonDto.Id;
            MassValue = moonDto.MassValue;
            MassExponent = moonDto.MassExponent;
            Temperature = moonDto.AvgTemp;
        }
    }
}
