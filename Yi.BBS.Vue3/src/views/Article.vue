<template>
  <div style="width: 90%; min-width: 1200px">
    <!-- <div style="width: 1200px;"> -->
    <el-row :gutter="20" class="top-div">
      <el-col :span="5">
        <el-row class="art-info-left">
          <el-col :span="24">
            <InfoCard header="主题信息" text="展开" hideDivider="true">
              <template #content>
                <el-button style="width: 100%; margin-bottom: 0.8rem"
                @click="loadDiscuss"
                  >首页</el-button
                >
                <el-button
                 v-hasPer="['bbs:article:add']"
                  @click="addArticle(0)"
                  type="primary"
                  style="width: 100%; margin-bottom: 0.8rem; margin-left: 0"
                  >添加子文章</el-button
                >
          <!--目录在这里 -->
          <TreeArticleInfo :data="articleData" @remove="delArticle" @update="updateArticle"
          @create="addNextArticle" @handleNodeClick="handleNodeClick" />
              </template>
            </InfoCard>
          </el-col>
          <el-col :span="24">
            <InfoCard :items="items" header="推荐好友" text="更多">
              <template #item="temp">
                <AvatarInfo />
              </template>
            </InfoCard>
          </el-col>
          <el-col :span="24">
            <InfoCard :items="items" header="推荐好友" text="更多">
              <template #item="temp">
                <AvatarInfo />
              </template>
            </InfoCard>
          </el-col>
        </el-row>
      </el-col>

      <el-col :span="14">
        <el-row class="left-div">
          <el-col :span="24">
            <AvatarInfo
              :size="50"
              :showWatching="true"
              :time="'2023-03-08 21:09:02'"
            ></AvatarInfo>

            <el-divider />
            <h2>{{ discuss.title }}</h2>
            <ArticleContentInfo :code="discuss.content"></ArticleContentInfo>

            <el-divider class="tab-divider" />

            <el-space :size="10" :spacer="spacer">
              <el-button icon="Pointer" text> 4</el-button>
              <el-button icon="Star" text> 0</el-button>
              <el-button icon="Share" text> 分享</el-button>
              <el-button icon="Operation" text> 操作</el-button>
            </el-space>
          </el-col>

          <el-col :span="24" class="comment"> 文章评论 </el-col>
        </el-row>
      </el-col>

      <el-col :span="5">
        <el-row class="right-div">
          <el-col :span="24">
            <InfoCard
              class="art-info-right"
              header="文章信息"
              text="更多"
              hideDivider="true"
            >
              <template #content>
                <div>
                  <ul class="art-info-ul">
                    
                    <li>
                      <el-button
                        type="primary"
                        size="default"
                        @click="updateHander(route.params.discussId)"
                        >编辑</el-button
                      >
                      <el-button
                        style="margin-left: 1rem"
                        type="danger"
                        @click="delHander(route.params.discussId)"
                        >删除</el-button
                      >
                    </li>
                    <li>分类： <span>文章</span></li>
                    标签：
                    <el-tag type="success">文章</el-tag>
                    <el-tag type="info">资源</el-tag>
                  </ul>
                </div>
              </template>
            </InfoCard>
          </el-col>
          <el-col :span="24">
            <InfoCard class="art-info-right" header="目录" hideDivider="true">
              <template #content>
                <div>
                  <ul class="art-info-ul">
                    <li v-for="(item, i) in catalogueData" :key="i">
                      <el-button
                        style="width: 100%; justify-content: left"
                        type="primary"
                        text
                        >{{ `${i + 1} ： ${item}` }}</el-button
                      >
                    </li>
                  </ul>
                </div>
              </template>
            </InfoCard>
          </el-col>
          <el-col :span="24">
            <InfoCard :items="items" header="推荐好友" text="更多">
              <template #item="temp">
                <AvatarInfo />
              </template>
            </InfoCard>
          </el-col>
          <el-col :span="24">
            <InfoCard :items="items" header="推荐好友" text="更多">
              <template #item="temp">
                <AvatarInfo />
              </template>
            </InfoCard>
          </el-col>
        </el-row>
      </el-col>
    </el-row>
  </div>
</template>
<script setup>
import { h, ref, onMounted } from "vue";
import AvatarInfo from "@/components/AvatarInfo.vue";
import InfoCard from "@/components/InfoCard.vue";
import ArticleContentInfo from "@/components/ArticleContentInfo.vue";

import TreeArticleInfo from "@/components/TreeArticleInfo.vue";
import { useRoute, useRouter } from "vue-router";

