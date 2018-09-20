using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using foursquareApi.Extensions;
using foursquareApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace foursquareApi.Service {
    public class FoursquareService : IFoursquareService {
        private string ClientSecret;
        private string ClientID;
        private const string BaseAddress = "https://api.foursquare.com";
        private readonly HttpClientHandler Handler = new HttpClientHandler ();

        public FoursquareService (IConfiguration _configuration) {
            // This information can be kept away using Secret Manager / Key Vault but for now.. 
            ClientSecret = _configuration.GetValue<string> ("Credentials:ClientSecret");
            ClientID = _configuration.GetValue<string> ("Credentials:ClientID");
        }

        public async Task<ActionResult<List<Place>>> GetRecommendedPlaces (Coordinates coordinates) {
            var url = "recommendations?ll=" + coordinates.Lat + "," + coordinates.Lng + "&v=20171121&intent=browse&limit=15&client_id=" + ClientID + "&client_secret=" + ClientSecret;
            var places = await SearchForPlaces (url);
            if (places == null) {
                return new NotFoundResult ();
            }
            return places;
        }

        private async Task<List<Place>> SearchForPlaces (string subUrl) {
            var result = "";
            using (var client = new HttpClient (Handler, false)) {
                client.BaseAddress = new System.Uri (BaseAddress);
                var response = await client.GetAsync ("/v2/search/" + subUrl);
                response.EnsureSuccessStatusCode ();
                result = await response.Content.ReadAsStringAsync ();
            }
        
            return DeserializePlaces (result); //DeserializePlaces could have been better as extension rather
        }

        private List<Place> DeserializePlaces (string res) {
            JObject foursquareApiResp = JObject.Parse (res);
            List<Place> places = JsonConvert.DeserializeObject<List<Place>> (foursquareApiResp["response"]["group"]["results"].ToString ());

            /*
             We have to manually map certain entities to our POCO since the model we deserializing into is somewhat structurally
             different from what we receiving from foursquare 
             */
            var placesJsonJtoken = foursquareApiResp["response"]["group"]["results"].Children ();
            places.DoubleLoop (placesJsonJtoken, (p, j) => CopyFromTo (p, j)); // courtesy of extension methods 

            return places;

        }

        // Extracting required information from nested json objects 
        private void CopyFromTo (Place place, JToken jToken) {
            var cordslat = jToken["venue"]["location"]["lat"];
            var cordslng = jToken["venue"]["location"]["lng"];

            place.Venue.VenueCategories = new List<string> ();
            foreach (var item in jToken["venue"]["categories"].Children ()) {
                place.Venue.VenueCategories.Add (item["name"].ToString ());
            }

            place.Venue.Location.Coordinates = new Coordinates { Lat = cordslat.ToString (), Lng = cordslng.ToString () };
        }
    }

}