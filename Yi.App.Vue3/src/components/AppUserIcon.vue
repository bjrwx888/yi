<template>
  <van-image round :width="width" :height="height" :src="getUrl()" />
</template>

<script setup lang="ts">
const props = defineProps({
  src: { type: String, default: "null", required: false },
  width: { type: String, default: "3rem", required: false },
  height: { type: String, default: "3rem", required: false },
});

const url = `${import.meta.env.VITE_APP_BASE_API}/file/`;
const getUrl = () => {
  const src = props.src;
  if (src === null || typeof src === "undefined" || src.trim() === "") {
    return "/icon.jpg";

    // 字符串为 null、未定义或为空字符串
  } else {
    return url + "src";
  }
};
</script>
<style scoped>
.col-body {
  text-align: center;
}
.col-body .van-icon {
  margin-bottom: 0.6rem;
}
p {
  font-size: large;
  font-weight: bold;
}
</style>
