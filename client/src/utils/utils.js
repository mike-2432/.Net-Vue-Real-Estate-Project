// UTILS //

/*
  This function adds a value to a key in local storage.
  Every key can hold multiple values and all values in the array are unique
*/
const addToLocalStorage = (key, data) => {
  const localStorageData = [];
  // Creates a new item if no items are found
  if (localStorage.getItem(key) !== null) {
    const currentData = JSON.parse(localStorage.getItem(key));
    localStorageData.push(...currentData);
  }
  // Returns if the value already exists
  if (localStorageData.find((item) => item === data)) return;
  // Adds to local storage
  localStorageData.push(data);
  localStorage.setItem(key, JSON.stringify(localStorageData));
};

/* 
  This fuction removes a single value from an array from a specific key in local storage
*/
const removeFromLocalStorage = (key, removeData) => {
  // Returns if the key does not exist
  if (localStorage.getItem(key) === null) return;
  // Removes the key if it does not hold any values
  if (localStorage.getItem(key) === "[]") {
    localStorage.removeItem(key);
    return;
  }
  // Updates local storage
  const localStorageData = JSON.parse(localStorage.getItem(key));
  for (let item of localStorageData) {
    if (item === removeData) {
      const newLocalStorageData = localStorageData.filter(
        (item) => item !== removeData
      );
      if (newLocalStorageData.length === 0) {
        localStorage.removeItem(key);
        return;
      }
      localStorage.setItem(key, JSON.stringify(newLocalStorageData));
    }
  }
};

/*
  This function removes the first value in an array from a specific key in local storage
*/
const removeFirstFromLocalStorage = (key) => {
  if (localStorage.getItem(key === null)) return;
  const localStorageData = JSON.parse(localStorage.getItem(key));
  localStorage.setItem(key, JSON.stringify(localStorageData.slice(1)));
};

/*
  Export 
*/
const utils = {
  addToLocalStorage,
  removeFromLocalStorage,
  removeFirstFromLocalStorage,
};
export default utils;
