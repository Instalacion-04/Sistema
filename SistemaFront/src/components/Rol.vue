<template>
  <v-layout aligm-start>
    <v-flex>
      <v-data-table
        :headers="Tabla_Encabezados"
        :items="roles"
        sort-by="calories"
        class="elevation-1"
        :search="buscar"
      >
        <template v-slot:top>
          <v-toolbar flat>
            <v-toolbar-title>Gestión de Roles</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
              <v-text-field class="text-xs-center" v-model="buscar" append-icon="search" label="Búsqueda" single-line hide-details></v-text-field>
            <v-spacer></v-spacer>
          
           
            
          </v-toolbar>
        </template>

        <template v-slot:item="props">
          <tr>
            <td>{{ props.item.nombre }}</td>
            <td>{{ props.item.descripcion }}</td>
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
    roles: [],
    Tabla_Encabezados: [
      { text: "Nombre", value: "nombre" },
      { text: "Descripción", value: "descripcion", sortable: false }
      
    ],
    buscar: '',
    
  }),

  computed: {
    
  },

  created() {
    this.listar();
  },

  methods: {
    listar() {
      let datos = this;
      axios
        .get("api/Roles/Listar")
        .then(function (response) {
          //console.log(response);
            datos.roles = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },   
  },
};
</script>