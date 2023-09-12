<template>
  <router-link :to="to" class="text-decoration-none text-danger card-style">
    <n-card
      :title="title"
      :segmented="{
        content: true,
        footer: 'soft',
      }"
    >
      <template #cover>
        <div style="text-align: -webkit-center">
          <img
            :src="require(`@/assets/${imgSrc}`)"
            style="max-height: 120px; max-width: 350px"
          />
        </div>
      </template>
      <p>{{ text }}</p>
    </n-card>
  </router-link>
</template>

<script>
import { NCard } from "naive-ui";
export default {
  components: {
    NCard,
  },
  props: {
    to: {
      type: String,
      default: "/",
    },
    title: {
      type: String,
      default: "Title",
    },
    imgSrc: {
      type: String,
      default: "/",
    },
    text: {
      type: String,
      default: "Text",
    },
  },
  data() {
    return {};
  },
};
</script>
<style>
.card-style {
  max-width: 300px;
  max-height: 140px;
}
</style>
