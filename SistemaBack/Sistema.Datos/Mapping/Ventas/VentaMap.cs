using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Ventas;

namespace Sistema.Datos.Mapping.Ventas
{
    public class VentaMap : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
           builder.ToTable("venta")
            .HasKey(v => v.idventa);
           builder.HasOne(v => v.persona)
           .WithMany(p => p.ventas)
           .HasForeignKey (v => v.idcliente);
        }
    }
}