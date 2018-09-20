using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using foursquareApi.Models;
using foursquareApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace foursquareApi.Controllers {
    [Route ("api/places")]
    [ApiController]
    public class PlacesController : ControllerBase {
        private readonly IFoursquareService FoursquareService;
        public PlacesController (IFoursquareService foursquareService) {
            FoursquareService = foursquareService;
        }

        [HttpGet]
        // [ProducesResponseType (200)]
        public async Task<ActionResult<List<Place>>> Get ([FromQuery] Coordinates coordinates) {
            return await FoursquareService.GetRecommendedPlaces (coordinates);
        }
    }

}