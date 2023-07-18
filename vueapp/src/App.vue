<template>
  <div class="d-flex flex-column min-vh-100">
    <NavPannel />
    <router-view></router-view>
    <FooterPannel />
  </div>
</template>

<script>
import NavPannel from './components/MainComponents/NavPannel.vue'
import FooterPannel from './components/MainComponents/FooterPannel.vue'

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
