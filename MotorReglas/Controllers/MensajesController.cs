using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using MotorReglas.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MotorReglas.Controllers
{
    public class MensajesController : ApiController
    {
        private readonly RuleEngineDBEntities _db = new RuleEngineDBEntities();

        private JsonResult<List<ResultadoDispositivo>> Evaluar(JObject mensaje)
        {
            var dispositivos = (from b in _db.Dispositivos
                               select new ResultadoDispositivo()
                               {
                                   Id = b.Id,
                                   Nombre = b.Nombre,
                                   Certeza = 0
                               }).ToList();
            var reglas = _db.Reglas;

            foreach (var r in reglas)
            {
                if (r.TipoEvaluacion.Equals("fact"))
                {
                    var match = _db.Hechos.Where(x => x.Nombre.Equals(r.PropiedadEvaluacion) && x.Valor.Equals(r.ValorPropiedadEvaluacion));
                    if (!match.Any()) continue;
                    {
                        var propiedades = _db.PropiedadDispositivo.Where(x => x.Nombre.Equals(r.PropiedadDispositivo) && x.Valor.Equals(r.ValorPropiedadDispositivo));
                        foreach (var p in propiedades)
                        {
                            var dispo = dispositivos.Where(x => x.Id == p.IdDisp);
                            dispo.First().Certeza += r.Certeza;
                        }
                    }
                }
                else if (r.TipoEvaluacion.Equals("message"))
                {
                    var prop = (string)mensaje[r.PropiedadEvaluacion];
                    if (prop.Equals(r.ValorPropiedadEvaluacion))
                    {
                        var propiedades = _db.PropiedadDispositivo.Where(x => x.Nombre.Equals(r.PropiedadDispositivo) && x.Valor.Equals(r.ValorPropiedadDispositivo));
                        foreach (var p in propiedades)
                        {
                            var dispo = dispositivos.Where(x => x.Id == p.IdDisp);
                            dispo.First().Certeza += r.Certeza;
                        }
                    }
                }               
            }

            dispositivos.Sort((x, y) => x.Certeza.CompareTo(y.Certeza));
            return Json(dispositivos);
        }

        // POST: api/Mensajes
        [HttpPost]
        public JsonResult<List<ResultadoDispositivo>> PostMensaje(object mensaje)
        {
            var objMensaje = (JObject)JsonConvert.DeserializeObject(mensaje.ToString());
            return Evaluar(objMensaje);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}