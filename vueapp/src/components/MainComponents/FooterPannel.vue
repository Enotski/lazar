<template>
    <div class="mt-auto footer fixed-bottom bg-light p-3 text-body-secondary footer-shadow">
        <div class="container-fluid text-secondary">
            <div class="row mr-row">
                <div class="row col-auto me-auto">
                    <div class="col-auto d-content align-center">
                        <span class="fw-semibold">lazar - 2023</span>
                    </div>
                    <div class="col-auto align-center d-none">
                        <select class="form-select text-reset" v-model='language' aria-label="Language">
                            <option value="en" selected>English</option>
                            <option value="ru">Russian</option>
                        </select>
                    </div>
                    <div class="row col-auto align-center p-0 ml-2 mr-2" title="Set light mode">
                    </div>
                </div>
                <div class="row col-auto ms-auto align-center">
                    <div class="col-auto d-content " title="Original articles about digital signal processing">
                        <a href="https://github.com/hukenovs/dsp-theory"
                            class="mdi mdi-github text-decoration-none text-reset fw-semibold"> Original articles</a>
                    </div>
                    <div class="col d-content ml-3" title="My github">
                        <a href="https://github.com/Enotski" class="mdi mdi-github text-reset text-decoration-none fw-semibold">
                            Enotski</a>
                    </div>
                    <div class="col-auto ml-3">
                        <span class="mdi mdi-vk" title="My Vk"></span>/<span class="mdi mdi-telegram"
                            title="My Telegram"></span><span class="fw-semibold">@kiki_bones</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent({
    data() {
        return {
            loading: false,
            post: null,
            links: [
                'mdi-facebook',
                'mdi-twitter',
                'mdi-linkedin',
                'mdi-instagram',
            ],
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
.footer-shadow {
    box-shadow: 0 -0.5rem 1rem rgb(0 0 0 / 15%);
}
</style>