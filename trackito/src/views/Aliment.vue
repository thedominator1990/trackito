<template>
    <div class="masterDiv">
        <div class="eventName">
            <i class="icon ion-ios-arrow-left" style="margin-top:25px; margin-right:30px;" @click="moveDown()"></i>
            <h1>{{ getCurrentDate() }}</h1>
            <i class="icon ion-ios-arrow-right" style="margin-top:25px; margin-left:30px;" @click="moveUp()"></i>
        </div>
      

        <div class="sectionDivs">
            <div class="section">
                <div class="activityGrid">
                    <span class="topRow">nom</span>
                    <span class="topRow">quantite</span>
                    <span class="topRow">calories</span>
                    <span class="topRow">gras</span>
                    <span class="topRow">proteines</span>
                    <span class="topRow">glucides</span>
                    <span class="topRow">supprimer</span>
                    <template v-for="alimentDay in currenEatingDay.alimentDays">
                        <span>{{alimentDay.alimentName}}</span>
                        <span v-if="toggleQuantityModification != alimentDay.id">{{alimentDay.quantity}}<i @click="activateModification(alimentDay.id, alimentDay.quantity)" class="icon ion-paintbrush" style="font-size: 18px"></i></span>
                        <span v-if="toggleQuantityModification == alimentDay.id"><input type="text" v-model="currentQuantity" /><i @click="acceptModificationQuanitty(alimentDay.id, alimentDay)" class="icon ion-ios-checkmark" style="font-size: 18px"></i></span>
                        <span>{{getNutrition(alimentDay,"calories")}}</span>
                        <span>{{getNutrition(alimentDay,"fat")}}</span>
                        <span>{{getNutrition(alimentDay,"protein")}}</span>
                        <span>{{getNutrition(alimentDay,"carbs")}}</span>
                        <span @click="deleteAlimentDay(alimentDay.id)" class="icon ion-ios-trash"></span>
                    </template>

                    <span class="topRow">Total:</span>
                    <span class="topRow">{{getTotals("quantity")}}<i class="icon ion-paintbrush" style="visibility: hidden;font-size: 18px "></i></span>
                    <span class="topRow">{{getTotals("calories")}}</span>
                    <span class="topRow">{{getTotals("fat")}}</span>
                    <span class="topRow">{{getTotals("protein")}}</span>
                    <span class="topRow">{{getTotals("carbs")}}</span>

                </div>
            </div>
        </div>
        <div class="addAliment">
            <div class="buttons">
                <button class="addAlimentButton" @click="toggleAddNewAliment()"> Ajouter nouvel aliment</button>
                <div class="addNewRowFlex" v-if="addNewAlimentToggle">
                    <p>Nom:	&nbsp	&nbsp	&nbsp</p>
                    <input class="inputAliment" type="text" v-model="newAlimentName" />
                </div>
                <div class="addNewRowFlex" v-if="addNewAlimentToggle">
                    <p>Quantite:  </p>
                    <input class="inputAliment" type="text" v-model="newAlimentQuantity" />
                </div>
                <div class="addNewRowFlex" v-if="addNewAlimentToggle">
                    <p>Calories: </p>
                    <input class="inputAliment" type="text" v-model="newAlimentCalories" />
                </div>
                <div class="addNewRowFlex" v-if="addNewAlimentToggle">
                    <p>Proteines: </p>
                    <input class="inputAliment" type="text" v-model="newAlimentProtein" />
                </div>
                <div class="addNewRowFlex" v-if="addNewAlimentToggle">
                    <p>Gras: </p>
                    <input class="inputAliment" type="text" v-model="newAlimentFat" />
                </div>
                <div class="addNewRowFlex" v-if="addNewAlimentToggle">
                    <p>Garbs: </p>
                    <input class="inputAliment" type="text" v-model="newAlimentCarbs" />
                </div>
                <div v-if="addNewAlimentToggle">
                    <i @click="addNewAliment()" class="icon ion-ios-checkmark" style="font-size: 30px"></i>
                    <i @click="cancelAddNewAliment()" class="icon ion-ios-trash" style="font-size: 30px"></i>
                </div>


                <button class="addAlimentButton" @click="toggleAddExistingAliment()"> Ajouter aliment Existant</button>
                <div class="alimentSelector" v-if="addExistingAlimentToggle">
                    <label for="aliments"> Aliments &nbsp</label>
                    <select id="aliment" @change="selectExistingAliment($event)" v-model="key" class="alimentDropDown">
                        <option v-for="aliment in allAliments" :value="aliment.id">{{aliment.name}}</option>
                    </select>

                </div>
                <div class="quantitySelector" v-if="addExistingAlimentToggle">
                    <div>
                        <p>Quanite &nbsp</p>
                    </div>
                    <div class="problemInput">
                        <input type="text" v-model="existingItemNewQuantity" class="alimentDropDown" />
                    </div>
                </div>
                <div v-if="addExistingAlimentToggle">
                    <i @click="addCurrentAliment()" class="icon ion-ios-checkmark" style="font-size: 30px"></i>
                    <i @click="canceladdCurrentAliment()" class="icon ion-ios-trash" style="font-size: 30px"></i>
                </div>


                <button class="addAlimentButton" @click="toggleModifyAliment()"> Modifier un aliment </button>
                <div class="alimentSelector" v-if="modifyAlimentToggle">
                    <label for="aliments"> Aliments</label>
                    <select id="aliment" @change="checkSelect($event)" v-model="key" class="alimentDropDown">
                        <option v-for="aliment in allAliments" :value="aliment.id">{{aliment.name}}</option>
                    </select>

                </div>


                <div>
                    <div class="addNewRowFlex" v-if="modifyAlimentFactsToggle">
                        <p>Nom:	&nbsp	&nbsp	&nbsp</p>
                        <input class="inputAliment" type="text" v-model="modifyAlimentName" />
                    </div>
                    <div class="addNewRowFlex" v-if="modifyAlimentFactsToggle">
                        <p>Quantite:  </p>
                        <input class="inputAliment" type="text" v-model="modifyAlimentQuantity" />
                    </div>
                    <div class="addNewRowFlex" v-if="modifyAlimentFactsToggle">
                        <p>Calories: </p>
                        <input class="inputAliment" type="text" v-model="modifyAlimentCalories" />
                    </div>
                    <div class="addNewRowFlex" v-if="modifyAlimentFactsToggle">
                        <p>Proteines: </p>
                        <input class="inputAliment" type="text" v-model="modifyAlimentProtein" />
                    </div>
                    <div class="addNewRowFlex" v-if="modifyAlimentFactsToggle">
                        <p>Gras: </p>
                        <input class="inputAliment" type="text" v-model="modifyAlimentFat" />
                    </div>
                    <div class="addNewRowFlex" v-if="modifyAlimentFactsToggle">
                        <p>Garbs: </p>
                        <input class="inputAliment" type="text" v-model="modifyAlimentCarbs" />
                    </div>
                    <div v-if="modifyAlimentFactsToggle">
                        <i @click="modifyCurrentAliment()" class="icon ion-ios-checkmark" style="font-size: 30px"></i>
                        <i @click="cancelModifyAliment()" class="icon ion-ios-trash" style="font-size: 30px"></i>
                    </div>
                </div>

            </div>


        </div>
    </div>

