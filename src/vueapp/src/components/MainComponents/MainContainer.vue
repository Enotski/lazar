<template>
  <div class="parallax">
    <!-- <div
      class="d-flex flex-column fill-height justify-center align-center text-white cards-mr-vertical content-container"
    > -->
    <div class="row content-container justify-center align-center">
      <div class="col-3 mb-3">
        <div class="card border-info">
          <router-link to="/signals" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Signals</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/1.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Analog, discrete, quantized, digital</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-3 mb-3">
        <div class="card border-info">
          <router-link to="/signals" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Signals</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/2.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Analog, discrete, quantized, digital</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-3 mb-3">
        <div class="card border-info">
          <router-link to="/signals" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Signals</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/3.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Analog, discrete, quantized, digital</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-3 mb-3">
        <div class="card border-info">
          <router-link to="/signals" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Signals</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/4.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Analog, discrete, quantized, digital</span>
            </div>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
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
