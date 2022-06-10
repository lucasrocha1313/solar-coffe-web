<template>
  <transition name="modal-fade">
    <div class="modal-backdrop">
      <div
        class="modal"
        role="dialog"
        aria-labelledby="modal-title"
        aria-describedby="modal-description"
      >
        <header class="modal-header" id="modal-title">
          <slot name="header"> </slot>
        </header>
        <section class="modal-body" id="modal-description">
          <slot name="body"> </slot>
        </section>
        <footer class="modal-footer">
          <slot name="footer"> </slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "SolarModal",
  components: {},
  methods: {
    close() {
      this.$emit("close");
    },
  },
});
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.modal-backdrop {
  position: fixed;
  top: 0;
  bottom: 0;
  right: 0;
  left: 0;
  background-color: rgb(0, 0, 0, 0.6);
  display: flex;
  justify-content: center;
  align-items: center;
}
.modal {
  background: #fff;
  box-shadow: 2px 2px 20px 1px;
  overflow-x: auto;
  display: flex;
  flex-direction: column;
  widows: initial;
  border-radius: 3px;
}

.modal-header {
  padding: 15px;
  display: flex;
  background-color: $solar-blue;
  color: #fff;
  font-size: 1.2rem;
  font-weight: bold;
  border-bottom: 1px solid #eee;
  justify-content: space-between;
}

.modal-footer {
  padding: 15px;
  display: flex;
  border-top: 1px solid #eee;
  justify-content: flex-end;
}

.modal-body {
  position: relative;
  padding: 2.4rem;
}

.modal-fade-ender,
.modal-fade-leave-active {
  opacity: 0;
}

.modal-fade-enter-active,
.modal-fade-leave-active {
  transition: opacity 0.5s ease;
}
</style>
