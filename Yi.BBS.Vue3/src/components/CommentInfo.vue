<template>
  <div v-for="item in commentList" :key="item.id">
    第一级：{{ item.content }},id{{ item.id }}  <el-button @click="replay(item.id,item.id)">回复</el-button>
    <div v-for="children in item.children" :key="children.id">
      -->> 第二级 {{ children.content }}, 评论者{{ children.createUser.nick }},
      被回复者{{ children.commentedUser.nick }},id{{ children.id }}
      <el-button @click="replay(children.id,item.id)">回复</el-button>
    </div>
  </div>
  <el-input v-model="form.content"></el-input>
  <el-button @click="addComment">发布</el-button>
</template>
<script setup>
import { onMounted, reactive, ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { getListByDiscussId, add } from "@/apis/commentApi.js";
//数据定义
const route = useRoute();
const router = useRouter();
const commentList = ref([]);
const query = reactive({});

const form = reactive({
  content: "",
  discussId: route.params.discussId,
  query,
  parentId: 0,
  rootId: 0,
});
onMounted(async () => {
  await loadComment();
});
const loadComment = async () => {
  const response = await getListByDiscussId(route.params.discussId, query);
  commentList.value = response.data.items;
};
const addComment = async () => {
  await add(form);
  await loadComment();
};
const replay= async(parentId,rootId)=>{
    form.parentId=parentId;
     form.rootId=rootId;
     await addComment();
}
</script>
