

namespace Bussines.Backend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Bussines.Backend.Models;
    using Bussines.Common.Models;
    using Bussines.Backend.Helpers;

    public class BandSController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: BandS
        public async Task<ActionResult> Index()
        {
            return View(await this.db.BandS.OrderBy(p => p.Description).ToListAsync());
        }

        // GET: BandS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bandS = await this.db.BandS.FindAsync(id);
            if (bandS == null)
            {
                return HttpNotFound();
            }
            return View(bandS);
        }

        // GET: BandS/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BandSView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/BandS";

                if (view.ImageFile != null)
                {

                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";

                }

                var bandS = this.ToBandS(view, pic);
                this.db.BandS.Add(bandS);
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private BandS ToBandS(BandSView view, string pic)
        {
            return new BandS
            {
                Description = view.Description,
                ImagePath = pic,
                Address = view.Address,
                Phone = view.Phone,
                BandSId = view.BandSId,
                 PublishOn= view.PublishOn,
                Remarks = view.Remarks,
            };
        }

        // GET: BandS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bandS = await this.db.BandS.FindAsync(id);

            if (bandS == null)
            {
                return HttpNotFound();
            }
            var view = this.ToView(bandS);
            return View(view);
        }

        private BandSView ToView(BandS bandS)
        {
            return new BandSView
            {
                Description = bandS.Description,
                ImagePath = bandS.ImagePath,
                Address = bandS.Address,
                Phone = bandS.Phone,
                BandSId = bandS.BandSId,
                PublishOn = bandS.PublishOn,
                Remarks = bandS.Remarks,
            };
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BandSView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/BandS";

                if (view.ImageFile != null)
                {

                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";

                }

                var bandS = this.ToBandS(view, pic);
                this.db.Entry(bandS).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: BandS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bandS = await this.db.BandS.FindAsync(id);
            if (bandS == null)
            {
                return HttpNotFound();
            }
            return View(bandS);
        }

        // POST: BandS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var bandS = await this.db.BandS.FindAsync(id);
            this.db.BandS.Remove(bandS);
            await this.db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
