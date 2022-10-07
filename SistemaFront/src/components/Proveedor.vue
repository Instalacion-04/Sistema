<template>
  <v-layout aligm-start>
    <v-flex>
      <v-data-table
        :headers="Tabla_Encabezados"
        :items="proveedores"
        sort-by="calories"
        class="elevation-1"
        :search="buscar"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Proveedores</v-toolbar-title>
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

                      <v-col cols="12" sm="12" md="12">
                        <v-text-field
                          v-model="nombre"
                          label="Nombre"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="12" sm="12" md="12">
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

                      <v-col cols="12" sm="12" md="12">
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
          </v-toolbar>
        </template>

        <template v-slot:item="props">
          <tr>
            <td>{{ props.item.nombre }}</td>
            <td>{{ props.item.tipo_persona }}</td>
            <td>{{ props.item.tipo_documento }}</td>
            <td>{{ props.item.num_documento }}</td>
            <td>{{ props.item.direccion }}</td>
            <td>{{ props.item.telefono }}</td>
            <td>{{ props.item.email }}</td>
            <td>
              <v-icon small class="mr-2" @click="EditarItem(props.item)"
                >edit</v-icon>            
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
    proveedores: [],
    Tabla_Encabezados: [
      { text: "Nombre", value: "nombre" },
      { text: "Tipo Persona", value: "tipo_persona" },
      { text: "Tipo Documento", value: "tipo_documento" },
      { text: "Número Documento", value: "num_documento" },
      { text: "Dirección", value: "direccion" },
      { text: "Teléfono", value: "telefono" },
      { text: "Email", value: "email" },
      { text: "Opciones", value: "condicion", sortable: false },
    ],
    buscar: "",
    editedIndex: -1,
    id: "",
    nombre:"",
    tipo_documento: "",
    documentos: ["DNI", "RUC", "PASAPORTE", "CEDULA"],
    num_documento: "",
    direccion: "",
    telefono: "",
    email: "",
    valida: 0,
    ValidarMensaje: [],
    adModal: 0,
    adAccion: 0,
    adNombre: "",
    adId: "",
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Proveedor" : "Actualizar Datos del Proveedor";
    },
  },

  created() {
    this.listar();

  },

  methods: {
    listar() {
      let me = this;
      let header={"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
      axios
        .get("api/Persona/ListarProveedores",configuracion)
        .then(function (response) {
          //console.log(response);
          me.proveedores = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
  
    EditarItem(item) {
      this.id = item.idpersona;
      this.nombre = item.nombre;
      this.tipo_documento = item.tipo_documento;
      this.num_documento = item.num_documento;
      this.direccion= item.direccion;
      this.telefono = item.telefono;
      this.email = item.email;
      this.editedIndex = 1;
      this.dialog = true;
    },
    CerrarForm() {
      this.dialog = false;
      this.LimpiarForm();
    },
    LimpiarForm() {
      this.id = ""
      this.nombre = "";
      this.tipo_documento = "";
      this.num_documento = "";
      this.direccion = "";
      this.telefono = "";
      this.email= "";
       this.editedIndex = -1;
    },
    GuardarRegistro() {
      if (this.ValidarForm()) {
        return;
      }
      if (this.editedIndex > -1) {
        //Codigo para editar
        let guardar = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios
          .put("api/Persona/Actualizar", {
            'idpersona': guardar.id,
            'tipo_persona':'Proveedor',
            'nombre':guardar.nombre,
            'tipo_documento':guardar.tipo_documento,
            'num_documento':guardar.num_documento,
            'direccion':guardar.direccion,
            'telefono':guardar.telefono,
            'email':guardar.email
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
          .post("api/Persona/Crear", {
            'tipo_persona':'Proveedor',
            'nombre':guardar.nombre,
            'tipo_documento':guardar.tipo_documento,
            'num_documento':guardar.num_documento,
            'direccion':guardar.direccion,
            'telefono':guardar.telefono,
            'email':guardar.email
           
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

      if (!this.tipo_documento) {
        this.ValidarMensaje.push("Seleccione una tipo de documento.");
      }
     

      if (this.ValidarMensaje.length) {
        this.valida = 1;
      }
      return this.valida;
    },

  },
};
</script>