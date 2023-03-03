<template>
  <el-row :gutter="20" class="top-div">
    <el-col :span="18">
      

      <el-row class="left-top-div" :gutter="20" >
        <el-col v-for="i in 9" :key="i" :span="2">
      </el-col>
    
        </el-row>
      <el-row class="left-div" >
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
      </el-row>

      </el-col>

     <el-col :span="6">
      <el-row class="right-div">
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
        <el-col :span="24">
        </el-col>
      </el-row>
    </el-col>
  </el-row>
</template>

<style scoped >
.left-div .el-col{
background-color: #FFFFFF;
min-height: 12rem;
margin-bottom: 1rem;
}
.right-div .el-col
{
  background-color:#FFFFFF;
  min-height: 10rem;
  margin-bottom: 1rem;
}


.left-top-div .el-col
{
min-height: 2rem;
background-color:#f21b1b;
margin-bottom: 1rem;
margin-right :10px;
margin-left :10px;
}
.top-div
{
  padding-top: 1rem;
}
</style>
