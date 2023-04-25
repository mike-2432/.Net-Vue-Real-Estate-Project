import routes from "../../router/externalRoutes.js";
import router from "../../router/index.js";

// AUTH MODULE //
const auth = {
  namespaced: true,

  state: () => ({
    loginErrMsg: "",
    registerErrMsg: "",
    // The forceRenderKey is used to refresh the navbar component after a successful login
    forceRenderKey: 0,
  }),

  mutations: {
    setLoginErrMsg(state, msg) {
      state.loginErrMsg = msg;
    },
    setRegisterErrMsg(state, msg) {
      state.registerErrMsg = msg;
    },
    refreshComponent(state) {
      state.forceRenderKey += 1;
    },
  },

  actions: {
    // Function removes the jwt from local storage if the response is not ok
    async checkLoginStatus() {
      // Gets the jwt from localstorage if present
      const jwt = window.localStorage.getItem("jwt");
      const response = await fetch(routes.apiRoute + "api/auth/loggedIn", {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      });
      if (!response.ok) {
        window.localStorage.removeItem("jwt");
      }
    },

    // Function logges the user in
    async login(context, requestData) {
      const response = await fetch(routes.apiRoute + "api/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      });
      const jsonResponse = await response.json();
      if (!response.ok) {
        context.commit("setLoginErrMsg", jsonResponse.message);
      } else {
        const jwt = jsonResponse.data;
        window.localStorage.setItem("jwt", jwt);
        context.commit("refreshComponent");
        router.push({ name: "houses" });
      }
    },

    // Function registers a new user
    async register(context, requestData) {
      const response = await fetch(routes.apiRoute + "api/auth/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestData),
      });
      const textResponse = await response.text();
      if (!response.ok) {
        context.commit("setRegisterErrMsg", textResponse);
      } else {
        context.commit("setRegisterErrMsg", "Success!");
        setTimeout(() => {
          router.push({ name: "login" });
        }, "1500");
      }
    },
  },
};

export default auth;
