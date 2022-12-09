using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Sistema.Entidades.Almacen;
using Sistema.Entidades.Ventas;

namespace Sistema.Entidades.Usuarios
{
    public class Usuario
    {
         public int idusuario { get; set; }
        [Required]
        public int idrol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public byte[] password_hash { get; set; }
        [Required]
        public byte[] password_salt { get; set; }
        public bool condicion { get; set; }

        [ForeignKey("idrol")]
        public Rol rol { get; set; } //* Tablas a las que hace referencia.
        //* El campo idrol es una llave foranea en esta entidad, la llave proviene de la tabla Rol.
        public ICollection<Ingreso> ingresos { get; set; } 
        //*Se pone la collecion de Ingreso debido a que la tabla ingreso esta usando la primary key(idusuario) de esta tabla.
        //* En la tabla ingreso aparece el campo idusuario como llave foranea
        public ICollection<Venta> ventas { get; set; }
    }   //*Se pone la collecion de Venta debido a que la tabla Venta esta usando la primary key(idusuario) de esta tabla.
        //* En la tabla Venta aparece el campo idusuario como llave foranea
}