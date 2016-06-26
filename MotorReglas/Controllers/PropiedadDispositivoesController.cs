using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MotorReglas.Models;

namespace MotorReglas.Controllers
{
    public class PropiedadDispositivoesController : ApiController
    {
        private RuleEngineDBEntities db = new RuleEngineDBEntities();

        // GET: api/PropiedadDispositivoes
        public IQueryable<PropiedadDispositivo> GetPropiedadDispositivo()
        {
            return db.PropiedadDispositivo;
        }

        // GET: api/PropiedadDispositivoes/5
        [ResponseType(typeof(PropiedadDispositivo))]
        public IHttpActionResult GetPropiedadDispositivo(int id)
        {
            PropiedadDispositivo propiedadDispositivo = db.PropiedadDispositivo.Find(id);
            if (propiedadDispositivo == null)
            {
                return NotFound();
            }

            return Ok(propiedadDispositivo);
        }

        // PUT: api/PropiedadDispositivoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPropiedadDispositivo(int id, PropiedadDispositivo propiedadDispositivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propiedadDispositivo.Id)
            {
                return BadRequest();
            }

            db.Entry(propiedadDispositivo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropiedadDispositivoExists(id))
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

        // POST: api/PropiedadDispositivoes
        [ResponseType(typeof(PropiedadDispositivo))]
        public IHttpActionResult PostPropiedadDispositivo(PropiedadDispositivo propiedadDispositivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropiedadDispositivo.Add(propiedadDispositivo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propiedadDispositivo.Id }, propiedadDispositivo);
        }

        // DELETE: api/PropiedadDispositivoes/5
        [ResponseType(typeof(PropiedadDispositivo))]
        public IHttpActionResult DeletePropiedadDispositivo(int id)
        {
            PropiedadDispositivo propiedadDispositivo = db.PropiedadDispositivo.Find(id);
            if (propiedadDispositivo == null)
            {
                return NotFound();
            }

            db.PropiedadDispositivo.Remove(propiedadDispositivo);
            db.SaveChanges();

            return Ok(propiedadDispositivo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropiedadDispositivoExists(int id)
        {
            return db.PropiedadDispositivo.Count(e => e.Id == id) > 0;
        }
    }
}