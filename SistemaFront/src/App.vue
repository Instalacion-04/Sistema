<template>
  <v-app id="inspire">
    <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" color="blue darken-3" dark app fixed>
      <v-toolbar-title style="width: 300px" class="ml-0 pl-3">
        <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
        <span class="hidden-sm-and-down">Sistema</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn @click="salir" v-if="logueado" icon>
        <v-icon>logout</v-icon>Salir
      </v-btn>
      <v-btn :to="{name:'login'}" v-else>
        <v-icon>token</v-icon>
      </v-btn>
    </v-app-bar>

    <v-navigation-drawer :clipped="$vuetify.breakpoint.lgAndUp" v-model="drawer" fixed app v-if="logueado">
      <v-list dense>
        
        <template v-if="esAdministrador || esAlmacenero || esVendedor">
          <v-list-item :to="{ name: 'home' }">
            <v-list-item-action>
              <v-icon>home</v-icon>
            </v-list-item-action>
            <v-list-item-title> Inicio </v-list-item-title>
          </v-list-item>
        </template>

        <template v-if="esAdministrador || esAlmacenero">
          <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title> Almacén </v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: 'categorias' }">
              <v-list-item-action>
                <v-icon>category</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Categorías </v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: 'articulos' }">
              <v-list-item-action>
                <v-icon>grain</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Artículos </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>

        <template v-if="esAdministrador || esAlmacenero">
          <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>Compras</v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: 'ingresos' }">
              <v-list-item-action>
                <v-icon>south_west</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Ingresos </v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: 'proveedores' }">
              <v-list-item-action>
                <v-icon>groups</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Proveedores </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>

        <template v-if="esAdministrador || esVendedor">
          <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>Ventas</v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: '' }">
              <v-list-item-action>
                <v-icon>sell</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Ventas </v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: 'clientes' }">
              <v-list-item-action>
                <v-icon>people</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Clientes </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>

        <template v-if="esAdministrador">
          <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>Accesos</v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: 'roles' }">
              <v-list-item-action>
                <v-icon>manage_accounts</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Roles </v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: 'usuarios' }">
              <v-list-item-action>
                <v-icon>account_box</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Usuarios </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>

        <template v-if="esAdministrador">
          <v-list-group>
            <v-list-item slot="activator">
              <v-list-item-content>
                <v-list-item-title>Consultas</v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: '' }">
              <v-list-item-action>
                <v-icon>paid</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Consulta Compras </v-list-item-title>
              </v-list-item-content>
            </v-list-item>

            <v-list-item :to="{ name: '' }">
              <v-list-item-action>
                <v-icon>trending_up</v-icon>
              </v-list-item-action>
              <v-list-item-content>
                <v-list-item-title> Consulta Ventas </v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list-group>
        </template>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <v-container fluid fill-height>
        <v-slide-y-transition mode="out-in">
          <router-view />
        </v-slide-y-transition>
      </v-container>
    </v-main>
  </v-app>
</template>




<script>
export default {
  name: "App",

  data: () => ({
    drawer: null,
  }),
  computed: {

    logueado() {
      return this.$store.state.usuario;

    },

    esAdministrador() {
      return this.$store.state.usuario && this.$store.state.usuario.rol == 'Administrador';

    },
    esAlmacenero() {
      return this.$store.state.usuario && this.$store.state.usuario.rol == 'Almacenero';

    },
    esVendedor() {
      return this.$store.state.usuario && this.$store.state.usuario.rol == 'Vendedor';

    }

  },
  created() {
    this.$store.dispatch("autoLogin");


  },
  methods:{
    salir(){
      this.$store.dispatch("salir")
    }
  }








};
</script>
