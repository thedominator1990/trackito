<template>

    <button @click="getAllTrainingOfOneUser(1)">{{currentMonth.name}}</button>
           
</template>

<script lang="ts">
    import api from "../../api/index.js"
    import { mapState, mapActions} from "pinia";
    import { useTrainingStore } from "../../stores/training.store"

    export default {
        data() {
            return {
                allTrainings: [],
            };

        },
        computed: {

            ...mapState(useTrainingStore, ["currentMonth"])

        },
        methods: {
            ...mapActions(useTrainingStore, ["getAllTrainingOfOneUser"]),
            getQueryTraining() {
                
                api.get('/api/trainings/1').then(res => {
                
                    console.log(res.data)
                })
                
            }
        },
    };
</script>