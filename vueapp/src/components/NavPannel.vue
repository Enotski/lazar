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
            to="/user-profile-page"
            class="nav nav-link text-secondary"
            >Me</router-link
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
                        label="Login*"
                        required
                        clearable
                        variant="outlined"
                      ></v-text-field>
                    </v-col>
                    <v-col v-if="register" cols="12" class="px-0">
                      <v-text-field
                        label="Email*"
                        required
                        clearable
                        variant="outlined"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" class="px-0">
                      <v-text-field
                        label="Password*"
                        type="password"
                        required
                        clearable
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
                  value="teal"
                  hide-details
                  class="pl-3"
                ></v-switch>
                <v-spacer></v-spacer>
                <v-btn color="red" variant="text" @click="dialog = false">
                  Close
                </v-btn>
                <v-btn color="teal" variant="text" @click="dialog = false">
                  Save
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-row>
      </div>
      <div class="col-auto">
        <div class="pr-5">
          <DxSelect
            :ref="rolesSelectRef"
            :data-url="getPageArticles"
            :width="width"
          />
        </div>

        <!-- <v-text-field
          clearable
          density="compact"
          prepend-inner-icon="mdi-magnify"
          single-line
          hide-details
          variant="outlined"
          class="search-field mr-5"
        ></v-text-field> -->
      </div>
    </div>
  </nav>
</template>

<script>
import { defineComponent } from "vue";

import DxSelect from "./DxSelect.vue";
const URL = "https://localhost:7188";

export default defineComponent({
  components: {
    DxSelect,
  },
  data() {
    return {
      width: "400",
      loading: false,
      post: null,
      dialog: false,
      register: false,
      getPageArticles: `${URL}/Roles/GetRoles`,
    };
  },
  created() {},
  watch: {},
  methods: {},
});
</script>
<style>
.search-field {
  width: 350px;
}
.badge-ref {
  text-decoration: none;
}
.btn-login {
  align-self: center;
}
</style>
