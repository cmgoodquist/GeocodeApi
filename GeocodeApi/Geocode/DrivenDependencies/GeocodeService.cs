﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeocodeApi.Geocode.DrivenDependencies
{
    public class GeocodeService : IGeocodeService
    {
        private readonly HttpClient _client;

        public GeocodeService(HttpClient client) => _client = client;

        public async Task<HttpResponseMessage> GeocodeAddress(string street, string city, string stateCode, string zipCode)
        {
            if (string.IsNullOrEmpty(street) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(stateCode) || stateCode.Length != 2)
                throw new InvalidAddressException();

            //Character substitution expected by target API.
            var plusEncodedStreet = street.Replace(' ', '+');
            var plusEncodedCity = city.Replace(' ', '+');
            var addressQueryString = $"?street={plusEncodedStreet}&city={plusEncodedCity}&state={stateCode}";
            if (!string.IsNullOrWhiteSpace(zipCode))
                addressQueryString += $"&zip={zipCode}";

            var response = await _client.GetAsync($"{addressQueryString}&benchmark=Public_AR_Current&format=json");
            return response;
        }
    }
}
