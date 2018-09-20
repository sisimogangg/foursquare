using Newtonsoft.Json;
using System.Collections.Generic;

namespace foursquareApi.Models {
    public class Location {
        [JsonProperty ("address")]
        public string Address { get; set; }

        [JsonProperty ("distance")]
        public int Distance { get; set; }

        [JsonProperty ("formattedAddress")]
        public List<string> FormattedAddress { get; set; }

        public Coordinates Coordinates { get; set; }
    }
}