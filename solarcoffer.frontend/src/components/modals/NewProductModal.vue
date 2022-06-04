<template>
    <solar-modal>
        <template v-slot:header>
            Add New Product
        </template>
        <template v-slot:body>
            <ul class="new-product">
                <li>
                    <label for="is-taxable">Is this product taxable</label>
                    <input type="checkbox" id="is-taxable" v-model="newProduct.isTaxable"/>
                </li>
                <li>
                    <label for="product-name">Name</label>
                    <input type="text" id="iproduct-name" v-model="newProduct.name"/>
                </li>
               <li>
                    <label for="product-desc">Description</label>
                    <input type="text" id="iproduct-desc" v-model="newProduct.description"/>
                </li>
                <li>
                    <label for="product-price">Price (USD)</label>
                    <input type="number" id="iproduct-price" v-model="newProduct.price"/>
                </li>
            </ul>
        </template>
        <template v-slot:footer>
            <solar-button
                type="button"
                @click.native="save"
                aria-label="save new item"
            >
                Save Product
            </solar-button>
            <solar-button
                type="button"
                @click.native="close"
                aria-label="close modal"
            >
                Close
            </solar-button>
        </template>
    </solar-modal>    
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue'
import SolarButton from "../SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue"

export default defineComponent({
    components: { SolarButton, SolarModal},
    name: 'NewProductModal',
    props: {},
    methods: {
        close() {
            this.$emit("close")
        },
        save() {
            this.$emit('save:product', this.newProduct)
        }
    },
    data() {
        return {
            newProduct:  {
                id: 0,
                createdOn: new Date(),
                updatedOn: new Date(),
                name: '',
                description: '',
                price: 0,
                isTaxable: false,
                isArchived: false
            }
        }
    }
})
</script>

<style scoped lang="scss">
    .new-product {
        list-style: none;
        padding: 0;
        margin: 0;
    }

    input  {
        width: auto;
        height: 1.8rem;
        margin-bottom: 1.2rem;
        font-size: 1.1rem;
        line-height: 1.3rem;
        padding: 0.2rem;
        color: #555;
    }

    label {
        font-weight: bold;
        display: block;
        margin-bottom: 0.3rem;
    }

</style>