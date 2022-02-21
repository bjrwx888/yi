<template>
  <el-row>
    <el-col :span="18">
      <el-row>
        <el-col v-for="index of 11" :key="index" :span="2"><div class="head-1">999</div></el-col>
         <el-col :span="2"><div class="head-1-right">  <el-button type="primary">设置标签</el-button></div></el-col>
      </el-row>

      <el-row>
        <el-col :span="24"><div class="head-2">000</div></el-col>
      </el-row>

      <el-row v-for="index of 4" :key="index" >
        <el-col :span="8" 
          ><div class="bg-white grid-content">666</div></el-col
        >
        <el-col :span="8" 
          ><div class="bg-white grid-content">777</div></el-col
        >
        <el-col :span="8" 
          ><div class="bg-white grid-content">888</div>
        </el-col>
      </el-row>



      <el-row >
        <el-col :span="24" 
          ><div ><el-button size="large" type="primary" class="foot-button">点击查看更多</el-button></div></el-col
        >
      </el-row>




    </el-col>

    <el-col :span="6">
      <el-row>
        <el-col :span="24"
          ><div class="bg-white right-1">111</div></el-col
        >
      </el-row>
      <el-row>
        <el-col :span="24" 
          ><div class="bg-white  right-2">222</div></el-col
        >
      </el-row>
      <el-row>
        <el-col :span="24" 
          ><div class="bg-white right-3">333</div></el-col
        >
      </el-row>
    </el-col>
  </el-row>
</template>

<script lang="ts">
</script>

<style scoped>
.head-1 {
  height: 30px;
  padding: 8px 8px 4px 8px;
  background: #ffffff;
}
.head-1-right{
    padding: 5px 0 0 0;
}
.foot-button{
  margin-top: 50px;

}
.head-2 {
  height: 40px;
  padding: 16px 24px 16px 24px;
  text-align: left;
  background: #ffffff;
}
.right-1{
min-height: 165px;
}
.right-2{
  min-height: 467px;
}
.right-3{
    min-height: 111px;
}

.bg-white {
  background: #ffffff;
}
.el-row {
  margin-bottom: 10px;
}
.el-col {
  border-radius: 4px;
  padding-left: 8px;
  padding-right: 8px;
}
.grid-content {
  border-radius: 4px;
  height: 256px;
}
</style>
