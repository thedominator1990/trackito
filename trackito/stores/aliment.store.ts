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

       

        }
    },
    actions: {
        async getAllAliment() {
            await api.get("api/Aliments").then(res => {
                this.allAliments = res.data
                console.log(this.allAliments)
            })

        },

        getIdFromDate(date) {

            let dateOfIncomingData = date.slice(0, 10);
            console.log("incoming date",dateOfIncomingData)
            let response = "default, cant find"

            for (var i = 0; i < this.allEatingDaysOfUser.length; i++) {
                let dateOfDataToFind = this.allEatingDaysOfUser[i].date.slice(0, 10);
                console.log("date to find", dateOfDataToFind)

                if (dateOfIncomingData == dateOfDataToFind) {
                    response = this.allEatingDaysOfUser[i].id
                }
            }
            console.log("response :", response)
            return response;
        },

        addDays(date, days) {
            let result = new Date(date);
            result.setDate(result.getDate() + days);

            let zero = "";

            if (result.getMonth() + 1 < 10) {
                zero = "0"
            }

            result = result.getFullYear() + "-" + (zero + String(result.getMonth() + 1)) + "-" + result.getDate()
            console.log(result)
          

            return result;
           

        },


        getCurrentDate() {
            let date = this.currenEatingDay.date
            console.log(this.currenEatingDay)
            console.log(this.currenEatingDay.date)
            console.log(date)
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


        async getAllEatingDaysOfOneUser(userId) {

            await api.get('api/Adays/').then(res => {
                console.log(res)
                this.allEatingDaysOfUser = res.data
                this.getCurrentEatingDay(0);
                this.getAllAliment();
            })


        },

        async modifyAlimentDayStore(alimentDayId, alimentDay) {

            await api.put(`api/AlimentDays/${alimentDayId}`, alimentDay).then(res => {
                console.log(res)
            })

            console.log(this.currenEatingDay);
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

        async postSetStore(alimentDay) {
            await api.post("api/AlimentDays/", alimentDay).then(res => { console.log(res) })

            this.currenTraining.sets.push(alimentDay);
        },
        async addAlimentStore(aliment) {
            await api.post("api/Aliments/", aliment).then(res => { console.log(res) })
        },
       
        getCurrentEatingDay(int) {
            this.currenEatingDay = this.allEatingDaysOfUser[int];
         
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
                        totalToReturn = totalToReturn + this.currenEatingDay.alimentDays[i].quantity
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