using Newtonsoft.Json;
using System.Collections.Generic;

namespace foursquareApi.Models {
    public class Venue {
        [JsonProperty ("id")]
        public string Id { get; set; }

        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("location")]
        public Location Location { get; set; }

        public List<string> VenueCategories { get; set; }

        [JsonProperty ("stats")]
        public Stats Stats { get; set; }
    }
}