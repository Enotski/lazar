<template>
  <div class="parallax">
    <!-- <div
      class="d-flex flex-column fill-height justify-center align-center text-white cards-mr-vertical content-container"
    > -->
    <div class="row content-container justify-center align-center">
      <div class="col-4 mb-3">
        <div class="card border-danger">
          <router-link to="/signals" class="text-decoration-none text-danger">
            <div class="card-header">
              <span class="nav">Signals</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/1.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Signals, analog, digital, Z-transform</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-info">
          <router-link to="/fourier" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Fourier Transform</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/2.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Discrete Fourier Transform. FFT, IFFT</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-success">
          <router-link to="/correlation" class="text-decoration-none text-success">
            <div class="card-header">
              <span class="nav">Correlation</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/3.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Correlation, convolution: linear / circular / fast</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-info">
          <router-link to="/noise" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Noise</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/4.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Random signals AWGN, Noise</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-danger">
          <router-link to="/modulation" class="text-decoration-none text-danger">
            <div class="card-header">
              <span class="nav">Modulation</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/5.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Modulation. AM-, FM-, Chirp signals</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-info">
          <router-link to="/filters" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Filters</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/6.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>IIR / FIR filters</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-info">
          <router-link to="/windows" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Windows</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/7.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Windows, filtration: Hann, Blackman, Flattop, Kaiser etc.</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-success">
          <router-link to="/resampling" class="text-decoration-none text-success">
            <div class="card-header">
              <span class="nav">Resampling</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/8.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>CIC filters, decimation, interpolation, moving average</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-danger">
          <router-link to="/spectrum" class="text-decoration-none text-danger">
            <div class="card-header">
              <span class="nav">Welch's Method</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/9.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Spectrum analysis: Welch's Method</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-success">
          <router-link to="/avg-spectrum" class="text-decoration-none text-success">
            <div class="card-header">
              <span class="nav">Average spectrum</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/10.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Spectrum analysis: average spectrum</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-danger">
          <router-link to="/filter-banks" class="text-decoration-none text-danger">
            <div class="card-header">
              <span class="nav">Filter banks</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/11.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Filter banks for audio processing</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-info">
          <router-link to="/farrow" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Farrow</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/12.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Filter Farrow</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-success">
          <router-link to="/mel" class="text-decoration-none text-success">
            <div class="card-header">
              <span class="nav">Mel-spectrum</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/13.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Mel-spectrum</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-info">
          <router-link to="/mfcc" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">MFCC</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/14.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Mel-frequency cepstrum</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-info">
          <router-link to="/wavelets" class="text-decoration-none text-info">
            <div class="card-header">
              <span class="nav">Wavelets</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/15.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Wavelets processings</span>
            </div>
          </router-link>
        </div>
      </div>
      <div class="col-4 mb-3">
        <div class="card border-danger">
          <router-link to="/goertzel" class="text-decoration-none text-danger">
            <div class="card-header">
              <span class="nav">Goertzel</span>
            </div>
            <div class="card-body p-0 overflow-hidden">
              <div class="h-25 text-center">
                <img src="../../assets/16.png" style="max-height: 150px" />
              </div>
            </div>
            <div class="card-footer">
              <span>Goertzel alghoritm</span>
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
