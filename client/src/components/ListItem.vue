<template>
    <div class="list-item-container">
        <!-- IMAGE -->
        <img class="house-img" :src="houseImage">

        <!-- INFORMATION CONTAINER -->
        <div @click="routeToSingleHouse" class="info-container">
            <h2>{{ data.streetName }} {{ data.houseNumber }} {{ data.addition }} </h2>
            <p>€ {{ priceWithPoint }}</p>
            <p class="location">{{ data.zip }} {{ data.city }}</p>
            <div class="horizontal-wrapper">
                <img src="@/assets/images/ic_bed@3x.png">
                <p>{{ data.bedrooms }}</p>
                <img src="@/assets/images/ic_bath@3x.png">
                <p>{{ data.bathrooms }}</p>
                <img src="@/assets/images/ic_size@3x.png">
                <p>{{ data.size }} m2</p>
            </div>
        </div>

        <!-- EDIT AND DELETE BUTTON -->
        <!-- Is shown if the item is owned by the user and if the item is on the houses page -->
        <div v-if="isNotRecommendation && data.madeByMe" class="options">
            <button @click="routeToEditListing">
                <img src="@/assets/images/ic_edit@3x.png">
            </button>
            <button @click="showWarning">
                <img src="@/assets/images/ic_delete@3x.png">
            </button>
        </div>

    </div>
</template>


<!-- SCRIPT -->
<script>
import { mapMutations } from "vuex";
import router from '../router/index.js';

export default {
    name: 'ListItem',
    props: {
        data: Object
    },
    data() {
        return {
            houseImage: "https://housestorage1.blob.core.windows.net/housecontainer/" + this.data.id
        }
    },
    computed: {
        // Converts the price to a price with a thousand separator
        priceWithPoint() { return this.data.price.toLocaleString() },
        // Checks if the page is the main page or the single house page
        // For the single house page, the edit and delete buttons are removed
        isNotRecommendation() {
            return this.$route.name === 'houses';
        }
    },
    methods: {
        ...mapMutations('deleteListing', ['setShowDeleteWarning']),
        routeToSingleHouse() {
            router.push({ name: 'house', params: { id: this.data.id } });
        },
        routeToEditListing() {
            router.push({ name: 'edit listing', params: { id: this.data.id } });
        },
        showWarning() {
            this.setShowDeleteWarning(this.data.id);
        }
    }

}
</script>


<!-- STYLES -->
<style scoped>
/* MOBILE */
.list-item-container {
    background-color: var(--clr-background-2);
    height: 9rem;
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
    height: 9rem;
    width: 9rem;
    object-fit: cover;
    padding: 1rem;
    border-radius: 1.4rem;
}

.info-container {
    padding: 1.4rem 0 1.4rem 0;
    display: flex;
    flex-direction: column;
    flex: 1;
}

.info-container > * {
    margin-bottom: 0.3rem;
}

.location {
    color: var(--clr-text-sec);
    opacity: 0.8;
    flex: 1;
}

.horizontal-wrapper {
    display: flex;
}

.horizontal-wrapper > p {
    margin-right: 1rem;
    color: var(--clr-text-sec);
}

.horizontal-wrapper > img {
    max-height: 1.2rem;
    max-width: 1.2rem;
    margin: auto 0.5rem auto 0;
}

.options {
    display: flex;
    background: none;
}

.options button {
    cursor: pointer;
}

.options button:nth-child(1) {
    padding: 1rem 0.5rem 1rem 1rem;
    height: 4rem;
    background-color: var(--clr-background-2);
    border: none;
}

.options button:nth-child(2) {
    padding: 1rem 1rem 1rem 0.5rem;
    height: 4rem;
    background-color: var(--clr-background-2);
    border: none;
    border-top-right-radius: var(--radius);
}

.options button img {
    height: 1rem;
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