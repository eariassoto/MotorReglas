using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorReglas.Models
{
    public class DispositivosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Certeza { get; set; }

        public List<PropiedadDTO> Propiedades { get; set; }
    }
}