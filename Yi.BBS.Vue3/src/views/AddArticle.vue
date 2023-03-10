<template>
  <div style="width: 100%;">
    <div class="body-div">
        <el-form  label-width="120px" label-position="left">

    <el-form-item label="分类：">
      <el-radio-group v-model="radio">
      <el-radio-button label="1" >文章</el-radio-button>
      <el-radio-button label="2"  >主题</el-radio-button>
      <el-radio-button label="3"  >板块</el-radio-button>
      <el-radio-button label="4" >其他</el-radio-button>
    </el-radio-group>
    </el-form-item>
    <el-form-item label="标题：">
      <el-input  placeholder="请输入" />
    </el-form-item>
    <el-form-item label="描述：">
      <el-input placeholder="请输入" />
    </el-form-item>
    <el-form-item label="内容：">
    
<MavonEdit height="30rem" v-model="text" :codeStyle="codeStyle"/>
    </el-form-item>
    <el-form-item label="封面：">
      <el-input placeholder="请输入" />
    </el-form-item>
    <el-form-item label="标签：">
      <el-input placeholder="请输入" />
    </el-form-item>
    <el-form-item >  <el-button class="submit-btn" type="primary">提交</el-button></el-form-item>
    </el-form>
  </div>
  </div>
</template>
<script setup>
import MavonEdit from '@/components/MavonEdit.vue'
import { ref } from 'vue';
const text=ref("")
const radio=ref(1)
const codeStyle=ref("atom-one-dark");
</script>
<style scoped>
.submit-btn
{
    width: 40%;
}
.body-div
{
    min-height: 1000px;
    background-color: #fff;
    margin: 1.5rem;
    padding: 1.5rem;
}

</style>