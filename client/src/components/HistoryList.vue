<template>
    <div v-if="getHistoryList.length > 0" class="container">
        <h1>Recent History</h1>
        <li class="house-item" v-for="house in getHistoryList" :key="house.id">
            <ListItem :data="house" />
        </li>
    </div>
</template>


<!-- SCRIPT -->
<script>
import { mapGetters, mapActions, mapMutations } from "vuex";
import ListItem from '../components/ListItem';

export default {
    name: 'HistoryList',
    components: {
        ListItem,
    },
    props: {
        id: Number,
        price: Number
    },
    computed: {
        ...mapGetters('getListings', ["getHistoryList"]),
    },
    methods: {
        ...mapActions('getListings', ["fetchHouses"]),
        ...mapMutations('getListings', ['setCurrentHouse']),
    },
}
</script>


<!-- STYLES -->
<style scoped>
.container h1 {
    margin-top: 3rem;
    margin-bottom: 1.5rem;
}
li {
    margin-bottom: 1.5rem;
}
</style>