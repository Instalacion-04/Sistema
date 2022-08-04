import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Categoria_Component from '../components/Categoria.vue'
import Articulos_Component from '../components/Articulos.vue'
import Rol_Component from '../components/Rol.vue'
import Usuarios_Component from '../components/Usuario.vue'
import Cliente_Component from '../components/Cliente.vue'
import Proveedor_Componet from '../components/Proveedor.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/categorias',
    name: 'categorias',
    component: Categoria_Component
  },
  {
    path: '/articulos',
    name: 'articulos',
    component: Articulos_Component
  },
  {
    path: '/roles',
    name: 'roles',
    component: Rol_Component
  },
  {
    path: '/usuarios',
    name: 'usuarios',
    component: Usuarios_Component
  },
  {
    path: '/clientes',
    name: 'clientes',
    component: Cliente_Component
  },
  {
    path: '/proveedores',
    name: 'proveedores',
    component: Proveedor_Componet
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
