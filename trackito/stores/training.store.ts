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

        }
    },
    actions: {

        getCurrentDate() {
            let date = this.currenTraining.dateTraining

            let year = date.slice(0, 4);

            let month = parseInt(date.slice(6, 7));
           
            let day = date.slice(9, 10);
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
                this.getCurrentTraining(0);
                console.log(this.allTrainingOfUser);
                console.log("current training", this.currenTraining)
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

        getCurrentTraining(int) {
            this.currenTraining = this.allTrainingOfUser[int];
        },
            
            
        }

        },
    
});
