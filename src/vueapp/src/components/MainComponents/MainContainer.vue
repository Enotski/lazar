<template>
  <div class="parallax">
    <div class="row content-container justify-center align-center m-0">
      <div v-for="item in dspCards" :key="item.to" class="col-4 mb-3">
        <DspPageCard
          :to="item.to"
          :title="item.title"
          :imgSrc="item.src"
          :text="item.text"
        ></DspPageCard>
      </div>
    </div>
  </div>
</template>

<script>
import DspPageCard from "../DspPages/DspPageCard.vue";
import { DspCards } from "@/content/dspCards";

export default {
  components: {
    DspPageCard,
  },
  data() {
    return {
      dspCards: DspCards,
    };
  },
};
</script>

<style>
.parallax {
  /* background-image: url("../../assets/bg-light.gif"); */
  background-attachment: fixed;
  display: flex;
  justify-content: center;
  position: relative;
  background-size: cover;
}
</style>
