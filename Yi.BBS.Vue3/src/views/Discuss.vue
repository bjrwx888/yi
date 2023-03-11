<template>
    <div style="width: 1200px;" class="body-div">
<div class="header">
    <el-form :inline="true" >
            <el-form-item label="标签：" >
      <el-input          placeholder="请输入标签"
      ></el-input>
    </el-form-item>
      
    <el-form-item label="内容：">
        <el-input
        placeholder="搜索当下分类下的内容"

      />
    </el-form-item>
    <div class="form-right">
            <el-button>重置</el-button>
             <el-button type="primary">查询</el-button>
            
             <el-dropdown>
    <span class="el-dropdown-link">
      展开
      <el-icon class="el-icon--right">
        <arrow-down />
      </el-icon>
    </span>

    <template #dropdown>
      <el-dropdown-menu>
        <el-dropdown-item>Action 1</el-dropdown-item>
        <el-dropdown-item>Action 2</el-dropdown-item>
        <el-dropdown-item>Action 3</el-dropdown-item>
        <el-dropdown-item disabled>Action 4</el-dropdown-item>
        <el-dropdown-item divided>Action 5</el-dropdown-item>
      </el-dropdown-menu>
    </template>
  </el-dropdown>
</div>

</el-form>
</div>


<el-tabs v-model="activeName"  @tab-click="handleClick">
    <el-tab-pane label="推荐" name="first">    </el-tab-pane>
    <el-tab-pane label="最新" name="second">   </el-tab-pane>
    <el-tab-pane label="最热" name="third">   </el-tab-pane>
  </el-tabs>
<div class="div-item" v-for="i in discussList" >
  <DisscussCard :title="i.title" :introduction="i.introduction" :createTime="i.createTime"/>
</div>
    </div>
</template>
<script setup>
import DisscussCard from '@/components/DisscussCard.vue'
import {getListByPlateId} from '@/apis/discussApi.js'
import { onMounted, ref } from 'vue'
import { useRouter,useRoute } from 'vue-router'
const router = useRouter()
const route=useRoute()
const activeName = ref('first')

const discussList=ref([]);

const handleClick = (tab, event) => {
  console.log(tab, event)
}

onMounted(async()=>{
 const response= await getListByPlateId(route.params.plateId);
 discussList.value=response.items;
})
</script>
<style scoped>
.body-div{
min-height: 1000px;
}
 .el-dropdown-link {
  cursor: pointer;
  color: var(--el-color-primary);
  display: flex;
  align-items: center;
}
.header{
background-color: #FFFFFF;
padding: 1rem;
margin: 1rem 0rem ;
}
.header .el-input
{
 
}
.el-tabs
{
    background-color: #FFFFFF;
 padding-left: 2rem;
}
.el-tabs >>> .el-tabs__header
{
    margin-bottom: 0;
}
.div-item
{
margin-bottom: 1rem;
}

.el-form {
    --el-form-label-font-size: var(--el-font-size-base);
    display: flex;
    align-items: center;
}
.el-form-item
{padding-top: 0.8rem;}
.form-right
{
    align-items: center;
display: flex;
    margin-left: auto;
}
.form-right .el-button
{
    margin-right: 0.6rem;
}
.header .el-input
{
    width:20rem;
}
</style>