using Newtonsoft.Json;

namespace foursquareApi.Models {
        public class Coordinates {
        [JsonProperty ("lat")]
        public string Lat { get; set; }

        [JsonProperty ("lng")]
        public string Lng { get; set; }
    }
}