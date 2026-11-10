// Dev: Rohit Tiwari
// Change: Updated configuration, fixed API query, added average temperature logic, and optimized HTTP calls.

namespace Test_Taste_Console_Application.Constants
{
    public static class UriPath
    {
        public const string BaseUri = "https://api.le-systeme-solaire.net";
        private const string BodiesUri = "/rest/bodies";

        public const string GetAllPlanetsWithMoonsQueryParameters =
            BodiesUri + "?data=id,semiMajorAxis,moons,moon,rel&filter[]=isPlanet,neq,false";

        // Motivation for API Interface change:
        // Added 'avgTemp' to the query parameters so moon temperature data is retrieved in bulk
        // alongside mass data, avoiding hundreds of individual HTTP requests per moon.
        public const string GetAllMoonsWithMassQueryParameters = BodiesUri +
            "?data=id,mass,massValue,massExponent,avgTemp&filter[]=aroundPlanet,gt,null";

        public const string GetMoonByIdQueryParameters = BodiesUri + "/";
    }
}
