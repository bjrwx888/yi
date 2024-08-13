<script setup>
import {ref} from "vue";

const props = defineProps(['data'])
const cardData=ref(props.data);

const emit =defineEmits(['onClick'])
const  onClick=()=>{
  emit('onClick',cardData.value)
}

</script>

<template>
  <div class="card-box">
    <div class="left">
      <div class="left-type">每日任务</div>
      <div class="content">
        <h2>{{cardData.name}}</h2>
       <h4>{{cardData.remarks}}</h4> 
      </div>
    </div>
    <div class="right">
      <div class="right-btn"> 
        <h3>过期时间：无限制</h3>
        <el-button @click="onClick()" type="primary">接受任务</el-button>
      </div>
      
    </div>
  
   
  </div>
</template>
<style scoped lang="scss">
.el-button{
  width: 100px;
  height: 40px;
}
h2{
  margin: 0 0 10px 0;
}
h3{
  margin: 0;
}
h4{
  margin: 0;
}
.card-box{
  padding: 10px;
  border: 2px solid #dcdfe6;
  display: flex;
  justify-content: space-between;
  height: 100px;
  width: 100%;
  align-content: center;
  flex-wrap: wrap;
  .right{
    display: flex;
    align-content: center;
    flex-wrap: wrap;
    .right-btn{
      text-align: right;
    }
  }
  .left{
    display: flex;
    .content{
      margin-left: 30px;
    }
    .left-type
    {
      border: 1px solid #dcdfe6;
      height: 60px;
      width: 100px;
      display: flex;
      align-content: center;
      flex-wrap: wrap;
      justify-content: center;
    }
  }
}

</style>