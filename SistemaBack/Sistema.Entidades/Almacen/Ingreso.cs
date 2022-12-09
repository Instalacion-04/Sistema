using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Sistema.Entidades.Usuarios;
using Sistema.Entidades.Ventas;

namespace Sistema.Entidades.Almacen
{
    public class Ingreso
    {   
        [Key]
        public int idingreso { get; set; }
        [Required]
        public int idproveedor { get; set; }
        [Required]
        public int idusuario { get; set; }
        [Required]
        public string tipo_comprobante { get; set; }
        public string serie_comprobante { get; set; }
        [Required]
        public string num_comprobante { get; set; }
        [Required]
        public DateTime fecha_hora { get; set; }
        [Required]
        public decimal impuesto { get; set; }
        [Required]
        public decimal total { get; set; }
        [Required]
        public string estado { get; set; }

        public ICollection<DetalleIngreso> detalles { get; set; }
               //*Se pone la collecion de DetalleIngreso debido a que en la entidad Detalle_ingreso o tabla Detalle_ingreso se hace uso de  la primary key de esta tabla
                   //* idingreso es una llave primary en esta entidad, y en detalleingreso es una llave foranea

        [ForeignKey("idusuario ")]
        public Usuario usuario { get; set; }
         //* idusuario es una llave foranea en esta entidad, y en Usuario  es una llave Primaria
        
        [ForeignKey("idproveedor ")]
        public Persona persona { get; set; }
        //*idproveedor es una llave foranea en esta entidad, y en Persona idpersona que es = a idproveedor  es una llave Primaria

    }
}