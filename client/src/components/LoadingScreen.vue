<template>
  <div class="loader"></div>
</template>

<!-- SCRIPT -->
<script>
export default {
  name: "LoadingScreen",
};
</script>

<!-- STYLES -->
<style scoped>
.loader {
  content: "";
  display: block;
  width: 7rem;
  height: 7rem;
  margin: auto;
  margin-top: 15%;
  border-radius: 50%;
  border: 0.5rem solid var(--clr-prim);
  border-color: var(--clr-prim) transparent;
  animation: loading 1.2s ease infinite;
}

@keyframes loading {
  0% {
    transform: rotate(0deg);
  }

  100% {
    transform: rotate(720deg);
  }
}
</style>
