<template>
  <div class="container">
    <!-- HOUSES HEADER -->
    <!-- contains the title and the button to create a new listing -->
    <HousesHeader />

    <!-- FILTER CONTAINER -->
    <!-- contains the searchbar and the sort buttons -->
    <FilterContainer />

    <!-- HOUSES LIST -->
    <div v-if="isLoaded === 'loaded' && getSortedList.length > 0">
      <li v-for="house in getSortedList" :key="house.id">
        <ListItem :data="house" />
      </li>
    </div>

    <!-- NO ITEMS FOUND -->
    <NotFound
      v-else-if="isLoaded === 'loaded' && getSortedList.length == 0"
      message="Not Results Found"
      messageTwo="Please try another keyword"
    />

    <!-- LOADING SCREEN -->
    <LoadingScreen v-else-if="isLoaded == 'notLoaded'" />

    <!-- UNABLE TO FETCH RESOURCES -->
    <NotFound v-else message="Something went wrong..." />
  </div>
</template>

<!-- SCRIPT -->
<script>
import { mapState, mapActions, mapGetters } from "vuex";
import HousesHeader from "../components/HousesHeader.vue";
import FilterContainer from "../components/FilterContainer.vue";
import ListItem from "../components/ListItem.vue";
import NotFound from "../components/NotFound.vue";
import LoadingScreen from "../components/LoadingScreen.vue";

export default {
  name: "HousesView",
  components: {
    HousesHeader,
    FilterContainer,
    ListItem,
    LoadingScreen,
    NotFound,
  },
  computed: {
    ...mapState("getListings", ["isLoaded"]),
    ...mapGetters("getListings", ["getSortedList"]),
  },
  methods: {
    ...mapActions("getListings", ["fetchHouses"]),
  },
  beforeMount() {
    this.fetchHouses();
  },
};
</script>

<!-- STYLES -->
<style scoped>
.container {
  width: var(--width);
  max-width: var(--max-width);
  margin: auto;
  margin-bottom: 8rem;
}
li {
  margin-bottom: 2rem;
}

@media screen and (min-width: 550px) {
  .container {
    margin-bottom: 4rem;
  }
}
</style>
