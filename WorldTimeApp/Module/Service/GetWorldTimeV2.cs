using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WorldTimeApp.Module.Model;

namespace WorldTimeApp.Module.Service
{
    public class GetWorldTimeV2
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        public GetWorldTimeV2(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<WorldTimeResponse> Get()
        {
            WorldTimeModel worldTimeRes = new WorldTimeModel();
            List<string> timeZonelistRes = new List<string>();
            var client = _clientFactory.CreateClient("WorldTime");
            HttpStatusCode? status = new HttpStatusCode();


            async Task GetTimeZones()
            {
                var response = await client.GetAsync("");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                timeZonelistRes = JsonConvert.DeserializeObject<List<string>>(content);

                status = response.StatusCode;

            }

            if (_config["Timezone"].Length > 0)
            {
                try
                {
                    var response = await client.GetAsync(_config["Timezone"]);
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();

                    worldTimeRes = JsonConvert.DeserializeObject<WorldTimeModel>(content);


                }
                catch (HttpRequestException ex)
                {
                    worldTimeRes = null;
                    await GetTimeZones();
                }
            }
            else
            {
                try
                {
                   await GetTimeZones();
                }
                catch (HttpRequestException ex)
                {
                    status = ex.StatusCode;
                }
            }

            var result = new WorldTimeResponse()
            {
                TimeUTC = worldTimeRes,
                TimeZoneList = timeZonelistRes,
                StatusCode = status
            };

            return result;
        }
    }
}
