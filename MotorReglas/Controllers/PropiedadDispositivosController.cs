using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MotorReglas.Models;

namespace MotorReglas.Controllers
{
    public class PropiedadDispositivosController : ApiController
    {
        private readonly RuleEngineDBEntities _db = new RuleEngineDBEntities();

        // GET: api/PropiedadDispositivos
        public IQueryable<PropiedadDispositivo> GetPropiedadDispositivo()
        {
            return _db.PropiedadDispositivo;
        }

        // GET: api/PropiedadDispositivos/5
        [ResponseType(typeof(PropiedadDispositivo))]
        public IHttpActionResult GetPropiedadDispositivo(int id)
        {
            PropiedadDispositivo propiedadDispositivo = _db.PropiedadDispositivo.Find(id);
            if (propiedadDispositivo == null)
            {
                return NotFound();
            }

            return Ok(propiedadDispositivo);
        }

        // PUT: api/PropiedadDispositivos/5
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

            _db.Entry(propiedadDispositivo).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
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

        // POST: api/PropiedadDispositivos
        [ResponseType(typeof(PropiedadDispositivo))]
        public IHttpActionResult PostPropiedadDispositivo(PropiedadDispositivo propiedadDispositivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.PropiedadDispositivo.Add(propiedadDispositivo);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = propiedadDispositivo.Id }, propiedadDispositivo);
        }

        // DELETE: api/PropiedadDispositivos/5
        [ResponseType(typeof(PropiedadDispositivo))]
        public IHttpActionResult DeletePropiedadDispositivo(int id)
        {
            PropiedadDispositivo propiedadDispositivo = _db.PropiedadDispositivo.Find(id);
            if (propiedadDispositivo == null)
            {
                return NotFound();
            }

            _db.PropiedadDispositivo.Remove(propiedadDispositivo);
            _db.SaveChanges();

            return Ok(propiedadDispositivo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropiedadDispositivoExists(int id)
        {
            return _db.PropiedadDispositivo.Count(e => e.Id == id) > 0;
        }
    }
}