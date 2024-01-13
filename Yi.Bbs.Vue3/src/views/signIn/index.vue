<template>
  <div class="everyday-box">
    <h4>每日签到页持续coding中~~~</h4>


    <el-button type="primary" @click="signInOnclic">点击完成签到</el-button>


  </div>
</template>

<script setup>
import { onMounted, ref, reactive, computed, nextTick, watch } from "vue";
import { signIn  } from "@/apis/integralApi.js";

const signInOnclic= async()=>{
   const { data: data}= await signIn();
   
   ElMessage({
        message: `恭喜！运气爆棚，今日获得：${data.value} 钱钱`,
        type: "success",
      });

}

</script>

<style lang="scss" scoped>
.everyday-box {
  width: 100%;
  height: 100%;
}
</style>
