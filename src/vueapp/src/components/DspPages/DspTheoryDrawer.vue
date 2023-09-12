<template>
  <n-button @click="showTheory = !showTheory">Formuls</n-button>
  <n-drawer
    v-model:show="showTheory"
    placement="right"
    :trap-focus="false"
    :default-width="500"
    resizable
  >
    <n-drawer-content title="Formuls" :native-scrollbar="false">
      <equation-wrapper
        v-for="eq in equations"
        :key="eq"
        :equation="eq"
      ></equation-wrapper>
    </n-drawer-content>
  </n-drawer>
</template>

<script>
import { NDrawer, NDrawerContent, NButton } from "naive-ui";
import "katex/dist/katex.css";

import EquationWrapper from "./EquationWrapper.vue";

export default {
  components: {
    NDrawer,
    NDrawerContent,
    EquationWrapper,
    NButton,
  },
  props: {
    equations: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      showTheory: false,
    };
  },
};
</script>
