<template>
    <n-config-provider :katex="katex">
        <n-equation :value="modelValue" :katex-options="katexOptions" />
    </n-config-provider>
</template>

<script>
import katex from 'katex';
import { NConfigProvider, NEquation } from "naive-ui";
export default {
    components: {
        NConfigProvider,
        NEquation
    },
    props: ['modelValue'],
    data() {
        return {
            katex,
            katexOptions: {
                displayMode: true,
                fleqn: true,
                macros: {
                    '\\f': '#1f(#2)'
                }
            },
        };
    },
    computed: {},
    methods: {},
};
</script>
