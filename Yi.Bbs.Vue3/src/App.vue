<script setup></script>

<template>
  <el-config-provider :locale="locale">
    <RouterView />
  </el-config-provider>
</template>
<script setup>
import signalR from "@/utils/signalR";
import useAuths from "@/hooks/useAuths";
import useConfigStore from "@/stores/config";
import { ElConfigProvider } from "element-plus";
import { onMounted,watch } from "vue";
const { tokenValue } = useAuths();
import zhCn from "element-plus/dist/locale/zh-cn.mjs";
const locale = zhCn;
const configStore = useConfigStore();

// 判断是否有loading有的话去掉
const loading = document.getElementById("Loading");
if (loading !== null) {
  document.body.removeChild(Loading);
}

//加载全局信息
onMounted(async () => {
  await configStore.getConfig();
  //    setInterval(() => {
  //  console.log("token的值："+tokenValue.value);
  //     }, 1000); // 1000毫秒，即1秒
});


// watch(
//   () => tokenValue,
//    (val,oldValue) => {
//     console.log("token发生改变");
//     if (val) {
//       signalR.close();
//       signalR.init(`main`);
//     }
//   },
//   { deep:true }
// );
</script>
<style scoped></style>
