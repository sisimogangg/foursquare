using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using foursquareApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace foursquareApi.Service {
    public interface IFoursquareService {
        Task<ActionResult<List<Place>>> GetRecommendedPlaces (Coordinates coordinates);
    }
}