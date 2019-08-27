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

namespace Bussines.Backend.Controllers
{
    public class BandSController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: BandS
        public async Task<ActionResult> Index()
        {
            return View(await db.BandS.ToListAsync());
        }

        // GET: BandS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandS bandS = await db.BandS.FindAsync(id);
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

        // POST: BandS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BandSId,Description,Address,Phone,Remarks,PublishOn")] BandS bandS)
        {
            if (ModelState.IsValid)
            {
                db.BandS.Add(bandS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bandS);
        }

        // GET: BandS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandS bandS = await db.BandS.FindAsync(id);
            if (bandS == null)
            {
                return HttpNotFound();
            }
            return View(bandS);
        }

        // POST: BandS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BandSId,Description,Address,Phone,Remarks,PublishOn")] BandS bandS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bandS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bandS);
        }

        // GET: BandS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandS bandS = await db.BandS.FindAsync(id);
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
            BandS bandS = await db.BandS.FindAsync(id);
            db.BandS.Remove(bandS);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
