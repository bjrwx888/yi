<template>
    <div style="width: 1200px;" class="body-div">
<div class="header">
    <el-form :inline="true" >
            <el-form-item label="标题：" >
      <el-input  v-model="query.title"        placeholder="请输入标题"
      ></el-input>
    </el-form-item>
      
    <el-form-item label="标签：">
        <el-input
        placeholder="搜索当下分类下的标签"

      />
    </el-form-item>
    <div class="form-right">
 
            <el-button>重置</el-button>
             <el-button type="primary" @click="async()=>{ await loadDiscussList();}">查询</el-button>
             <el-button @click="enterEditArticle" type="primary"     v-hasPer="['bbs:discuss:add']">分享</el-button>    
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


<el-tabs v-model="activeName"  @tab-change="handleClick">
    <el-tab-pane label="最新" name="new">    </el-tab-pane>
    <el-tab-pane label="推荐" name="suggest">   </el-tab-pane>
    <el-tab-pane label="最热" name="host">   </el-tab-pane>
  </el-tabs>

  <el-collapse  >
      <el-collapse-item >
        <template #title>
          <div class="collapse-top">
          已置顶主题<el-icon class="header-icon">
            <info-filled />
          </el-icon>
          </div>
        </template>
<div class="div-item" v-for="i in topDiscussList" >
  <DisscussCard :title="i.title" :introduction="i.introduction" :creationTime="i.creationTime" :agreeNum="i.agreeNum" :id="i.id" :user="i.user" :color="i.color"  :seeNum="i.seeNum" badge="置顶"/>
</div>
</el-collapse-item>
</el-collapse>
<el-divider v-show="topDiscussList.length>0" />
  
<div class="div-item" v-for="i in discussList" >
  <DisscussCard :title="i.title" :introduction="i.introduction" :creationTime="i.creationTime" :agreeNum="i.agreeNum" :id="i.id" :color="i.color"  :seeNum="i.seeNum" :user="i.user"/>
</div>
<div>
    <el-pagination
      v-model:current-page="query.pageNum"
      v-model:page-size="query.pageSize"
      :page-sizes="[10, 20, 30, 50]"
      :background="true"
      layout="total, sizes, prev, pager, next, jumper"
      :total="total"
      @size-change="async(val)=>{ await loadDiscussList();}"
      @current-change="async(val)=>{ await loadDiscussList();}"
    />
  </div>

<el-empty v-if="discussList.length==0" description="空空如也" />
    </div>
</template>


<script setup>
import DisscussCard from '@/components/DisscussCard.vue'
import {getList,getTopList} from '@/apis/discussApi.js'
import { onMounted, ref,reactive  } from 'vue'
import { useRoute,useRouter } from 'vue-router'

//数据定义
const route=useRoute()
const router=useRouter()
const activeName = ref('new')
//主题内容
const discussList=ref([]);
//置顶主题内容
const topDiscussList = ref([]);
const total=ref(100)
const query=reactive({
  pageNum:1,
  pageSize:10,
  title:'',
  plateId:route.params.plateId,
  type:activeName.value
})

const handleClick =async (tab, event) => {
  query.type=activeName.value ;
  await loadDiscussList();
}

onMounted(async()=>{
 await loadDiscussList();
})

//加载discuss
const loadDiscussList=async()=>{
  const response= await getList(query);
 discussList.value=response.data.items;
 total.value=Number( response.data.total);

 //全查，无需参数
const topResponse=await getTopList();
topDiscussList.value=topResponse.data.items;
}

//进入添加主题页面
const enterEditArticle=()=>{
  //跳转路由
var routerPer= { path: '/editArt', query: {
     operType: 'create',
     artType:'discuss',
     plateId:route.params.plateId,
     }}
router.push(routerPer);
}
</script>
<style scoped>
.el-pagination
{margin: 2rem 0rem 2rem 0rem;justify-content: right;}
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
.collapse-top
{
  padding-left: 2rem;
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