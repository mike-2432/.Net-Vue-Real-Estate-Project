<template>
  <!-- PAGE CONTAINER -->
  <!-- Only view if the house is loaded -->
  <div v-if="isLoaded === 'loaded'" class="page-container">
    <!-- BACK BUTTON -->
    <router-link to="/" class="back-btn">
      <img class="desktop-only" src="@/assets/images/ic_back_grey@3x.png" />
      <img class="mobile-only" src="@/assets/images/ic_back_white@3x.png" />
      <p class="desktop-only">Back to overview</p>
    </router-link>

    <!-- MAIN CONTAINER -->
    <!-- Contains the image and the information about the house -->
    <div class="main-container">
      <div class="main-container-wrapper">
        <!-- Image -->
        <div class="image-wrapper">
          <img :src="houseImage" />
          <div class="mobile-only mobile-border-style"></div>
        </div>

        <!-- Information container -->
        <div class="info-container">
          <!-- Shows the edit and delete buttons if the house is created by the user -->
          <div v-if="singleHouse.madeByMe" class="edit-delete-btn-wrapper">
            <button @click="routeToEditListing" class="btn">
              <img
                class="mobile-only"
                src="@/assets/images/ic_edit_white@3x.png"
              />
              <img class="desktop-only" src="@/assets/images/ic_edit@3x.png" />
            </button>
            <button @click="setShowDeleteWarning(singleHouse.id)" class="btn">
              <img
                class="mobile-only"
                src="@/assets/images/ic_delete_white@3x.png"
              />
              <img
                class="desktop-only"
                src="@/assets/images/ic_delete@3x.png"
              />
            </button>
          </div>

          <!-- information about the house -->
          <h1>
            {{ singleHouse.streetName }} {{ singleHouse.houseNumber }}
            {{ singleHouse.addition }}
          </h1>
          <div class="horizontal-wrapper">
            <img src="@/assets/images/ic_location@3x.png" />
            <p class="semibold">{{ singleHouse.zip }} {{ singleHouse.city }}</p>
          </div>
          <div class="horizontal-wrapper">
            <img src="@/assets/images/ic_price@3x.png" />
            <p class="semibold">{{ priceWithPoint }}</p>
            <img src="@/assets/images/ic_size@3x.png" />
            <p class="semibold">{{ singleHouse.size }} m2</p>
            <img src="@/assets/images/ic_construction_date@3x.png" />
            <p class="semibold">Built in {{ singleHouse.constructionYear }}</p>
          </div>
          <div class="horizontal-wrapper">
            <img src="@/assets/images/ic_bed@3x.png" />
            <p class="semibold">{{ singleHouse.bedrooms }}</p>
            <img src="@/assets/images/ic_bath@3x.png" />
            <p class="semibold">{{ singleHouse.bathrooms }}</p>
            <img src="@/assets/images/ic_garage@3x.png" />
            <p v-if="singleHouse.hasGarage" class="semibold">yes</p>
            <p v-else class="semibold">no</p>
          </div>
          <p>{{ singleHouse.description }}</p>
        </div>
      </div>
    </div>

    <!-- RECOMMENDATIONS -->
    <div class="recommendation-and-history-container">
      <RecommendationList :id="singleHouse.id" :price="singleHouse.price" />
      <HistoryList :id="singleHouse.id" :price="singleHouse.price" />
    </div>
  </div>

  <!-- PAGE NOT FOUND -->
  <NotFound v-else-if="isLoaded === 'notFound'" message="Page not found" />

  <!-- LOADING -->
  <LoadingScreen v-else />
</template>

<!-- SCRIPT -->
<script>
import { mapState, mapMutations, mapActions } from "vuex";
import router from "../router/index.js";
import routes from "../router/externalRoutes.js";
import RecommendationList from "../components/RecommendationList.vue";
import HistoryList from "../components/HistoryList.vue";
import NotFound from "../components/NotFound.vue";
import LoadingScreen from "../components/LoadingScreen.vue";

