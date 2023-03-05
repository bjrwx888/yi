<template>
  <el-row :gutter="20" class="top-div">
    <el-col :span="17">
      

      <el-row class="left-top-div" :gutter="20">

     
        <el-col v-for="i in 9" :key="i" :span="2">
          最新
      </el-col>

        </el-row>
      <el-row class="left-div">

        <el-col :span="8" v-for="i in 6" class="plate" :style="{  'padding-left': i%3==1?0:0.2+'rem','padding-right': i%3==0?0:0.2+'rem'}" >
<PlateCard/>
          </el-col>

        <el-col :span="24" v-for="i in 10" :key="i">
<DisscussCard/>
          
        </el-col>
      </el-row>

      </el-col>

     <el-col :span="7">
      <el-row class="right-div">
        <el-col :span="24" >
          
          <InfoCard  header="其他" text="详情">
          <template #content>
            等待入驻
          </template>
          </InfoCard>
        </el-col>

        <el-col :span="24" >
         <InfoCard  header="用户" text="详情">
          <template #content>
            你好啊。好兄弟
          </template>
          </InfoCard>
        </el-col>

        <el-col :span="24" >
          <InfoCard :items=items  header="本月排行" text="更多">
           <template #item="temp">
          {{temp}}
           </template>
           </InfoCard>
         </el-col>


         <el-col :span="24" >
          <InfoCard :items=items  header="推荐好友" text="更多">
           <template #item="temp">
          {{temp}}
           </template>
           </InfoCard>
         </el-col>

      </el-row>
    </el-col>
  </el-row>
</template>

<script setup>
import DisscussCard from '@/components/DisscussCard.vue'
import InfoCard from '@/components/InfoCard.vue'
import PlateCard from '@/components/PlateCard.vue'
const items=[{user:"用户1"},{user:"用户2"},{user:"用户3"}]
</script>
<style scoped >
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


.left-top-div .el-col
{
min-height: 2rem;
background-color:#FAFAFA;
margin-bottom: 1rem;
margin-left: 0.6rem;
}
.top-div
{

  padding-top: 1rem;
}
.left-top-div
{
  font-size:small;
 text-align: center;
 line-height: 2rem;
}


</style>
