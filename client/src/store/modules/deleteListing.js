import router from "../../router/index.js";
import routes from "../../router/externalRoutes.js";
import Router from "../../router/index.js";

// DELETE LISTING MODULE //
const deleteListing = {
  namespaced: true,
  state: () => ({
    showDeleteWarning: false,
    deleteId: "",
  }),
  mutations: {
    setShowDeleteWarning(state, id) {
      state.showDeleteWarning = !state.showDeleteWarning;
      if (state.showDeleteWarning) state.deleteId = id;
      else state.deleteId = "";
    },
  },
  actions: {
    // Function to delete a house
    async deleteHouse({ commit, state }) {
      const jwt = window.localStorage.getItem("jwt");
      // Request to delete the house
      await fetch(routes.apiRoute + "api/houses/" + state.deleteId, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      });
      // Request to delete the image
      await fetch(routes.apiRoute + "api/storage/" + state.deleteId, {
        method: "DELETE",
      });
      commit("setShowDeleteWarning", state.deleteId);

      // Refreshes the page if the current page is the houses page, otherwise redirects to the houses page
      if (Router.currentRoute._rawValue.fullPath === "/") {
        router.go(0);
      } else {
        router.push({ name: "houses" });
      }
    },
  },
};

export default deleteListing;
