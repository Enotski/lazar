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
    <div v-if="loggedIn">
      <div class="row">
        <div class="col-auto">
          <router-link to="/users" class="nav nav-link text-secondary"
            >Users</router-link
          >
        </div>
        <div class="col-auto">
          <router-link to="/user-profile" class="nav nav-link text-secondary"
            >Me</router-link
          >
        </div>
        <div class="col-auto">
          <router-link to="/event-log" class="nav nav-link text-secondary"
            >logs</router-link
          >
        </div>
      </div>
    </div>
    <div class="d-flex">
      <div class="col me-5">
        <n-dropdown trigger="hover" :options="items" @select="handleNavSelec">
          <n-button>Navigation</n-button>
        </n-dropdown>
      </div>
      <div class="col me-3">
        <n-button @click="showModal = true" ghost circle type="info">
          <template #icon>
            <n-icon><account-icon /></n-icon>
          </template>
        </n-button>
        <n-modal v-model:show="showModal">
          <n-card
            style="width: 800px"
            title="Login / Register"
            :bordered="false"
            size="huge"
            role="dialog"
            aria-modal="true"
          >
            <template #header-extra></template>
            <n-form
              ref="formRef"
              inline
              :label-width="80"
              :model="formValue"
              :rules="rules"
              size="medium"
            >
              <n-form-item label="Login" path="login">
                <n-input
                  v-model:value="formValue.login"
                  placeholder="Input login"
                />
              </n-form-item>
              <div class="me-3" v-if="isRegisterForm">
                <n-form-item label="Email" path="email">
                  <n-input
                    v-model:value="formValue.email"
                    type="email"
                    placeholder="Input email"
                  />
                </n-form-item>
              </div>
              <n-form-item label="Password" path="password">
                <n-input
                  v-model:value="formValue.password"
                  type="password"
                  placeholder="Input Password"
                />
              </n-form-item>
              <n-form-item>
                <n-button @click="submit">Log in</n-button>
              </n-form-item>
            </n-form>
            <template #footer>
              <n-radio-group v-model:value="registerRadio">
                <n-radio-button
                  v-for="item in registerFormRadioGroup"
                  :key="item.key"
                  :value="item.key"
                  :label="item.label"
                />
              </n-radio-group>
            </template>
          </n-card>
        </n-modal>
      </div>
    </div>
  </nav>
</template>

<script>
import { defineComponent } from "vue";
import { AccountCircleOutlined as AccountIcon } from "@vicons/material";
import {
  NButton,
  NDropdown,
  NModal,
  NCard,
  NForm,
  NFormItem,
  NInput,
  NIcon,
  NRadioGroup,
  NRadioButton,
} from "naive-ui";

export default defineComponent({
  components: {
    NButton,
    NDropdown,
    NModal,
    NCard,
    NForm,
    NFormItem,
    NInput,
    NIcon,
    NRadioGroup,
    NRadioButton,
    AccountIcon,
  },
  data() {
    return {
      loading: false,
      post: null,
      dialog: false,
      registerRadio: 0,
      showModal: false,
      options: [],
      registerFormRadioGroup: [
        { key: 0, label: "Log in" },
        { key: 1, label: "Register" },
      ],
      items: [
        { key: "dsp", label: "DSP" },
        { key: "correlation", label: "Correlation" },
        { key: "farrow", label: "Farrow" },
        { key: "filter-banks", label: "FilterBanks" },
        { key: "filters", label: "Filters" },
        { key: "fourier", label: "Fourier" },
        { key: "goertzel", label: "Goertzel" },
        { key: "mel-spectrum", label: "MelSpectrum" },
        { key: "mfcc", label: "Mfcc" },
        { key: "modulation", label: "Modulation" },
        { key: "noise", label: "Noise" },
        { key: "resampling", label: "Resampling" },
        { key: "signals", label: "Signals" },
        { key: "spectrum", label: "Spectrum" },
        { key: "wavelets", label: "Wavelets" },
        { key: "windows", label: "Windows" },
      ],
      errorMessages: "",
      login: null,
      email: null,
      password: null,
      formHasErrors: false,
      formValue: {
        login: this.login,
        email: this.email,
        password: this.password,
      },
      rules: {
        login: {
          required: true,
          message: "Please input your login",
          trigger: "blur",
        },
        email: {
          required: true,
          message: "Please input your email",
          trigger: ["input", "blur"],
        },
        password: {
          required: true,
          message: "Please input your password",
          trigger: ["input", "blur"],
        },
      },
    };
  },
  computed: {
    isRegisterForm() {
      return this.registerRadio === 1;
    },
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
    currentUser() {
      return this.$store.state.auth.user;
    },
    showAdminBoard() {
      if (this.currentUser && this.currentUser["roles"]) {
        return this.currentUser["roles"].includes("admin");
      }

      return false;
    },
    showModeratorBoard() {
      if (this.currentUser && this.currentUser["roles"]) {
        return this.currentUser["roles"].includes("moderator");
      }

      return false;
    },
    form() {
      return {
        login: this.formValue.login,
        email: this.formValue.email,
        password: this.formValue.password,
      };
    },
  },
  created() {
    if (this.loggedIn) {
      this.$router.push("/user-profile");
    }
  },
  watch: {},
  methods: {
    handleValidateClick(e) {
      e.preventDefault();
      this.formRef.value?.validate((errors) => {
        console.log(errors);
      });
    },
    handleNavSelec(key) {
      this.$router.push("/" + key);
    },
    logOut() {
      this.$store.dispatch("auth/logout");
      this.$router.push("/login");
    },
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
      Object.keys(this.form).forEach((f) => {
        if (!el.form[f]) el.formHasErrors = true;
        if (el.$refs[f] !== undefined) el.$refs[f].validate(true);
      });

      this.$store.dispatch("auth/login", this.form).then(
        () => {
          this.$router.push("/user-profile");
        },
        (error) => {
          this.loading = false;
          this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString();
        }
      );
      // if (this.register) {
      //   this.loading = true;

      // this.$store.dispatch("auth/sign-in", user).then(
      //   () => {
      //     this.$router.push("/user-profile");
      //   },
      //   (error) => {
      //     this.loading = false;
      //     this.message =
      //       (error.response &&
      //         error.response.data &&
      //         error.response.data.message) ||
      //       error.message ||
      //       error.toString();
      //   }
      // );

      //   await sendRequest(
      //     `${apiUrl}/auth/sign-in`,
      //     "POST",
      //     this.form
      //   ).then(async function (data) {
      //     console.log(data.result);
      //   });
      // } else {
      //   await sendRequest(
      //     `${apiUrl}/auth/register`,
      //     "POST",
      //     {login: this.form.login, password: this.form.password}
      //   ).
      //   then(async function (data) {
      //     console.log(data.result);
      //   });
      //}
    },
  },
});
</script>
<style>
.badge-ref {
  text-decoration: none;
}
</style>
