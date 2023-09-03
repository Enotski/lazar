<template>
  <div class="parallax">
    <!-- <div
      class="d-flex flex-column fill-height justify-center align-center text-white cards-mr-vertical content-container"
    > -->
    <div class="row content-container justify-center align-center">
      <div class="col-3 mb-3">
        <n-card size="small">
          <template #cover>
            <div>
              <router-link to="/signals">
                <div>
                  <div class="h-25">
                    <img src="../../assets/SignalsCover.png" />
                  </div>
                  <span class="nav nav-link text-secondary">Signals</span>
                </div></router-link
              >
              <div>
                <span>Analog, discrete, quantized, digital</span>
              </div>
            </div>
          </template>
        </n-card>
      </div>
      <div class="col-3 mb-3">
        <n-card title="Card with Cover" size="small">
          <template #cover>
            <div class="h-25">
              <img src="../../assets/CorrelationCover.png" />
            </div>
          </template>
          <div>Card Content</div>
        </n-card>
      </div>
      <div class="col-3 mb-3">
        <n-card title="Card with Cover" size="small">
          <template #cover>
            <div class="h-25">
              <img src="../../assets/FourierCover.png" />
            </div>
          </template>
          <div>Card Content</div>
        </n-card>
      </div>
      <div class="col-3 mb-3">
        <n-card title="Card with Cover" size="small">
          <template #cover>
            <div class="h-25">
              <img src="../../assets/NoizeCover.png" />
            </div>
          </template>
          <div>Card Content</div>
        </n-card>
      </div>
    </div>
  </div>
</template>

<script>
import { NCard } from "naive-ui";

export default {
  components: {
    NCard,
  },
  data() {
    return {};
  },
};
</script>

<style>
.parallax {
  background-image: url("../../assets/bg-light.gif");
  background-attachment: fixed;
  display: flex;
  justify-content: center;
  position: relative;
  background-size: cover;
}
</style>
