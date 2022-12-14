import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Categoria_Component from '../components/Categoria.vue'
import Articulos_Component from '../components/Articulos.vue'
import Rol_Component from '../components/Rol.vue'
import Usuarios_Component from '../components/Usuario.vue'
import Cliente_Component from '../components/Cliente.vue'
import Proveedor_Componet from '../components/Proveedor.vue'
import Login_Component from '../components/Login.vue'
import Ingreso_Component from '../components/Ingreso.vue'
import store from '../store'
import Venta_Component from '../components/Venta.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
    meta :{
      administrador : true,
      almacenero : true,
      vendedor : true
    }
  },
  {
    path: '/categorias',
    name: 'categorias',
    component: Categoria_Component,
    meta :{
      administrador : true,
      almacenero : true,
    }
  },
  {
    path: '/ingresos',
    name: 'ingresos',
    component: Ingreso_Component,
    meta :{
      administrador : true,
      almacenero : true,
    }
  },
  {
    path: '/articulos',
    name: 'articulos',
    component: Articulos_Component,
    meta :{
      administrador : true,
      almacenero : true,
    }
  },
  {
    path: '/roles',
    name: 'roles',
    component: Rol_Component,
    meta :{
      administrador : true,
    }
  },
  {
    path: '/usuarios',
    name: 'usuarios',
    component: Usuarios_Component,
    meta :{
      administrador : true,
    }
  },
  {
    path: '/clientes',
    name: 'clientes',
    component: Cliente_Component,
    meta :{
      administrador : true,
      vendedor : true
    }
  },
  {
    path: '/proveedores',
    name: 'proveedores',
    component: Proveedor_Componet,
    meta :{
      administrador : true,
      almacenero : true,
      
    }
  },
  {
    path: '/login',
    name: 'login',
    component: Login_Component,
    meta :{
      libre: true
    }
  },
  {
    path: '/ventas',
    name: 'ventas',
    component: Venta_Component,
    meta :{
      administrador : true,
      vendedor : true,
    }
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

router.beforeEach((to, from, next) => {
if(to.matched.some(record => record.meta.libre)){
  next()
} else if (store.state.usuario && store.state.usuario.rol=='Administrador'){
  if(to.matched.some(record => record.meta.administrador)){
    next()
  }
}else if (store.state.usuario && store.state.usuario.rol=='Almacenero'){
  if(to.matched.some(record => record.meta.almacenero)){
    next()
  }
}else if (store.state.usuario && store.state.usuario.rol=='Vendedor'){
  if(to.matched.some(record => record.meta.vendedor)){
    next()
  }
}else{
  next({
    name: 'login'
  })
}
})
export default router
