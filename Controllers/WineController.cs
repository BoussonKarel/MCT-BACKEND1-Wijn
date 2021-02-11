using System;
using System.Collections.Generic;
using System.Linq;
using MCT_BACKEND1_Wijn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MCT_BACKEND1_Wijn.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WineController : ControllerBase
    {
        private readonly static List<Wine> _wines = new List<Wine>();
        private readonly ILogger<WineController> _logger;

        public WineController(ILogger<WineController> logger) {
            _logger = logger;

            if (_wines == null || _wines.Count() == 0) {
                _wines.Add(new Wine() {
                    WineId = 1,
                    Name = "Sangrato Barolo",
                    Country = "Italië",
                    Price = 35,
                    Color = "red",
                    Year = 2005,
                    Grapes = "Nebiollo"
                });
            }

            _logger.LogInformation("ctor");
        }

        [HttpGet]
        [Route("wines")]
        public ActionResult<List<Wine>> GetWines() {
            return new OkObjectResult(_wines);
        }

        [HttpGet]
        [Route("wine/{wineId}")]
        public ActionResult<Wine> GetWine(int wineId) {
            var wine = _wines.Where(w => w.WineId == wineId).SingleOrDefault();
            if (wine == null) {
                return new NotFoundObjectResult(wineId);
            }
            else {
                return wine;
            }
        }

        [HttpPost]
        [Route("wines")]
        public ActionResult<Wine> AddWine(Wine wine) {
            if (wine == null)
                return new BadRequestResult();

            wine.WineId = _wines.Count + 1;_wines.Add(wine);
            return new OkObjectResult(wine);
        }

        [HttpDelete]
        [Route("wine/{wineId}")]
        public ActionResult<Wine> DeleteWine(int wineId) {
            // Wine wine = _wines.Find((delegate(Wine w) {
            //     return w.WineId == wineId;
            // }));

            Wine wine = _wines.Find(w => w.WineId == wineId);

            if (wine != null) {
                _wines.Remove(wine);
            }
            else {
                return new NotFoundObjectResult(wineId);
            }

            return wine;
        }

        [HttpPut]
        [Route("wine")]
        public ActionResult<Wine> UpdateWine(Wine wine) {
            Wine existingWine = _wines.Find(w => w.WineId == wine.WineId);

            if (wine != null) {
                _wines.Remove(existingWine);
                _wines.Add(wine);
            }
            else {
                return new NotFoundObjectResult(wine);
            }

            return wine;
        }
    }
}
