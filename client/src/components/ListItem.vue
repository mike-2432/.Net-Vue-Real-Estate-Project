<template>
  <div class="list-item-container" @click="routeToSingleHouse">
    <!-- IMAGE -->
    <img class="house-img" :src="houseImage" />

    <!-- INFORMATION CONTAINER -->
    <div class="info-container">
      <!-- Upper information, contains the streetname and the buttons -->
      <div class="information-header">
        <h2 :class="shrinkLongStreetname">
          {{ data.streetName }} {{ data.houseNumber }} {{ data.addition }}
        </h2>

        <!-- Edit and Delete button -->
        <!-- Is shown if the item is owned by the user and if the user is on the houses page -->
        <div v-if="isNotRecommendation && data.madeByMe" class="options">
          <button @click="routeToEditListing">
            <img id="edit-btn" src="@/assets/images/ic_edit@3x.png" />
          </button>
          <button @click="showWarning">
            <img id="delete-btn" src="@/assets/images/ic_delete@3x.png" />
          </button>
        </div>
      </div>

      <!-- Lower information -->
      <p>€ {{ priceWithPoint }}</p>
      <p class="location">{{ data.zip }} {{ data.city }}</p>
      <div class="horizontal-wrapper">
        <img src="@/assets/images/ic_bed@3x.png" />
        <p>{{ data.bedrooms }}</p>
        <img src="@/assets/images/ic_bath@3x.png" />
        <p>{{ data.bathrooms }}</p>
        <img src="@/assets/images/ic_size@3x.png" />
        <p>{{ data.size }} m2</p>
      </div>
    </div>
  </div>
</template>

<!-- SCRIPT -->
<script>
import { mapMutations } from "vuex";
import router from "../router/index.js";

export default {
  name: "ListItem",
  props: {
    data: Object,
  },
  data() {
    return {
      houseImage:
        "https://housestorage1.blob.core.windows.net/housecontainer/" +
        this.data.id,
    };
  },
  computed: {
    // Converts the price to a price with a thousand separator
    priceWithPoint() {
      return this.data.price.toLocaleString();
    },
    // Checks if the page is the main page or the single house page
    // For the single house page, the edit and delete buttons are removed
    isNotRecommendation() {
      return this.$route.name === "houses";
    },
    // Checks the length of the streetname.
    // If the length of the streetname is longer than 20 characters, reduces the font size
    shrinkLongStreetname() {
      if (this.data.streetName.length > 25) return "reduce-font-size";
      return "";
    },
  },
  methods: {
    ...mapMutations("deleteListing", ["setShowDeleteWarning"]),
    routeToSingleHouse(e) {
      if (e.target.id !== "edit-btn" && e.target.id !== "delete-btn") {
        router.push({ name: "house", params: { id: this.data.id } });
      }
    },
    routeToEditListing() {
      router.push({ name: "edit listing", params: { id: this.data.id } });
    },
    showWarning() {
      this.setShowDeleteWarning(this.data.id);
    },
  },
};
</script>

<!-- STYLES -->
<style scoped>
/* MOBILE */
.list-item-container {
  background-color: var(--clr-background-2);
  min-height: 9rem;
  border-radius: var(--radius);
  display: flex;
  justify-content: space-between;
  cursor: pointer;
  transition: var(--transition);
}

.list-item-container:hover {
  transform: scale(1.008);
  transition: var(--transition);
}

.house-img {
  height: 8rem;
  width: 8rem;
  object-fit: cover;
  padding: 1rem;
  border-radius: 1.4rem;
}

.info-container {
  padding: 1.4rem 1rem 1.4rem 0;
  display: flex;
  flex-direction: column;
  width: 100%;
}

.info-container > * {
  margin-bottom: 0.3rem;
}

.information-header {
  display: flex;
  justify-content: space-between;
}

.location {
  color: var(--clr-text-sec);
  opacity: 0.8;
  flex: 1;
}

.horizontal-wrapper {
  display: flex;
  flex-wrap: wrap;
}

.horizontal-wrapper > p {
  margin-right: 0.7rem;
  font-size: 0.9em;
  color: var(--clr-text-sec);
}
.horizontal-wrapper > p:nth-child(6) {
  margin-right: 0;
}

.horizontal-wrapper > img {
  max-height: 1rem;
  max-width: 1rem;
  margin: auto 0.5rem auto 0;
}

.options {
  display: flex;
  align-items: flex-start;
}

.options button {
  cursor: pointer;
}

.options button {
  background-color: var(--clr-background-2);
  border: none;
  padding: 0 0.2rem 0.2rem 0.2rem;
  margin-left: 0.3rem;
}

.options button img {
  height: 0.9rem;
}

.reduce-font-size {
  font-size: 1em;
}

@media screen and (min-width: 340px) {
  .options button:nth-child(2) {
    margin-left: 0.5rem;
  }
  .options button img {
    height: 1rem;
  }
  .house-img {
    height: 9rem;
    width: 9rem;
  }
}

@media screen and (min-width: 390px) {
  .horizontal-wrapper > p {
    font-size: 1em;
    margin-right: 0.9rem;
  }

  .horizontal-wrapper > img {
    max-height: 1.2rem;
    max-width: 1.2rem;
  }
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 550px) {
  .list-item-container {
    height: 11rem;
    border-radius: var(--radius);
    display: flex;
  }

  .house-img {
    height: 11rem;
    width: 11rem;
  }

  .info-container p {
    font-size: 16px;
  }

  .options button img {
    height: 1.5rem;
  }
}
</style>
