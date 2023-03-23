<template>
  <div class="header-container">
    <!-- Title-->
    <h1>Houses</h1>

    <!-- Desktop create new listing button -->
    <router-link class="create-btn-desktop" to="/createListing">
      <img src="@/assets/images/ic_plus_white@3x.png" />
      <p>CREATE NEW</p>
    </router-link>

    <!-- Mobile create new listing button -->
    <router-link class="create-btn-mobile" to="/createListing">
      <img src="@/assets/images/ic_plus_grey@3x.png" />
    </router-link>
  </div>
</template>

<!-- SCRIPT -->
<script>
export default {
  name: "HousesHeader",
};
</script>

<!-- STYLES -->
<style scoped>
/*  MOBILE */
.header-container {
  margin-bottom: 2rem;
  margin-top: 2.5rem;
  position: relative;
  display: flex;
}

.header-container h1 {
  margin: auto;
}

.create-btn-mobile img {
  position: absolute;
  right: 0;
  height: 100%;
}

.create-btn-desktop {
  display: none;
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 550px) {
  .header-container {
    justify-content: space-between;
    margin-top: 4rem;
  }

  .header-container h1 {
    margin: 0;
  }

  .create-btn-mobile {
    display: none;
  }

  .create-btn-desktop {
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 1rem;
    height: 3rem;
    font-weight: var(--font-weight-bold);
    font-family: var(--ff-montserrat);
    background-color: var(--clr-prim);
    border-radius: var(--radius);
    color: var(--clr-text-white);
    transition: var(--transition);
    opacity: 0.9;
  }

  .create-btn-desktop:hover {
    transform: scale(1.03);
    opacity: 1;
    transition: var(--transition);
  }

  .create-btn-desktop img {
    padding-right: 0.6rem;
    height: 100%;
  }
}
</style>
