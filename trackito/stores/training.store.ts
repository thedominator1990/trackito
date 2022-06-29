import { defineStore } from "pinia";
import api from "../api/index.js"

export const useTrainingStore = defineStore("training", {
    state: () => {
        return {

            allTrainingOfUser: [],
            currentMonth: { id: 1, name: "January" },
            currenTraining: {},
            modifySetPin: 1000,
            modifySetWeight: 1000,
            modifySetRepetition: 1000,
            trainingOfCurrentMonth: [],
            allTrainings: [],
            allExercise: [],
            currentUser: {},

        }
    },
    actions: {
        selectTrainingFromDate(date) {

            let trainingId = this.getIdFromDate(date)

            if (Number.isInteger(trainingId)) {
            } else {

                let newTraining = {};
                newTraining.dateTraining = date
                newTraining.idUser = this.currentUser.id

                this.postTraining(newTraining);
                this.getAllTrainingOfOneUser(1);

                console.log("newTraining.date", newTraining.date)

                trainingId = this.getIdFromDate(newTraining.date)
                console.log("newTraining", trainingId)

            }

            this.changeCurrentTrainingDay(trainingId);


        },

        getIdFromDate(date) {
            console.log("DATE", date)
            console.log("ALL TRAINING OF USER", this.allTrainingOfUser)
            let dateOfIncomingData = date.slice(0, 10);
            let response = "default, cant find"

            for (var i = 0; i < this.allTrainingOfUser.length; i++) {
                let dateOfDataToFind = this.allTrainingOfUser[i].dateTraining.slice(0, 10);


                if (dateOfIncomingData == dateOfDataToFind) {
                    response = this.allTrainingOfUser[i].id
                }
            }
            console.log("REPONSE", response)
            return response;
        },

        addDays(date, days) {
            let result = new Date(date);
            result.setDate(result.getDate() + days);


            let zeroDay = ""
            let zeroMonth = "";

            if (result.getMonth() + 1 < 10) {
                zeroMonth = "0"
            }
            if (result.getDate() < 10) {
                zeroDay = "0"
            }

            result = result.getFullYear() + "-" + (zeroMonth + String(result.getMonth() + 1)) + "-" + zeroDay + result.getDate()

            console.log("RESULT", result)

            return result;


        },
        async getUser(int) {
            await api.get(`api/Users/${int}`).then(res => {

                this.currentUser = res.data
                console.log("CURRENT USER", this.currentUser)
                for (let x = 0; x < this.allTrainingOfUser.length; x++) {
                    if (this.allTrainingOfUser[x].id == this.currentUser.lastViewedTrainingId) {
                        this.currenTraining = this.allTrainingOfUser[x]
                    }
                }
                console.log("current training day get user function", this.currenTraining)
            })


        },

        async changeCurrentTrainingDay(newId) {

            //console.log("CURRENT USER" this.currentUser)

            let user = this.currentUser;
            user.lastViewedTrainingId = newId;
            console.log("userId:", user.id)
            console.log("user", user)


            await api.put(`api/Users/${user.id}`, user).then(res => {
                console.log(res)
            })

            this.getUser(1);

        },

        async postTraining(training) {

            await api.post("api/trainings", training).then(res => {
                console.log(res)
            })

        },

        getCurrentDate() {
            let date = this.currenTraining.dateTraining

            let year = date.slice(0, 4);

            let month = parseInt(date.slice(6, 7));
           
            let day = date.slice(8, 10);
            let monthToString
            switch (month) {
                case 1: monthToString = "Janvier";
                    break;
                case 2: monthToString = "Février";
                    break;
                case 3: monthToString = "Mars";
                    break;
                case 4: monthToString = "Avril";
                    break;
                case 5: monthToString = "Mai";
                    break;
                case 6: monthToString = "Juin";
                    break;
                case 7: monthToString = "Juillet";
                    break;
                case 8: monthToString = "Août";
                    break;
                case 9: monthToString = "Septembre";
                    break;
                case 10: monthToString = "Octobre";
                    break;
                case 11: monthToString = "Novembre";
                    break;
                case 12: monthToString = "Décembre";
                    break;
                default: console.log("wtf");
                    break;
            }

            let returnString = day + " " + monthToString + " " + year


            return returnString;
        },
        
     

         async getAllTrainingOfOneUser(userId) {

            await api.get('api/trainings/').then(res => {
                
                this.allTrainingOfUser = res.data
                this.getUser(1)
                this.getAllExercises();
                console.log(this.allTrainingOfUser);
                console.log("current training", this.currenTraining)
            })


        },
        async getAllExercises() {
            api.get("api/Exercises").then(res => {
                this.allExercise = res.data
            })
        },

        async modifySetStore(setId, set) {
           
            await api.put(`api/Sets/${setId}`, set).then(res => {
                console.log(res)
            })
            for (var x = 0; x < this.currenTraining.sets.length; x++) {
                if (this.currenTraining.sets[x].id == setId) {
                    this.currenTraining.sets[x].repetition = set.Repetition
                    this.currenTraining.sets[x].weight = set.Weight
                }
            }
        },

        async deleteSet(setId) {
            await api.delete(`api/Sets/${setId}`).then(res => {
                console.log(res)
            })
            this.currenTraining.sets = this.currenTraining.sets.filter(set => set.id != setId);
        },

        async postSetStore(set) {
            await api.post("api/Sets/", set).then(res => { console.log(res) })

            this.currenTraining.sets.push(set);
        },

        async postExercise(exercise) {
            await api.post("api/Exercises", exercise).then(res => {
                console.log(res)
            })
            this.getAllExercises();
        },

        getCurrentTraining(int) {
            this.currenTraining = this.allTrainingOfUser[int];
        },
            
            
        }

        },
    
});
