<template>
  <div class="container">
    <h1>Recommended for you</h1>
    <li class="house-item" v-for="house in getRecommendedList" :key="house.id">
      <ListItem :data="house" />
    </li>
  </div>
</template>

<!-- SCRIPT -->
<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import ListItem from "../components/ListItem";

export default {
  name: "RecommendationList",
  components: {
    ListItem,
  },
  props: {
    id: Number,
    price: Number,
  },
  computed: {
    ...mapGetters("getListings", ["getRecommendedList"]),
  },
  methods: {
    ...mapActions("getListings", ["fetchHouses"]),
    ...mapMutations("getListings", ["setCurrentHouse"]),
  },
  // Fetches the houses and sets the current house
  // The current house is not shown on the recommendation list
  beforeMount() {
    this.fetchHouses();
    const data = { id: this.id, price: this.price };
    this.setCurrentHouse(data);
  },
};
</script>

<!-- STYLES -->
<style scoped>
.container h1 {
  margin-bottom: 1.5rem;
}
li {
  margin-bottom: 1.5rem;
}
</style>
