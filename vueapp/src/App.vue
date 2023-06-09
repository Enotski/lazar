<template>
  <div class="d-flex flex-column min-vh-100">
    <div>
      <NavPannel />
    </div>
    <div>
      <MainContainer />
    </div>
    <FooterPannel />
  </div>
</template>

<script>
import NavPannel from './components/NavPannel.vue'
import FooterPannel from './components/FooterPannel.vue'
import MainContainer from './components/MainContainer.vue'

export default {
  name: 'App',
  components: {
    NavPannel,
    FooterPannel,
    MainContainer
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
