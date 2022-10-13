<template>
  <v-layout aligm-start>
    <v-flex>
      <!--Vista de ingreso y sus elementos -->
      <v-data-table
        :headers="Tabla_Encabezados"
        :items="ingresos"
        class="elevation-1"
        :search="buscar"
        v-if="verNuevo == 0"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Ingreso</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-text-field
              v-if="verNuevo == 0"
              class="text-xs-center"
              v-model="buscar"
              append-icon="search"
              label="Búsqueda"
              single-line
              hide-details
            ></v-text-field>
            <v-spacer></v-spacer>

            <v-btn
              v-if="verNuevo == 0"
              @click="mostrarNuevo"
              color="primary"
              dark
              class="mb-2"
            >
              Nuevo
            </v-btn>

            <v-dialog v-model="adModal" max-width="290">
              <v-card>
                <v-card-title class="headline" v-if="adAccion == 1"
                  >¿Activar Item?</v-card-title
                >
                <v-card-title class="headline" v-if="adAccion == 2"
                  >¿Desactivar Item?</v-card-title
                >

                <v-card-text>
                  Estás a punto de
                  <span v-if="adAccion == 1">Activar</span>
                  <span v-if="adAccion == 2">Desactivar</span>
                  el ítem {{ adNombre }}
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="green darken-1"
                    text
                    @click="activarDesactivarCerrar"
                    >Cancelar</v-btn
                  >
                  <v-btn
                    v-if="adAccion == 1"
                    color="orange darken-4"
                    text
                    @click="activar"
                    >Activar</v-btn
                  >
                  <v-btn
                    v-if="adAccion == 2"
                    color="orange darken-4"
                    text
                    @click="desactivar"
                    >Desactivar</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>

        <template v-slot:item="props">
          <tr>
            <td>{{ props.item.usuario }}</td>
            <td>{{ props.item.proveedor }}</td>
            <td>{{ props.item.tipo_comprobante }}</td>
            <td>{{ props.item.serie_comprobante }}</td>

            <td>{{ props.item.num_comprobante }}</td>
            <td>{{ props.item.fecha_hora }}</td>
            <td>{{ props.item.impuesto }}</td>
            <td>{{ props.item.total }}</td>
            <td>
              <v-chip color="primary" outlined>
                <div v-if="props.item.estado == 'Aceptado'">
                  <span class="indigo--text">Aceptado</span>
                </div>

                <div v-else>
                  <span class="red--text">{{ props.item.estado }}</span>
                </div>
              </v-chip>
            </td>

            <td>
              <v-icon small class="mr-2" @click="EditarItem(props.item)"
                >edit</v-icon
              >

              <template v-if="props.item.estado == 'Aceptado'">
                <v-icon small @click="activarDesactivarMostrar(2, props.item)"
                  >lock_open</v-icon
                >
              </template>

              <template v-else>
                <v-icon small @click="activarDesactivarMostrar(1, props.item)"
                  >lock</v-icon
                >
              </template>
            </td>
          </tr>
        </template>

        <template v-slot:no-data>
          <v-btn color="primary" @click="listar">Actualizar </v-btn>
        </template>
      </v-data-table>

      <!--Este bloque son todos los elementos de nuevo o lo que se ve en nuevo-->
      <v-container grid-list-sm class="pa-4 white" v-if="verNuevo">
        <v-layout row wrap>
          <v-flex xs12 sm4 md4 lg4 xl4>
            <v-select
              v-model="tipo_comprobante"
              :items="comprobantes"
              label="Tipo Comprobante"
            ></v-select>
          </v-flex>

          <v-flex xs12 sm4 md4 lg4 xl4>
            <v-text-field
              v-model="serie_comprobante"
              label="Serie Comprobante"
            ></v-text-field>
          </v-flex>

          <v-flex xs12 sm4 md4 lg4 xl4>
            <v-text-field
              v-model="num_comprobante"
              label="Número  Comprobante"
            ></v-text-field>
          </v-flex>

          <v-flex xs12 sm8 md8 lg8 xl8>
            <v-select
              v-model="idproveedor"
              :items="proveedores"
              label="Proveedor"
            ></v-select>
          </v-flex>

          <v-flex xs12 sm4 md4 lg4 xl4>
            <v-text-field
              type="number"
              v-model="impuesto"
              label="Impuesto"
            ></v-text-field>
          </v-flex>

          <v-flex xs12 sm8 md8 lg8 xl8>
            <v-text-field
              @keyup.enter="buscarCodigo()"
              v-model="codigo"
              label="Agregar por Código"
            ></v-text-field>
          </v-flex>

          <v-flex xs12 sm2 md2 lg2 xl2>
            <v-btn @click="mostrarArticulos()" small fab dark color="teal">
              <v-icon dark>list</v-icon>
            </v-btn>
          </v-flex>

          <v-flex xs12 sm2 md2 lg2 xl2 v-if="errorArticulo">
            <div class="red--text" v-text="errorArticulo"></div>
          </v-flex>

          <v-flex xs12 sm12 md12 lg12 xl12>
            <v-data-table
              :headers="cabeceraDetalles"
              :items="detalles"
              hide-default-footer
              class="elevation-1"
            >
              <template v-slot:item="props">
                <tr>
                  <td>
                    <v-icon
                      small
                      class="mr-2"
                      @click="eliminarDelDetalle(detalles, props.item)"
                      >delete</v-icon
                    >
                  </td>
                  <td>{{ props.item.articulo }}</td>
                  <td>
                    <v-text-field
                      type="number"
                      v-model="props.item.cantidad"
                    ></v-text-field>
                  </td>
                  <td>
                    <v-text-field
                      type="number"
                      v-model="props.item.precio"
                    ></v-text-field>
                  </td>
                  <td>$ {{ props.item.cantidad * props.item.precio }}</td>
                </tr>
              </template>
              <template v-slot:no-data>
                <h3>No hay artículos agregados al detalle</h3>
              </template>
            </v-data-table>

            <v-flex class="text-xs-right">
              <strong>Total Parcial:</strong> $
              {{ (totalParcial = (total - totalImpuesto).toFixed(2)) }}
            </v-flex>

            <v-flex class="text-xs-right">
              <strong>Total Impuesto:</strong> $
              {{
                (totalImpuesto = (
                  (total * impuesto) /
                  (100 + impuesto)
                ).toFixed(2))
              }}
            </v-flex>
            <v-flex class="text-xs-right">
              <strong>Total Neto:</strong> $
              {{ (total = calcularTotal.toFixed(2)) }}
            </v-flex>
          </v-flex>
          <v-flex xs12 sm12 md12 lg12 xl12>
            <div
              class="red--text"
              v-for="v in ValidarMensaje"
              :key="v"
              v-text="v"
            ></div>
          </v-flex>

          <v-flex xs12 sm12 md12 lg12 xl12>
            <v-btn @click="ocultarNuevo" color="blue darken-1" text
              >Cancelar Operación</v-btn
            >
            <v-btn color="success">Guardar</v-btn>
          </v-flex>
        </v-layout>
      </v-container>

      <v-dialog v-model="verArticulo" max-width="1000px">
        <v-card>
          <v-card-title>
            <span class="headline">Seleccione un artículo</span>
          </v-card-title>
          <v-card-text>
            <v-container grid-list-md>
              <v-layout wrap>
                <v-flex xs12 sm12 md12 lg12 xl12>
                  <v-text-field
                    append-icon="search"
                    class="text-xs-center"
                    v-model="texto"
                    label="Ingrese artículo a buscar"
                    @keyup.enter="listarArticulo()"
                  >
                  </v-text-field>
                  <template>
                    <v-data-table
                      :headers="cabeceraArticulos"
                       :items="articulos"
                      class="elevation-1"
                    >
                       <template v-slot:item="props">
                        <td class="justify-center layout px-0">
                          <v-icon
                            small
                            class="mr-2"
                            @click="agregaralDetalle(props.item)"
                          >
                            add
                          </v-icon>
                        </td>
                        <td>{{ props.item.nombre }}</td>
                        <td>{{ props.item.categoria }}</td>
                        <td>{{ props.item.descripcion }}</td>
                        <td>{{ props.item.stock }}</td>
                        <td>{{ props.item.precio_venta }}</td>
                      </template>

                      <template slot="no-data">
                        <h3>No hay artículos para mostrar.</h3>
                      </template>
                    </v-data-table>
                  </template>
                </v-flex>
              </v-layout>
            </v-container>
          </v-card-text>
          <v-card-actions>

          <v-flex xs12 sm2 md2 lg2 xl2 v-if="errorArticulo">
            <div class="red--text" v-text="errorArticulo"></div>
          </v-flex>
            <v-spacer></v-spacer>
            <v-btn @click="ocultarArticulos()" color="blue darken" text>
              Cancelar Detalles
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-flex>
  </v-layout>
