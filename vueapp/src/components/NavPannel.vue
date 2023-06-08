<template>
    <div>
        <nav class="navbar d-flex justify-content-between navbar-light bg-white border-bottom box-shadow mb-3">
            <div>
                <v-text-field clearable prepend-icon="mdi-moon-waning-crescent" variant="underlined"></v-text-field>
            </div>
            <div>
                <v-btn icon="$vuetify" variant="text"></v-btn>
            </div>
        </nav>
    </div>
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