<template>
    <div class="search-container">

        <!-- Search Field -->
        <div class="search-field">
            <img src="@/assets/images/ic_search@3x.png">
            <input v-model="searchFilter" type="text" name="search" placeholder="Search for a street or city" />
            <button class="clear-btn" @click="clearSearchFilter">
                <img src="@/assets/images/ic_clear@3x.png">
            </button>
        </div>

        <!-- Filter Buttons -->
        <div class="sort-container">
            <button @click="sort($event)" :class="[$data.activeSort == 'Price' ? 'active' : 'inactive']">Price</button>
            <button @click="sort($event)" :class="[$data.activeSort == 'Size' ? 'active' : 'inactive']">Size</button>
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
        }
    },
    methods: {
        ...mapMutations("getListings", ["setSortOption", "setReverseOption", "setSearchFilter"]),

        sort(e) {
            if (e.target.innerHTML == 'Price') {
                if(this.activeSort === 'Price') {
                    this.setReverseOption('switch')
                } else {
                    this.activeSort = 'Price';
                    this.setSortOption('price');
                    this.setReverseOption('false');
                }               
            }
            else if (e.target.innerHTML == 'Size') {
                if (this.activeSort === 'Size') {
                    this.setReverseOption('switch')
                } else {
                    this.activeSort = 'Size';
                    this.setSortOption('size');
                    this.setReverseOption('false');
                }
            }
        },

        clearSearchFilter() {
            this.searchFilter = "";
        }
    },
    watch: {
        searchFilter: function () {
            this.setSearchFilter(this.searchFilter);
        },
    },
    beforeMount() {
        this.setSearchFilter("");  
        this.setSortOption('price');
        this.setReverseOption('false');      
    }
}
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
    margin-bottom: 2rem;
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
    padding: 0 10px;
}

.search-field input {
    padding-left: 1.5rem;
    padding-right: 1.5rem;
    flex: 1;
    border: none;
    background-color: var(--clr-ter-light);
}

.sort-container {
    height: 3rem;
    border-radius: 0;
    margin-bottom: 2rem;
}

.sort-container button {
    width: 50%;
    height: inherit;
    border: none;
    color: var(--clr-text-white);
    cursor: pointer;
}

.sort-container button:nth-child(1) {
    border-radius: var(--radius) 0 0 var(--radius);
}

.sort-container button:nth-child(2) {
    border-radius: 0 var(--radius) var(--radius) 0;
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
@media screen and (min-width: 550px) {
    .clear-btn {
        display: none;
    }
}
@media screen and (min-width: 600px) {
    .search-container {
        flex-direction: row;
        justify-content: space-between;
    }

    .search-field {
        width: 30rem;
        margin-right: 1rem;
    }

    .sort-container {
        width: 20rem;
    }
}
</style>