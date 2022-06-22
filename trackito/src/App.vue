<template>
  
  <aliment/>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
    import HelloWorld from './components/HelloWorld.vue';
    import training from './views/Training.vue'
    import aliment from './views/Aliment.vue'
    import { useAlimentStore } from '../stores/aliment.store'
    import { useTrainingStore } from "../stores/training.store"
    import { mapActions } from 'pinia'

export default defineComponent({
  name: 'App',
  components: {
      aliment,
      training
  },

    methods: {
        ...mapActions(useAlimentStore, ["getAllEatingDaysOfOneUser"]),
        ...mapActions(useTrainingStore, ["getAllTrainingOfOneUser"]),


    }

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
