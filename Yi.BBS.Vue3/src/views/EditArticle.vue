<template>
  <div style="width: 100%">
    <div class="body-div">
      <el-form label-width="120px" :model="editForm" label-position="left" :rules="rules" ref="ruleFormRef">
        <el-form-item label="分类：">
          <el-radio-group v-model="radio">
            <el-radio-button label="discuss">主题</el-radio-button>
            <el-radio-button label="article">文章</el-radio-button>
            <el-radio-button label="plate">板块</el-radio-button>
            <el-radio-button label="orther">其他</el-radio-button>
          </el-radio-group>
        </el-form-item>
        <el-form-item v-if="route.query.artType == 'article'" label="子文章名称：" prop="name">
          <el-input placeholder="请输入" v-model="editForm.name" />
        </el-form-item>
        <el-form-item v-else label="标题：" prop="title">
          <el-input placeholder="请输入" v-model="editForm.title" />
        </el-form-item>
        <el-form-item label="描述：" prop="introduction">
          <el-input placeholder="请输入" v-model="editForm.introduction" />
        </el-form-item>
        <el-form-item label="内容：" prop="content">
          <MavonEdit height="30rem" v-model="editForm.content" :codeStyle="codeStyle" />
        </el-form-item>
        <el-form-item label="封面：">
          <el-input placeholder="请输入" />
        </el-form-item>
        <el-form-item label="标签：" prop="types">
          <el-input placeholder="请输入" v-model="editForm.types" />
        </el-form-item>
        <el-form-item>
          <el-button @click="submit(ruleFormRef)" class="submit-btn" type="primary">提交</el-button></el-form-item>
      </el-form>
    </div>
  </div>
</template>
<script setup>
import MavonEdit from "@/components/MavonEdit.vue";
import { ref, reactive, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";

import {
  add as discussAdd,
  update as discussUpdate,
  get as discussGet,
} from "@/apis/discussApi.js";

import {
  add as articleAdd,
  update as articleUpdate,
  get as articleGet,
} from "@/apis/articleApi.js";

//数据定义
const route = useRoute();
const router = useRouter();
const radio = ref(route.query.artType);
const codeStyle = "atom-one-dark";

//整个页面上的表单
const editForm = reactive({
  title: "",
  types: "",
  introduction: "",
  content: "",
  name: ""
});

//组装主题内容： 需要更新主题信息
const discuss = {

};

//组装文章内容：需要添加的文章信息
const article = {
};

//定义效验规则
const ruleFormRef = ref(null);
const rules = reactive({
  title: [
    { required: true, message: "请输入标题", trigger: "blur" },
    { min: 3, max: 40, message: "长度 3 到 20", trigger: "blur" },
  ],
  name: [
    { required: true, message: "请输入子文章名称", trigger: "blur" },
  ],
  content: [
    { required: true, message: "请输入内容", trigger: "blur" },
    { min: 10, max: 4000, message: "长度 10 到4000", trigger: "blur" },
  ],
});
//提交按钮,需要区分操作类型
const submit = async (formEl) => {
  if (!formEl) return;
  await formEl.validate(async (valid, fields) => {
    if (valid) {
      //dicuss主题处理
      if (route.query.artType == "discuss") {


        discuss.title = editForm.title;
        discuss.types = editForm.types;
        discuss.introduction = editForm.introduction;
        discuss.content = editForm.content;
        discuss.plateId = discuss.plateId ?? route.query.plateId

        //主题创建
        if (route.query.operType == "create") {
          const response = await discussAdd(discuss);

          ElMessage({
            message: `[${discuss.title}]主题创建成功！`,
            type: 'success',
        })
          var routerPer = { path: `/article/${response.data.id}` };
          router.push(routerPer);
        }
        //主题更新
        else if (route.query.operType == "update") {
          await discussUpdate(route.query.discussId, discuss);

          ElMessage({
            message: `[${discuss.title}]主题更新成功！`,
            type: 'success',
        })
          var routerPer = { path: `/article/${route.query.discussId}` };
          router.push(routerPer);
        }
      }

      //artcle文章处理
      else if (route.query.artType == "article") {
        //组装文章内容：需要添加的文章信息
        article.content = editForm.content;
        article.name = editForm.name;
        article.discussId = route.query.discussId;
        article.parentId = route.query.parentArticleId
        //文章创建
        if (route.query.operType == "create") {
          const response = await articleAdd(article);
          ElMessage({
            message: `[${article.name}]文章创建成功！`,
            type: 'success',
        })
          var routerPer = { path: `/article/${route.query.discussId}/${response.data.id}` };
          router.push(routerPer);
        }
        //文章更新
        else if (route.query.operType == "update") {
          await articleUpdate(route.query.articleId, article);
          ElMessage({
            message: `[${article.name}]文章更新成功！`,
            type: 'success',
        })
          var routerPer = { path: `/article/${route.query.discussId}/${route.query.articleId}` };
          router.push(routerPer);
        }
      }
      //添加成功后跳转到该页面
      // var routerPer = { path: `/discuss/${discuss.plateId}` };
      // router.push(routerPer);
      // ruleFormRef.value.resetFields();
      // discuss.plateId = route.query.plateId;
    }
  });
};

onMounted(async () => {
  //如果是更新操作，需要先查询
  if (route.query.operType == "update") {

    //更新主题
    if (route.query.artType == "discuss") {
      await loadDiscuss();

      //更新文章
    } else if (route.query.artType == "article") {
      await loadArticle();
    }
  }
});
//加载主题
const loadDiscuss = async () => {
  const response = await discussGet(route.query.discussId);
  const res = response.data
  editForm.content = res.content;
  editForm.title = res.title;
  editForm.types = res.types;
  editForm.introduction = res.introduction;
  discuss.plateId = res.plateId;
};
//加载文章
const loadArticle = async () => {
  const response = await articleGet(route.query.articleId);
  const res = response.data
  editForm.content = res.content;
  editForm.name = res.name;
  editForm.discussId = res.discussId;
};
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