</template>

<script lang="ts">
    import api from "../../api/index.js"
    import { mapState, mapActions } from "pinia";
    import { useAlimentStore } from "../../stores/aliment.store"

    export default {
        data() {
            return {
                toggleQuantityModification: 1000,
                currentQuantity: 1000,
                addNewAlimentToggle: false,
                modifyAlimentToggle: false,
                modifyAlimentFactsToggle: false,
                addExistingAlimentToggle: false

                newAlimentName: "",
                newAlimentQuantity: 0,
                newAlimentCalories: 0,
                newAlimentProtein: 0,
                newAlimentFat: 0,
                newAlimentCarbs: 0,

                modifyAlimentName: "",
                modifyAlimentQuantity: 0,
                modifyAlimentCalories: 0,
                modifyAlimentProtein: 0,
                modifyAlimentFat: 0,
                modifyAlimentCarbs: 0,
                modifyAlimentId: 0,

                existingItemNewQuantity: 0,
                existingItemId: 0,


            };

        },


        computed: {

            ...mapState(useAlimentStore, ["currentMonth", "currenEatingDay","allAliments"]),

        },
        
        methods: {
            ...mapActions(useAlimentStore, ["getAllEatingDaysOfOneUser", "modifyAlimentDayStore", "deleteAlimentDayStore", "postAlimentDayStore", "getNutrition", "getCurrentDate", "getIdFromDate", "addDays", "getTotals", "addAlimentStore",
                "getAllAliment", "modifyAlimentStore","selectTrainingFromDate"
            ]),

            moveUp() {
                let nextDay = this.addDays(this.currenEatingDay.date, 1);
                this.selectTrainingFromDate(nextDay);

            },
            moveDown() {
                let nextDay = this.addDays(this.currenEatingDay.date, -1);
                this.selectTrainingFromDate(nextDay);

            },

            checkClick() {
                console.log("click checked")
            },
            toggleModifyAliment() {
                this.modifyAlimentToggle = true;
            }
            checkSelect(event) {
                this.getAllAliment();
                let alimentToModify = this.allAliments.find(aliment => aliment.id == event.target.value)

                this.modifyAlimentName = alimentToModify.name
                this.modifyAlimentQuantity = alimentToModify.quantity;
                this.modifyAlimentCalories = alimentToModify.calories
                this.modifyAlimentProtein = alimentToModify.protein;
                this.modifyAlimentFat = alimentToModify.fat;
                this.modifyAlimentCarbs = alimentToModify.carbs
                this.modifyAlimentId = alimentToModify.id

                this.modifyAlimentFactsToggle = true;

                console.log("aliment to modify", alimentToModify)
            },
            toggleAddExistingAliment() {
                this.addExistingAlimentToggle = true;
            },
            selectExistingAliment(id) {
                this.existingItemId = id.target.value



            },
            addCurrentAliment() {
                let newAlimentDay = {}
                newAlimentDay.quantity = this.existingItemNewQuantity;
                newAlimentDay.alimentId = this.existingItemId;
                newAlimentDay.dayId = this.currenEatingDay.id;

                

                this.postAlimentDayStore(newAlimentDay);

            },
            canceladdCurrentAliment() {
                this.addExistingAlimentToggle = false; 
            },
            modifyCurrentAliment() {
                let alimentToSend = {
                name : this.modifyAlimentName,
                quanity : this.modifyAlimentQuantity,
                calories : this.modifyAlimentCalories,
                protein : this.modifyAlimentProtein,
                fat : this.modifyAlimentFat,
                carbs : this.modifyAlimentCarbs,
                id : this.modifyAlimentId


                }
                alimentToSend.name = this.modifyAlimentName;
                alimentToSend.quanity = this.modifyAlimentQuantity
                alimentToSend.calories = this.modifyAlimentCalories
                alimentToSend.protein = this.modifyAlimentProtein
                alimentToSend.fat = this.modifyAlimentFat
                alimentToSend.carbs = this.modifyAlimentCarbs
                alimentToSend.id = this.modifyAlimentId

                if (parseInt(alimentToSend.quantity) < 1) {
                    alimentToSend.quantity = 1
                }

                this.modifyAlimentStore(alimentToSend, alimentToSend.id);
                this.modifyAlimentFactsToggle = false
                this.modifyAlimentToggle = false;

            },

            cancelModifyAliment() {
                this.modifyAlimentFactsToggle = false
            },

            toggleAddNewAliment() {
                this.addNewAlimentToggle = true;
            },

            addNewAliment() {
                let newAliment = {}

                newAliment.name = this.newAlimentName
                newAliment.quantity =  this.newAlimentQuantity
                newAliment.calories = this.newAlimentCalories
                newAliment.protein = this.newAlimentProtein
                newAliment.fat =   this.newAlimentFat
                newAliment.carbs = this.newAlimentCarbs

                if (parseInt(newAliment.quantity) < 1) {
                    newAliment.quantity = 1
                }

                this.addNewAlimentToggle = false;

                this.addAlimentStore(newAliment);
                
               

            },
            cancelAddNewAliment() {
                this.addNewAlimentToggle = false;
            }

            activateModification( id : number, quantity: number): void {
                this.toggleQuantityModification = id;
                this.currentQuantity = quantity;
            },

            deleteAlimentDay(id: number): void {
                this.deleteAlimentDayStore(id);
            },

            acceptModificationQuanitty(id: number, alimentDay: Object): void {
                alimentDay.quantity = this.currentQuantity;
                this.modifyAlimentDayStore(id, alimentDay);
                this.toggleQuantityModification = 100000;

            },
            sendNewAlimentToDB() {
                let aliment = {}
               aliment.name = this.newAlimentName
               aliment.quantity = this.newAlimentQuantity
               aliment.calories = this.newAlimentCalories
               aliment.protein = this.newAlimentProtein
               aliment.fat =  this.newAlimentFat
                aliment.carbs = this.newAlimentCarbs

                this.addAlimentStore(aliment)
                    ;


            },

        },
    };
</script>
<style scoped>
    .headerDiv{
        display:flex;
        flex-direction:row;
    }
    .problemInput{
         align-self:flex-start;
         margin-top:15px
    }
    .quantitySelector {
        display: flex;
        flex-direction: row;
    }
    .alimentSelector {
        margin-top: 10px;
        display: flex;
        flex-direction: row;
    }
    .alimentDropDown{
        width:100px;
        margin-right:10px;
    }
    .inputAliment {
        height: 20px;
        margin-top: 13px;
        min-width: 100px;
        max-width: 150px;
    }
    .addNewRowFlex {
        display: flex;
        flex-direction: row;
        justify-content:flex-start;

     
    }
    .addAlimentButton {
        margin-top:10px;
        background-color: #546e7a;
        color: #eceff1;
        text-shadow: 0.5px 0.5px #eceff1;
        width:30%;
    }

    i {
        margin-left: 3px;
        font-size: 1px;
    }
    .buttons{
        display:flex;
        flex-direction:column;
    }
    .eventName {
        display:flex;
        flex-direction:row;
        justify-content:center;
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
        grid-template-columns: repeat(7, 1fr);
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
    }

        .rowFlexActivity p {
            height: 100%;
        }

        .rowFlexActivity input {
            height: 100%;
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

