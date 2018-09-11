using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using News.Models;
using Microsoft.AspNet.Identity;

namespace News.Controllers
{
    public class ArticlesController : Controller
    {
        private NewsEntities2 db = new NewsEntities2();

        // GET: Articles
        public ActionResult Index()
        {
            var articles = db.Articles.Include(a => a.Journalist);
            return View(articles.ToList());
        }

        // GET: Artiles
        public ActionResult IndexUserNames()
        {
            //return View(db.Artiles.ToList());
            string currentUserId = User.Identity.GetUserId();
            return View(db.Articles.Where(m => m.JournalistID == currentUserId).ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // GET: Articles/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.JournalistID = new SelectList(db.Journalists, "JournalistID", "JournalistFirstName");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ArticleID,JournalistID,Tile,Date,Topic")] Article articles)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(articles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JournalistID = new SelectList(db.Journalists, "JournalistID", "JournalistFirstName", articles.JournalistID);
            return View(articles);
        }

        // GET: Artiles/CreateIndi
        [Authorize(Roles = "Journalist")]
        public ActionResult CreateIndividual()
        {
            Article article = new Article();
            string currentUserId = User.Identity.GetUserId();
            article.JournalistID = currentUserId;
            return View(article);
        }
        // POST: Artiles/CreateIndi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Journalist")]
        public ActionResult CreateIndividual([Bind(Include = "ArticleID,JournalistID,Tile,Date,Topic")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "Journalist")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            ViewBag.JournalistID = new SelectList(db.Journalists, "JournalistID", "JournalistFirstName", articles.JournalistID);
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Journalist")]
        public ActionResult Edit([Bind(Include = "ArticleID,JournalistID,Tile,Date,Topic")] Article articles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JournalistID = new SelectList(db.Journalists, "JournalistID", "JournalistFirstName", articles.JournalistID);
            return View(articles);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Journalist")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Journalist")]
        public ActionResult DeleteConfirmed(int id)
        {
            Article articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
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
