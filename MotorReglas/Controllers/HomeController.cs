using MotorReglas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotorReglas.Controllers
{
    public class HomeController : Controller
    {
        RuleEngineDBEntities db = new RuleEngineDBEntities();
        public ActionResult Index(string Action)
        {
            var hechos = db.Hechos;
            var reglas = db.Reglas;
            var disp = db.Dispositivos;

            // seteo las certezas en cero
            foreach (var d in disp)
            {
                if(d.Certeza != 0)
                    d.Certeza = 0;
            }
            
            // evaluacion de reglas
            foreach(var r in reglas)
            {
                var match = db.Hechos.Where(x => x.Nombre.Equals(r.NombreHecho) && x.Estado.Equals(r.EstadoHecho));
                if(match.Count() > 0)
                {
                    var propiedades = db.PropiedadDispositivo.Where(x => x.Nombre.Equals(r.PropiedadDisp) && x.Valor.Equals(r.ValorPropiedad));
                    foreach (var p in propiedades)
                    {
                        var dispo = db.Dispositivos.Find(p.IdDisp);
                        dispo.Certeza += r.Certeza;
                    }
                }
            }
            

            db.SaveChanges();

            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
