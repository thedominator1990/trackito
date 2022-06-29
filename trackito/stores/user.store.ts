import { defineStore } from "pinia";
import api from "../api/index.js"

export const useUserStore = defineStore("user", {
    state: () => {
        return {

            currentUser: {},
            allUsers: [],
            currentUserId: 0,

       

        }
    },
    actions: {

        async getALlUsers() {
            await api.get("api/Users").then(res => {
                console.log(res.data)
                this.allUsers = res.data;
            })
        },

        getOneUser(id) {
            for (var x = 0; x < this.allUsers.length; x++) {
                if (this.allUsers[x].id == id) {
                    this.currentUser = this.allUsers[x]
                }
            }
        }


        

        },
        
      
       
            
    })

