<template>

    <button @click="checkDate()">{{getCurrentDate()}}</button>

    <div class="masterDiv">

        <div class="head">

            <h1>{{getCurrentDate()}}</h1>

        </div>

        <div class="sectionDivs" v-for="exercise in currenTraining.exercises">
            <div class="section">
                <h3>{{exercise.name}}</h3>
                <div class="activityGrid">
                    <span class="topRow">Poids</span>
                    <span class="topRow">Repetition</span>
                    <span class="topRow">Action</span>
                    <template v-for="set in currenTraining.sets">
                        <template v-if="set.exerciseId == exercise.id">

                            <span v-if="modifySetPin != set.id" v-bind:key="set.weight">{{set.weight}}</span>
                            <span v-if="modifySetPin == set.id" v-bind:key="set.weight">
                                <input v-model="modifySetWeight" type="text" />
                            </span>

                            <span v-if="modifySetPin != set.id" v-bind:key="set.repetition">{{set.repetition}}</span>
                            <span v-if="modifySetPin == set.id" v-bind:key="set.repetition">
                                <input v-model="modifySetRepetition" type="text" />
                            </span>
                            <div class="iconDiv" v-if="modifySetPin != set.id">
                                <i @click="modifySet(set)"
                                   class="icon ion-paintbrush"></i>
                                <i @click="deleteSet(set.id)" class="icon ion-ios-trash"></i>
                            </div>
                            <div @click="acceptSetModification(set.id)" class="iconDiv" v-if="modifySetPin == set.id">
                                <i class="icon ion-ios-checkmark"></i>
                            </div>
                        </template>

                    </template>
                </div>
            </div>
            <div class="rowFlexActivity" v-if="toggleAddSet == true && modifySetPin == exercise.id">
                <p>Poids:</p><input v-model="newSetWeight">
                <p>Repetitions:</p><input v-model="newSetReps">
                <p class="checkMarkNewActivity"><i @click="acceptNewSet(exercise.id , currenTraining.id)" class="icon ion-ios-checkmark"></i></p>
                <p class="checkMarkNewActivity"><i @click="cancelNewSet()" class="icon ion-ios-close"></i></p>
            </div>
            <div class="addActivityButtonDiv" v-if="toggleAddSet == false">
                <button class="addActivityButton" @click="addSet(exercise.id)">
                    Ajouter un Set
                </button>

            </div>
        </div>


    </div>
</template>

<script lang="ts">
    import api from "../../api/index.js"
    import { mapState, mapActions } from "pinia";
    import { useTrainingStore } from "../../stores/training.store"

    export default {
        data() {
            return {
                modifySetPin: 1000,
                modifySetWeight: 1000,
                modifySetRepetition: 1000,
                toggleAddSet: false,
                newSetReps: 100000,
                newSetWeight: 10000,



            };

        },
        computed: {

            ...mapState(useTrainingStore, ["currentMonth", "allTrainingOfUser", "currenTraining"]),


        },
        methods: {
            

            modifySet: function (set) {
                this.modifySetPin = set.id;
                this.modifySetWeight = set.weight;
                this.modifySetRepetition = set.repetition;

            },
            acceptSetModification: function (setId) {
                this.modifySetPin = 100000000000;

                let setToModify = {};
                setToModify.Id = setId;
                setToModify.Repetition = this.modifySetRepetition;
                setToModify.Weight = this.modifySetWeight;

                this.modifySetStore(setId, setToModify);


            },
            acceptNewSet: function (exerciseId, trainingId) {

                let newSet = {};
                newSet.repetition = this.newSetReps;
                newSet.weight = this.newSetWeight;
                newSet.trainingId = trainingId
                newSet.exerciseId = exerciseId;

                this.postSetStore(newSet);
                this.toggleAddSet = false;
                this.modifySetPin = 10000;




            },
            cancelNewSet: function () {
                this.toggleAddSet = false;
                this.modifySetPin = 1000;
            },
            addSet: function (id) {
                this.modifySetPin = id;
                this.toggleAddSet = true;
            },

            ...mapActions(useTrainingStore, ["getAllTrainingOfOneUser", "getCurrentTraining", "modifySetStore", "deleteSet",
                "postSetStore", "getCurrentDate"]),

            async beforeMount() {
                await this.getAllTrainingOfOneUser(1);
                this.getCurrentTraining(0);
            },

        },
    };
</script>
<style scoped>
    .eventName {
        text-align: center;
        margin-top: 15px;
    }

    .addSectionButtonDiv {
        color: #eceff1;
        background-color: #263238;
        display: flex;
        justify-content: center;
        margin-top: 20px;
    }

    .addSectionButton {
        width: 100%;
    }

    .addActivityButton {
        color: black;
        background-color: #90a4ae;
        display: flex;
        justify-content: flex-start;
        border-radius: 5px;
        padding: 2px 4px 2px 4px;
    }

    .masterDiv {
        margin-left: 15%;
        margin-right: 15%;
        padding-left: 0.5%;
        padding-right: 0.5%;
    }

    .pastilleDiv {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        margin-top: 20px;
    }

    .pastille {
        background-color: #263238;
        color: #eceff1;
        padding: 10px 10px 10px 10px;
        width: 20%;
        border-radius: 20px;
        box-shadow: 2px 2px #78909c;
    }

    .eventName {
        background-color: #263238;
        color: #eceff1;
        text-shadow: 0.5px 0.5px#ECEFF1;
    }

    .masterDiv {
        background-color: #cfd8dc;
    }

    ul {
        list-style-type: none;
    }

    .sectionDivs {
        margin-top: 20px;
    }

    .sectionTitle {
        text-align: center;
    }

    .activityGrid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
    }

    .activityGridMember {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
    }

    .activityGrid > span {
        padding: 8px 4px;
    }

    .activityGridMember > span {
        padding: 8px 4px;
    }

    .topRow {
        background-color: #546e7a;
        color: #eceff1;
        text-shadow: 0.5px 0.5px #eceff1;
    }

    i {
        font-size: 24px;
        margin-right: 10%;
    }

    .pastille i {
        font-size: 16px;
        margin-right: 5%;
        margin-left: 5%;
    }

    .rowFlex {
        display: flex;
    }

    .rowFlexSection {
        display: flex;
        flex-direction: row;
        justify-content: center;
    }

        .rowFlexSection input {
            height: 100%;
            margin-left: 1%;
            margin-right: 1%;
        }

    .rowFlexActivity {
        display: flex;
        flex-direction: row;
        justify-content: center;
    }

        .rowFlexActivity p {
        }

        .rowFlexActivity input {
            margin-left: 1%;
            margin-right: 1%;
        }

        .rowFlexActivity i {
            align-self: center;
            margin-left: 1%;
            margin-right: 1%;
        }

    .checkMarkNewActivity {
        margin-top: -0.5%;
        margin-right: 1%;
        margin-left: 1%;
    }

    input {
        border: solid;
        border-radius: 3px;
        border-width: 0.1px;
    }
</style>

