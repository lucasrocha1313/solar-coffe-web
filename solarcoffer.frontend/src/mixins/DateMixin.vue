<script>
import moment from "moment";

export default {
  name: "DateMixin",
  methods: {
    humanizeDate(date) {
      return moment(date).format("MMMM Do YYYY");
    },
  },
};
</script>
