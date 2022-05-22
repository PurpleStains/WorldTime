using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WorldTimeApp.Module.Model
{
    public class WorldTimeResponse
    {
        public WorldTimeModel TimeUTC { get; set; }
        public List<string> TimeZoneList { get; set; }
        public HttpStatusCode ?StatusCode { get; set; }

    }
}
