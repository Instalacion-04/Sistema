using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Almacen;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Datos.Mapping.Ventas;
using Sistema.Entidades.Almacen;
using Sistema.Entidades.Usuarios;
using Sistema.Entidades.Ventas;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; } //Se expone la colecion de categorias
        public DbSet<Articulo> Articulos { get; set; } //Se expone la colecion de articulos
        public DbSet<Rol> Roles { get; set; } //Se expone la colecion de articulos
        public DbSet<Usuario> Usuarios { get; set; } //Se expone la colecion de articulos
        public DbSet<Persona> Personas { get; set; } //Se expone la colecion de articulos
        public DbSet<Ingreso> Ingresos { get; set; } //Se expone la colecion de la entidad ingresos
        public DbSet<DetalleIngreso> DetallesIngresos { get; set; } //Se expone la colecion de la entidad ingresos
        public DbSet<Venta> Ventas { get; set; } //Se expone la colecion de la entidad Ventas
            public DbSet<DetalleVenta> DetallesVentas { get; set; } //Se expone la colecion de la entidad Deralle_Ventas


        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Permite Mapear las entidades con la base de datos 
        {
            //Como se deben mapear las tablas respecto a las entidades
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap()); //Se aplica la configuracion de de mapeo a la tabla categoria
            modelBuilder.ApplyConfiguration(new ArticuloMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PersonaMap());
            modelBuilder.ApplyConfiguration(new IngresoMap());
            modelBuilder.ApplyConfiguration(new DetalleIngresoMap());
            modelBuilder.ApplyConfiguration(new VentaMap());
                        modelBuilder.ApplyConfiguration(new DetalleVentaMap());

        }
    }
}