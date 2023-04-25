<!-- NAVBAR -->
<template>
  <div class="nav-container">
    <div class="nav-center">
      <!-- DESKTOP -->
      <img class="logo" src="@/assets/images/img_logo_dtt@3x.png" />
      <nav class="desktop-links">
        <ul>
          <li
            @click="routeToHousesPage"
            :class="[isAboutPage || isUserPage ? 'inactive' : 'active']"
          >
            Houses
          </li>
          <li
            @click="routeToAboutPage"
            :class="[isAboutPage ? 'active' : 'inactive']"
          >
            About
          </li>
          <li
            @click="routeToLoginPage"
            v-if="!isLoggedIn"
            class="align-right"
            :class="[isUserPage ? 'active' : 'inactive']"
          >
            Login
          </li>
          <li
            v-if="isLoggedIn"
            @click="handleLogout"
            class="inactive align-right"
          >
            Logout
          </li>
        </ul>
      </nav>

      <!-- MOBILE -->
      <nav class="mobile-links">
        <ul>
          <img
            @click="routeToHousesPage"
            class="icon"
            :src="
              $data[isAboutPage || isUserPage ? 'homeIcon' : 'homeIconActive']
            "
          />
          <img
            @click="routeToAboutPage"
            class="icon"
            :src="$data[isAboutPage ? 'infoIconActive' : 'infoIcon']"
          />
          <img
            @click="routeToLoginPage"
            v-if="!isLoggedIn"
            class="icon"
            :src="$data[isUserPage ? 'userIconActive' : 'userIcon']"
          />
          <img
            v-if="isLoggedIn"
            @click="handleLogout"
            class="icon"
            :src="$data['userExitIcon']"
          />
        </ul>
      </nav>
    </div>
  </div>
</template>

<!-- SCRIPT -->
<script>
import { mapActions } from "vuex";
import router from "../router/index.js";

export default {
  name: "NavbarComponent",
  data() {
    return {
      homeIcon: require("@/assets/images/ic_mobile_navigarion_home@3x.png"),
      homeIconActive: require("@/assets/images/ic_mobile_navigarion_home_active@3x.png"),
      infoIcon: require("@/assets/images/ic_mobile_navigarion_info@3x.png"),
      infoIconActive: require("@/assets/images/ic_mobile_navigarion_info_active@3x.png"),

      userIcon: require("@/assets/images/ic_user.svg"),
      userIconActive: require("@/assets/images/ic_user_active.svg"),
      userExitIcon: require("@/assets/images/ic_user_exit.svg"),
      userExitIconActive: require("@/assets/images/ic_user_exit_active.svg"),
    };
  },
  computed: {
    isAboutPage() {
      return this.$route.name == "about";
    },
    isUserPage() {
      if (this.$route.name == "login" || this.$route.name == "register")
        return true;
      return false;
    },
    isLoggedIn() {
      if (window.localStorage.getItem("jwt") !== null) return true;
      return false;
    },
  },
  methods: {
    ...mapActions("auth", ["checkLoginStatus"]),
    handleLogout() {
      window.localStorage.removeItem("jwt");
      router.go(0);
    },
    routeToHousesPage() {
      router.push({ name: "houses" });
    },
    routeToAboutPage() {
      router.push({ name: "about" });
    },
    routeToLoginPage() {
      router.push({ name: "login" });
    },
  },
  beforeMount() {
    // Checks the login status
    // Deletes the jwt from local storage if the jwt is not valid anymore
    this.checkLoginStatus();
  },
};
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
  cursor: pointer;
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
    position: relative;
  }

  .logo {
    display: block;
    height: 60%;
    margin: auto 0;
  }

  .desktop-links {
    display: block;
  }

  .desktop-links ul {
    display: flex;
  }

  .align-right {
    position: absolute;
    right: 0;
  }

  .desktop-links ul li {
    padding: 0 0 0 3.5rem;
    font-family: var(--ff-montserrat);
    cursor: pointer;
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
