using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Sistema.Entidades.Usuarios;

namespace Sistema.Entidades.Ventas
{
    public class Venta
    {
        [Key]
         public int idventa { get; set; } //*Primary Key
        [Required]
        public int idcliente { get; set; } //!Fk hace referencia a tabla persona
        [Required]
        public int idusuario { get; set; } //?fk hace referencia a tabla usuario 
        [Required]
        public string tipo_comprobante { get; set; } //todo: Campos propios sin referencia a otras tablas 
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

        public ICollection<DetalleVenta> detalles { get; set; }

        [ForeignKey("idusuario ")]
        public Usuario usuario { get; set; }
        //*Se pone la collecion de Usuario debido a que en esta entidad o tabla, se esta usando la primary key(idusuario) de la tabla Usuario, idusuario es una llave foranea en esta entidad.
        //* En la tabla Usuario aparece el campo idusuario como llave Primaria

        [ForeignKey("idcliente ")]
        public Persona persona { get; set; }
        //*Se pone la collecion de Persona debido a que en esta entidad o tabla, se esta usando la primary key(idpersona) de la tabla Persona, idpersona es una llave foranea en esta entidad, solo que hubo un error en vez de poner idpersona se puso idcliente.
        //* En la tabla Persona aparece el campo idpersona como llave Primaria
    }
}