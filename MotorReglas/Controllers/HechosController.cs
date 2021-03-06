﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MotorReglas.Models;
using System.Web.Http.Results;

namespace MotorReglas.Controllers
{
    public class HechosController : ApiController
    {
        private RuleEngineDBEntities db = new RuleEngineDBEntities();

        // GET: api/Hechos
        public JsonResult<List<Hechos>> GetHechos()
        {
            return Json(db.Hechos.ToList());
        }

        // GET: api/Hechos/5
        [ResponseType(typeof(Hechos))]
        public IHttpActionResult GetHechos(int id)
        {
            Hechos hechos = db.Hechos.Find(id);
            if (hechos == null)
            {
                return NotFound();
            }

            return Ok(hechos);
        }

        // PUT: api/Hechos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHechos(int id, Hechos hechos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hechos.Id)
            {
                return BadRequest();
            }

            db.Entry(hechos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HechosExists(id))
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

        // POST: api/Hechos
        [ResponseType(typeof(Hechos))]
        public IHttpActionResult PostHechos(Hechos hechos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hechos.Add(hechos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hechos.Id }, hechos);
        }

        // DELETE: api/Hechos/5
        [ResponseType(typeof(Hechos))]
        public IHttpActionResult DeleteHechos(int id)
        {
            Hechos hechos = db.Hechos.Find(id);
            if (hechos == null)
            {
                return NotFound();
            }

            db.Hechos.Remove(hechos);
            db.SaveChanges();

            return Ok(hechos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HechosExists(int id)
        {
            return db.Hechos.Count(e => e.Id == id) > 0;
        }
    }
}