<template>
    <nav class="navbar d-flex fixed-top bg-light justify-content-between box-shadow mb-3 shadow p-3 py-2">
        <div>
            <span class="mdi mdi-waveform text-info bi me-2 fs-3"></span><span
                class="navbar-brand mb-0 h1 text-secondary fw-medium">DSP interactive articles</span>
        </div>
        <div>
            <router-link to="/users">Go to Users</router-link>
            <v-spacer></v-spacer>
            <router-link to="/">Go to Main</router-link>
            <v-spacer></v-spacer>
            <router-link to="/user-profile-page">Go to UsersPage</router-link>
        </div>
        <div class="d-flex flex-row-reverse">
            <div class="col-auto btn-login">
                <v-row justify="center">
                    <v-dialog v-model="dialog" persistent width="800">
                        <template v-slot:activator="{ props }">
                            <v-btn icon="mdi-account-circle-outline" v-bind="props" variant="text" title="Sign Up / Log In"
                                class="text-secondary"></v-btn>
                        </template>
                        <v-card class="p-3">
                            <v-card-title>
                                <span class="text-h5 text-secondary">User Profile</span>
                            </v-card-title>
                            <v-card-text>
                                <v-container>
                                    <v-row>
                                        <v-col cols="12" class="px-0">
                                            <v-text-field label="Login*" required></v-text-field>
                                        </v-col>
                                        <v-col v-if="register" cols="12" class="px-0">
                                            <v-text-field label="Email*" required></v-text-field>
                                        </v-col>
                                        <v-col cols="12" class="px-0">
                                            <v-text-field label="Password*" type="password" required></v-text-field>
                                        </v-col>
                                    </v-row>
                                </v-container>
                            </v-card-text>
                            <v-card-actions class="px-3">
                                <v-switch v-model="register" label="Register now" color="teal" value="teal" hide-details
                                    class="pl-3"></v-switch>
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
                <v-text-field clearable density="compact" prepend-inner-icon="mdi-magnify" single-line hide-details
                    variant="outlined" class="search-field mr-5"></v-text-field>
            </div>
        </div>
    </nav>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent({
    data() {
        return {
            loading: false,
            post: null,
            dialog: false,
            register: false
        };
    },
    created() {
    },
    watch: {
    },
    methods: {
    },
});
</script>
<style>
.search-field {
    width: 350px;
}

.btn-login {
    align-self: center;
}
</style>