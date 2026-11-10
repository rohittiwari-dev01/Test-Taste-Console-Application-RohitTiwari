// Dev: Rohit Tiwari
// Change: Updated configuration, fixed API query, added average temperature logic, and optimized HTTP calls.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Test_Taste_Console_Application.Constants;
using Test_Taste_Console_Application.Domain.DataTransferObjects;
using Test_Taste_Console_Application.Domain.DataTransferObjects.JsonObjects;
using Test_Taste_Console_Application.Domain.Objects;
using Test_Taste_Console_Application.Domain.Services.Interfaces;
using Test_Taste_Console_Application.Utilities;

namespace Test_Taste_Console_Application.Domain.Services
{
    /// <inheritdoc />
    public class MoonService : IMoonService
    {
        private readonly HttpClientService _httpClientService;
        private Collection<Moon> _cachedMoons;

        public MoonService(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public IEnumerable<Moon> GetAllMoons()
        {
            if (_cachedMoons != null && _cachedMoons.Count > 0)
            {
                return _cachedMoons;
            }

            Console.WriteLine(OutputString.LoadingMoonsData);
            var allMoons = new Collection<Moon>();

            try
            {
                var response = _httpClientService.Client
                    .GetAsync(UriPath.GetAllMoonsWithMassQueryParameters)
                    .Result;

                //If the status code isn't 200-299, then the function returns an empty collection.
                if (!response.IsSuccessStatusCode)
                {
                    Logger.Instance.Warn($"{LoggerMessage.GetRequestFailed}{response.StatusCode}");
                    return allMoons;
                }

                var content = response.Content.ReadAsStringAsync().Result;

                //The JSON converter uses DTO's, that can be found in the DataTransferObjects folder, to deserialize the response content.
                var results = JsonConvert.DeserializeObject<JsonResult<MoonDto>>(content);

                //The JSON converter can return a null object. 
                if (results?.Bodies == null) return allMoons;

                foreach (MoonDto moonDto in results.Bodies)
                {
                    if (moonDto != null)
                    {
                        allMoons.Add(new Moon(moonDto));
                    }
                }

                _cachedMoons = allMoons;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error($"{LoggerMessage.GetRequestFailed}{ex.Message}");
            }

            return allMoons;
        }
    }
}
