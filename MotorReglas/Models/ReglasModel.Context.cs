﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MotorReglas.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RuleEngineDBEntities : DbContext
    {
        public RuleEngineDBEntities()
            : base("name=RuleEngineDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Dispositivos> Dispositivos { get; set; }
        public virtual DbSet<Hechos> Hechos { get; set; }
        public virtual DbSet<PropiedadDispositivo> PropiedadDispositivo { get; set; }
        public virtual DbSet<Reglas> Reglas { get; set; }
    }
}
