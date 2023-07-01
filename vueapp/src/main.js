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
import DspPage from './components/DspPage.vue'
import FarrowPage from './components/FarrowPage.vue'
import FilterBanksPage from './components/FilterBanksPage.vue'
import FiltersPage from './components/FiltersPage.vue'
import FourierPage from './components/FourierPage.vue'
import GoertzelPage from './components/GoertzelPage.vue'
import MelSpectrumPage from './components/MelSpectrumPage.vue'
import MfccPage from './components/MfccPage.vue'
import ModulationPage from './components/ModulationPage.vue'
import NoisePage from './components/NoisePage.vue'
import ResamplingPage from './components/ResamplingPage.vue'
import SignalsPage from './components/SignalsPage.vue'
import SpectrumPage from './components/SpectrumPage.vue'
import WaveletsPage from './components/WaveletsPage.vue'
import WindowsPage from './components/WindowsPage.vue'
import CorrelationPage from './components/CorrelationPage.vue'

import MainContainer from './components/MainContainer.vue'
const routes = [
    { path: '/', component: MainContainer },
    { path: '/users', component: UsersPage },
    { path: '/user-profile-page', component: UserProfilePage },
    { path: '/correlation-page', component: CorrelationPage },
    { path: '/dsp-page', component: DspPage },
    { path: '/farrow-page', component: FarrowPage },
    { path: '/filter-banks-page', component: FilterBanksPage },
    { path: '/filters-page', component: FiltersPage },
    { path: '/fourier-page', component: FourierPage },
    { path: '/goertzel-page', component: GoertzelPage },
    { path: '/mel-spectrum-page', component: MelSpectrumPage },
    { path: '/mfcc-page', component: MfccPage },
    { path: '/modulation-page', component: ModulationPage },
    { path: '/noise-page', component: NoisePage },
    { path: '/resampling-page', component: ResamplingPage },
    { path: '/signals-page', component: SignalsPage },
    { path: '/spectrum-page', component: SpectrumPage },
    { path: '/wavelets-page', component: WaveletsPage },
    { path: '/windows-page', component: WindowsPage },
]

const router = createRouter({
    routes: routes,
    history: createWebHistory()
})

createApp(App).use(vuetify).use(router).mount('#app')