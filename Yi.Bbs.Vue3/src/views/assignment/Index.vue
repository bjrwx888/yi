<script setup>
import {getCanReceiveAssignment} from '@/apis/assignmentApi'
import {onMounted, ref} from "vue";

const canReceiveAssignmentList=ref([]);
onMounted( async ()=>{
  const {data:canReceiveAssignmentListData}= await getCanReceiveAssignment();
  canReceiveAssignmentList.value=canReceiveAssignmentListData;
  
});
</script>

<template>
  
  <div>
    <div v-for="item in canReceiveAssignmentList">{{item}}</div>
  </div>
</template>