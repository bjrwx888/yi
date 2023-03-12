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
             <el-button @click="enterEditArticle" type="primary">分享</el-button>    
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
  <DisscussCard :title="i.title" :introduction="i.introduction" :createTime="i.createTime" :id="i.id"/>
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
import {getList} from '@/apis/discussApi.js'
import { onMounted, ref,reactive  } from 'vue'
import { useRoute,useRouter } from 'vue-router'

//数据定义
const route=useRoute()
const router=useRouter()
const activeName = ref('first')
const discussList=ref([]);
const total=ref(100)
const query=reactive({
  pageNum:1,
  pageSize:10,
  title:'',
  plateId:route.params.plateId
})

const handleClick = (tab, event) => {
  console.log(tab, event)
}

onMounted(async()=>{
 await loadDiscussList();
})

//加载discuss
const loadDiscussList=async()=>{
  const response= await getList(query);
 discussList.value=response.items;
 total.value=Number( response.total);
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