import { get as discussGet, del as discussDel } from "@/apis/discussApi.js";
import {all as articleall,del as articleDel}  from "@/apis/articleApi.js";
//数据定义
const route = useRoute();
const router = useRouter();
const spacer = h(ElDivider, { direction: "vertical" });
const items = [{ user: "用户1" }, { user: "用户2" }, { user: "用户3" }];


//子文章数据
const articleData =ref([]);
//主题内容
const discuss = ref({});

//目录数据
const catalogueData = ref([]);

//子文章初始化
const loadArticleData=async()=>
{
    articleData.value=  await  articleall(route.params.discussId);
}

//主题初始化
const loadDiscuss = async () => {
  discuss.value = await discussGet(route.params.discussId);
  ContentHander();
};
//加载文章及目录
const ContentHander=()=>{
 //加载目录
 var reg = /(#{1,6})\s(.*)/g;
  var myArray = discuss.value.content.match(reg);
  if (myArray != null) {
    catalogueData.value = myArray.map((x) => {
      return x.replace(/#/g, "").replace(/\s/g, "");
    });
  }

}
//添加树型子文章
const addArticle = (parentArticleId) => {
  //跳转路由
  var routerPer = {
    path: "/editArt",
    query: {
      operType: "create",
      artType: "article",
      discussId: route.params.discussId,
      parentArticleId: parentArticleId,
    },
  };
  router.push(routerPer);
};

//删除主题
const delHander = async (ids) => {
  ElMessageBox.confirm(`确定是否删除编号[${ids}]的主题吗?`, "警告", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(async () => {
    const response = await discussDel(ids);
    //删除成功后，跳转到主页
    router.push("/index");
    ElMessage({
      type: "success",
      message: "删除成功",
    });
  });
};
//更新操作
const updateHander = (discussId) => {
  //跳转路由
  var routerPer = {
    path: "/editArt",
    query: {
      operType: "update",
      artType: "discuss",
      discussId: discussId,
    },
  };
  router.push(routerPer);
};

//跳转添加子菜单
const addNextArticle=(node,data)=>{
  //跳转路由
  var routerPer = {
    path: "/editArt",
    query: {
      operType: "create",
      artType: "article",
      discussId: data.discussId,
      parentArticleId: data.id,
    },
  };
  router.push(routerPer);
}

//跳转更新子菜单
const updateArticle=(node,data)=>{
  //跳转路由
  var routerPer = {
    path: "/editArt",
    query: {
      operType: "update",
      artType: "article",
      discussId: data.discussId,
      parentArticleId: data.parentId,
      articleId:data.id
    },
  };
  router.push(routerPer);
}
//单机节点
const handleNodeClick=(data)=>{
  discuss.value.content=data.content;
  ContentHander();
}
//删除子文章
const delArticle=( node,data)=>{
    ElMessageBox.confirm(`确定是否删除编号[${data.id}]的子文章吗?`, "警告", {
    confirmButtonText: "确认",
    cancelButtonText: "取消",
    type: "warning",
  }).then(async () => {

   await articleDel(data.id);
   await loadArticleData();
  
    ElMessage({
      type: "success",
      message: "删除成功",
    });
  });

}
onMounted(async () => {
  await loadDiscuss();
  await loadArticleData();
});
</script>
<style scoped >
.comment {
  height: 40rem;
}
.art-info-left .el-col {
  margin-bottom: 1rem;
}

.art-info-ul span {
  margin-left: 1rem;
}

.art-info-ul .el-tag {
  margin-left: 1rem;
}

.art-info-ul {
  padding: 0;
  margin: 0;
}

li {
  list-style: none;
  margin-bottom: 0.5rem;
}

.art-info-right {
  height: 100%;
}

.left-div .el-col {
  background-color: #ffffff;
  min-height: 12rem;
  margin-bottom: 1rem;
}

.right-div .el-col {
  background-color: #ffffff;
  min-height: 10rem;
  margin-bottom: 1rem;
}

.left-top-div .el-col {
  min-height: 2rem;
  background-color: #fafafa;
  margin-bottom: 1rem;
  margin-left: 10px;
}

.top-div {
  padding-top: 1rem;
}

.left-top-div {
  font-size: small;
  text-align: center;
  line-height: 2rem;
}

h2 {
  margin-bottom: 0.5em;
  color: rgba(0, 0, 0, 0.85);
  font-weight: 600;
  font-size: 30px;
  line-height: 1.35;
}

.left-div .el-col {
  padding: 1.4rem 1.4rem 0.5rem 1.4rem;
}

.el-space {
  display: flex;
  vertical-align: top;
  justify-content: space-evenly;
}

.tab-divider {
  margin-bottom: 0.5rem;
}
</style>
