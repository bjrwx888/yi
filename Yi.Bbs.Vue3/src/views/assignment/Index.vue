<script setup>
import {getAssignmentList, getCanReceiveAssignment, acceptAssignment, receiveAssignment} from '@/apis/assignmentApi'
import {onMounted, reactive, ref} from "vue";

const canReceiveAssignmentList=ref([]);

const assignmentList=ref([]);
const queryForm=reactive({
  assignmentQueryState:"Progress"
});

onMounted( async ()=>{
 await  refreshData();
});
//刷新数据
const  refreshData=async ()=>{

  const {data:canReceiveAssignmentListData}= await getCanReceiveAssignment();
  canReceiveAssignmentList.value=canReceiveAssignmentListData;

  const {data}= await getAssignmentList(queryForm);
  assignmentList.value=data;
}

//接收任务
const onClickAcceptAssignment=async (id)=>{
   await acceptAssignment(id);
  await  refreshData();
}

const onClickReceiveAssignment=async (id)=>{
  await receiveAssignment(id);
  await  refreshData();
}

//切换tab
const  changeTab=async (state)=>{
  queryForm.assignmentQueryState=state;
  await  refreshData();
}
</script>

<template>

  <h3>可接收任务</h3>
  <div>
    <div v-for="item in canReceiveAssignmentList">{{item}}
      <button type="button" @click="onClickAcceptAssignment(item.id)">接收任务</button>
    </div>
  </div>

<hr/>
  <h3>已接收任务
    <button type="button" @click="changeTab('Progress')">切换正在进行</button>
    <button type="button" @click="changeTab('End')">切换已结束</button></h3>
  <div>
    <div v-for="item in assignmentList">{{item}}
      <button type="button" @click="onClickReceiveAssignment(item.id)">领取奖励</button>
    </div>
  </div>
</template>