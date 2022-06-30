<script lang="js">
export default {
  name: "CurrencyMixin",
  methods: {
    currencyUSD(value) {
      if (isNaN(value)) {
        return "-";
      }
      return `$${value.toFixed(2)}`;
    },
  },
};
</script>
