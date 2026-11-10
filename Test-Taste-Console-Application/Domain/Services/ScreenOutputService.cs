// Dev: Rohit Tiwari
// Change: Updated configuration, fixed API query, added average temperature logic, and optimized HTTP calls.

using System;
using System.Linq;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.Objects;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;

namespace Test_Taste_Console_Application.Domain.Services
{
    /// <inheritdoc />
    public class ScreenOutputService : IOutputService
    {
        private readonly IPlanetService _planetService;

        private readonly IMoonService _moonService;

        public ScreenOutputService(IPlanetService planetService, IMoonService moonService)
        {
            _planetService = planetService;
            _moonService = moonService;
        }

        public void OutputAllPlanetsAndTheirMoonsToConsole()
        {
            //The service gets all the planets from the API.
            var planets = _planetService.GetAllPlanets().ToArray();

            //If the planets aren't found, then the function stops and tells that to the user via the console.
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            Console.WriteLine(OutputString.WritingPlanetsAndMoons);

            //The column sizes and labels for the planets are configured here. 
            var columnSizesForPlanets = new[] { 20, 20, 30, 20 };
            var columnLabelsForPlanets = new[]
            {
                OutputString.PlanetNumber, OutputString.PlanetId, OutputString.PlanetSemiMajorAxis,
                OutputString.TotalMoons
            };

            //The column sizes and labels for the moons are configured here.
            //The second moon's column needs the 2 extra '-' characters so that it's aligned with the planet's column.
            var columnSizesForMoons = new[] { 20, 70 + 2 };
            var columnLabelsForMoons = new[]
            {
                OutputString.MoonNumber, OutputString.MoonId
            };

            //The for loop creates the correct output.
            for (int i = 0, j = 1; i < planets.Length; i++, j++)
            {
                //First the line is created.
                ConsoleWriter.CreateLine(columnSizesForPlanets);

                //Under the line the header is created.
                ConsoleWriter.CreateText(columnLabelsForPlanets, columnSizesForPlanets);

                //Under the header the planet data is shown.
                ConsoleWriter.CreateText(
                    new[]
                    {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Id),
                        planets[i].SemiMajorAxis.ToString(),
                        planets[i].Moons.Count.ToString()
                    },
                    columnSizesForPlanets);

                //Under the planet data the header for the moons is created.
                ConsoleWriter.CreateLine(columnSizesForPlanets);
                ConsoleWriter.CreateText(columnLabelsForMoons, columnSizesForMoons);

                //The for loop creates the correct output.
                for (int k = 0, l = 1; k < planets[i].Moons.Count; k++, l++)
                {
                    ConsoleWriter.CreateText(
                        new[]
                        {
                            l.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(planets[i].Moons.ElementAt(k).Id)
                        },
                        columnSizesForMoons);
                }

                //Under the data the footer is created.
                ConsoleWriter.CreateLine(columnSizesForMoons);
                ConsoleWriter.CreateEmptyLines(2);
            }
        }

        public void OutputAllMoonsAndTheirMassToConsole()
        {
            var moons = _moonService.GetAllMoons().ToArray();

            if (!moons.Any())
            {
                Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            Console.WriteLine(OutputString.WritingMoonsAndMass);

            var columnSizesForMoons = new[] { 20, 20, 30, 20 };
            var columnLabelsForMoons = new[]
            {
                OutputString.MoonNumber, OutputString.MoonId, OutputString.MoonMassExponent, OutputString.MoonMassValue
            };

            ConsoleWriter.CreateHeader(columnLabelsForMoons, columnSizesForMoons);

            for (int i = 0, j = 1; i < moons.Length; i++, j++)
            {
                ConsoleWriter.CreateText(
                    new[]
                    {
                        j.ToString(), CultureInfoUtility.TextInfo.ToTitleCase(moons[i].Id),
                        moons[i].MassExponent.ToString(), moons[i].MassValue.ToString()
                    },
                    columnSizesForMoons);
            }

            ConsoleWriter.CreateLine(columnSizesForMoons);
            ConsoleWriter.CreateEmptyLines(2);
        }

        public void OutputAllPlanetsAndTheirAverageMoonGravityToConsole()
        {
            var planets = _planetService.GetAllPlanets().ToArray();
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            Console.WriteLine(OutputString.WritingPlanetsAverageMoonGravity);

            var columnSizes = new[] { 20, 30 };
            var columnLabels = new[]
            {
                OutputString.PlanetId, OutputString.PlanetMoonAverageGravity
            };

            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            foreach (Planet planet in planets)
            {
                if (planet.HasMoons())
                {
                    ConsoleWriter.CreateText(new string[] { $"{CultureInfoUtility.TextInfo.ToTitleCase(planet.Id)}", $"{planet.AverageMoonGravity}" }, columnSizes);
                }
                else
                {
                    ConsoleWriter.CreateText(new string[] { $"{CultureInfoUtility.TextInfo.ToTitleCase(planet.Id)}", $"-" }, columnSizes);
                }
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);
        }

        public void OutputPlanetsWithMoonsAndAverageMoonTemperature()
        {
            var planets = _planetService.GetAllPlanets().ToArray();
            if (!planets.Any())
            {
                Console.WriteLine(OutputString.NoPlanetsFound);
                return;
            }

            var planetsWithMoons = planets.Where(p => p.HasMoons()).ToArray();
            if (!planetsWithMoons.Any())
            {
                Console.WriteLine(OutputString.NoMoonsFound);
                return;
            }

            Console.WriteLine(OutputString.WritingPlanetsAverageMoonTemperature);

            var columnSizes = new[] { 20, 40 };
            var columnLabels = new[]
            {
                OutputString.PlanetId, OutputString.PlanetMoonAverageTemperature
            };

            ConsoleWriter.CreateHeader(columnLabels, columnSizes);

            foreach (var planet in planetsWithMoons)
            {
                ConsoleWriter.CreateText(
                    new string[]
                    {
                        CultureInfoUtility.TextInfo.ToTitleCase(planet.Id),
                        $"{planet.AverageMoonTemperature}"
                    },
                    columnSizes);
            }

            ConsoleWriter.CreateLine(columnSizes);
            ConsoleWriter.CreateEmptyLines(2);
        }
    }
}
