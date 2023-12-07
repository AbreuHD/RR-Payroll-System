using Core.Domain.Common;
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
        public DbSet<Contrato> Contrato { get; set; }
        public DbSet<Deducciones> Deducciones { get; set; }
        public DbSet<DetalleNomina> DetalleNomina { get; set; }
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
        public DbSet<TipoDePago> TipoDePagos { get; set; }


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
            modelBuilder.Entity<Contrato>().ToTable("Contrato");
            modelBuilder.Entity<Deducciones>().ToTable("Deducciones");
            modelBuilder.Entity<DetalleNomina>().ToTable("DetalleNomina");
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
            modelBuilder.Entity<TipoDePago>().ToTable("TipoDePagos");

            #endregion

            #region PK's
            modelBuilder.Entity<Actividades>().HasKey(x => x.Id);
            modelBuilder.Entity<ActividadesAsignadas>().HasKey(x => x.Id);
            modelBuilder.Entity<Asistencia>().HasKey(x => x.Id);
            modelBuilder.Entity<Contrato>().HasKey(x => x.Id);
            modelBuilder.Entity<Deducciones>().HasKey(x => x.Id);
            modelBuilder.Entity<DetalleNomina>().HasKey(x => x.Id);
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
            modelBuilder.Entity<TipoDePago>().HasKey(x => x.Id);

            #endregion


            #region Relations  
            modelBuilder.Entity<Actividades>()
                .HasOne(a => a.ActividadesAsignadas)
                .WithMany(aa => aa.Actividades)
                .HasForeignKey(a => a.IdActividadAsignada);

            //modelBuilder.Entity<ActividadesAsignadas>()
            //    .HasOne(aa => aa.EmpleadoProyecto)
            //    .WithOne(ep => ep.ActividadesAsignadas)
            //    .HasForeignKey<ActividadesAsignadas>(aa => aa.IdEmpleadoProyecto)
            //    .IsRequired();


            modelBuilder.Entity<ActividadesAsignadas>()
                .HasOne(aa => aa.Estado)
                .WithMany(e => e.ActividadesAsignadas)
                .HasForeignKey(aa => aa.IdEstado).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.EmpleadoProyecto)
                .WithOne(ep => ep.Asistencia)
                .HasForeignKey<Asistencia>(a => a.IdEmpleadoProyecto);

            modelBuilder.Entity<Deducciones>()
                .HasMany(d => d.Pagos)
                .WithOne(p => p.Deducciones)
                .HasForeignKey(p => p.IdDeducciones);

            modelBuilder.Entity<DetalleNomina>()
                .HasOne(dn => dn.Proyecto)
                .WithMany(p => p.DetalleNominas)
                .HasForeignKey(dn => dn.IdProyecto);

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
                .HasOne(p => p.TipoDePago)
                .WithMany(tp => tp.Pagos)
                .HasForeignKey(p => p.IdTipoDePago);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Empleado)
                .WithMany(e => e.Pagos)
                .HasForeignKey(p => p.IdEmpleado);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Percepciones)
                .WithMany(pe => pe.Pagos)
                .HasForeignKey(p => p.IdPercepciones);


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
                .HasForeignKey(p => p.IdEstado);

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
            #endregion

        }

    }
}
