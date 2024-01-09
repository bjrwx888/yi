<template>
  <div class="message">{{ message }}</div>
</template>

<script setup>
import { ref, watch } from "vue";
import { useRoute } from "vue-router";
import { authOtherLogin, authOtherBind } from "@/apis/auth.js";

const route = useRoute();

const code = ref(route.query.code);
const type = ref(route.query.state);

const message = ref("");
const scheme = ref("");
watch(
  () => code.value,
  async (val) => {
    if (val) {
      // 使用正则表达式提取路由参数
      const regex = /\/auth\/([\w-]+)[?]?/;
      const result = regex.exec(route.fullPath);
      const authParam = result != null ? result[1] : null;
      switch (authParam) {
        case "gitee":
          scheme.value = "Gitee";
          break;
        case "qq":
          scheme.value = "QQ";
          break;
      }
      if (type.value === "0") {
        const res = await authOtherLogin({ code: val }, scheme.value);
        console.log(res, "登录的");
      } else if (type.value === "1") {
        const res = await authOtherBind({ code: val }, scheme.value);
        console.log(res, "绑定的");
      }
      message.value = "授权成功";
      // window.close();
    }
  },
  { immediate: true }
);
</script>

<style lang="scss">
.message {
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  font-size: 20px;
}
</style>
