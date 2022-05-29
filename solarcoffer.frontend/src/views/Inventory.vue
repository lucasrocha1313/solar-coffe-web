<template>
    <div class="inventory-container">
        <h1 id="invetory-title">
            Inventory Dashboard
        </h1>
        <hr/>
        <div class="inventory-actions">
            <solar-button @click.native="showNewProductModel" id="addNewBtn">
                Add new item
            </solar-button>
            <solar-button @click.native="showShipmentModel" id="receiveShipmentBtn">
                Receive Shipment
            </solar-button>
        </div>
        <table id="inventory-table" class="table">
            <th>Item</th>
            <th>Quantity On-Hand</th>
            <th>Unit Price</th>
            <th>Taxable</th>
            <th>Delete</th>
            <tr v-for="item in inventory" :key="item.id">
                <td>
                    {{item.product.name}}
                </td>
                <td>
                    {{item.quantityOnHand}}
                </td>
                <td>
                    {{currencyUSD(item.product.price)}}
                </td>
                <td>
                    <span v-if="item.product.isTaxable" >
                        Yes
                    </span>
                    <span v-else>
                        No
                    </span>
                </td>
                <td>
                    <div>X</div>
                </td>
            </tr>
        </table>

        <shipment-modal
            v-if="isShipmentVisible"
            :inventory="inventory"
            @close="closeModal"/>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue"
    import SolarButton from "@/components/SolarButton.vue"
    import ShipmentModal from "@/components/modals/ShipmentModal.vue"

    export default defineComponent({
        name: 'Inventory',
        components:{ SolarButton, ShipmentModal },
        methods: {
            currencyUSD(value: number) {
                if(isNaN(value)) {
                    return '-'
                }
                return `$${value.toFixed(2)}`
            },
            showNewProductModel() {
            },
            showShipmentModel() {

            },
            closeModal() {
                this.isShipmentVisible= false
                this.isNewProductVisible = false
            }
        },
        data() {
            return {
                isShipmentVisible: true,
                isNewProductVisible: true,
                inventory: [
                    {
                        id: 1,
                        product: {
                            id: 1,
                            createdOn: new Date(),
                            updatedOn: new Date(),
                            name: 'Some Product',
                            description: 'Good stuff',
                            price: 100,
                            isTaxable: true,
                            isArchived: false
                        },
                        quantityOnHand: 2,
                        idealQuantity: 5
                    },
                    {
                        id: 2,
                        product: {
                            id: 2,
                            createdOn: new Date(),
                            updatedOn: new Date(),
                            name: 'Some Product 2',
                            description: 'Good stuff 2',
                            price: 200,
                            isTaxable: false,
                            isArchived: false
                        },
                        quantityOnHand: 4,
                        idealQuantity: 10
                    }
                ]
            }
        }        
    })
</script>

<style scoped lang="scss">
</style>