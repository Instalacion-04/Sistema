using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos.Mapping.Almacen
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("categoria") //Mapeo de la tabla categoria, es necesario mapear el nombre de la tabla y su llave primaria
                .HasKey(c => c.idcategoria); //mapeo de la llave primaria de la tabla
            builder.Property(c => c.nombre)
                .HasMaxLength(50);
            builder.Property(c => c.descripcion)
                .HasMaxLength(256);
            
        }
    }
}