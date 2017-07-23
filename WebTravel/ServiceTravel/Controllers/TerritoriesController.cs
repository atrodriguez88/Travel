using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ModelTravel;
using System.Web.Http.Cors;

namespace ServiceTravel.Controllers
{
    [EnableCors("*","*","*")]
    [Authorize]
    [RoutePrefix("api/Territories")]
    public class TerritoriesController : ApiController
    {
        private DBTravelEntities db = new DBTravelEntities();

        // GET: api/Territories
        //This is a example
        [Route("")]
        [AllowAnonymous]
        public List<string> GetTerritories()
        {
            return db.Territories.Select(t =>t.TerritoryDescription).Take(10).ToList();
        }
        
        [Route("Unit")]
        [AllowAnonymous]
        public List<UnitPrice_DistictEmployee> GetUnitPrice()
        {
            return db.UnitPrice_DistictEmployee.ToList();
        }
        // GET: api/Territories/5
        [ResponseType(typeof(Territory))]
        public async Task<IHttpActionResult> GetTerritory(string id)
        {
            Territory territory = await db.Territories.FindAsync(id);
            if (territory == null)
            {
                return NotFound();
            }

            return Ok(territory.TerritoryDescription);
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TerritoryExists(string id)
        {
            return db.Territories.Count(e => e.TerritoryID == id) > 0;
        }
    }
}