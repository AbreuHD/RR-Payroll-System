﻿using Core.Domain.Common;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Context
{
    public class PersistenceContext : DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options){ }

        public DbSet<Actividades> Actividades { get; set; }
        public DbSet<ActividadesAsignadas> ActividadesAsignadas { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Deducciones> Deducciones { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<EmpleadoProyectos> EmpleadoProyectos { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Horas> Horas { get; set; }
        public DbSet<Licencia> Licencias { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<Nacionalidad> Nacionalidads { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Percepciones> Percepciones { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<TipoPago> TipoPago { get; set; }
        public DbSet<TipoCuenta> TipoCuenta { get; set; }
        public DbSet<TipoBanco> TipoBanco { get; set; }
        public DbSet<Pago_Deducciones> Pago_Deducciones { get; set; }
        public DbSet<Pago_Percepciones> Pago_Percepciones { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        entry.Entity.LastModifiedAt = DateTime.Now;
                        entry.Entity.LastModifiedBy = "System";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTime.Now;
                        entry.Entity.CreatedBy = "System";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region DBNames
            modelBuilder.Entity<Actividades>().ToTable("Actividades");
            modelBuilder.Entity<ActividadesAsignadas>().ToTable("ActividadesAsignadas");
            modelBuilder.Entity<Asistencia>().ToTable("Asistencias");
            modelBuilder.Entity<Deducciones>().ToTable("Deducciones");
            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<EmpleadoProyectos>().ToTable("EmpleadoProyectos");
            modelBuilder.Entity<Estado>().ToTable("Estado");
            modelBuilder.Entity<Horas>().ToTable("Horas");
            modelBuilder.Entity<Licencia>().ToTable("Licencias");
            modelBuilder.Entity<Municipio>().ToTable("Municipios");
            modelBuilder.Entity<Nacionalidad>().ToTable("Nacionalidads");
            modelBuilder.Entity<Pago>().ToTable("Pagos");
            modelBuilder.Entity<Percepciones>().ToTable("Percepciones");
            modelBuilder.Entity<Permiso>().ToTable("Permisos");
            modelBuilder.Entity<Provincia>().ToTable("Provincias");
            modelBuilder.Entity<Proyecto>().ToTable("Proyectos");
            modelBuilder.Entity<Puesto>().ToTable("Puestos");
            modelBuilder.Entity<TipoPago>().ToTable("TipoPago");
            modelBuilder.Entity<TipoBanco>().ToTable("TipoBanco");
            modelBuilder.Entity<TipoCuenta>().ToTable("TipoCuenta");

            #endregion

            #region PK's
            modelBuilder.Entity<Actividades>().HasKey(x => x.Id);
            modelBuilder.Entity<ActividadesAsignadas>().HasKey(x => x.Id);
            modelBuilder.Entity<Asistencia>().HasKey(x => x.Id);
            modelBuilder.Entity<Deducciones>().HasKey(x => x.Id);
            modelBuilder.Entity<Empleado>().HasKey(x => x.Id);
            modelBuilder.Entity<EmpleadoProyectos>().HasKey(x => x.Id);
            modelBuilder.Entity<Estado>().HasKey(x => x.Id);
            modelBuilder.Entity<Horas>().HasKey(x => x.Id);
            modelBuilder.Entity<Licencia>().HasKey(x => x.Id);
            modelBuilder.Entity<Municipio>().HasKey(x => x.Id);
            modelBuilder.Entity<Nacionalidad>().HasKey(x => x.Id);
            modelBuilder.Entity<Pago>().HasKey(x => x.Id);
            modelBuilder.Entity<Percepciones>().HasKey(x => x.Id);
            modelBuilder.Entity<Permiso>().HasKey(x => x.Id);
            modelBuilder.Entity<Provincia>().HasKey(x => x.Id);
            modelBuilder.Entity<Proyecto>().HasKey(x => x.Id);
            modelBuilder.Entity<Puesto>().HasKey(x => x.Id);
            modelBuilder.Entity<TipoPago>().HasKey(x => x.Id);

            #endregion


            #region Relations  
            modelBuilder.Entity<ActividadesAsignadas>()
                .HasOne(a => a.Actividad)
                .WithMany(aa => aa.ActividadesAsignadas)
                .HasForeignKey(a => a.IdActividad);

            //modelBuilder.Entity<ActividadesAsignadas>()
            //    .HasOne(aa => aa.Estado)
            //    .WithMany(e => e.ActividadesAsignadas)
            //    .HasForeignKey(aa => aa.IdEstado).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.EmpleadoProyecto)
                .WithOne(ep => ep.Asistencia)
                .HasForeignKey<Asistencia>(a => a.IdEmpleadoProyecto);

            modelBuilder.Entity<Deducciones>()
                .HasMany(d => d.Pago_Deducciones)
                .WithOne(p => p.Deducciones)
                .HasForeignKey(p => p.IdDeducciones);

            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Nacionalidad)
                .WithMany(n => n.Empleados)
                .HasForeignKey(e => e.IdNacionalidad);

            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Provincia)
                .WithMany(p => p.Empleado)
                .HasForeignKey(e => e.IdProvincia);
                //.HasForeignKey<Empleado>(e => e.IdProvincia);

            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Estado)
                .WithMany(es => es.Empleados)
                .HasForeignKey(e => e.IdEstado);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Empleado)
                .WithMany(e => e.Pagos)
                .HasForeignKey(p => p.IdEmpleado).OnDelete(DeleteBehavior.NoAction); ;

            modelBuilder.Entity<Pago_Percepciones>()
                .HasOne(p => p.Pago)
                .WithMany(pe => pe.Pago_Percepciones)
                .HasForeignKey(p => p.IdPago).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pago_Percepciones>()
                .HasOne(p => p.Percepciones)
                .WithMany(pe => pe.Pago_Percepciones)
                .HasForeignKey(p => p.IdPercepciones).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pago_Deducciones>()
                .HasOne(p => p.Pago)
                .WithMany(pe => pe.Pago_Deducciones)
                .HasForeignKey(p => p.IdPago).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pago_Deducciones>()
                .HasOne(p => p.Deducciones)
                .WithMany(pe => pe.Pago_Deducciones)
                .HasForeignKey(p => p.IdDeducciones).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Horas>()
                .HasOne(h => h.Asistencia)
                .WithMany(a => a.Horas)
                .HasForeignKey(h => h.IdAsistencia);

            modelBuilder.Entity<Municipio>()
                .HasOne(m => m.Provincia)
                .WithMany(p => p.Municipios)
                .HasForeignKey(m => m.IdProvincia);

            modelBuilder.Entity<Proyecto>()
                .HasOne(p => p.Estado)
                .WithMany(es => es.Proyecto)
                .HasForeignKey(p => p.IdEstado).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmpleadoProyectos>()
                .HasOne(ep => ep.Puesto)
                .WithMany(p => p.EmpleadoProyecto)
                .HasForeignKey(ep => ep.IdPuesto);

            //modelBuilder.Entity<EmpleadoProyecto>()
            //    .HasOne(ep => ep.Contrato)
            //    .WithMany(c => c.EmpleadoProyectos)
            //    .HasForeignKey(ep => ep.IdCOntrato);

            modelBuilder.Entity<EmpleadoProyectos>()
                .HasOne(ep => ep.Proyecto)
                .WithMany(p => p.EmpleadoProyectos)
                .HasForeignKey(ep => ep.IdProyecto).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EmpleadoProyectos>()
                .HasOne(ep => ep.Empleado)
                .WithMany(e => e.EmpleadoProyectos)
                .HasForeignKey(ep => ep.IdEmpleado);

            modelBuilder.Entity<Licencia>()
                .HasOne(e => e.Empleado)
                .WithMany(l => l.Licencias)
                .HasForeignKey(e => e.IdEmpleado);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.TipoPago)
                .WithMany(tp => tp.Pagos)
                .HasForeignKey(p => p.IdTipoPago);

            modelBuilder.Entity<TipoPago>()
                .HasOne(p => p.TipoBanco)
                .WithMany(tp => tp.TipoPagos)
                .HasForeignKey(p => p.IdTipoBanco);

            modelBuilder.Entity<TipoPago>()
                .HasOne(p => p.TipoCuenta)
                .WithMany(tp => tp.TipoPagos)
                .HasForeignKey(p => p.IdTipoCuenta);

            modelBuilder.Entity<Actividades>()
                .HasOne(ep => ep.Proyecto)
                .WithMany(p => p.Actividades)
                .HasForeignKey(ep => ep.IdProyecto).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ActividadesAsignadas>()
                .HasOne(aa => aa.EmpleadoProyecto)
                .WithMany(ep => ep.ActividadesAsignadas)
                .HasForeignKey(aa => aa.IdEmpleadoProyecto).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Permiso>()
                .HasOne(p => p.Asistencia)
                .WithMany(a => a.Permiso)
                .HasForeignKey(p => p.IdAsistencia)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

        }

    }
}
