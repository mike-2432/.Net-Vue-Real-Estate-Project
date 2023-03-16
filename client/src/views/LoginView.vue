<template>    
    <!-- PAGE CONTAINER -->
    <div class="page-container">

        <!-- HEADER -->
        <h1>Login</h1>

        <!-- FORM -->
        <form @submit.prevent="handleSubmit" class="form-wrapper">
            <div v-for="item in formValues" :key="item.id">

                <!-- Form Item -->
                <label :htmlFor="item.label">{{ item.name }}</label>
                <input 
                    v-model="requestData[item.key]" 
                    :type="item.type" 
                    :placeholder="item.placeholder"
                    :pattern="item.pattern" 
                    :min="item.min"
                    :max="item.max"
                    :required="item.required" 
                />
            </div>  
            <button class="submit-btn" type="submit">Submit</button>     
        </form>

    </div>
</template>

<script>
import { mapActions } from 'vuex';

export default {
    data() {
        return {
            requestData: {
                username: "testuser",
                password: "123456Aa!",
            },
            formValues: [                    
                {
                    id: 1,
                    name: "username",
                    key: "username",
                    type: "text",
                    placeholder: "Username",
                    label: "Username",
                    required: "required",
                },
                {
                    id: 2,
                    name: "password",
                    key: "password",
                    type: "password",
                    placeholder: "Password",
                    label: "Password",
                    required: "required",
                }
            ]
        }
    },
    methods: {
        ...mapActions('auth', ['login']),
        handleSubmit() {
            this.login(this.requestData);            
        }
    },
}

</script>

<style scoped>
/* MOBILE */
.page-container {
    width: 80vw;
    margin: auto;
    margin-top: 4rem;
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 550px) {
    .page-container {
        max-width: var(--max-width);
    }
}

</style>