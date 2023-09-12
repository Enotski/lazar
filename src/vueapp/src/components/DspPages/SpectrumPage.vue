<template>
  <div class="container-fluid dsp-content-container">
    <DspTheoryDrawer :equations="equations"></DspTheoryDrawer>
  </div>
</template>

<script>
import DspTheoryDrawer from "./DspTheoryDrawer.vue";
import { TheoryEquations as equation } from "@/content/theoryEquations";

export default {
  components: {
    DspTheoryDrawer,
  },
  data() {
    return {
      equations: equation.e_9,
    };
  },
  computed: {},
  mounted() {},
  methods: {},
};
</script>
