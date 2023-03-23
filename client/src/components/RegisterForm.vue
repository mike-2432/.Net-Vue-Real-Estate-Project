<template>
  <div>
    <!-- TITLE -->
    <h1 class="auth-title">Register</h1>
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
    <div class="auth-error-message">{{ registerErrMsg }}</div>
  </div>
</template>

<!-- SCRIPT -->
<script>
import { mapState, mapMutations, mapActions } from "vuex";

export default {
  name: "RegisterForm",
  data() {
    return {
      requestData: {
        username: "",
        email: "",
        password: "",
        passwordRepeat: "",
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
          name: "email",
          key: "email",
          type: "email",
          placeholder: "Email",
          label: "Email",
          required: "required",
        },
        {
          id: 3,
          name: "password",
          key: "password",
          type: "password",
          placeholder: "Password",
          label: "Password",
          required: "required",
        },
        {
          id: 4,
          name: "repeat password",
          key: "passwordRepeat",
          type: "password",
          placeholder: "Repeat Password",
          label: "Repeat Password",
          required: "required",
        },
      ],
    };
  },
  computed: {
    ...mapState("auth", ["registerErrMsg"]),
  },
  methods: {
    ...mapMutations("auth", ["setRegisterErrMsg"]),
    ...mapActions("auth", ["login", "register"]),
    handleSubmit() {
      if (this.requestData.password !== this.requestData.passwordRepeat) {
        this.setRegisterErrMsg("Passwords do not match");
        return;
      }
      this.register(this.requestData);
    },
  },
};
</script>

<!-- STYLES -->
<style scoped>
@import "../styles/authFormStyles.css";
</style>
