<template>
  <v-app class="d-flex flex-column min-vh-100">
    <NavPannel />
    <router-view></router-view>
    <FooterPannel />
  </v-app>
</template>

<script>
import NavPannel from './components/NavPannel.vue'
import FooterPannel from './components/FooterPannel.vue'

export default {
  name: 'App',
  components: {
    NavPannel,
    FooterPannel,
  },
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
