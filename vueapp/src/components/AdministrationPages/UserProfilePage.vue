<template>
  <div class="container-fluid content-container">
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
            <v-col cols="12" class="px-0">
              <v-text-field
                ref="email"
                label="Email*"
                v-model="email"
                required
                clearable
                :error-messages="errorMessages"
                :rules="[
                  () => !!email || 'This field is required',
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
        <v-btn color="red" variant="text" @click="reset">Reset</v-btn>
        <v-btn color="teal" variant="text" @click="submit">Save</v-btn>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script>
export default {
  data() {
    return {
      login: "",
      email: "",
      password: "",
      errorMessages:[],
    };
  },
  computed: {},
  methods: {
    submit() {

    },
    reset() {

    },
  },
};
</script>
