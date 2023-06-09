<template>
    <nav class="navbar d-flex fixed-top bg-light justify-content-between box-shadow mb-3 shadow p-3 py-2">
        <div>
            <span class="mdi mdi-waveform text-info bi me-2 fs-3"></span><span class="navbar-brand mb-0 h1 text-secondary fw-medium">DSP interactive articles</span>
        </div>
        <div class="search-field">
            <v-text-field clearable density="compact" prepend-inner-icon="mdi-magnify" single-line
                hide-details variant="outlined"></v-text-field>
        </div>
        <div>
            <v-btn icon="mdi-account-circle-outline" variant="text" title="Sign Up / Log In" class="text-secondary"></v-btn>
        </div>
    </nav>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent({
    data() {
        return {
            loading: false,
            post: null
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
</style>