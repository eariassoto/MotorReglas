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


            string strHechos = "<strong>Hechos:</strong><br>";
            foreach(var he in hechos)
            {
                strHechos += he.Nombre + " <strong>set</strong> " + he.Estado + "<br>";
            }

            var strReglas = "<strong>Reglas:</strong><br>";
            foreach (var r in reglas)
            {
                strReglas += "<strong>if</strong> " + r.NombreHecho + " <strong>is</strong> " + r.EstadoHecho + " <strong>then device with</strong> " + r.PropiedadDisp + " <strong>set</strong> " + r.ValorPropiedad + " <strong>certainty of</strong> " + r.Certeza + "<br><br>";
            }

            var strDisp = "<strong>Dispositivos:</strong><br>";
            foreach (var d in disp)
            {
                strDisp += "<strong>" + d.Nombre + "</strong> <strong>Certeza:</strong> " + d.Certeza + " <br>";
                var prop = d.PropiedadDispositivo;
                foreach(var p in prop)
                {
                    strDisp += "   " + p.Nombre + " <strong>set</strong> " + p.Valor + "<br>";
                }
                strDisp += "<br>";
            }

            ViewBag.Title = "Home Page";
            ViewBag.strHechos = strHechos;
            ViewBag.strReglas = strReglas;
            ViewBag.strDisp = strDisp;
            return View();
        }
    }
}
