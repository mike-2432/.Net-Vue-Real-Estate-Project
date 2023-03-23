import routes from "../../router/externalRoutes.js";

// GET ALL LISTINGS MODULE //
const getListings = {
  namespaced: true,
  state: () => ({
    // isLoaded has three states: notLoaded, notFound and loaded
    isLoaded: "notLoaded",
    houseList: [],
    sortOption: "price",
    reverseOption: false,
    showMyListings: false,
    searchFilter: "",
    // The current house is for the recommendationlist.
    // The recommendationlist is sorted on items where the price is closest to the price of the current house
    currentHouse: {},
  }),
  mutations: {
    setHouseList(state, houseList) {
      state.houseList = houseList;
      state.isLoaded = true;
    },
    setSortOption(state, sort) {
      state.sortOption = sort;
    },
    setReverseOption(state, param) {
      if (param === "switch") state.reverseOption = !state.reverseOption;
      if (param === "false") state.reverseOption = false;
    },
    switchShowMyListings(state) {
      state.showMyListings = !state.showMyListings;
    },
    setSearchFilter(state, filter) {
      state.searchFilter = filter;
    },
    setCurrentHouse(state, data) {
      state.currentHouse = data;
    },
    setIsLoaded(state, param) {
      state.isLoaded = param;
    },
  },
  actions: {
    // Function to fetch all houses
    async fetchHouses(context) {
      try {
        // Gets the jwt from localstorage if present
        const jwt = window.localStorage.getItem("jwt");
        const response = await fetch(routes.apiRoute + "api/houses", {
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + jwt,
          },
        });
        const data = await response.json();
        context.commit("setHouseList", data);
        context.commit("setIsLoaded", "loaded");
      } catch (err) {
        context.commit("setIsLoaded", "notFound");
      }
    },
  },
  getters: {
    // Sorted list based on the sortOption, reverseOption and searchFilter
    getSortedList(state) {
      const searchFilter = state.searchFilter.toLowerCase().split(" ");

      // Filters the houses //
      let filtered = [...state.houseList].filter((house) => {
        return searchFilter.every(
          (i) =>
            house.streetName.toLowerCase().includes(i) ||
            house.city.toLowerCase().includes(i)
        );
      });

      // Sort only the owned listings
      if (state.showMyListings == true) {
        filtered = filtered.filter((house) => house.madeByMe == true);
      }

      // Sorts the houses //
      const sorted = filtered.sort((a, b) => {
        if (a[state.sortOption] > b[state.sortOption]) {
          return 1;
        }
        if (a[state.sortOption] < b[state.sortOption]) {
          return -1;
        }
        return 0;
      });
      if (state.reverseOption) sorted.reverse();
      return sorted;
    },

    // List of recommendations that is based on the price closest the the current houseprice
    getRecommendedList(state) {
      const currentPrice = state.currentHouse.price;
      const currentId = state.currentHouse.id;
      const recommendedList = [...state.houseList]
        .filter((house) => house.id !== currentId && !house.madeByMe)
        .sort((a, b) => {
          return (
            Math.abs(a.price - currentPrice) - Math.abs(b.price - currentPrice)
          );
        })
        .slice(0, 4);
      return recommendedList;
    },

    // Recent History List showing the 4 last viewed houses
    getHistoryList(state) {
      if (localStorage.getItem("history") === null) return;
      const history = JSON.parse(localStorage.getItem("history"));
      const historyList = [...state.houseList].filter((house) => {
        return history.some((id) => id === house.id);
      });
      return historyList;
    },
  },
};

export default getListings;
