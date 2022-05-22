using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WorldTimeApp.Module.Model;

namespace WorldTimeApp.Module.Service
{
    public class GetWorldTime : IGetWorldTime
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;
        public GetWorldTime(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient();
        }

        public WorldTimeResponse Get()
        {
            string url = @"http://worldtimeapi.org/api/timezone/";
            WorldTimeModel worldTime = new WorldTimeModel();
            List<string> timeZoneList = new List<string>();
            WorldTimeResponse result = new WorldTimeResponse();
            HttpStatusCode ?status = new HttpStatusCode();
            HttpResponseMessage response;


            List<string> GetTimeZones()
            {
                response = _client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                timeZoneList = JsonConvert.DeserializeObject<List<string>>(response.Content.ReadAsStringAsync().Result);

                return timeZoneList;
            }

            WorldTimeModel GetTime(string zone)
            {
                response = _client.GetAsync(url + zone).Result;
                response.EnsureSuccessStatusCode();

                worldTime = JsonConvert.DeserializeObject<WorldTimeModel>(response.Content.ReadAsStringAsync().Result);

                return worldTime;
            }

            if (_config["Timezone"].Length > 0)
            {
                    try
                    {
                        worldTime = GetTime(_config["Timezone"]);
                    }
                    catch (HttpRequestException ex)
                    {
                        timeZoneList = GetTimeZones();
                        status = ex.StatusCode;
                    }
            }
            else
            {
                try
                {
                    timeZoneList = GetTimeZones();

                }
                catch (HttpRequestException ex)
                {
                    status = ex.StatusCode;
                }
            }


            result = new WorldTimeResponse()
            {
                TimeUTC = worldTime,
                TimeZoneList = timeZoneList,
                StatusCode = status
            };

            return result;
        }
    }
}
