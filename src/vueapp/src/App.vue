<template>
  <div class="d-flex flex-column min-vh-100">
    <n-message-provider>
      <n-dialog-provider>
        <NavPannel />
      </n-dialog-provider>
    </n-message-provider>
    <router-view></router-view>
    <FooterPannel />
  </div>
</template>

<script>
import NavPannel from "./components/MainComponents/NavPannel.vue";
import FooterPannel from "./components/MainComponents/FooterPannel.vue";
import { NDialogProvider, NMessageProvider } from "naive-ui";

export default {
  name: "App",
  components: {
    NavPannel,
    FooterPannel,
    NDialogProvider,
    NMessageProvider,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
