<template >
  <div style="width: 1200px;">
  <el-row :gutter="20" class="top-div" >

    <el-col :span="17">
      <div class="scrollbar">
        <ScrollbarInfo/>
      </div>
   
     
      <el-row class="left-div">
        <el-col :span="8" v-for="i in plateList" class="plate" :style="{  'padding-left': i%3==1?0:0.2+'rem','padding-right': i%3==0?0:0.2+'rem'}" >
          <PlateCard :name="i.name" :introduction="i.introduction" :id="i.id"/>
          </el-col>

        <el-col :span="24" v-for="i in discussList">
<DisscussCard :title="i.title" :introduction="i.introduction" :creationTime="i.creationTime" :id="i.id" :user="i.user" :seeNum="i.seeNum"/>
          
        </el-col>
      </el-row>

      </el-col>

     <el-col :span="7">
      <el-row class="right-div">
        <el-col :span="24" >
          
     
          <el-carousel trigger="click" height="150px">
      <el-carousel-item v-for="item in 4" :key="item">
        <h3 class="small justify-center" text="2xl">你好{{ item }}</h3>
      </el-carousel-item>
    </el-carousel>

        </el-col>

        <el-col :span="24" >
         <InfoCard  header="简介" text="详情">
          <template #content >
            <div class="introduce">

              没有什么能够阻挡，人类对代码<span style="color: #1890ff;">优雅</span>的最求
            </div>
       
          </template>
          </InfoCard>
        </el-col>

        <el-col :span="24" >
          <InfoCard :items=items  header="本月排行" text="更多">
           <template #item="temp">
            <AvatarInfo>
<template #bottom>
  本月积分：680
</template>
              
            </AvatarInfo>
           </template>
           </InfoCard>
         </el-col>


         <el-col :span="24" >
          <InfoCard :items=items  header="推荐好友" text="更多">
           <template #item="temp">
            <AvatarInfo/>
           </template>
           </InfoCard>
         </el-col>
         <el-col :span="24" >
          <InfoCard :items=items  header="其他" text="更多">
           <template #item="temp">
          {{temp}}
           </template>
           </InfoCard>
         </el-col>

         <el-col :span="24" style=" background: transparent;">
        <BottomInfo/>
        </el-col>
      </el-row>
    </el-col>
  </el-row>
</div>
</template>

<script setup>
import DisscussCard from '@/components/DisscussCard.vue'
import InfoCard from '@/components/InfoCard.vue'
import PlateCard from '@/components/PlateCard.vue'
import ScrollbarInfo from '@/components/ScrollbarInfo.vue'
import AvatarInfo from '@/components/AvatarInfo.vue'
import BottomInfo from '@/components/BottomInfo.vue'

import {getList} from '@/apis/plateApi.js'
import {getList as discussGetList} from '@/apis/discussApi.js'
import { onMounted, ref ,reactive} from 'vue'
var plateList=ref([]);
var discussList=ref([]);
  const items=[{user:"用户1"},{user:"用户2"},{user:"用户3"}]
 //主题查询参数 
  const query=reactive({
  pageNum:1,
  pageSize:10,
});
  onMounted(async()=>{
 const response=  await getList();
 plateList.value= response.items;

const discussReponse=await discussGetList(query);
discussList.value= discussReponse.items;
  });


</script>
<style scoped >
.introduce
{
color: rgba(0,0,0,.45);
font-size: small;
}
.plate
{
  background: transparent !important;

}
.left-div .el-col{
background-color: #FFFFFF;

margin-bottom: 1rem;
}
.right-div .el-col
{
  background-color:#FFFFFF;
  margin-bottom: 1rem;
}



.top-div
{

  padding-top: 0.5rem;
}
.scrollbar
{ display: block;
  margin-bottom: 0.5rem;
}
</style>
