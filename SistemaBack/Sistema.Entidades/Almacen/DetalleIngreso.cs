using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Entidades.Almacen
{
    public class DetalleIngreso
    {
        public int iddetalle_ingreso { get; set; }
        [Required]
        public int idingreso { get; set; }
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        
        [Column(TypeName = "decimal(18,4)")]
        public decimal precio { get; set; }

        [ForeignKey("idingreso")]
        public Ingreso ingreso { get; set; }
            [ForeignKey("idarticulo")]
        public Articulo articulo { get; set; }
    }
}