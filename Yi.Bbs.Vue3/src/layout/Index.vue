<template>
  <div class="common-layout">
    <el-container>
      <el-header
        class="common-header"
        ref="header"
        :class="[isFixed ? 'fixed' : '']"
      >
        <AppHeader />
      </el-header>
      <el-main>
        <AppBody />
      </el-main>
    </el-container>
  </div>
</template>
<script setup>
import { ref, onMounted } from "vue";
import AppHeader from "./AppHeader.vue";
import AppBody from "./AppBody.vue";

const header = ref(null);
const isFixed = ref(false);

onMounted(() => {
  window.addEventListener("scroll", handleScroll);
});

const handleScroll = () => {
  const scrollTop =
    window.scrollY ||
    document.documentElement.scrollTop ||
    document.body.scrollTop;
  const currentEle = header.value.$el;
  if (scrollTop > currentEle.offsetTop) {
    isFixed.value = true;
  } else {
    isFixed.value = false;
  }
};
</script>

<style scoped lang="scss">
.common {
  &-header {
    background-color: #fff;
    width: 100%;
    display: flex;
    justify-content: center;
  }
}

.el-main {
  margin: 0;
  padding: 0;
  min-height: 10rem;
  background-color: #f0f2f5;
}

.fixed {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 99;
}
</style>
