// Dev: Rohit Tiwari
// Change: Updated configuration, fixed API query, added average temperature logic, and optimized HTTP calls.

using System.Linq;
using Newtonsoft.Json;
using Test_Taste_Console_Application.Domain.DataTransferObjects.JsonObjects;

namespace Test_Taste_Console_Application.Domain.DataTransferObjects
{
    //The converter is needed for this DTO. Because of the converter, all the properties need to get the JsonProperty annotation. 
    [Newtonsoft.Json.JsonConverter(typeof(JsonPathConverter))]
    public class MoonDto
    {
        [JsonProperty("id")] public string Id { get; set; }

        //The property moon is used to set the Id property. 
        [JsonProperty("moon")]
        public string Moon
        {
            get => Id;
            set => Id = value;
        }

        //The path of the specific moon
        [JsonProperty("rel")] public string Rel { get; set; }
        public string URLId => !string.IsNullOrEmpty(Rel) ? Rel.Split('/').Last() : Id;

        //The path to the nested property is created by using a dot. 
        [JsonProperty("mass.massValue")] public float MassValue { get; set; }
        [JsonProperty("mass.massExponent")] public float MassExponent { get; set; }

        // Motivation for API Interface / DTO change:
        // Added AvgTemp mapped to 'avgTemp' from the Solar System API to deserialize each moon's temperature.
        [JsonProperty("avgTemp")] public float AvgTemp { get; set; }
    }
}
