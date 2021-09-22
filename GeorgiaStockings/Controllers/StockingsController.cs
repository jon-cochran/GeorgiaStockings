using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GeorgiaStockings.Models;

namespace GeorgiaStockings.Controllers
{
    public class StockingsController : Controller
    {
        private GeorgiaStockingsEntities db = new GeorgiaStockingsEntities();

        // GET: Stockings
        //public ActionResult Index()
        //{
        //    var stockings = db.Stockings.Include(s => s.Note).Include(s => s.Schedule);
        //    return View(stockings.ToList());
        //}

        public ActionResult Index(string stockingsCounty, string searchString)
        {
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            var CountyLst = new List<string>();

            var CountyQry = from c in db.Stockings
                            orderby c.County
                            select c.County;

            CountyLst.AddRange(CountyQry.Distinct());
            ViewBag.stockingsCounty = new SelectList(CountyLst);

            var stockings = from s in db.Stockings
                            select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                stockings = stockings.Where(s => s.WaterBody.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(stockingsCounty))
            {
                stockings = stockings.Where(x => x.County == stockingsCounty);
            }

            return View(stockings);
        }

        // GET: Stockings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocking stocking = db.Stockings.Find(id);
            if (stocking == null)
            {
                return HttpNotFound();
            }
            return View(stocking);
        }

        // GET: Stockings/Create
        public ActionResult Create()
        {
            ViewBag.NoteId = new SelectList(db.Notes, "NoteId", "Description");
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "Name");
            return View();
        }

        // POST: Stockings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockingId,WaterBody,County,ScheduleId,NoteId")] Stocking stocking)
        {
            if (ModelState.IsValid)
            {
                db.Stockings.Add(stocking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NoteId = new SelectList(db.Notes, "NoteId", "Description", stocking.NoteId);
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "Name", stocking.ScheduleId);
            return View(stocking);
        }

        // GET: Stockings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocking stocking = db.Stockings.Find(id);
            if (stocking == null)
            {
                return HttpNotFound();
            }
            ViewBag.NoteId = new SelectList(db.Notes, "NoteId", "Description", stocking.NoteId);
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "Name", stocking.ScheduleId);
            return View(stocking);
        }

        // POST: Stockings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockingId,WaterBody,County,ScheduleId,NoteId")] Stocking stocking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stocking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NoteId = new SelectList(db.Notes, "NoteId", "Description", stocking.NoteId);
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "Name", stocking.ScheduleId);
            return View(stocking);
        }

        // GET: Stockings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stocking stocking = db.Stockings.Find(id);
            if (stocking == null)
            {
                return HttpNotFound();
            }
            return View(stocking);
        }

        // POST: Stockings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stocking stocking = db.Stockings.Find(id);
            db.Stockings.Remove(stocking);
            db.SaveChanges();
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
