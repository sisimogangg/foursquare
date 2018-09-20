using Newtonsoft.Json;

namespace foursquareApi.Models {
    public class Place {

        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("displayType")]
        public string DisplayType { get; set; }

        [JsonProperty ("venue")]
        public Venue Venue { get; set; }

        [JsonProperty ("photo")]
        public Photo Photo { get; set; }

    }

}