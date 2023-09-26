<template>
    <el-row>
   <el-col  :span="4"> <TableList></TableList></el-col>
  <el-col  :span="20">   <FieldList class="fileList"></FieldList></el-col>
    </el-row>
</template>
<script setup>
import FieldList from './components/FieldList.vue'
import TableList from './components/TableList.vue'
</script>
<style scoped>
.fileList{

   
}
.tableList{

}
</style>