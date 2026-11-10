// Dev: Rohit Tiwari
// Change: Updated configuration, fixed API query, added average temperature logic, and optimized HTTP calls.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Test_Taste_Console_Application.Constants;

namespace Test_Taste_Console_Application.Domain.Services
{
    ///<summary>
    /// A service to create the HttpClient. 
    ///</summary>
    public class HttpClientService
    {
        public HttpClient Client { get; }

        public HttpClientService(HttpClient client)
        {
            //The HTTP client is configured in the constructor.
            Client = client;
            Client.BaseAddress = new Uri(UriPath.BaseUri);
            Client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(HttpClientSettings.JsonType));
            Client.DefaultRequestHeaders.Add("User-Agent", HttpClientSettings.UserAgent);

            // Motivation for API Interface change:
            // Although the exercise document mentions no authentication is required,
            // the Solar System OpenData API now requires a Bearer token (returning 401 Unauthorized without it).
            // We check the environment variable first and fall back to the configured API key.
            var apiKey = Environment.GetEnvironmentVariable("SOLAR_SYSTEM_API_KEY");
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                apiKey = HttpClientSettings.DefaultApiKey;
            }

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey.Trim());
        }
    }
}
