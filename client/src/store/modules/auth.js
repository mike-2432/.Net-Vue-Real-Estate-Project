import routes from '../../router/externalRoutes.js';
import router from '../../router/index.js';

// AUTH MODULE //
const auth = {
    namespaced: true,

    state: () => ({
        jwt: "",
    }),

    mutations: {
        setJwt(state, jwt) {
            window.localStorage.setItem('jwt', jwt);
            state.jwt = jwt;
        },
    },

    actions: {
        // Function for logging in
        async login(context, requestData) {
            try {
                const response = await fetch(routes.apiRoute + "api/auth/login", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(requestData)
                });              
                const jsonResponse = await response.json();
                const jwt = jsonResponse.data;
                if(response.ok) {
                    context.commit("setJwt", jwt);
                    router.push({ name: 'houses'});
                }
            } catch(err) {
                console.log(err);
            }
        },
    },
}

export default auth;