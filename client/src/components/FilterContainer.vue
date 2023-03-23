<template>
  <div class="search-container">
    <!-- Search Field -->
    <div class="search-field">
      <img src="@/assets/images/ic_search@3x.png" />
      <input
        v-model="searchFilter"
        type="text"
        name="search"
        placeholder="Search for a street or city"
      />
      <button class="clear-btn" @click="clearSearchFilter">
        <img src="@/assets/images/ic_clear@3x.png" />
      </button>
    </div>

    <!-- Filter Buttons -->
    <div class="sort-container">
      <button
        @click="sort($event)"
        :class="[$data.activeSort == 'Price' ? 'active' : 'inactive']"
      >
        Price
      </button>
      <button
        @click="sort($event)"
        :class="[$data.activeSort == 'Size' ? 'active' : 'inactive']"
      >
        Size
      </button>
      <button
        v-if="isLoggedIn"
        @click="sort($event)"
        :class="[$data.myListings === true ? 'active' : 'inactive']"
      >
        Owned
      </button>
    </div>
  </div>
</template>

<!-- SCRIPT -->
<script>
import { mapMutations } from "vuex";
export default {
  name: "FilterContainer",
  data() {
    return {
      searchFilter: "",
      activeSort: "Price",
      myListings: false,
    };
  },
  computed: {
    isLoggedIn() {
      if (window.localStorage.getItem("jwt") !== null) return true;
      return false;
    },
  },
  methods: {
    ...mapMutations("getListings", [
      "setSortOption",
      "setReverseOption",
      "setSearchFilter",
      "switchShowMyListings",
    ]),
    // Clears the search input
    clearSearchFilter() {
      this.searchFilter = "";
    },
    // Sort method
    sort(e) {
      if (e.target.innerHTML.trim() == "Price") {
        if (this.activeSort === "Price") {
          this.setReverseOption("switch");
        } else {
          this.activeSort = "Price";
          this.setSortOption("price");
          this.setReverseOption("false");
        }
      } else if (e.target.innerHTML.trim() == "Size") {
        if (this.activeSort === "Size") {
          this.setReverseOption("switch");
        } else {
          this.activeSort = "Size";
          this.setSortOption("size");
          this.setReverseOption("false");
        }
      } else if (e.target.innerHTML.trim() == "Owned") {
        this.myListings = !this.myListings;
        this.switchShowMyListings();
      }
    },
  },
  watch: {
    searchFilter: function () {
      this.setSearchFilter(this.searchFilter);
    },
  },
  beforeMount() {
    this.setSearchFilter("");
    this.setSortOption("price");
    this.setReverseOption("false");
  },
};
</script>

<!-- STYLES -->
<style scoped>
/* MOBILE */
.search-container {
  display: flex;
  flex-direction: column;
}

.search-field {
  padding: 0 1.5rem;
  margin-bottom: 1rem;
  height: 3rem;
  background-color: var(--clr-ter-light);
  border-radius: var(--radius);
  display: flex;
}

.search-field img {
  height: 1.1rem;
  margin: auto;
}

.clear-btn {
  background: none;
  border: none;
  padding: 0 0 0 10px;
  cursor: pointer;
  display: flex;
  justify-content: center;
}

.search-field input {
  padding-left: 1.5rem;
  padding-right: 1.5rem;
  flex: 1;
  border: none;
  background-color: var(--clr-ter-light);
}

.sort-container {
  display: flex;
  justify-content: space-around;
  height: 3rem;
  margin-bottom: 2rem;
}

.sort-container button {
  border: none;
  color: var(--clr-text-white);
  cursor: pointer;
  padding-left: auto;
  width: 100%;
}

.sort-container button:nth-child(1) {
  border-radius: var(--radius) 0 0 var(--radius);
}

.sort-container button:nth-child(2) {
  border-radius: 0 var(--radius) var(--radius) 0;
}

.sort-container button:nth-child(3) {
  border-radius: var(--radius);
  margin-left: 1rem;
}

.active {
  background-color: var(--clr-prim);
  font-weight: var(--font-weight-bold);
}

.inactive {
  background-color: var(--clr-ter-normal);
  font-weight: var(--font-weight-medium);
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 700px) {
  .search-container {
    flex-direction: row;
    justify-content: space-between;
  }

  .search-field {
    width: 30rem;
    margin-right: 1rem;
  }

  .sort-container {
    width: 25rem;
  }
}
</style>
