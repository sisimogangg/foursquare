using System;
using System.Collections.Generic;
using foursquareApi.Models;
using Newtonsoft.Json.Linq;

namespace foursquareApi.Extensions {
    public static class Extensions {

        // Iterating through two lists at the same time 
        public static void DoubleLoop<Place, JToken> (this IEnumerable<Place> places, IEnumerable<JToken> jTokens, Action<Place, JToken> action) {
            using (var pE = places.GetEnumerator ())
            using (var jE = jTokens.GetEnumerator ()) {
                while (pE.MoveNext () && jE.MoveNext ()) {
                    action (pE.Current, jE.Current);
                }
            }
        }
    }
}