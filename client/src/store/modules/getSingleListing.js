import routes from "../../router/externalRoutes.js";
import utils from "../../utils/utils.js";

// GET SINGLE LISTING MODULE //
const getSingleListing = {
  namespaced: true,
  state: () => ({
    // isLoaded has three states: notLoaded, notFound and loaded
    isLoaded: "notLoaded",
    singleHouse: "",
  }),
  mutations: {
    setSingleHouse(state, data) {
      state.singleHouse = data;
      // Adds the single house to the history in local storage
      // The history contains a maximum of 4 houses
      if (localStorage.getItem("history") === null) {
        utils.addToLocalStorage("history", data.id);
      } else {
        const history = JSON.parse(localStorage.getItem("history"));
        if (history.length > 3 && history.every((id) => id !== data.id)) {
          utils.removeFirstFromLocalStorage("history");
        }
        utils.addToLocalStorage("history", data.id);
      }
    },
    setIsLoaded(state, param) {
      state.isLoaded = param;
    },
  },
  actions: {
    // Function to fetch a house
    async fetchHouse(context, param) {
      context.commit("setIsLoaded", "notLoaded");
      try {
        const jwt = window.localStorage.getItem("jwt");
        const response = await fetch(
          routes.apiRoute + "api/houses/" + param.id,
          {
            headers: {
              "Content-Type": "application/json",
              Authorization: "Bearer " + jwt,
            },
          }
        );
        const data = await response.json();
        context.commit("setSingleHouse", data);
        context.commit("setIsLoaded", "loaded");
      } catch (err) {
        context.commit("setIsLoaded", "notFound");
      }
    },
  },
};

export default getSingleListing;
