<!-- CREATE LISTING VIEW -->
<!-- This template contains both the option to edit an existing listing and -->
<!-- create a new listing, which is dependend on the url -->

<template>
    <!-- OUTER CONTAINER -->
    <!-- This container is exclusively used for the background image -->
    <div class="page-outer-container">

        <!-- PAGE CONTAINER -->
        <div class="page-container">

            <!-- HEADER -->
            <div class="header">
                <button class="back-btn"><router-link to="/">
                        <img src="@/assets/images/ic_back_grey@3x.png">
                        <p class="desktop-only">Back to overview</p>
                    </router-link></button>
                <h1 v-if="isNewListing">Create new listing</h1>
                <h1 v-else>Edit listing</h1>
            </div>

            <!-- FORM -->
            <form @submit.prevent="handleSubmit" class="form-wrapper">
                <div v-for="item in formValues" :key="item.id">

                    <!-- Form Item -->
                    <div v-if="!item.isImage" class="form-item-wrapper" :class="item.inputWidth">
                        <label :htmlFor="item.label">{{ item.name }}</label>
                        <input 
                            v-model="requestData[item.key]" 
                            :type="item.type" 
                            :placeholder="item.placeholder"
                            :pattern="item.pattern" 
                            :min="item.min"
                            :max="item.max"
                            :required="item.required" 
                        />
                        <div :class="item.picture"></div>
                    </div>

                    <!-- Form Image -->
                    <div v-if="item.isImage" class="form-item-wrapper upload-img">
                        <label :htmlFor="item.label">{{ item.name }}</label>
                        <div class="upload-img-wrapper">
                            <input style="opacity:0" 
                                type="file" 
                                @change="uploadFile($event)" 
                                :required="isNewListing"
                            />
                            <button class="upload-img-btn">
                                <img src="@/assets/images/ic_upload@3x.png" />
                            </button>
                            <img v-if="previewImage" class="preview-img" :src="previewImage" />
                        </div>
                    </div>

                </div>
                <button class="submit-btn" type="submit">Submit</button>
            </form>

        </div>
    </div>
</template>


<!-- SCRIPT -->
<script>
import { mapState, mapActions } from 'vuex';
import FormValues from '../assets/forms/houseFormValues.js';

export default {
    data() {
        return {
            previewImage: "",
            requestImage: "",
            requestData: {
                streetName: "",
                houseNumber: "",
                Addition: "",
                zip: "",
                city: "",
                price: "",
                size: "",
                hasGarage: false,
                bedrooms: "",
                bathrooms: "",
                constructionYear: "",
                description: ""
            },
            formValues: FormValues,
        }
    },
    computed: {
        ...mapState('getSingleListing', ['isLoaded', 'singleHouse']),
        // Checks the url to see whether a listing needs to be created or edited
        isNewListing() { return this.$route.name === "create listing" }
    },
    methods: {
        ...mapActions('getSingleListing', ["fetchHouse"]),
        ...mapActions('createListing', ['createHouse', 'editHouse', 'uploadImage']),
        uploadFile(e) {
            const image = e.target.files[0];
            this.requestImage = image;
            this.previewImage = URL.createObjectURL(image);
        },
        handleSubmit() {
            if (this.isNewListing) {
                const payload = { form:this.requestData, image:this.requestImage }
                this.createHouse(payload);
            } else {
                this.requestData.id = this.singleHouse.id;
                const payload = { form:this.requestData, image:this.requestImage }
                this.editHouse(payload);
            }
        }
    },
    beforeMount() {
        // Fetches the house if the page is the edit listing page
        if (this.$route.name === 'edit listing') {
            this.fetchHouse(this.$route.params);
        }
    },

    watch: {
        isLoaded() {
            if (this.isLoaded === 'loaded') {
                // Maps the response values to the form data
                this.requestData.id = this.singleHouse.id;                
                this.requestData.streetName = this.singleHouse.streetName;
                this.requestData.houseNumber = this.singleHouse.houseNumber;
                this.requestData.Addition = this.singleHouse.addition;                
                this.requestData.price = this.singleHouse.price;
                this.requestData.zip = this.singleHouse.zip;
                this.requestData.city = this.singleHouse.city;
                this.requestData.size = this.singleHouse.size;
                this.requestData.garage = this.singleHouse.hasGarage;
                this.requestData.constructionYear = this.singleHouse.constructionYear;
                this.requestData.bedrooms = this.singleHouse.bedrooms;
                this.requestData.bathrooms = this.singleHouse.bathrooms;
                this.requestData.description = this.singleHouse.description;
                
                // Image is not yet loading properly in this version
                // old code //
                //
                // this.previewImage = this.singleHouse.image
                //
            }
        }
    }
}
</script>


