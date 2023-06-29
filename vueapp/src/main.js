import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHistory } from 'vue-router'

// Vuetify
import 'vuetify/styles'
import 'bootstrap/dist/css/bootstrap.css'
import '@mdi/font/css/materialdesignicons.min.css'
import 'devextreme/dist/css/dx.light.css'
import './css/site.css'

import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

import '../utils/requestUtils'

const vuetify = createVuetify({
    components,
    directives,
})

import UsersPage from './components/UsersPage.vue'
import UserProfilePage from './components/UserProfilePage.vue'
import MainContainer from './components/MainContainer.vue'
const routes = [
    { path: '/', component: MainContainer },
    { path: '/users', component: UsersPage },
    { path: '/user-profile-page', component: UserProfilePage },
]

const router = createRouter({
    routes: routes,
    history: createWebHistory()
})

createApp(App).use(vuetify).use(router).mount('#app')