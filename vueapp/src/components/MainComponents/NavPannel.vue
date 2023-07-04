<template>
  <nav
    class="navbar d-flex fixed-top bg-light justify-content-between box-shadow mb-3 shadow p-3 py-2"
  >
    <div>
      <router-link
        to="/"
        class="mdi mdi-waveform text-indigo bi me-2 fs-3 badge-ref"
        ><span class="navbar-brand mb-0 h1 text-secondary fw-medium"
          >DSP interactive articles</span
        ></router-link
      >
    </div>
    <div>
      <div class="row">
        <div class="col-auto">
          <router-link to="/users" class="nav nav-link text-secondary"
            >Users</router-link
          >
        </div>
        <div class="col-auto">
          <router-link
            to="/user-profile"
            class="nav nav-link text-secondary"
            >Me</router-link
          >
        </div>
        <div class="col-auto">
          <router-link
            to="/event-log"
            class="nav nav-link text-secondary"
            >logs</router-link
          >
        </div>
      </div>
    </div>
    <div class="d-flex flex-row-reverse">
      <div class="col-auto btn-login">
        <v-row justify="center">
          <v-dialog v-model="dialog" persistent width="800">
            <template v-slot:activator="{ props }">
              <v-btn
                icon="mdi-account-circle-outline"
                v-bind="props"
                variant="text"
                title="Sign Up / Log In"
                class="text-secondary"
              ></v-btn>
            </template>
            <v-card class="p-3">
              <v-card-title>
                <span class="text-h5 text-secondary">User Profile</span>
              </v-card-title>
              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col cols="12" class="px-0">
                      <v-text-field
                        ref="login"
                        label="Login*"
                        v-model="login"
                        required
                        clearable
                        :error-messages="errorMessages"
                        :rules="[() => !!login || 'This field is required']"
                        variant="outlined"
                      ></v-text-field>
                    </v-col>
                    <v-col v-if="register" cols="12" class="px-0">
                      <v-text-field
                        ref="email"
                        label="Email*"
                        v-model="email"
                        required
                        clearable
                        :error-messages="errorMessages"
                        :rules="[
                          () =>
                            (!!email && register) || 'This field is required',
                        ]"
                        variant="outlined"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" class="px-0">
                      <v-text-field
                        ref="password"
                        label="Password*"
                        type="password"
                        v-model="password"
                        required
                        clearable
                        :error-messages="errorMessages"
                        :rules="[() => !!password || 'This field is required']"
                        variant="outlined"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>
              <v-card-actions class="px-3">
                <v-switch
                  v-model="register"
                  label="Register now"
                  color="teal"
                  hide-details
                  class="pl-3"
                ></v-switch>
                <v-spacer></v-spacer>
                <v-btn color="red" variant="text" @click="dialog = false">
                  Close
                </v-btn>
                <v-btn color="teal" variant="text" @click="submit">
                  Save
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-row>
      </div>
      <div class="col-auto">
        <div class="pr-5">
          <v-menu transition="slide-x-transition">
            <template v-slot:activator="{ props }">
              <v-btn color="secondary" variant="plain" v-bind="props"
                >Navigation</v-btn
              >
            </template>

            <v-list>
              <v-list-item v-for="(item, index) in items" :key="index">
                <router-link
                  :to="item.ref"
                  class="nav nav-link text-secondary"
                  >{{ item.title }}</router-link
                >
              </v-list-item>
            </v-list>
          </v-menu>
        </div>
      </div>
    </div>
  </nav>
</template>

<script>
import { defineComponent } from "vue";

import { sendRequest, apiUrl } from "../../../utils/requestUtils";

export default defineComponent({
  components: {},
  data() {
    return {
      loading: false,
      post: null,
      dialog: false,
      register: false,
      items: [
        { ref: "/dsp", title: "DSP" },
        { ref: "/correlation", title: "Correlation" },
        { ref: "/farrow", title: "Farrow" },
        { ref: "/filter-banks", title: "FilterBanks" },
        { ref: "/filters", title: "Filters" },
        { ref: "/fourier", title: "Fourier" },
        { ref: "/goertzel", title: "Goertzel" },
        { ref: "/mel-spectrum", title: "MelSpectrum" },
        { ref: "/mfcc", title: "Mfcc" },
        { ref: "/modulation", title: "Modulation" },
        { ref: "/noise", title: "Noise" },
        { ref: "/resampling", title: "Resampling" },
        { ref: "/signals", title: "Signals" },
        { ref: "/spectrum", title: "Spectrum" },
        { ref: "/wavelets", title: "Wavelets" },
        { ref: "/windows", title: "Windows" },
      ],
      errorMessages: "",
      login: null,
      email: null,
      password: null,
      formHasErrors: false,
    };
  },
  computed: {
    form() {
      return {
        login: this.login,
        email: this.email,
        password: this.password,
      };
    },
  },
  created() {},
  watch: {},
  methods: {
    resetForm() {
      this.errorMessages = [];
      this.formHasErrors = false;

      Object.keys(this.form).forEach((f) => {
        this.$refs[f].reset();
      });
    },
    submit: async function () {
      this.formHasErrors = false;
      this.dialog = false;
      let el = this;
      Object.keys(this.form)
        .forEach((f) => {
          if (!el.form[f]) el.formHasErrors = true;
          if (el.$refs[f] !== undefined) el.$refs[f].validate(true);
        });

      if (this.register) {
        await sendRequest(
          `${apiUrl}user-profile/register-user`,
          "POST",
          this.form
        );
      } else {
        await sendRequest(
          `${apiUrl}/user-profile/login-user`,
          "POST",
          this.form
        ).
        then(async function (data) {
          console.log(data.result);
        });
      }
    },
  },
});
</script>
<style>
.badge-ref {
  text-decoration: none;
}
.btn-login {
  align-self: center;
}
</style>
