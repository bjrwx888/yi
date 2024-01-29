<template>
    <div class="container">
        <div class="top">
        <h4>当前等级: 1-小白</h4>
        <h4>当前钱钱: 100</h4>
        <div class="title">
            <div class="left">当前等级经验:</div>
            <div class="right"> 
                <el-progress  :percentage="50" :stroke-width="15" striped  striped-flow />
                <div>1623/2000</div>
            </div>
        </div>
      
        </div>

        <div class="bottom">
            <el-input-number v-model="num" :min="1" :max="10000" />
            
            <el-button type="primary">升级</el-button>

            <span>所需钱钱：50</span>
        </div>


        <el-table :data="levelData" border style="width: 100%"  >
    <el-table-column prop="currentLevel" label="等级" width="80" align="center" />
    <el-table-column prop="name" label="称号" width="180" align="center" />
    <el-table-column prop="minExperience" label="所需经验"  width="180" align="center" />
    <el-table-column prop="nick" label="其他"  align="center" />
  </el-table>


    </div>
    
</template>
<script  setup>
import {getList} from '@/apis/levelApi.js'
import { ref,onMounted, reactive } from 'vue'

const levelData =ref([]);
const num=ref(1);

const query=reactive({
    skipCount:0,
    maxResultCount:20
})
onMounted(async () => {
   await loadLevelData();
})

const loadLevelData=  async () => {
   const {data:{items}} = await getList(query);
    levelData.value = items;
}
</script>
<style lang="scss" scoped >
.container{
padding: 20px 20px;
.title{
    display: flex;
    .left{
        width: 15%;
    }
    .right{
        width: 60%;
    }
}
.bottom{
    margin: 20px 0px;
        .el-button{
            margin-left: 10px;
            margin-right: 10px;
        }
}
}
</style>>