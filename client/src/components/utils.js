// UTILS //

// This function splits a streetname into three seperate values
const splitStreet = (street) => {
    const streetArray = street.split("");
    let streetName = '';
    let houseNumber = '';
    let addition = '';
    let startOfAddition = false;

    for(let [index, char] of streetArray.entries()) {
        // Addition
        if (char !== " " && startOfAddition) {
            addition = addition + char;
        }

        // Streetname
        if ((isNaN(char) || char === " ") && !startOfAddition) {
            streetName = streetName + char;
        } 

        // Housenumber
        if ((!isNaN(char) && char !== " ") && !startOfAddition) {
            houseNumber = houseNumber + char;
            // Remove final space in streetname
            if (streetArray[index - 1] === " ") streetName = streetName.slice(0, index - 1);
            // Start addition
            if (isNaN(streetArray[index + 1])) startOfAddition = true;      
        }
    }
    return { streetName, houseNumber, addition };
}


// This function adds a value to a key in local storage. 
// Every key can hold multiple values and all values in the array are unique
const addToLocalStorage = (key, data) => {
	const localStorageData = [];

    // Creates a new item if no items are found
	if(localStorage.getItem(key) !== null) {
		const currentData = JSON.parse(localStorage.getItem(key));
		localStorageData.push(...currentData);
	}

    // Returns if the value already exists
    if(localStorageData.find(item => item === data)) return;

    // Adds to local storage
	localStorageData.push(data);
	localStorage.setItem(key, JSON.stringify(localStorageData));
}


// This fuction removes a single value from an array from a specific key in local storage
const removeFromLocalStorage = (key, removeData) => {
    // Returns if the key does not exist
	if(localStorage.getItem(key) === null) return;

    // Removes the key if it does not hold any values
	if(localStorage.getItem(key) === "[]") {
		localStorage.removeItem(key);
		return;
	}

	// Updates local storage
	const localStorageData = JSON.parse(localStorage.getItem(key));
	for(let item of localStorageData) {
		if(item === removeData) {
			const newLocalStorageData = localStorageData.filter((item) => item !== removeData);
			if(newLocalStorageData.length === 0) {
				localStorage.removeItem(key);
				return;
			}
			localStorage.setItem(key, JSON.stringify(newLocalStorageData));
		}
	}
}


// This function removes the first value in an array from a specific key in local storage
const removeFirstFromLocalStorage = (key) => {
    if(localStorage.getItem(key === null)) return;
    const localStorageData = JSON.parse(localStorage.getItem(key));
    localStorage.setItem(key, JSON.stringify(localStorageData.slice(1)));

}


// EXPORT FUNCTIONS //
const utils = {
    splitStreet,
    addToLocalStorage,
    removeFromLocalStorage,
    removeFirstFromLocalStorage
};
export default utils;