export default {
  name: "SingleHouseView",
  data() {
    return {
      houseImage: "",
    };
  },
  components: {
    RecommendationList,
    HistoryList,
    NotFound,
    LoadingScreen,
  },
  computed: {
    ...mapState("getSingleListing", ["isLoaded", "singleHouse"]),
    // Converts the price to a price with a thousand separator
    priceWithPoint() {
      return this.singleHouse.price.toLocaleString();
    },
  },
  methods: {
    ...mapMutations("deleteListing", ["setShowDeleteWarning"]),
    ...mapActions("getSingleListing", ["fetchHouse"]),
    routeToEditListing() {
      router.push({
        name: "edit listing",
        params: { id: this.singleHouse.id },
      });
    },
  },
  beforeMount() {
    this.fetchHouse(this.$route.params);
  },
  watch: {
    // Fetches the house again if the route changes
    $route() {
      this.fetchHouse(this.$route.params);
    },
    isLoaded() {
      this.houseImage = routes.blobRoute + this.singleHouse.id;
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

.page-container {
  width: 100%;
  position: relative;
}

.back-btn {
  position: absolute;
  top: 1rem;
  left: 1rem;
  border: none;
  background: none;
  display: flex;
  height: 2rem;
  line-height: 2rem;
  font-size: 16px;
  font-family: var(--ff-montserrat);
  font-weight: var(--font-weight-semibold);
  z-index: 1;
}

.back-btn img {
  padding: 0.4rem;
}

.edit-delete-btn-wrapper {
  position: absolute;
  top: 0;
  right: 1rem;
  display: flex;
}

.btn {
  border: none;
  background: none;
  padding: 1rem 0.75rem;
  cursor: pointer;
}

.btn img {
  height: 1.5rem;
}

.main-container {
  position: static;
}
.main-container-wrapper {
  background-color: var(--clr-background-2);
}

.image-wrapper {
  position: relative;
}

.image-wrapper img {
  width: 100%;
  object-fit: cover;
}

.mobile-border-style {
  position: absolute;
  bottom: 0;
  height: 2rem;
  width: 100%;
  border-radius: 40px 40px 0 0;
  background-color: var(--clr-background-2);
}

.info-container {
  position: static;
  margin: 0 1.8rem;
  padding-bottom: 4.5rem;
}

.info-container p {
  color: var(--clr-text-sec);
}

.info-container > * {
  margin-bottom: 0.8rem;
}

.info-container > *:last-child {
  padding-top: 0.8rem;
}

.horizontal-wrapper {
  display: flex;
}

.horizontal-wrapper > p {
  margin-right: 0.6rem;
}

.horizontal-wrapper > p:nth-child(6) {
  margin-right: 0;
}

.horizontal-wrapper > img {
  max-height: 1.2rem;
  max-width: 1.2rem;
  margin: auto 0.5rem auto 0;
}

.recommendation-and-history-container {
  width: 90vw;
  margin: auto;
  padding-top: 3rem;
  margin-bottom: 7rem;
}

.semibold {
  font-weight: var(--font-weight-semibold);
}

@media screen and (min-width: 340px) {
  .horizontal-wrapper > p {
    margin-right: 1rem;
  }
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 550px) {
  .desktop-only {
    display: block;
  }

  .mobile-only {
    display: none;
  }

  .page-container {
    position: static;
    padding-top: 7rem;
    padding-bottom: 8rem;
    width: var(--width);
    max-width: var(--max-width);
    margin: auto;
  }

  .back-btn {
    top: 8rem;
    left: inherit;
    cursor: pointer;
  }

  .edit-delete-btn-wrapper {
    right: 0;
  }

  .btn {
    padding-top: 0.5rem;
    margin-left: 0.5rem;
  }

  .main-container {
    width: 100%;
  }

  .info-container {
    position: relative;
    margin: 1.8rem;
    padding-bottom: 5rem;
  }

  .info-container h1 {
    max-width: 85%;
  }

  .info-container > * {
    margin-bottom: 1rem;
  }

  .info-container > *:last-child {
    padding-top: 1rem;
  }
  .recommendation-and-history-container {
    margin: 0;
  }
}

@media screen and (min-width: 1300px) {
  .page-container {
    display: flex;
    justify-content: space-between;
  }

  .main-container {
    width: 60%;
  }

  .recommendation-and-history-container {
    width: 35%;
    margin: 0;
    padding-top: 0;
  }
}
</style>
