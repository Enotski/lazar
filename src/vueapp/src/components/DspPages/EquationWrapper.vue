<template>
  <n-config-provider :katex="katex">
    <n-equation :value="equation" :katex-options="katexOptions" />
  </n-config-provider>
</template>

<script>
import katex from "katex";
import { NConfigProvider, NEquation } from "naive-ui";
export default {
  components: {
    NConfigProvider,
    NEquation,
  },
  props: {
    equation: {
      type: String,
      default: "",
    },
  },
  data() {
    return {
      katex,
      katexOptions: {
        displayMode: true,
        fleqn: true,
        macros: {
          "\\f": "#1f(#2)",
        },
      },
    };
  },
};
</script>
