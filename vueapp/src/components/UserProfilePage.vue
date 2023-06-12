<template>
    <div class="container-fluid content-container">
        <div class="col">

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

export default {
    data() {
        return {
            name:"",
            email:""
        };
    },
};
</script>