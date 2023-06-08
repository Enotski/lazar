<template>
    <div class="mt-auto border-top footer pt-2 text-body-secondary">
        <div class="container-fluid">
            <div class="row mr-row">
                <div class="row col-auto me-auto">
                    <div class="col-auto d-content align-center">
                        <span class="mdi mdi-waveform">lazar - 2023</span>
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
                            class="mdi mdi-github text-decoration-none text-reset"> Original articles</a>
                    </div>
                    <div class="col d-content" title="My github">
                        <a href="https://github.com/Enotski" class="mdi mdi-github text-reset text-decoration-none">
                            Enotski</a>
                    </div>
                    <div class="col-auto">
                        <span class="mdi mdi-vk" title="My Vk"></span>/<span class="mdi mdi-telegram"
                            title="My Telegram"></span> @kiki_bones
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