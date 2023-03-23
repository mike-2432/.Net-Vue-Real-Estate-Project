<template>
  <div>
    <!-- TITLE -->
    <h1 class="auth-title">Login</h1>
    <div class="auth-title-underline"></div>

    <!-- FORM -->
    <form @submit.prevent="handleSubmit" class="auth-form-wrapper">
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
      <button class="auth-submit-btn" type="submit">Submit</button>
    </form>
    <button @click="routeToRegister" class="auth-redirect-btn">Register</button>
    <div class="auth-error-message">{{ loginErrMsg }}</div>
  </div>
</template>

<!-- SCRIPT -->
<script>
import { mapState, mapActions } from "vuex";
import router from "../router/index.js";

export default {
  name: "LoginForm",
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
        },
      ],
    };
  },
  computed: {
    ...mapState("auth", ["loginErrMsg"]),
  },
  methods: {
    ...mapActions("auth", ["login"]),
    routeToRegister() {
      router.push({ name: "register" });
    },
    handleSubmit() {
      this.login(this.requestData);
    },
  },
};
</script>

<!-- STYLES -->
<style scoped>
@import "../styles/authFormStyles.css";
</style>
