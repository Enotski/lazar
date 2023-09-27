<template>
  <div class="mt-auto footer bg-light p-3 text-body-secondary footer-shadow">
    <div class="container-fluid text-secondary">
      <div class="row mr-row">
        <div class="row col-auto me-auto">
          <div class="col-auto d-content align-center">
            <span class="fw-semibold">lazar - 2023</span>
          </div>
        </div>
        <div class="row col-auto ms-auto align-center">
          <div
            class="col-auto d-content"
            title="Original articles about digital signal processing"
          >
            <a
              href="https://github.com/hukenovs/dsp-theory"
              class="text-decoration-none text-reset fw-semibold"
              ><n-icon><git-icon /></n-icon> Original articles</a
            >
          </div>
          <div class="col d-content ml-3" title="My github">
            <a
              href="https://github.com/Enotski"
              class="text-reset text-decoration-none fw-semibold"
            >
              <n-icon><git-icon /></n-icon>Enotski</a
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { LogoGithub as GitIcon } from "@vicons/carbon";
import { NIcon } from "naive-ui";

export default {
  components: {
    NIcon,
    GitIcon,
  },
  data() {
    return {
      loading: false,
      post: null,
      links: ["mdi-facebook", "mdi-twitter", "mdi-linkedin", "mdi-instagram"],
    };
  },
  created() {},
  watch: {},
  methods: {},
};
</script>
<style>
.footer-shadow {
  box-shadow: 0 -0.5rem 1rem rgb(0 0 0 / 15%);
}
</style>
