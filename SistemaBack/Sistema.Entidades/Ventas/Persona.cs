using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sistema.Entidades.Almacen;

namespace Sistema.Entidades.Ventas
{
    public class Persona
    {   
       
        public int idpersona { get; set; } //Esta es una llave primaria que esta referenciada con la entidad o tabla de ingreso
                                            //El campo deberia llamarse en la tabla ingreso como idpersona pero esta como idprovedor.
        [Required]
        public string tipo_persona { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3, ErrorMessage ="El nombre debe tener mas de 3 carácteres y menos de 100")]
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }

        public ICollection<Ingreso> ingresos  { get; set; }
    }
}