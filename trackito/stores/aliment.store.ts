import { defineStore } from "pinia";
import api from "../api/index.js"

export const useAlimentStore = defineStore("aliment", {
    state: () => {
        return {

            allEatingDaysOfUser: [],
            allAliments: [],
            currentMonth: { id: 1, name: "January" },
            currenEatingDay: {},
            currentEatingDayTotals: [],
            currentUserId: 0,
            currentUser: {},

       

        }
    },
    actions: {

        selectTrainingFromDate(date) {

            let trainingId = this.getIdFromDate(date)

            if (Number.isInteger(trainingId)) {
            } else {

                let newAday = {};
                newAday.date = date
                newAday.idUser = this.currentUserId

                this.postAday(newAday);
                this.getAllEatingDaysOfOneUser(1);

                console.log("newAday.date", newAday.date)

               trainingId = this.getIdFromDate(newAday.date)
                console.log("trainingId", trainingId)

            }

            this.changeCurrentEatingDay(trainingId);


        },

        getIdFromDate(date) {
            
            let dateOfIncomingData = date.slice(0, 10);
            let response = "default, cant find"

            for (var i = 0; i < this.allEatingDaysOfUser.length; i++) {
                let dateOfDataToFind = this.allEatingDaysOfUser[i].date.slice(0, 10);
           

                if (dateOfIncomingData == dateOfDataToFind) {
                    response = this.allEatingDaysOfUser[i].id
                }
            }
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

            result = result.getFullYear() + "-" + (zeroMonth + String(result.getMonth() + 1)) + "-" + zeroDay+ result.getDate()
           
            console.log("RESULT", result)

            return result;
           

        },


        getCurrentDate() {
            let date = this.currenEatingDay.date
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


        async getAllEatingDaysOfOneUser(userId) {

            await api.get('api/Adays/').then(res => {
                console.log("api aday get all call", res)
                this.allEatingDaysOfUser = res.data
                this.getUser(1)
                this.getAllAliment();
                this.currentUserId = userId;
            })


        },

        async getAllAliment() {
            await api.get("api/Aliments").then(res => {
                this.allAliments = res.data
            })

        },

        async modifyAlimentStore(aliment, id) {
            await api.put(`api/Aliments/${id}`, aliment).then(res => {
                console.log(res)
            })
        },


        async modifyAlimentDayStore(alimentDayId, alimentDay) {

            await api.put(`api/AlimentDays/${alimentDayId}`, alimentDay).then(res => {
                console.log(res)
            })

            for (var x = 0; x < this.currenEatingDay.alimentDays; x++) {
                if (this.currenEatingDay.alimentDays[x].id == alimentDayId) {
                    this.currenEatingDay.alimentDays[x].quantity = alimentDay.quantity
                }
            }
        },


        async deleteAlimentDayStore(alimentDayId) {
            await api.delete(`api/AlimentDays/${alimentDayId}`).then(res => {
                console.log(res)
            })
            this.currenEatingDay.alimentDays = this.currenEatingDay.alimentDays.filter(alimentDay => alimentDay.id != alimentDayId);
        },

        async postAlimentDayStore(alimentDay) {
            await api.post("api/AlimentDays/", alimentDay).then(res => { console.log(res) })


            this.getAllEatingDaysOfOneUser(1);
        },
        async addAlimentStore(aliment) {
            await api.post("api/Aliments/", aliment).then(res => { console.log(res) })
        },

        async postAday(aday) {
            await api.post("api/Adays", aday).then(res => {
                console.log(res)
            })

        },

        async getUser(int) {
            await api.get(`api/Users/${int}`).then(res => {
                
                this.currentUser = res.data
                for (let x = 0; x < this.allEatingDaysOfUser.length; x++) {
                    if (this.allEatingDaysOfUser[x].id == this.currentUser.lastViewedEatingId) {
                        this.currenEatingDay = this.allEatingDaysOfUser[x]
                    }
                }
                console.log("current eating day get user function", this.currenEatingDay)
            })

            
        },

        async changeCurrentEatingDay(newId) {
            

            let user = this.currentUser;
            user.lastViewedEatingId = newId;
            console.log("userId:", user.id)
            console.log("user", user)


            await api.put(`api/Users/${user.id}`, user).then(res => {
                console.log(res)
            })

            this.getUser(1);

        },


        
        getNutrition(alimentDay, fact) {
            let aliment = this.currenEatingDay.aliments.find(aliment => aliment.id == alimentDay.alimentId)

            let multiplier = alimentDay.quantity / aliment.quantity
            let factToReturn;
            const factToGet = fact;
            switch (factToGet) {
                case 'protein':
                    factToReturn = (multiplier * aliment.protein)

                    break;
                case 'carbs':
                    factToReturn = (multiplier * aliment.carbs)
                    break; 
                case 'fat':
                    factToReturn = (multiplier * aliment.fat)
                    break;
                case 'calories':
                    factToReturn = (multiplier * aliment.calories)
                    break;
                default:
                    console.log(factToGet);

            }

            factToReturn = Math.round(factToReturn)

            return factToReturn
        },

        getTotals(fact) {

            let totalToReturn = 0;
            const factToGet = fact;
            switch (factToGet) {
                case 'protein':
                    for (var i = 0; i < this.currenEatingDay.alimentDays.length; i++) {
                        totalToReturn = totalToReturn + this.getNutrition(this.currenEatingDay.alimentDays[i], 'protein')
                        console.log("get in the protein loop")
                    }

                    break;
                case 'quantity':

                    for (var i = 0; i < this.currenEatingDay.alimentDays.length; i++) {
                        totalToReturn = parseInt(totalToReturn) + parseInt(this.currenEatingDay.alimentDays[i].quantity)
                    }

                    break;
                case 'carbs':
                    for (var i = 0; i < this.currenEatingDay.alimentDays.length; i++) {
                        totalToReturn = totalToReturn + this.getNutrition(this.currenEatingDay.alimentDays[i], 'carbs')
                    }
                    break;
                case 'fat':
                    for (var i = 0; i < this.currenEatingDay.alimentDays.length; i++) {
                        totalToReturn = totalToReturn + this.getNutrition(this.currenEatingDay.alimentDays[i], 'fat')
                    }
                    break;
                case 'calories':
                    for (var i = 0; i < this.currenEatingDay.alimentDays.length; i++) {
                        totalToReturn = totalToReturn + this.getNutrition(this.currenEatingDay.alimentDays[i], 'calories')
                    }
                    break;
                default:
                    console.log(factToGet);

            }

            totalToReturn = Math.round(totalToReturn)

            return totalToReturn

        },
        
      
       
            
    }

})
    
});