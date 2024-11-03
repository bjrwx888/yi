<script setup>
import {computed, ref, watch} from "vue";
const data=ref({});
const realData=ref({});
const props = defineProps([
  "data","realData"
]);
const emit = defineEmits(['clickBuy'])
watch(()=>props.data,(n)=>{
  data.value=n;
},{deep:true, immediate: true})

watch(()=>props.realData,(n)=>{
  realData.value=n;
},{deep:true, immediate: true})

const clickBuy=async ()=>{
  
  emit('clickBuy',data.value)
}

const isConformToRule=computed(()=>{
  if (data.value.isLimit===true)
  {
    return true;
  }
  if (realData.value.money>=data.value.needMoney
      &&realData.value.value>=data.value.needValue
      &&realData.value.points>=data.value.needPoints
      &&data.value.stockNumber>0)
  {
   return true; 
  }
  return false
  
})
</script>


<template>
  <el-card  shadow="hover">
    <template #header>{{data.name}}</template>
    <img
        :src="data.imageUrl"
        style="width: 100%"
     alt=""/>
    简介：{{data.describe}}
    <ul>
      <li :class="{'less-li': realData.money<data.needMoney}">所需钱钱：{{data.needMoney}}</li>
      <li :class="{'less-li': realData.value<data.needValue}">所需价值：{{data.needValue}}</li>
      <li :class="{'less-li': realData.points<data.needPoints}">所需积分：{{data.needPoints}}</li>
      <li>限购数量：{{data.limitNumber}}</li>
      <li>剩余：{{data.stockNumber}}</li>
    </ul>
    <el-divider />
    <div class="bottom"> 
      
      <el-button v-if="!isConformToRule" :disabled="true" type="danger">条件不足</el-button>
      <el-button v-else :disabled="data.isLimit" type="success" @click="clickBuy">{{data.isLimit===true?"已申请":"申请购买"}} </el-button>
    </div>
  </el-card>
</template>

<style scoped lang="scss">
.bottom
{
  display: flex;
  justify-content: flex-end;
  margin-top: 20px;
}
.less-li{
  color: red;
}
</style>