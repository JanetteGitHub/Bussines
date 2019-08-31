
namespace Bussines.API.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Bussines.API.Helpers;
    using Bussines.Common.Models;
    using Bussines.Domain.Models;
   
    public class BandSController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/BandS
        public IQueryable<BandS> GetBandS()
        {
            return this.db.BandS.OrderBy(p=> p.Description);
        }

        // GET: api/BandS/5
        [ResponseType(typeof(BandS))]
        public async Task<IHttpActionResult> GetBandS(int id)
        {
            BandS bandS = await this.db.BandS.FindAsync(id);
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
            if (bandS.ImageArray != null && bandS.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(bandS.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/BandSs";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    bandS.ImagePath = fullPath;
                }
            }

            this.db.Entry(bandS).State = EntityState.Modified;

            try
            {
                await this.db.SaveChangesAsync();
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

            return Ok(bandS);
        }

        // POST: api/BandS
        [ResponseType(typeof(BandS))]
        public async Task<IHttpActionResult> PostBandS(BandS bandS)
        {
            bandS.PublishOn = DateTime.Now.ToUniversalTime();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bandS.ImageArray != null && bandS.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(bandS.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/BandSs";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    bandS.ImagePath = fullPath;
                }
            }


            this.db.BandS.Add(bandS);
            await this.db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bandS.BandSId }, bandS);
        }

        // DELETE: api/BandS/5
        [ResponseType(typeof(BandS))]
        public async Task<IHttpActionResult> DeleteBandS(int id)
        {
            BandS bandS = await this.db.BandS.FindAsync(id);
            if (bandS == null)
            {
                return NotFound();
            }

            this.db.BandS.Remove(bandS);
            await this.db.SaveChangesAsync();

            return Ok(bandS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BandSExists(int id)
        {
            return this.db.BandS.Count(e => e.BandSId == id) > 0;
        }
    }
}