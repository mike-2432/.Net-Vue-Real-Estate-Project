<template>
    <div>
        <div class="delete-background"></div>
        <div class="delete-box">
            <h2>Delete Listing</h2>
            <p>Are you sure you want to delete this listing?</p>
            <p>This action cannot be undone</p>
            <button @click="deleteHouse" class="btn red">YES, DELETE</button>
            <button @click="setShowDeleteWarning" class="btn black">GO BACK</button>
        </div>
    </div>
</template>


<!-- SCRIPT -->
<script>
import { mapState, mapMutations, mapActions } from 'vuex';

export default {
    name: 'DeleteWarning',
    computed: {
        ...mapState('deleteListing', ['deleteId'])
    },
    methods: {
        ...mapMutations('deleteListing', ['setShowDeleteWarning']),
        ...mapActions('deleteListing', ['deleteHouse'])
    }
}
</script>


<!-- STYLES -->
<style scoped>
/* MOBILE */
.delete-background {
    position: fixed;
    width: 100vw;
    height: 100vh;
    top: 0;
    background-color: var(--clr-sec);
    opacity: 0.3;
    z-index: 1;
}

.delete-box {
    position: fixed;
    top: 30vh;
    width: 35rem;
    max-width: 90vw;
    left: 50%;
    transform: translate(-50%, 0);
    background-color: var(--clr-background-1);
    border-radius: var(--radius);
    z-index: 1;
    padding: 2rem;
    display: flex;
    flex-direction: column;
    align-items: center;
}

.delete-box h2 {
    margin-bottom: 1rem;
}

.delete-box p {
    font-size: 12px;
    color: var(--clr-text-sec);
}

.delete-box p:nth-child(3) {
    margin-bottom: 1rem;
}

.btn {
    background: none;
    border: none;
    padding: 0.6rem 0;
    width: 70%;
    border-radius: var(--radius);
    margin-top: 1rem;
    font-size: 12px;
    color: var(--clr-text-white);
    cursor: pointer;
}

.red {
    background-color: var(--clr-prim);
}

.black {
    background-color: var(--clr-sec);
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 550px) {
    .delete-box {    
        padding: 3rem 2.5rem 2.5rem 2.5rem;
    
    }
    .btn {
        font-size: 14px;
    }
    .delete-box p {
        font-size: 16px;
        color: var(--clr-text-sec);
    }
}
</style>