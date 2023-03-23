<!-- CREATE LISTING VIEW -->
<!-- This template contains both the option to edit an existing listing and -->
<!-- create a new listing, which is dependend on the url -->
<template>
  <!-- OUTER CONTAINER -->
  <!-- This container is exclusively used for the background image -->
  <!-- If the page is the edit listing page, wait until the house is fetched -->
  <div class="page-outer-container" v-if="dataIsLoaded">
    <!-- PAGE CONTAINER -->
    <div class="page-container">
      <!-- HEADER -->
      <div class="header">
        <button class="back-btn">
          <router-link to="/">
            <img src="@/assets/images/ic_back_grey@3x.png" />
            <p class="desktop-only">Back to overview</p>
          </router-link>
        </button>
        <h1 v-if="isNewListing">Create new listing</h1>
        <h1 v-else>Edit listing</h1>
      </div>

      <!-- FORM -->
      <!-- Iterates over al form values -->
      <form @submit.prevent="handleSubmit" class="form-wrapper">
        <div v-for="item in formValues" :key="item.id">
          <!-- Form Item -->
          <div
            v-if="!item.isImage && !item.isGarage"
            class="form-item-wrapper"
            :class="item.inputWidth"
          >
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

          <!-- Form Garage Option -->
          <div
            v-if="item.isGarage"
            class="form-item-wrapper"
            :class="item.inputWidth"
          >
            <label :htmlFor="item.label">{{ item.name }}</label>
            <select v-model="requestData[item.key]">
              <option value="false">No</option>
              <option value="true">Yes</option>
            </select>
          </div>

          <!-- Form Image -->
          <div v-if="item.isImage" class="form-item-wrapper upload-img">
            <label :htmlFor="item.label">{{ item.name }}</label>
            <div class="upload-img-wrapper">
              <input
                style="opacity: 0"
                type="file"
                @change="uploadFile($event)"
                :required="isNewListing"
              />
              <button class="upload-img-btn">
                <img src="@/assets/images/ic_upload@3x.png" />
              </button>
              <img
                v-if="previewImage"
                class="preview-img"
                :src="previewImage"
              />
            </div>
          </div>
        </div>

        <!-- Form Text Area -->
        <div class="text-area">
          <label>Description*</label>
          <textarea
            v-model="requestData.description"
            placeholder="Enter description"
            rows="4"
            required
          >
          </textarea>
        </div>

        <!-- Error message-->
        <p class="error-message">{{ errorMessage }}</p>

        <!-- Submit button -->
        <button
          :class="['submit-btn', isSubmitButtonDisabled ? 'disabled' : '']"
          type="submit"
        >
          Submit
        </button>
      </form>
    </div>
  </div>
  <!-- LOADING -->
  <LoadingScreen v-else />
</template>

<!-- SCRIPT -->
<script>
import { mapState, mapActions } from "vuex";
import LoadingScreen from "../components/LoadingScreen.vue";
import FormValues from "../assets/forms/houseFormValues.js";

export default {
  data() {
    return {
      previewImage: "",
      requestImage: "",
      requestData: {
        streetName: "",
        houseNumber: "",
        Addition: "",
        zip: "",
        city: "",
        price: "",
        size: "",
        hasGarage: "",
        bedrooms: "",
        bathrooms: "",
        constructionYear: "",
        description: "",
      },
      formValues: FormValues,
    };
  },
  components: {
    LoadingScreen,
  },
  computed: {
    ...mapState("getSingleListing", ["isLoaded", "singleHouse"]),
    ...mapState("createListing", ["errorMessage"]),

    // Checks the url to see whether a listing needs to be created or edited
    isNewListing() {
      return this.$route.name === "create listing";
    },

    // Shows loading screen only if the page is not a new listing and if data is fetched
    dataIsLoaded() {
      if (this.isNewListing) return true;
      if (this.isLoaded === "loaded") return true;
      return false;
    },

    // Checks if a values are filled in, returns true and disables the submit button if values are missing
    isSubmitButtonDisabled() {
      if (this.isNewListing) {
        if (!this.requestImage) return true;
      }
      for (const [key, value] of Object.entries(this.requestData)) {
        if (key !== "Addition") {
          if (!value) return true;
        }
      }
      return false;
    },
  },
  methods: {
    ...mapActions("getSingleListing", ["fetchHouse"]),
    ...mapActions("createListing", ["createHouse", "editHouse", "uploadImage"]),

    // Sets the image file
    uploadFile(e) {
      const image = e.target.files[0];
      this.requestImage = image;
      this.previewImage = URL.createObjectURL(image);
    },

    // Submits the form
    handleSubmit() {
      // Convert string garage to bool
      this.requestData.hasGarage =
        this.requestData.hasGarage === "false" ? false : true;
      // Sends the form data
      if (this.isNewListing) {
        const payload = {
          form: this.requestData,
          image: this.requestImage,
        };
        this.createHouse(payload);
      } else {
        this.requestData.id = this.singleHouse.id;
        const payload = { form: this.requestData, image: this.requestImage };
        this.editHouse(payload);
      }
    },
  },
  beforeMount() {
    // Fetches the house if the page is the edit listing page
    if (this.$route.name === "edit listing") {
      this.fetchHouse(this.$route.params);
    }
  },
  watch: {
    isLoaded() {
      if (this.isLoaded === "loaded") {
        // Maps the response values to the form data
        this.requestData.id = this.singleHouse.id;
        this.requestData.streetName = this.singleHouse.streetName;
        this.requestData.houseNumber = this.singleHouse.houseNumber;
        this.requestData.Addition = this.singleHouse.addition;
        this.requestData.price = this.singleHouse.price;
        this.requestData.zip = this.singleHouse.zip;
        this.requestData.city = this.singleHouse.city;
        this.requestData.size = this.singleHouse.size;
        this.requestData.hasGarage =
          this.singleHouse.hasGarage === true ? "true" : "false";
        this.requestData.constructionYear = this.singleHouse.constructionYear;
        this.requestData.bedrooms = this.singleHouse.bedrooms;
        this.requestData.bathrooms = this.singleHouse.bathrooms;
        this.requestData.description = this.singleHouse.description;

        this.previewImage =
          "https://housestorage1.blob.core.windows.net/housecontainer/" +
          this.singleHouse.id;

        // Image is not yet loading properly in this version
        // old code //
        //
        // this.previewImage = this.singleHouse.image
        //
      }
    },
  },
};
</script>