</template>


<script>
import axios from "axios";
export default {
  data: () => ({
    dialog: false,
    ingresos: [],
    Tabla_Encabezados: [
      { text: "Usuario", value: "usuario" },
      { text: "Proveedor", value: "proveedor" },
      { text: "Tipo Comprobante", value: "tipo_comprobante" },
      { text: "Serie Comprobante", value: "serie_comprobante" },
      { text: "Número Comprobante", value: "num_comprobante", sortable: false },
      { text: "Fecha", value: "fecha_hora", sortable: false },
      { text: "Impuesto", value: "impuesto", sortable: false },
      { text: "Total", value: "total", sortable: false },
      { text: "Estado", value: "estado", sortable: false },
      { text: "Acciones", value: "", sortable: false },
    ],

    cabeceraDetalles: [
      { text: "Borrar", value: "borrar", soportable: false },
      { text: "Articulo", value: "articulo" },
      { text: "Cantidad", value: "cantidad" },
      { text: "Precio", value: "precio", soportable: false },
      { text: "Subtotal", value: "subtotal", soportable: false },
    ],

    detalles: [],
    buscar: "",
    editedIndex: -1,
    id: "",
    idproveedor: "",
    proveedores: [],
    tipo_comprobante: "",
    comprobantes: ["FACTURA", "BOLETA", "TICKET", "GUIA"],
    serie_comprobante: "",
    num_comprobante: "",
    impuesto: 18,
    codigo: "",
    verNuevo: 0,
    errorArticulo: null,
    totalParcial: 0,
    totalImpuesto: 0,
    total: 0,
    cabeceraArticulos: [
                    {text: 'Seleccionar', value: 'seleccionar', sortable: false },
                    { text: 'Artículo', value: 'articulo'},
                    { text: 'Categoría', value: 'categoria' },
                    { text: 'Descripción', value: 'descripcion', sortable: false },
                    { text: 'Stock', value: 'stock', sortable: false  },
                    { text: 'Precio Venta', value: 'precio_venta', sortable: false  }            
                ],
    articulos: [],
    texto:'',
    verArticulo: 0,
    valida: 0,
    ValidarMensaje: [],
    adModal: 0,
    adAccion: 0,
    adNombre: "",
    adId: "",
  }),

  computed: {
    calcularTotal: function () {
      var resultado = 0.0;
      for (var i = 0; i < this.detalles.length; i++) {
        resultado =
          resultado + this.detalles[i].precio * this.detalles[i].cantidad;
      }
      return resultado;
    },
  },

  created() {
    this.listar();
    this.Select();
  },

  methods: {
    mostrarNuevo() {
      this.verNuevo = 1;
    },
    ocultarNuevo() {
      this.verNuevo = 0;
  
    },

    buscarCodigo() {
      let me = this;
      me.errorArticulo = null;
      let header = { Authorization: "Bearer " + this.$store.state.token };
      let configuracion = { headers: header };
      axios
        .get("api/Articulos/BuscarCodigoIngreso/" + this.codigo, configuracion)
        .then(function (response) {
          me.agregaralDetalle(response.data);
        })
        .catch(function (error) {
          console.log(error);
          me.errorArticulo = "No existe el artículo";
        });
    },
    listarArticulo() {
      let me = this;
      let header = { Authorization: "Bearer " + this.$store.state.token };
      let configuracion = { headers: header };
      axios
        .get("api/Articulos/ListarIngreso/"+me.texto, configuracion)
        .then(function (response) {
          //console.log(response);
          me.articulos = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    mostrarArticulos() {
      this.verArticulo = 1;
    },
    ocultarArticulos() {
      this.verArticulo = 0;
      
    },



    agregaralDetalle(data = []) {
      this.errorArticulo = null;
      if (this.encuentra(data["idarticulo"])) {
        this.errorArticulo = "El artículo ya fue agregado a la lista.";
      } else {
        this.detalles.push({
          idarticulo: data["idarticulo"],
          articulo: data["nombre"],
          cantidad: 1,
          precio: 1,
        });
      }
    },
    encuentra(id) {
      var sw = 0;
      for (var i = 0; i < this.detalles.length; i++) {
        if (this.detalles[i].idarticulo == id) {
          sw = 1;
        }
      }
      return sw;
    },

    eliminarDelDetalle(arr, item) {
      var i = arr.indexOf(item);
      if (i !== -1) {
        arr.splice(i, 1);
      }
    },

    listar() {
      let me = this;
      let header = { Authorization: "Bearer " + this.$store.state.token };
      let configuracion = { headers: header };
      axios
        .get("api/Ingreso/Listar", configuracion)
        .then(function (response) {
          //console.log(response);
          me.ingresos = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    Select() {
      let me = this;
      let header = { Authorization: "Bearer " + this.$store.state.token };
      let configuracion = { headers: header };
      var proveedoresArray = [];
      axios
        .get("api/Persona/SelectProveedores", configuracion)
        .then(function (response) {
          proveedoresArray = response.data;
          proveedoresArray.map(function (x) {
            me.proveedores.push({ text: x.nombre, value: x.idpersona });
          });
        })
        .catch(function (error) {
          console.log(error);
        });
    },

    EditarItem(item) {
      this.id = item.idusuario;
      this.idrol = item.idrol;
      this.nombre = item.nombre;
      this.tipo_documento = item.tipo_documento;
      this.num_documento = item.num_documento;
      this.direccion = item.direccion;
      this.telefono = item.telefono;
      this.email = item.email;
      this.password = item.password_hash;
      this.passwordAnt = item.password_hash;
      this.editedIndex = 1;
      this.dialog = true;
    },
    CerrarForm() {
      this.dialog = false;
      this.LimpiarForm();
    },
    LimpiarForm() {
      this.articulo = "";
      this.categoria = "";
      this.nombre = "";
      this.descripcion = "";
      this.precio_venta = "";
      this.editedIndex = -1;
    },
    GuardarRegistro() {
      if (this.ValidarForm()) {
        return;
      }
      let header = { Authorization: "Bearer " + this.$store.state.token };
      let configuracion = { headers: header };
      if (this.editedIndex > -1) {
        //Codigo para editar
        let guardar = this;
        if (guardar.password != guardar.passwordAnt) {
          guardar.actPassword = true;
        }

        axios
          .put(
            "api/Usuarios/Actualizar",
            {
              idusuario: guardar.id,
              idrol: guardar.idrol,
              nombre: guardar.nombre,
              tipo_documento: guardar.tipo_documento,
              num_documento: guardar.num_documento,
              direccion: guardar.direccion,
              telefono: guardar.telefono,
              email: guardar.email,
              password: guardar.password,
              act_password: guardar.actPassword,
            },
            configuracion
          )
          .then(function (response) {
            guardar.LimpiarForm();
            guardar.listar();
            guardar.CerrarForm();
          })
          .catch(function (error) {
            console.log(error);
          });
      } else {
        //codigo guaradr
        let guardar = this;
        let header = { Authorization: "Bearer " + this.$store.state.token };
        let configuracion = { headers: header };
        axios
          .post(
            "api/Usuarios/Crear",
            {
              idrol: guardar.idrol,
              nombre: guardar.nombre,
              tipo_documento: guardar.tipo_documento,
              num_documento: guardar.num_documento,
              direccion: guardar.direccion,
              telefono: guardar.telefono,
              email: guardar.email,
              password: guardar.password,
            },
            configuracion
          )
          .then(function (response) {
            guardar.LimpiarForm();
            guardar.listar();
            guardar.CerrarForm();
          })
          .catch(function (error) {
            console.log(error);
          });
      }
    },
    ValidarForm() {
      this.valida = 0;
      this.ValidarMensaje = [];
      if (this.nombre.length < 3 || this.nombre.length > 100) {
        this.ValidarMensaje.push(
          "El nombre debe tener mas de 3 carácteres y menos de 100 carácteres."
        );
      }
      if (!this.idrol) {
        this.ValidarMensaje.push("Seleccione una rol.");
      }
      if (!this.tipo_documento) {
        this.ValidarMensaje.push("Seleccione una tipo de documento.");
      }
      if (!this.email) {
        this.ValidarMensaje.push("Ingrese una dirección de correo.");
      }
      if (!this.password) {
        this.ValidarMensaje.push("Ingrese una contraseña.");
      }

      if (this.ValidarMensaje.length) {
        this.valida = 1;
      }
      return this.valida;
    },
    activarDesactivarMostrar(accion, item) {
      this.adModal = 1;
      this.adNombre = item.nombre;
      this.adId = item.idusuario;
      if (accion == 1) {
        this.adAccion = 1;
      } else if (accion == 2) {
        this.adAccion = 2;
      } else {
        this.adModal = 0;
      }
    },
    activarDesactivarCerrar() {
      this.adModal = 0;
    },
    activar() {
      let guardar = this;
      let header = { Authorization: "Bearer " + this.$store.state.token };
      let configuracion = { headers: header };
      axios
        .put("api/Usuarios/Activar/" + this.adId, {}, configuracion)
        .then(function (response) {
          guardar.adModal = 0;
          guardar.adAccion = 0;
          guardar.adNombre = "";
          guardar.adId = "";
          guardar.listar();
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    desactivar() {
      let guardar = this;
      let header = { Authorization: "Bearer " + this.$store.state.token };
      let configuracion = { headers: header };
      axios
        .put("api/Usuarios/Desactivar/" + this.adId, {}, configuracion)
        .then(function (response) {
          guardar.adModal = 0;
          guardar.adAccion = 0;
          guardar.adNombre = "";
          guardar.adId = "";
          guardar.listar();
        })
        .catch(function (error) {
          console.log(error);
        });
    },
  },
};
</script>