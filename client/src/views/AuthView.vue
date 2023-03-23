<template>
  <div class="page-container">
    <div class="auth-box">
      <LoginForm v-if="isLogin" />
      <RegisterForm v-if="isRegister" />
    </div>
  </div>
</template>

<!-- SCRIPT -->
<script>
import LoginForm from "../components/LoginForm.vue";
import RegisterForm from "../components/RegisterForm.vue";

export default {
  components: {
    LoginForm,
    RegisterForm,
  },
  computed: {
    // Checks the url to see whether a the login or register component needs to be used
    isLogin() {
      return this.$route.name === "login";
    },
    isRegister() {
      return this.$route.name === "register";
    },
  },
};
</script>

<!-- STYLES -->
<style scoped>
/* MOBILE */
.page-container {
  margin: auto;
}
.auth-box {
  position: relative;
  margin: 4rem auto 0 auto;
  padding-top: 3rem;
  width: 90vw;
  max-width: 30rem;
  height: 35rem;
  background-color: var(--clr-background-2);
  text-align: center;
  box-shadow: var(--shadow);
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 550px) {
  .auth-box {
    height: 38rem;
    margin: 6rem auto 0 auto;
  }
  .page-container {
    max-width: var(--max-width);
  }
}
</style>
