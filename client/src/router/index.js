import { createRouter, createWebHistory } from "vue-router";
import externalRoutes from "./externalRoutes";

// Setting up the routes
const routes = [
  {
    path: "/",
    name: "houses",
    component: () => import("../views/HousesView.vue"),
  },
  {
    path: "/house/:id",
    name: "house",
    component: () => import("../views/SingleHouseView.vue"),
  },
  {
    path: "/createListing",
    name: "create listing",
    meta: {
      requiresAuth: true,
    },
    component: () => import("../views/CreateListingView.vue"),
  },
  {
    path: "/editListing/:id",
    name: "edit listing",
    meta: {
      requiresAuth: true,
    },
    component: () => import("../views/CreateListingView.vue"),
  },
  {
    path: "/login",
    name: "login",
    component: () => import("../views/AuthView.vue"),
  },
  {
    path: "/register",
    name: "register",
    component: () => import("../views/AuthView.vue"),
  },
  {
    path: "/about",
    name: "about",
    component: () => import("../views/AboutView.vue"),
  },
  {
    path: "/:catchAll(.*)",
    component: () => import("../components/NotFound.vue"),
    props: { message: "Page not Found" },
  },
];

// Creating the router
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

// Global Guards
router.beforeEach(async (to, _, next) => {
  if (to.matched.some((route) => route.meta.requiresAuth)) {
    const allowed = await loginStatus();
    if (!allowed) next({ name: "login" });
  }
  next();
});

// Function to check if a user is logged in
const loginStatus = async () => {
  try {
    // Gets the jwt from localstorage if present
    const jwt = window.localStorage.getItem("jwt");
    const response = await fetch(
      externalRoutes.apiRoute + "api/auth/loggedIn",
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      }
    );
    if (response.ok) return true;
    window.localStorage.removeItem("jwt");
    return false;
  } catch (err) {
    console.log(err);
  }
};

// Export router
export default router;
