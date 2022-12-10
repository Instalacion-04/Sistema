using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Sistema.Entidades.Almacen;

namespace Sistema.Entidades.Ventas
{
    public class DetalleVenta
    {
         [Key]
        public int iddetalle_venta { get; set; }
        [Required] 
        public int idventa { get; set; }
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public decimal descuento { get; set; }
        
         [ForeignKey("idventa")]
        public Venta venta { get; set; } 
                   /*Se pone la collecion de Venta debido a que en la entidad Venta o tabla Venta está la primary key que se 
                   /*usa en esta tabla, idventa en esta entidad es una llave foranea*/
        
        [ForeignKey("idarticulo")]
        public Articulo articulo { get; set; }
               /*Se pone la collecion de Articulo debido a que en la entidad Articulo o tabla Articulo está la primary key que se */
                   /*usa en esta tabla, idarticulo en esta entidad es una llave foranea.*/
    }
}