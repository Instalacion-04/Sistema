<template>
  <v-layout aligm-start>
    <v-flex>
      <v-data-table
        :headers="Tabla_Encabezados"
        :items="categorias"
        sort-by="calories"
        class="elevation-1"
        :search="buscar"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Gestión de Categorías</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
              <v-text-field class="text-xs-center" v-model="buscar" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
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
                        <v-text-field
                          v-model="descripcion"
                          label="Descripción"
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
                <v-card-title class="headline" v-if="adAccion==1">¿Activar Item?</v-card-title>
                <v-card-title class="headline" v-if="adAccion==2">¿Desactivar Item?</v-card-title>

                <v-card-text>
                  Estás a punto de 
                  <span v-if="adAccion==1">Activar</span>
                  <span v-if="adAccion==2">Desactivar</span>
                  el ítem {{ adNombre }}
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="green darken-1" text @click="activarDesactivarCerrar">Cancelar</v-btn>
                  <v-btn v-if="adAccion==1" color="orange darken-4" text @click="activar">Activar</v-btn>
                  <v-btn v-if="adAccion==2" color="orange darken-4" text @click="desactivar">Desactivar</v-btn>
                </v-card-actions>
                
              </v-card>  
            </v-dialog>
            
          </v-toolbar>
        </template>

        <template v-slot:item="props">
          <tr>
            <td>{{ props.item.idcategoria }}</td>
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
            <td>{{ props.item.nombre }}</td>
            <td>{{ props.item.descripcion }}</td>
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
    dialogDelete: false,
    categorias: [],
    Tabla_Encabezados: [
      { text: "Id", value: "idcategoria", sortable: false },
      { text: "Opciones", value: "opciones", sortable: false },
      { text: "Nombre", value: "nombre" },
      { text: "Descripción", value: "descripcion", sortable: false },
      { text: "Estado", value: "condicion", sortable: false },
    ],
    buscar: '',
    editedIndex: -1,
    id: "",
    nombre: "",
    descripcion: "",
    valida: 0,
    ValidarMensaje: [],
    adModal: 0,
    adAccion: 0,
    adNombre: "",
    adId: "",
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo Registro" : "Editar Registro";
    },
  },

  created() {
    this.listar();
  },

  methods: {
    listar() {
      let me = this;
      let header = {"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
      axios
        .get("api/Categorias/Listar",configuracion)
        .then(function (response) {
          //console.log(response);
          me.categorias = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },

    EditarItem(item) {
      this.id = item.idcategoria;
      this.nombre = item.nombre;
      this.descripcion = item.descripcion;
      this.editedIndex = 1;
      this.dialog = true;
    },
    CerrarForm() {
      this.dialog = false;
      this.LimpiarForm();
    },
    LimpiarForm() {
      this.id = "";
      this.nombre = "";
      this.descripcion = "";
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
        axios
          .put("api/Categorias/Actualizar", {
            idcategoria: guardar.id,
            nombre: guardar.nombre,
            descripcion: guardar.descripcion,
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
        axios
          .post("api/Categorias/Crear", {
            nombre: guardar.nombre,
            descripcion: guardar.descripcion,
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
      if (this.nombre.length < 3 || this.nombre.length > 50) {
        this.ValidarMensaje.push(
          "El nombre debe tener mas de 3 carácteres y menos de 50 carácteres"
        );
      }
      if (this.ValidarMensaje.length) {
        this.valida = 1;
      }
      return this.valida;
    },
    activarDesactivarMostrar(accion, item) {
    this.adModal=1;
    this.adNombre=item.nombre;
    this.adId=item.idcategoria;
      if(accion==1){
       this.adAccion=1;
      }
      else if (accion==2){
      this.adAccion=2;
      }
      else{
        this.adModal=0;

      }
    },
    activarDesactivarCerrar(){
  this.adModal=0;
    },
    activar(){
      let guardar = this;
      let header={"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
        axios
          .put("api/Categorias/Activar/"+ this.adId, {            
          },configuracion)
          .then(function (response) {
          guardar.adModal=0;
          guardar.adAccion=0;
          guardar.adNombre="";
          guardar.adId="";
          guardar.listar();
          })
          .catch(function (error) {
            console.log(error);
          });
    },
    desactivar(){
      let guardar = this;
      let header={"Authorization" : "Bearer " + this.$store.state.token};
      let configuracion= {headers : header};
        axios
          .put("api/Categorias/Desactivar/"+ this.adId, {            
          },configuracion)
          .then(function (response) {
          guardar.adModal=0;
          guardar.adAccion=0;
          guardar.adNombre="";
          guardar.adId="";
          guardar.listar();
          },configuracion)
          .catch(function (error) {
            console.log(error);
          });
    },
    
  },
};
</script>