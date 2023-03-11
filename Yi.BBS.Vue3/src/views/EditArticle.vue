<template>
  <div style="width: 100%;">
    <div class="body-div">
      <el-form label-width="120px" :model="discuss" label-position="left" :rules="rules" ref="ruleFormRef">

        <el-form-item label="分类：">
          <el-radio-group v-model="radio">
            <el-radio-button label="1">文章</el-radio-button>
            <el-radio-button label="2">主题</el-radio-button>
            <el-radio-button label="3">板块</el-radio-button>
            <el-radio-button label="4">其他</el-radio-button>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="标题：" prop="title">
          <el-input placeholder="请输入" v-model="discuss.title" />
        </el-form-item>
        <el-form-item label="描述：" prop="introduction">
          <el-input placeholder="请输入" v-model="discuss.introduction" />
        </el-form-item>
        <el-form-item label="内容：" prop="content">
          {{ discuss.content + "[111]" }}
          <MavonEdit height="30rem" v-model="discuss.content" :codeStyle="codeStyle" />
        </el-form-item>
        <el-form-item label="封面：">
          <el-input placeholder="请输入" />
        </el-form-item>
        <el-form-item label="标签：" prop="types">
          <el-input placeholder="请输入" v-model="discuss.types" />
        </el-form-item>
        <el-form-item> <el-button @click="submit(ruleFormRef)" class="submit-btn"
            type="primary">提交</el-button></el-form-item>
      </el-form>
    </div>
  </div>
</template>
<script setup>
import MavonEdit from '@/components/MavonEdit.vue'
import { ref, reactive,onMounted } from 'vue';
import { useRoute } from 'vue-router'

import { add as discussAdd, update as discussUpdate,get as discussGet } from '@/apis/discussApi.js'

//数据定义
const route = useRoute()
const radio = ref(1)
const codeStyle = "atom-one-dark"
//需要添加的主题信息
const discuss = reactive({
  title: "",
  types: "",
  introduction: "",
  content: "",
  plateId: route.params.id
});
//定义效验规则
const ruleFormRef = ref(null)
const rules = reactive({
  title: [
    { required: true, message: '请输入标题', trigger: 'blur' },
    { min: 3, max: 20, message: '长度 3 到 20', trigger: 'blur' },
  ],

})
//提交按钮,需要区分操作类型
const submit = async (formEl) => {
  if (!formEl) return
  await formEl.validate(async (valid, fields) => {
    if (valid) {
      //创建
      if (route.params.operType == 'create') {
        await discussAdd(discuss)

      }
      //更新
      else if (route.params.operType == 'update') {
        await discussUpdate(route.params.id, discuss)
      }

      ruleFormRef.value.resetFields();
      discuss.plateId = route.params.id;
    }
  })

}

onMounted(async()=>{
  //如果是更新操作，需要先查询
  if (route.params.operType == 'update') {
        await loadDiscuss();
      }
})
//加载主题
const loadDiscuss=async()=>{
 const response= await discussGet(route.params.id);
 discuss.content=  response.content;
 discuss.title=  response.title;
 discuss.types=  response.types;
 discuss.introduction=  response.introduction;
}
// const clear = (source) => {
//   const keys = Object.keys(source);
//   let obj = {};
//   keys.forEach((item) => {
//     obj[item] = "";
//   });
//   Object.assign(source, obj);
// };

</script>
<style scoped>
.submit-btn {
  width: 40%;
}

.body-div {
  min-height: 1000px;
  background-color: #fff;
  margin: 1.5rem;
  padding: 1.5rem;
}
</style>