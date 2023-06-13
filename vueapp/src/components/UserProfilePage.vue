<template>
  <div class="container-fluid content-container">
    <div class="col">
      <div>
        <v-btn color="teal" @click="fetch">Fetch from API</v-btn>
        {{ messageBlock }}
      </div>
    </div>
    <div class="col">
      <v-text-field v-model="name"></v-text-field>
      <v-text-field v-model="email"></v-text-field>
      <v-text-field type="password" required></v-text-field>
      <v-btn color="teal">Save</v-btn>
    </div>
  </div>
</template>

<script>
import axios from "axios";

const config = {
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
  },
  withCredentials: false
};

export default {
  data() {
    return {
      name: "",
      email: "",
      message: "",
    };
  },
  computed: {
    messageBlock: function () {
      return this.message;
    },
  },
  methods: {
    async fetch() {
      try {
        const url = "https://localhost:7188/WeatherForecast";
        const response = await axios.get(url, config);
        const data = response.data;

        this.message = data;
      } catch (error) {
        this.message = error;
      }
    },
  },
};
</script>
