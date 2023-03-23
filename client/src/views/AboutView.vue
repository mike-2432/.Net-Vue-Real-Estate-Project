<template>
  <div class="page-container">
    <h1 class="margin-bottom-big">About DTT Real Estate</h1>
    <p class="margin-bottom-small">
      Lorem ipsum dolor sit amet consectetur adipisicing elit. Harum molestias,
      quasi asperiores eius, voluptatem sed veritatis est reprehenderit ipsa
      expedita perferendis perspiciatis pariatur in iste sit? Alias eveniet amet
      est! Lorem ipsum dolor sit amet consectetur adipisicing elit. Quas neque
      blanditiis eum beatae? Reiciendis nobis libero aliquid, officia asperiores
      beatae porro. Vitae alias perspiciatis distinctio dicta unde, expedita
      quibusdam omnis.
    </p>
    <p class="margin-bottom-big">
      Lorem ipsum dolor sit amet consectetur adipisicing elit. Nostrum,
      explicabo ab error corrupti quos qui mollitia voluptatibus placeat cumque
      ipsa similique laudantium omnis est facere asperiores totam! Voluptatum,
      iure enim!
    </p>

    <h1 class="margin-bottom-small">Design and Development</h1>
    <div class="horizontal-wrapper">
      <img src="@/assets/images/img_logo_dtt@3x.png" />
      <div class="margin-left-small vertical-center">
        <p>By DTT</p>
        <a href="https://www.d-tt.nl/">www.d-tt.nl</a>
      </div>
    </div>
  </div>
</template>

<style scoped>
.page-container {
  width: var(--width);
  max-width: var(--max-width);
  margin: auto;
  margin-top: 4rem;
}

.margin-bottom-small {
  margin-bottom: 1rem;
}

.margin-bottom-big {
  margin-bottom: 2rem;
}

.margin-left-small {
  margin-left: 1rem;
}

.vertical-center {
  margin-top: auto;
  margin-bottom: auto;
}

.horizontal-wrapper {
  display: flex;
}

img {
  height: 3rem;
}

p {
  color: var(--clr-text-sec);
}

a {
  color: var(--clr-hyperlink);
}

@media screen and (min-width: 550px) {
  img {
    height: 5rem;
  }
}
</style>
