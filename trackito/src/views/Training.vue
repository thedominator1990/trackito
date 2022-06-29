<template>

    <div class="masterDiv">

        
            <div class="eventName">
                <i class="icon ion-ios-arrow-left" style="margin-top:25px; margin-right:30px;" @click="moveDown()"></i>
                <h1>{{ getCurrentDate() }}</h1>
                <i class="icon ion-ios-arrow-right" style="margin-top:25px; margin-left:30px;" @click="moveUp()"></i>
            </div>


        <div class="sectionDivs" v-for="exercise in currenTraining.exercises">
            <div class="section">
                <div class="exerciseName">
                    <div>
                        <h3>{{exercise.name}}</h3>
                    </div>

                    <div class="problemIcon">
                        <i @click="deleteExercise(exercise.id)" class="icon ion-ios-trash"></i>
                    </div>

                </div>

                <div class="activityGrid">
                    <span class="topRow">Poids</span>
                    <span class="topRow">Repetition</span>
                    <span class="topRow">Action</span>
                    <template v-for="set in currenTraining.sets">
                        <template v-if="set.exerciseId == exercise.id">

                            <span v-if="modifySetPin != set.id" v-bind:key="set.weight">{{set.weight}}</span>
                            <span v-if="modifySetPin == set.id" v-bind:key="set.weight">
                                <input  v-model="modifySetWeight" type="text" />
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
                <p>Poids:</p><input class="problemInput" v-model="newSetWeight">
                <p>Repetitions:</p><input class="problemInput" v-model="newSetReps">
                                   <div class="buttonDiv">
                                       <p class="checkMarkNewActivity"><i @click="acceptNewSet(exercise.id , currenTraining.id)" class="icon ion-ios-checkmark"></i></p>
                                       <p class="checkMarkNewActivity"><i @click="cancelNewSet()" class="icon ion-ios-close"></i></p>
                                   </div>
            </div>
            <div class="addActivityButtonDiv" v-if="toggleAddSet == false">
                <button class="addActivityButton" @click="addSet(exercise.id)">
                    Ajouter un Set
                </button>

            </div>



        </div>
        <div class="addExercise">
            <button class="addActivityButton" @click="toggleAddExistingExercise()"> Ajouter un exercise Existant</button>
            <div class="trainingSelector" v-if="addExistingExerciseToggle">
                <div class="buttonDiv">
                    <label for="exercise"> Exercises: &nbsp</label>
                    <select id="exercise" @change="selectExistingExercise($event)" v-model="key" class="exerciseDropDown">
                        <option v-for="exercise in allExercise" :value="exercise.id">{{exercise.name}}</option>
                    </select>
                </div>


                <div class="buttonDiv">
                    <p class="checkMarkNewActivity"><i @click="acceptNewExercise()" class="icon ion-ios-checkmark"></i></p>
                    <p class="checkMarkNewActivity"><i @click="cancelNewExercise()" class="icon ion-ios-close"></i></p>
                </div>

            </div>

            <button class="addActivityButton" @click="toggleAddNewExercise()"> Ajouter un nouvel exercise</button>
            <div class="addNewExercise" v-if="addNewExerciseToggle">
                <div>
                    <div class="buttonDiv">
                        <p>Nom : </p>
                        <input class="problemInput" type="text" v-model="newExerciseName" />
                    </div>
                    <div class="buttonDiv">
                        <p>Muscle : </p>
                        <input class="problemInput" type="text" v-model="newExerciseMuscle" />
                    </div>

                </div>
                <div class="buttonDiv">
                    <p class="checkMarkNewActivity"><i @click="acceptAddNewExercise()" class="icon ion-ios-checkmark"></i></p>
                    <p class="checkMarkNewActivity"><i @click="cancelAddNewExercise()" class="icon ion-ios-close"></i></p>
                    </div>
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
                newSetReps: 0,
                newSetWeight: 0,

                addExistingExerciseToggle: false,
                currentExerciseSelectedId: 1000,

                addNewExerciseToggle: false,
                newExerciseName: "",
                newExerciseMuscle:"",



            };

        },
        computed: {

            ...mapState(useTrainingStore, ["currentMonth", "allTrainingOfUser", "currenTraining","allExercise"]),


        },
        methods: {
            moveUp() {
                let nextDay = this.addDays(this.currenTraining.dateTraining, 1);
                this.selectTrainingFromDate(nextDay);

            },
            moveDown() {
                let nextDay = this.addDays(this.currenTraining.dateTraining, -1);
                this.selectTrainingFromDate(nextDay);

            },



            toggleAddNewExercise() {
                this.addNewExerciseToggle = true;
            },
            cancelAddNewExercise() {
                this.addNewExerciseToggle = false;
            },
            acceptAddNewExercise() {
                let newExercise = {};
                newExercise.primaryMuscle = this.newExerciseMuscle;
                newExercise.name = this.newExerciseName;

                this.postExercise(newExercise)
                this.addNewExerciseToggle = false;

            },
            selectExistingExercise(event) {

                this.currentExerciseSelectedId = event.target.value;

            },
            acceptNewExercise() {
                for (var x = 0; x < this.allExercise.length; x++) {
                    console.log("current Id", this.currentExerciseSelectedId)
                    if (this.allExercise[x].id == this.currentExerciseSelectedId) {
                        this.currenTraining.exercises.push(this.allExercise[x])
                    }
                }
            },
            toggleAddExistingExercise() {
                this.addExistingExerciseToggle = true
            },
            cancelNewExercise() {
                this.addExistingExerciseToggle = false
            },

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
            deleteExercise(exerciseId){

                for (var x = 0; x < this.currenTraining.sets.length; x++) {
                    if (this.currenTraining.sets[x].exerciseId == exerciseId) {
                        this.deleteSet(this.currenTraining.sets[x].id)
                    }
                }

                for (var y = 0; y < this.currenTraining.exercises.length; y++) {

                    if (this.currenTraining.exercises[y] == exerciseId) {
                        this.currenTraining.exercises.splice(y, 1)
                        console.log(this.currenTraining.exercises)
                    }

                }

             },

            ...mapActions(useTrainingStore, ["getAllTrainingOfOneUser", "getCurrentTraining", "modifySetStore", "deleteSet",
                "postSetStore", "getCurrentDate", "postExercise", "selectTrainingFromDate", "addDays"]),

            async beforeMount() {
                await this.getAllTrainingOfOneUser(1);
                this.getCurrentTraining(0);
            },

        },
    };
</script>
<style scoped>
    .problemInput {
        align-self: flex-start;
        margin-top: 15px;
        height: 18px;
    }
    .addExercise{
        margin-top: 15px;
    }
    .buttonDiv {
        margin-top:15px;
        display: flex;
        flex-direction: row;
    }
    .exerciseDropDown {
        width: 100px;
        margin-right: 10px;
    }
    .trainingSelector {
        margin-top: 10px;
        display: flex;
        flex-direction: column;
    }
    .problemIcon{
        margin-top: 15px;
        margin-left: 10px;
    }
    .exerciseName {
        display: flex;
        flex-direction: row;
    }
    .eventName {
        display: flex;
        flex-direction: row;
        justify-content: center;
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
        margin-top: 10px;
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
        margin-left: 2%;
    }

    input {
        border: solid;
        border-radius: 3px;
        border-width: 0.1px;
    }
</style>

