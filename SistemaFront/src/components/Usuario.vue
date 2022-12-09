<template>
  <v-layout aligm-start>
    <v-flex>
      <v-data-table
        :headers="Tabla_Encabezados"
        :items="usuarios"
        sort-by="calories"
        class="elevation-1"
        :search="buscar"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Listado de Usuarios</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-text-field
              class="text-xs-center"
              v-model="buscar"
              append-icon="search"
              label="Búsqueda"
              single-line
              hide-details
            ></v-text-field>
            <v-spacer></v-spacer>

            <v-dialog v-model="dialog" max-width="500px">
              <template v-slot:activator="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Agregar
                </v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="text-h5">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>

                      <v-col cols="12" sm="6" md="6">
                        <v-text-field
                          v-model="nombre"
                          label="Nombre"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="12" sm="6" md="6">
                        <v-select
                          v-model="idrol"
                          :items="roles"
                          label="Rol"
                        >
                        </v-select>
                      </v-col>

                      <v-col cols="12" sm="6" md="6">
                        <v-select v-model="tipo_documento" 
                                  :items="documentos" 
                                  label="Tipo Documento">
                        </v-select>
                      </v-col>

                      <v-col cols="12" sm="6" md="6">
                        <v-text-field
                          v-model="num_documento"
                          label="Número Documento"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="12" sm="6" md="6">
                        <v-text-field
                          v-model="direccion"
                          label="Dirección"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="12" sm="6" md="6">
                        <v-text-field
                          v-model="telefono"
                          label="Teléfono"
                        ></v-text-field>
                      </v-col>


                      <v-col cols="12" sm="6" md="6">
                        <v-text-field
                          v-model="email"
                          label="Email"
                        ></v-text-field>
                      </v-col>


                      <v-col cols="12" sm="6" md="6">
                        <v-text-field
                          type="password"
                          v-model="password"
                          label="Password"
                        ></v-text-field>
                      </v-col>


                      <v-col cols="12" sm="12" md="12" v-show="valida">
                        <div
                          class="red--text"
                          v-for="v in ValidarMensaje"
                          :key="v"
                          v-text="v"
                        ></div>
                      </v-col>

                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="CerrarForm">
                    Cerrar
                  </v-btn>
                  <v-btn color="blue darken-1" text @click="GuardarRegistro">
                    Guardar
                  </v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>

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
            <td>{{ props.item.nombre }}</td>
            <td>{{ props.item.rol }}</td>
            <td>{{ props.item.tipo_documento }}</td>
            <td>{{ props.item.num_documento }}</td>
            <td>{{ props.item.direccion }}</td>
            <td>{{ props.item.telefono }}</td>
            <td>{{ props.item.email }}</td>
            <td>
              <v-chip color="primary" outlined
                ><div v-if="props.item.condicion">
                  <span class="indigo--text">Activo</span>
                </div>

                <div v-else>
                  <span class="red--text">Inactivo</span>
                </div></v-chip
              >
            </td>
            <td>
              <v-icon small class="mr-2" @click="EditarItem(props.item)"
                >edit</v-icon
              >

              <template v-if="props.item.condicion">
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
    </v-flex>
  </v-layout>
</template>


<script>
import axios from "axios";
export default {
  data: () => ({
    dialog: false,
    usuarios: [],
    Tabla_Encabezados: [
      { text: "Nombre", value: "nombre" },
      { text: "Rol", value: "rol" },
      { text: "Tipo Documento", value: "tipo_documento" },
      { text: "Número Documento", value: "num_documento" },
      { text: "Dirección", value: "direccion" },
      { text: "Teléfono", value: "telefono" },
      { text: "Email", value: "email" },
      { text: "Estado", value: "condicion", sortable: false },
      { text: "Opciones", value: "condicion", sortable: false },
    ],
    buscar: "",
    editedIndex: -1,
    id: "",
    idrol: "",
    roles: [],
    nombre:"",
    tipo_documento: "",
    documentos: ["DNI", "RUC", "PASAPORTE", "CEDULA"],
    num_documento: "",
    direccion: "",
    telefono: "",
    email: "",
    password: "",
    actPassword: false,
    passwordAnt:'',
    valida: 0,
    ValidarMensaje: [],
    adModal: 0,
    adAccion: 0,
    adNombre: "",
    adId: "",
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Usuario" : "Actualizar Datos de Usuario";
    },
  },

  created() {
    this.listar();
    this.Select();
  },

  methods: {
    listar() {
      let me = this;
      let header={"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
      axios
        .get("api/Usuarios/Listar",configuracion)
        .then(function (response) {
          //console.log(response);
          me.usuarios = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    Select() {
      let me = this;
      let header={"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
      var rolesArray = [];
      axios
        .get("api/Roles/Select",configuracion)
        .then(function (response) {
          rolesArray = response.data;
         rolesArray.map(function (x) {
            me.roles.push({ text: x.nombre, value: x.idrol });
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
      this.direccion= item.direccion;
      this.telefono = item.telefono;
      this.email = item.email;
      this.password = item.password_hash;
      this.passwordAnt=item.password_hash;
      this.editedIndex = 1;
      this.dialog = true;
    },
    CerrarForm() {
      this.dialog = false;
      this.LimpiarForm();
    },
    LimpiarForm() {
      this.id = "";
      this.idrol = "";
      this.nombre = "";
      this.tipo_documento = "";
      this.num_documento = "";
      this.direccion = "";
      this.telefono = "";
      this.email= "";
      this.password= "";
      this.passwordAnt="";
      this.actPassword=false;
        this.editedIndex = -1;
    },
    GuardarRegistro() {
      if (this.ValidarForm()) {
        return;
      }
      let header={"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
      if (this.editedIndex > -1) {
        //Codigo para editar
        let guardar = this;
        if(guardar.password!=guardar.passwordAnt)
        {
          guardar.actPassword=true;
        }

        axios
          .put("api/Usuarios/Actualizar", {
            'idusuario': guardar.id,
            'idrol':guardar.idrol,
            'nombre':guardar.nombre,
            'tipo_documento':guardar.tipo_documento,
            'num_documento':guardar.num_documento,
            'direccion':guardar.direccion,
            'telefono':guardar.telefono,
            'email':guardar.email,
            'password':guardar.password,
            'act_password':guardar.actPassword
          },configuracion)
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
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
         axios
          .post("api/Usuarios/Crear", {
            'idrol':guardar.idrol,
            'nombre':guardar.nombre,
            'tipo_documento':guardar.tipo_documento,
            'num_documento':guardar.num_documento,
            'direccion':guardar.direccion,
            'telefono':guardar.telefono,
            'email':guardar.email,
            'password':guardar.password
          },configuracion)
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
      let header={"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
      axios
        .put("api/Usuarios/Activar/" + this.adId, {},configuracion)
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
      let header={"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
      axios
        .put("api/Usuarios/Desactivar/" + this.adId, {},configuracion)
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