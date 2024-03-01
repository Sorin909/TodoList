import { createRouter, createWebHashHistory } from 'vue-router'
import TodosView from '../views/TodosView.vue'

const routes = [
  
  {
    path: '/',
    name: 'todos',
    component: TodosView
  },
  
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
