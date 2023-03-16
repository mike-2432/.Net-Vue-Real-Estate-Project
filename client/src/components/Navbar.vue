<!-- NAVBAR -->
<template>
    <div class="nav-container">
        <div class="nav-center">

            <!-- DESKTOP -->
            <img class="logo" src="@/assets/images/img_logo_dtt@3x.png">
            <nav class="desktop-links">
                <ul>
                    <router-link to="/">
                        <li :class="[isAboutPage ? 'inactive' : 'active']">Houses</li>
                    </router-link>
                    <router-link to="/about">
                        <li :class="[isAboutPage ? 'active' : 'inactive']">About</li>
                    </router-link>
                </ul>
            </nav>

            <!-- MOBILE -->
            <nav class="mobile-links">
                <ul>
                    <router-link to="/">
                        <img class="icon" :src="$data[isAboutPage ? 'homeIcon' : 'homeIconActive']">
                    </router-link>
                    <router-link to="/about">
                        <img class="icon" :src="$data[isAboutPage ? 'infoIconActive' : 'infoIcon']">
                    </router-link>
                </ul>
            </nav>

        </div>
    </div>
</template>


<!-- SCRIPT -->
<script>
export default {
    name: 'NavbarComponent',
    data() {
        return {
            homeIcon: require("@/assets/images/ic_mobile_navigarion_home@3x.png"),
            homeIconActive: require("@/assets/images/ic_mobile_navigarion_home_active@3x.png"),
            infoIcon: require("@/assets/images/ic_mobile_navigarion_info@3x.png"),
            infoIconActive: require("@/assets/images/ic_mobile_navigarion_info_active@3x.png"),
        }
    },
    computed: {
        isAboutPage() {
            return this.$route.name == "about";
        }
    },
}
</script>


<!-- STYLES -->
<style scoped>
.nav-container {
    background: var(--clr-background-2);
    height: var(--nav-height);
    line-height: var(--nav-height);
    width: 100%;
    box-shadow: var(--shadow);
    position: fixed;
    bottom: 0;
    z-index: 1;
}

.nav-center {
    height: inherit;
    width: var(--width);
    margin: auto;
    display: flex;
}

.mobile-links ul {
    width: var(--width);
    display: flex;
    justify-content: space-around;
}

.icon {
    padding: 1rem;
    height: var(--nav-height);
    display: flex;
}

.logo {
    display: none;
}

.desktop-links {
    display: none;
}

/* Desktop Adjustments */
@media screen and (min-width: 550px) {
    .mobile-links {
        display: none;
    }

    .nav-container {
        position: static;
    }

    .nav-center {
        max-width: var(--max-width);
    }

    .logo {
        display: block;
        height: 60%;
        margin: auto 0;
        padding-right: 2rem;
    }

    .desktop-links {
        display: block;
    }

    .desktop-links ul {
        display: flex;
    }

    .desktop-links ul li {
        padding: 0 2rem;
        font-family: var(--ff-montserrat);
    }

    .active {
        font-weight: var(--font-weight-bold);
    }

    .inactive {
        color: var(--clr-text-sec);
        opacity: 0.9;
        font-weight: var(--font-weight-medium);
    }
}
</style>