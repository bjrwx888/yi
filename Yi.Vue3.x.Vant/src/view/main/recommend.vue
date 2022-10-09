<template>
  <van-pull-refresh v-model="refreshing" @refresh="onRefresh">
    <van-list
      class="list"
      v-model:loading="loading"
      :finished="finished"
      finished-text="没有更多了"
      @load="onLoad"
    >
      <van-row v-for="item in list" class="row">
        <van-col span="4" class="leftCol">
          <van-image
            round
            width="3rem"
            height="3rem"
            src="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg"
          />
        </van-col>

        <van-col span="14" class="centerTitle">
       <span class="justtitle">   大白</span>
          <br />
          <span class="subtitle">一小时前</span>
        </van-col>

        <van-col span="6" class="down">
          <van-icon name="arrow-down" @click="show = true" />
        </van-col>

        <van-col class="rowBody" span="24"
          >这是第:{{
            item
          }}个,不要害怕重新开始，因为这一次你不是从头开始，而是从经验开始</van-col
        >

        <van-col
          span="8"
          v-for="item of 9"
          :key="item"
          class="imageCol"
          @click="imageShow = true"
          ><van-image
            width="100%"
            height="7rem"
            src="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg"
            radius="5"
          />
        </van-col>

        <van-col span="24" class="bottomRow">
          <van-grid direction="horizontal" :column-num="3">
            <van-grid-item icon="share-o" text="分享" />
            <van-grid-item icon="comment-o" text="评论" />
            <van-grid-item icon="good-job-o" text="点赞" />
          </van-grid>
        </van-col>
      </van-row>
    </van-list>
  </van-pull-refresh>

  <!-- 功能页面 -->
  <van-action-sheet
    v-model:show="show"
    :actions="actions"
    cancel-text="取消"
    close-on-click-action
  />

  <!-- 图片预览 -->
  <van-image-preview
    v-model:show="imageShow"
    :images="images"
    @change="onChange"
    :closeable=true
  >
    <template v-slot:index>第{{ index }}页</template>
  </van-image-preview>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { ImagePreview, Toast } from "vant";

const VanImagePreview = ImagePreview.Component;


const imageShow = ref(false);
const index = ref(0);
const images = [
  "https://fastly.jsdelivr.net/npm/@vant/assets/apple-1.jpeg",
  "https://fastly.jsdelivr.net/npm/@vant/assets/apple-2.jpeg",
];
const onChange = (newIndex: any) => {
  index.value = newIndex;
};

const list = ref<Number[]>([]);
const loading = ref(false);
const finished = ref(false);
const refreshing = ref(false);

const show = ref(false);
const actions = [
  { name: "取消关注" },
  { name: "将TA拉黑" },
  { name: "举报"}
];
const onLoad = () => {
  setTimeout(() => {
    if (refreshing.value) {
      list.value = [];
      refreshing.value = false;
    }

    for (let i = 0; i < 10; i++) {
      list.value.push(list.value.length + 1);
    }
    loading.value = false;

    if (list.value.length >= 40) {
      finished.value = true;
    }
  }, 100);
};

const onRefresh = () => {
  // 清空列表数据
  finished.value = false;

  // 重新加载数据
  // 将 loading 设置为 true，表示处于加载状态
  loading.value = true;
  onLoad();
};
</script>
<style scoped>
.list {
  background-color: #efefef;
}
.row {
  background-color: white;
  padding-top: 1rem;
  margin-bottom: 1rem;
  padding-left: 1rem;
  padding-right: 1rem;
}
.rowBody {
  text-align: left;
  background-color: white;
  min-height: 2rem;
  margin-top: 1rem;
  margin-bottom: 1rem;
}
.title {
  padding-top: 1rem;

  min-height: 3rem;
  text-align: left;
}
.leftCol {
  align-content: left;
  text-align: left;
}
.centerTitle {
  text-align: left;
}
.imageCol {
  padding: 0.1rem 0.1rem 0.1rem 0.1rem;
}
.subtitle{
color: #CBCBCB;

}

.justtitle{
  font-size: large;
}
.bottomRow{
  color: #979797;
}
.down
{
  text-align: right;
  padding-right: 0.5rem;
}
</style>