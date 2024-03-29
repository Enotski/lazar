import { createApp } from "vue";
import App from "./App.vue";
import { createRouter, createWebHistory } from "vue-router";

import "bootstrap/dist/css/bootstrap.css";
import "devextreme/dist/css/dx.light.css";
import "./css/site.css";

import UsersPage from "./components/AdministrationPages/UsersPage.vue";
import SystemLogPage from "./components/AdministrationPages/SystemLogPage.vue";
import AvgSpectrumPage from "./components/DspPages/AvgSpectrumPage.vue";
import FarrowPage from "./components/DspPages/FarrowPage.vue";
import FilterBanksPage from "./components/DspPages/FilterBanksPage.vue";
import FiltersPage from "./components/DspPages/FiltersPage.vue";
import FourierPage from "./components/DspPages/FourierPage.vue";
import GoertzelPage from "./components/DspPages/GoertzelPage.vue";
import MelSpectrumPage from "./components/DspPages/MelSpectrumPage.vue";
import MfccPage from "./components/DspPages/MfccPage.vue";
import ModulationPage from "./components/DspPages/ModulationPage.vue";
import NoisePage from "./components/DspPages/NoisePage.vue";
import ResamplingPage from "./components/DspPages/ResamplingPage.vue";
import SignalsPage from "./components/DspPages/SignalsPage.vue";
import SpectrumPage from "./components/DspPages/SpectrumPage.vue";
import WaveletsPage from "./components/DspPages/WaveletsPage.vue";
import WindowsPage from "./components/DspPages/WindowsPage.vue";
import CorrelationPage from "./components/DspPages/CorrelationPage.vue";
import MainContainer from "./components/MainComponents/MainContainer.vue";

const routes = [
  { path: "/", component: MainContainer },
  { path: "/users", component: UsersPage },
  { path: "/system-log", component: SystemLogPage },
  { path: "/correlation", component: CorrelationPage },
  { path: "/avg-spectrum", component: AvgSpectrumPage },
  { path: "/farrow", component: FarrowPage },
  { path: "/filter-banks", component: FilterBanksPage },
  { path: "/filters", component: FiltersPage },
  { path: "/fourier", component: FourierPage },
  { path: "/goertzel", component: GoertzelPage },
  { path: "/mel", component: MelSpectrumPage },
  { path: "/mfcc", component: MfccPage },
  { path: "/modulation", component: ModulationPage },
  { path: "/noise", component: NoisePage },
  { path: "/resampling", component: ResamplingPage },
  { path: "/signals", component: SignalsPage },
  { path: "/spectrum", component: SpectrumPage },
  { path: "/wavelets", component: WaveletsPage },
  { path: "/windows", component: WindowsPage },
];

const router = createRouter({
  routes: routes,
  history: createWebHistory(),
});
createApp(App).use(router).mount("#app");
