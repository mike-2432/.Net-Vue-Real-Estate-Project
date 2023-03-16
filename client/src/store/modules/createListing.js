import router from '../../router/index.js';
import routes from '../../router/externalRoutes.js';
import utils from '../../components/utils.js';

// CREATE LISTING MODULE //
// this module contains methods for creating a new house and editing an existing house
const createListing = {
    namespaced: true,  

    actions: {
        // Function to create the house and upload the image
        async createHouse(_, requestData) {
            const { form, image } = requestData;
            try {      
                // Gets the jwt from localstorage if present
                const jwt = window.localStorage.getItem('jwt');

                // Post request for the form data  
                // =========================== //     
                const response = await fetch(routes.apiRoute + "api/houses", {
                    method: "POST",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer '+ jwt,
                    },
                    body: JSON.stringify(form),
                });
                const data = await response.json();   
                utils.addToLocalStorage('myListings', data.id);

                // Post request for the image
                // ======================= //  
                let formImage = new FormData();
                // Set the image name equal to the id of the house
                formImage.append('file', image, data.id);
                const imageResponse = await fetch(routes.apiRoute + "api/storage", {
                    method: "POST",
                    headers: {
                        'Authorization': 'Bearer '+ jwt,
                    },
                    body: formImage
                });

                // Redirects to the new house page
                if(response.ok && imageResponse.ok) router.push({ path: 'house/'+ data.id });
            } catch(err) {
                console.log(err)
            }
        },

        // Function to edit a house and upload a new image image
        async editHouse(_, requestData) {
            const { form, image } = requestData;
            try {    
                // Gets the jwt from localstorage if present
                const jwt = window.localStorage.getItem('jwt');

                // Post request for the form data  
                // =========================== //             
                const response = await fetch(routes.apiRoute + "api/houses", {
                    method: "PUT",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': 'Bearer '+ jwt,
                    },
                    body: JSON.stringify(form),
                });
                
                // Return when no new image is present
                if(!image) {
                    if(response.ok) router.push({ path: '/house/' + form.id});
                    return;
                }

                // Post request for the image
                // ======================= //
                let formImage = new FormData();
                // Set the image name equal to the id of the house
                formImage.append('file', image, String(form.id));
                const imageResponse = await fetch(routes.apiRoute + "api/storage", {
                    method: "PUT",
                    headers: {
                        'Authorization': 'Bearer '+ jwt,
                    },
                    body: formImage
                });

                // Redirects to the edited house page
                if(response.ok && imageResponse.ok) router.push({ path: '/house/' + form.id});
            } catch(err) {
                console.log(err)
            }
        }
    }
}

export default createListing;