<template>
    <a href="/aliment" >Alimentation</a> |
    <a href="/" @click="FINDTHEFUCKINGLOCATION()" >Training</a> 
    <component :is="currentView" />


</template>

<script lang="ts">
import { defineComponent } from 'vue';
    import training from './views/Training.vue'
    import aliment from './views/Aliment.vue'
    import { useAlimentStore } from '../stores/aliment.store'
    import { useTrainingStore } from "../stores/training.store"
    import { mapActions } from 'pinia'

    const routes = {
        '/': training,
        '/aliment': aliment,
    }

   

export default defineComponent({
  name: 'App',
 

    data() {
        return {
            currentPath: window.location.pathname,
        }
    },

    computed: {
        currentView() {
            return routes[this.currentPath] || aliment
        }
    },

    mounted() {
        window.addEventListener('hashchange', () => {
            this.currentPath = window.location.hash
        })
    },


    methods: {
        ...mapActions(useAlimentStore, ["getAllEatingDaysOfOneUser"]),
        ...mapActions(useTrainingStore, ["getAllTrainingOfOneUser"]),

        FINDTHEFUCKINGLOCATION() {
            console.log(window.location.pathname);
        },


    },

  beforeMount() {

      this.getAllEatingDaysOfOneUser(1);
      this.getAllTrainingOfOneUser(1);

    }
});
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
