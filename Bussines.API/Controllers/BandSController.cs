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
using Bussines.Common.Models;
using Bussines.Domain.Models;

namespace Bussines.API.Controllers
{
    public class BandSController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/BandS
        public IQueryable<BandS> GetBandS()
        {
            return db.BandS;
        }

        // GET: api/BandS/5
        [ResponseType(typeof(BandS))]
        public async Task<IHttpActionResult> GetBandS(int id)
        {
            BandS bandS = await db.BandS.FindAsync(id);
            if (bandS == null)
            {
                return NotFound();
            }

            return Ok(bandS);
        }

        // PUT: api/BandS/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBandS(int id, BandS bandS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bandS.BandSId)
            {
                return BadRequest();
            }

            db.Entry(bandS).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BandSExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BandS
        [ResponseType(typeof(BandS))]
        public async Task<IHttpActionResult> PostBandS(BandS bandS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BandS.Add(bandS);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bandS.BandSId }, bandS);
        }

        // DELETE: api/BandS/5
        [ResponseType(typeof(BandS))]
        public async Task<IHttpActionResult> DeleteBandS(int id)
        {
            BandS bandS = await db.BandS.FindAsync(id);
            if (bandS == null)
            {
                return NotFound();
            }

            db.BandS.Remove(bandS);
            await db.SaveChangesAsync();

            return Ok(bandS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BandSExists(int id)
        {
            return db.BandS.Count(e => e.BandSId == id) > 0;
        }
    }
}