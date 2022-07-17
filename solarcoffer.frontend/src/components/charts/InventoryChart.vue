<template>
  <div v-if="isTimelineBuilt">
    <apexchart
      type="area"
      :width="'100%'"
      :height="300"
      :options="options"
      :series="series"
    >
    </apexchart>
  </div>
</template>

<script>
import { sync } from "vuex-pathify";
import VueApexCharts from "vue3-apexcharts";
import moment from "moment";

export default {
  name: "InventoryChart",
  components: { apexchart: VueApexCharts },
  data() {
    return {
      snapshotTimeline: sync("snapshotTimeline"),
    };
  },
  async created() {
    await this.$store.dispatch("assignSnapshots");
  },
  computed: {
    options: function () {
      return {
        datalabels: {
          enabled: false,
        },
        fill: {
          type: "gradient",
        },
        stroke: {
          curve: "smooth",
        },
        xaxis: {
          categories: this.snapshotTimeline.timeline.map((t) =>
            moment(t).format("YYYY-MM-DD HH:mm:ss")
          ),
          type: "datetime",
        },
      };
    },
    series: function () {
      return this.snapshotTimeline.productInventorySnapshots.map((p) => ({
        name: p.productId,
        data: p.quantityOnHand,
      }));
    },
    isTimelineBuilt() {
      return this.$store.state.isTimelineBuilt;
    },
  },
};
</script>

<style scoped lang="scss"></style>
