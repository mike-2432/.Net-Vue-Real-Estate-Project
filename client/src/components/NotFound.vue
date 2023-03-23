<template>
  <div class="container">
    <img src="@/assets/images/img_empty_houses@3x.png" />
    <p>{{ message }}</p>
    <p>{{ messageTwo }}</p>
  </div>
</template>

<script>
export default {
  name: "NotFound",
  props: {
    message: String,
    messageTwo: String,
  },
};
</script>

<style scoped>
.container {
  display: flex;
  align-items: center;
  flex-direction: column;
  padding-top: 10vh;
}
img {
  max-width: 75vw;
  width: 30rem;
  height: auto;
  margin-bottom: 2rem;
}
p {
  color: var(--clr-text-sec);
}
</style>
