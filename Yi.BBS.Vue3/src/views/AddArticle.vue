<template>
    <div class="body-div">
        <el-form  label-width="120px" label-position="left">

    <el-form-item label="分类：">
      <el-input  placeholder="请输入" />
    </el-form-item>
    <el-form-item label="标题：">
      <el-input  placeholder="请输入" />
    </el-form-item>
    <el-form-item label="描述：">
      <el-input placeholder="请输入" />
    </el-form-item>
    <el-form-item label="内容：">
      <el-input placeholder="请输入" />
    </el-form-item>
    <el-form-item label="分面：">
      <el-input placeholder="请输入" />
    </el-form-item>
    <el-form-item label="标签：">
      <el-input placeholder="请输入" />
    </el-form-item>
    <el-form-item >  <el-button class="submit-btn" type="primary">提交</el-button></el-form-item>
    </el-form>
  </div>
</template>
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