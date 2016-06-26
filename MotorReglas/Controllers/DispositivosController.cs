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
using System.Web.Http.Results;

namespace MotorReglas.Controllers
{
    public class DispositivosController : ApiController
    {
        private RuleEngineDBEntities db = new RuleEngineDBEntities();

        // GET: api/Dispositivos
        public JsonResult<IQueryable<DispositivosDTO>> GetDispositivos()
        {
            var dispositivos = from b in db.Dispositivos
                               select new DispositivosDTO()
                               {
                                   Id = b.Id,
                                   Nombre = b.Nombre,
                                   Certeza = b.Certeza,
                                   Propiedades = (from p in db.PropiedadDispositivo.Where(x => x.IdDisp == b.Id)
                                                 select new PropiedadDTO()
                                                 {
                                                     Id = p.Id,
                                                     IdDisp = p.IdDisp,
                                                     Nombre = p.Nombre,
                                                     Valor = p.Valor
                                                 }).ToList()
                        };
            //dispositivos.ElementAt(0).Propiedades.Add(new PropiedadDTO() { Nombre = "a", Valor = "b" });
            Console.Write("fml");
            return Json(dispositivos);
        }
        
        // GET: api/Dispositivos/5
        [ResponseType(typeof(Dispositivos))]
        public IHttpActionResult GetDispositivos(int id)
        {
            Dispositivos dispositivos = db.Dispositivos.Find(id);
            if (dispositivos == null)
            {
                return NotFound();
            }

            return Ok(dispositivos);
        }

        // PUT: api/Dispositivos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDispositivos(int id, Dispositivos dispositivos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dispositivos.Id)
            {
                return BadRequest();
            }

            db.Entry(dispositivos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositivosExists(id))
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

        // POST: api/Dispositivos
        [ResponseType(typeof(Dispositivos))]
        public IHttpActionResult PostDispositivos(Dispositivos dispositivos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dispositivos.Add(dispositivos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dispositivos.Id }, dispositivos);
        }

        // DELETE: api/Dispositivos/5
        [ResponseType(typeof(Dispositivos))]
        public IHttpActionResult DeleteDispositivos(int id)
        {
            Dispositivos dispositivos = db.Dispositivos.Find(id);
            if (dispositivos == null)
            {
                return NotFound();
            }

            db.Dispositivos.Remove(dispositivos);
            db.SaveChanges();

            return Ok(dispositivos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DispositivosExists(int id)
        {
            return db.Dispositivos.Count(e => e.Id == id) > 0;
        }
    }
}