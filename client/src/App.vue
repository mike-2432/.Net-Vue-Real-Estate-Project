<template>
  <div>
    <!-- The forceRenderKey is used to refresh the component after the user logs in -->
    <Navbar :key="{forceRenderKey}"/>
    <DeleteWarning v-if="showDeleteWarning" />
    <router-view></router-view>
  </div>
</template>


<!-- SCRIPT -->
<script>
import { mapState } from 'vuex'
import Navbar from './components/Navbar.vue';
import DeleteWarning from './components/DeleteWarning.vue';

export default {
  name: 'App',
  components: {
    Navbar,
    DeleteWarning,
  },
  computed: {
    ...mapState('deleteListing', ['showDeleteWarning']),
    ...mapState('auth', ['forceRenderKey'])
  },
}
</script>


<!-- STYLES -->
<style>
  @import './styles/styleVariables.css';
  @import './styles/globalStyles.css';
</style>