<!-- STYLES -->
<style scoped>
/* MOBILE */
.desktop-only {
    display: none;
}

.page-outer-container {
    background-image: url("../assets/images/img_background@3x.png");
    background-size: cover;
    background-color: var(--clr-background-1);
    opacity: 0.8;
}

.page-container {
    width: 90vw;
    position: relative;
    margin: auto;
    padding-bottom: 6rem;
}

.header {
    display: flex;
    justify-content: center;
    position: relative;
    margin-bottom: 1.5rem;
    padding-top: 4rem;
    height: 2rem;
    line-height: 2rem;
}

.header button {
    position: absolute;
    left: 0;
    display: flex;
    align-items: center;
    cursor: pointer;
}

.back-btn {
    border: none;
    background: none;
}

.back-btn > * {
    display: flex;
    align-items: center;
}

.back-btn img {
    height: 2rem;
    padding: 0.3rem;
    margin-right: 1rem;
}

.form-wrapper {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    width: 90vw;
    justify-content: space-between;
}

.form-item-wrapper {
    margin-top: 1.6rem;
    display: flex;
    flex-direction: column;
}

.full-width {
    width: 90vw;
}

.half-width {
    width: 43vw;
}

.form-item-wrapper label {
    margin-bottom: 0.7rem;
    font-family: var(--ff-montserrat);
    font-weight: var(--font-weight-semibold);
    color: var(--clr-text-sec);
}

.form-item-wrapper input {
    border: none;
    background-color: var(--clr-background-2);
    height: 3rem;
    padding: 0 1.1rem;
    border-radius: var(--radius);
}

.form-item-wrapper input[type=checkbox] {
    height: 2.5rem;
    cursor: pointer;
}

.upload-img-wrapper input{
    width: 6rem;
    height: 6rem;
    cursor: pointer;
}

.upload-img-btn {
    position: absolute;
    left: 0;
    width: 6rem;
    height: 6rem;
    border: 3px dashed var(--clr-sec);
    opacity: 0.8;
    z-index: -1;
    background: none;
}

.upload-img-btn img {
    height: 30%;    
}

.preview-img {
    position: absolute;
    left: 0;
    width: 6rem;
    height: 6rem;
    z-index: -1;
    object-fit: cover;
}

.submit-btn {
    border: none;
    background-color: var(--clr-prim);
    padding: 1rem 4rem;
    border-radius: var(--radius);
    color: var(--clr-text-white);
    font-size: 16px;
    margin-top: 2rem;
    margin-left: auto;
    cursor: pointer;
    text-transform: uppercase;
}

/* DESKTOP ADJUSTMENTS */
@media screen and (min-width: 550px) {
    .desktop-only {
        display: block;
    }

    .page-container {
        max-width: var(--max-width);
    }

    .header {
        flex-direction: column;
        height: auto;
        justify-content: space-between;
    }

    .header h1 {
        margin-top: 4rem;
    }

    .back-btn {
        position: static;
    }

    .form-wrapper {
        width: 25rem;
    }

    .form-item-wrapper label {
        font-size: 14px;
    }

    .full-width {
        width: 25rem;
    }

    .half-width {
        width: 12rem;
    }
}
</style>