<!-- STYLES -->
<style scoped>
/* MOBILE */
.desktop-only {
  display: none;
}

.page-outer-container {
  background-image: url("../assets/images/img_background@3x.png");
  background-size: cover;
  background-color: var(--clr-background-1);
  opacity: 0.8;
}

.page-container {
  width: 90vw;
  position: relative;
  margin: auto;
  padding-bottom: 6rem;
}

.error-message {
  color: red;
  margin-top: 1rem;
}

.header {
  display: flex;
  justify-content: center;
  position: relative;
  margin-bottom: 1.5rem;
  padding-top: 4rem;
  height: 2rem;
  line-height: 2rem;
}

.header button {
  position: absolute;
  left: 0;
  display: flex;
  align-items: center;
  cursor: pointer;
}

.back-btn {
  border: none;
  background: none;
}

.back-btn > * {
  display: flex;
  align-items: center;
}

.back-btn img {
  height: 2rem;
  padding: 0.3rem;
  margin-right: 1rem;
}

.form-wrapper {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  width: 90vw;
  justify-content: space-between;
}

.form-item-wrapper {
  margin-top: 1.6rem;
  display: flex;
  flex-direction: column;
}

.full-width {
  width: 90vw;
}

.half-width {
  width: 43vw;
}

label {
  margin-bottom: 0.7rem;
  font-family: var(--ff-montserrat);
  font-weight: var(--font-weight-semibold);
  color: var(--clr-text-sec);
}

.form-item-wrapper input,
.form-item-wrapper select {
  border: none;
  background-color: var(--clr-background-2);
  font-family: var(--ff-open-sans);
  color: var(--clr-text-sec);
  height: 3rem;
  padding: 0 1.1rem;
  border-radius: var(--radius);
}
.form-item-wrapper option {
  font-family: var(--ff-open-sans);
  color: var(--clr-text-sec);
}

.form-item-wrapper input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.form-item-wrapper input[type="number"] {
  -moz-appearance: textfield;
}

.text-area {
  margin-top: 1rem;
  width: 100%;
}

textarea {
  resize: none;
  width: inherit;
  border: none;
  border-radius: var(--radius);
  margin-top: 1rem;
  padding: 1rem;
  font-family: var(--ff-open-sans);
  color: var(--clr-text-sec);
}

textarea:focus {
  outline: none;
}

.upload-img-wrapper input {
  width: 6rem;
  height: 6rem;
  cursor: pointer;
}

.upload-img-btn {
  position: absolute;
  left: 0;
  width: 6rem;
  height: 6rem;
  border: 3px dashed var(--clr-sec);
  opacity: 0.8;
  z-index: -1;
  background: none;
}

.upload-img-btn img {
  height: 30%;
}

.preview-img {
  position: absolute;
  left: 0;
  width: 6rem;
  height: 6rem;
  z-index: -1;
  object-fit: cover;
}

.submit-btn {
  border: none;
  background-color: var(--clr-prim);
  padding: 1rem 4rem;
  border-radius: var(--radius);
  color: var(--clr-text-white);
  font-size: 16px;
  margin-top: 2rem;
  margin-left: auto;
  cursor: pointer;
  text-transform: uppercase;
}

.disabled {
  background-color: var(--clr-text-sec);
  cursor: default;
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 550px) {
  .desktop-only {
    display: block;
  }

  .page-container {
    max-width: var(--max-width);
  }

  .header {
    flex-direction: column;
    height: auto;
    justify-content: space-between;
  }

  .header h1 {
    margin-top: 4rem;
  }

  .back-btn {
    position: static;
  }

  .form-wrapper {
    width: 25rem;
  }

  label {
    font-size: 14px;
  }

  .full-width {
    width: 25rem;
  }

  .half-width {
    width: 12rem;
  }
}
</style>
