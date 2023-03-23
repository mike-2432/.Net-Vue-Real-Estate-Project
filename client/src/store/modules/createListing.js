import router from "../../router/index.js";
import routes from "../../router/externalRoutes.js";
// import utils from "../../utils/utils.js";

// CREATE LISTING MODULE //
// this module contains methods for creating a new house and editing an existing house
const createListing = {
  namespaced: true,
  state: () => ({
    errorMessage: "",
  }),
  mutations: {
    setErrorMessage(state, message) {
      state.errorMessage = message;
    },
  },

  actions: {
    // Function to create the house and upload the image
    async createHouse(context, requestData) {
      const { form, image } = requestData;
      const jwt = window.localStorage.getItem("jwt");
      try {
        // Post request for the form data
        // =========================== //
        const response = await fetch(routes.apiRoute + "api/houses", {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + jwt,
          },
          body: JSON.stringify(form),
        });
        const data = await response.json();

        // Post request for the image
        // ======================= //
        let formImage = new FormData();
        // Set the image name equal to the id of the house
        formImage.append("file", image, data.id);
        await fetch(routes.apiRoute + "api/storage", {
          method: "POST",
          headers: {
            Authorization: "Bearer " + jwt,
          },
          body: formImage,
        });

        // Redirects to the new house page
        router.push({ path: "house/" + data.id });
      } catch {
        context.commit(
          "setErrorMessage",
          "Something went wrong, please try again later."
        );
      }
    },

    // Function to edit a house and upload a new image
    async editHouse(context, requestData) {
      const { form, image } = requestData;
      const jwt = window.localStorage.getItem("jwt");
      // Post request for the form data
      // =========================== //
      const response = await fetch(routes.apiRoute + "api/houses", {
        method: "PUT",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
        body: JSON.stringify(form),
      });
      // Return when no new image is present
      if (!image) {
        if (response.ok) {
          router.push({ path: "/house/" + form.id });
          return;
        } else {
          context.commit(
            "setErrorMessage",
            "Something went wrong, please try again later."
          );
          return;
        }
      }
      // Post request for the image
      // ======================= //
      let formImage = new FormData();
      // Set the image name equal to the id of the house
      formImage.append("file", image, String(form.id));
      const imageResponse = await fetch(routes.apiRoute + "api/storage", {
        method: "PUT",
        headers: {
          Authorization: "Bearer " + jwt,
        },
        body: formImage,
      });
      // Redirects to the edited house page
      if (response.ok && imageResponse.ok) {
        router.push({ path: "/house/" + form.id });
        return;
      }
      // Throws an error if response is not ok
      context.commit(
        "setErrorMessage",
        "Something went wrong, please try again later."
      );
    },
  },
};

export default createListing;
