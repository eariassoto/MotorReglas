using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MotorReglas.Models;

namespace MotorReglas.Controllers
{
    public class ReglasController : ApiController
    {
        private RuleEngineDBEntities db = new RuleEngineDBEntities();

        // GET: api/Reglas
        public IQueryable<Reglas> GetReglas()
        {
            return db.Reglas;
        }

        // GET: api/Reglas/5
        [ResponseType(typeof(Reglas))]
        public IHttpActionResult GetReglas(int id)
        {
            Reglas reglas = db.Reglas.Find(id);
            if (reglas == null)
            {
                return NotFound();
            }

            return Ok(reglas);
        }

        // PUT: api/Reglas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReglas(int id, Reglas reglas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reglas.Id)
            {
                return BadRequest();
            }

            db.Entry(reglas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReglasExists(id))
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

        // POST: api/Reglas
        [ResponseType(typeof(Reglas))]
        public IHttpActionResult PostReglas(Reglas reglas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reglas.Add(reglas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reglas.Id }, reglas);
        }

        // DELETE: api/Reglas/5
        [ResponseType(typeof(Reglas))]
        public IHttpActionResult DeleteReglas(int id)
        {
            Reglas reglas = db.Reglas.Find(id);
            if (reglas == null)
            {
                return NotFound();
            }

            db.Reglas.Remove(reglas);
            db.SaveChanges();

            return Ok(reglas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReglasExists(int id)
        {
            return db.Reglas.Count(e => e.Id == id) > 0;
        }
    }
}