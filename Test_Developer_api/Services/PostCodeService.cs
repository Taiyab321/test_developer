using Developer_Test_Api.Dto;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using Test_Developer_api.Configuration;
using Test_Developer_api.Services.Interfaces;

namespace Test_Developer_api.Services
{
    public class PostCodeService : IPostCodeService
    {
        static HttpClient client = new HttpClient();

        private readonly AppConfiguration _appConfiguration;
        public PostCodeService(IConfiguration configuration, IOptions<AppConfiguration> appConfiguration)
        {
            _appConfiguration = appConfiguration.Value;
        }
        public async Task<AutocompletePostcodeDto> GetAutocompletePostcode(string postCode)
        {
            AutocompletePostcodeDto autocompletePostcode = new AutocompletePostcodeDto();
            try
            {
                var urlAutocompletePostcode = _appConfiguration.UrlPostcode + "postcodes/" + postCode + "/autocomplete";

                HttpResponseMessage response = await client.GetAsync(urlAutocompletePostcode);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    autocompletePostcode = JsonConvert.DeserializeObject<AutocompletePostcodeDto>(result);
                }
                return autocompletePostcode;
            }
            catch (Exception ex)
            {
                var w32ex = ex as Win32Exception;
                autocompletePostcode.Status = w32ex.ErrorCode;
                return autocompletePostcode;
            }
        }

        public async Task<LookupPostcodeDto> GetLookupPostcode(string postCode)
        {
            var urlLookupPostcode = _appConfiguration.UrlPostcode + "postcodes/" + postCode;

            LookupPostcodeDto lookupPostcode = new LookupPostcodeDto();
            try
            {
                HttpResponseMessage response = await client.GetAsync(urlLookupPostcode);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    
                    lookupPostcode = JsonConvert.DeserializeObject<LookupPostcodeDto>(result);

                    if (lookupPostcode != null && lookupPostcode.Result != null)
                    {
                        if (lookupPostcode.Result.Latitude < _appConfiguration.MinLatitude)
                        {
                            lookupPostcode.Result.Area = "South";
                        }
                        else if (lookupPostcode.Result.Latitude >= _appConfiguration.MinLatitude && lookupPostcode.Result.Latitude < _appConfiguration.MaxLatitude)
                        {
                            lookupPostcode.Result.Area = "Midlands";
                        }
                        else if (lookupPostcode.Result.Latitude >= _appConfiguration.MaxLatitude)
                        {
                            lookupPostcode.Result.Area = "North";
                        }
                        else
                        {
                            lookupPostcode.Result.Area = "Not Define";
                        }
                    }
                }
                return lookupPostcode;
            }
            catch (Exception ex)
            {
                var w32ex = ex as Win32Exception;
                lookupPostcode.Status = w32ex.ErrorCode;
                return lookupPostcode;
            }
        }
    }
}
