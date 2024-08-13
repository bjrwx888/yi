<script setup>
import {getAssignmentList, getCanReceiveAssignment, acceptAssignment, receiveAssignment} from '@/apis/assignmentApi'
import {onMounted, reactive, ref} from "vue";
import AssignmentCard from "./components/AssignmentCard.vue"
const canReceiveAssignmentList = ref([]);

const assignmentList = ref([]);
const queryForm = reactive({
  assignmentQueryState: "Progress"
});

//当前选择table页
const currentTableSelect = ref("canAccept");

//切换tab
const changeClickTable = async (tabName) => {
  console.log(tabName,"tabName")
  switch (tabName) {
    case "canAccept":
      const {data: canReceiveAssignmentListData} = await getCanReceiveAssignment();
      canReceiveAssignmentList.value = canReceiveAssignmentListData;
      return;
    case "progress":
      queryForm.assignmentQueryState = "Progress";
      break;
    case "end":
      queryForm.assignmentQueryState = "End";
      break;
  }
  const {data} = await getAssignmentList(queryForm);
  assignmentList.value = data;
}
onMounted(async () => {
  const {data: canReceiveAssignmentListData} = await getCanReceiveAssignment();
  canReceiveAssignmentList.value = canReceiveAssignmentListData;
});
//刷新数据
const refreshData = async () => {

  const {data: canReceiveAssignmentListData} = await getCanReceiveAssignment();
  canReceiveAssignmentList.value = canReceiveAssignmentListData;

  const {data} = await getAssignmentList(queryForm);
  assignmentList.value = data;
}

//接收任务
const onClickAcceptAssignment = async (item) => {
  await acceptAssignment(item.id);
  await refreshData();
}

const onClickReceiveAssignment = async (id) => {
  await receiveAssignment(id);
  await refreshData();
}

//切换tab
const changeTab = async (state) => {
  queryForm.assignmentQueryState = state;
  await refreshData();
}


</script>

<template>
  <div class="content-body">
    <el-tabs
        v-model="currentTableSelect"
        type="border-card"
        @tab-change="changeClickTable"
    >
      <el-tab-pane label="可接受" name="canAccept"/>
      <el-tab-pane label="已接受" name="progress"/>
      <el-tab-pane label="已结束" name="end"/>
      
      <div v-for="item in canReceiveAssignmentList" v-if="currentTableSelect==='canAccept'">
        <AssignmentCard :data="item" @onClick="onClickAcceptAssignment"/>
      </div>

      <div v-for="item in assignmentList" v-else>{{ item }}
        <button type="button" @click="onClickReceiveAssignment(item.id)">领取奖励</button>
      </div>
    </el-tabs>
  </div>
</template>
<style scoped lang="scss">
.content-body{
  
  padding: 30px ;
}


</style>