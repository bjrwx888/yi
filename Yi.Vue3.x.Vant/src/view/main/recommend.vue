<template>
  <van-pull-refresh v-model="refreshing" @refresh="onRefresh">
    <van-list
      class="list"
      v-model:loading="loading"
      :finished="finished"
      finished-text="没有更多了"
      @load="onLoad"
    >
      <van-row v-for="item in list" :key="item" class="row">
        
     
        <van-col span="6" class="leftCol">
          <van-image
            round
            width="3rem"
            height="3rem"
            src="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg"
          />
        </van-col>

        <van-col span="12">
            标题
            <br>
            副标题
        </van-col>

        <van-col span="6">
            <van-icon name="arrow-down" @click="show=true" />
        </van-col>



        <van-col class="rowBody" span="24">这是第:{{ item }}个</van-col>

        <van-col span="24">
          <van-grid direction="horizontal" :column-num="3">
            <van-grid-item icon="share-o" text="分享" />
            <van-grid-item icon="comment-circle-o" text="评论" />
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
  description="这是一段描述信息"
  close-on-click-action
/>
</template>

<script setup lang="ts">
import { ref } from "vue";
const list = ref<Number[]>([]);
const loading = ref(false);
const finished = ref(false);
const refreshing = ref(false);
const show = ref(false);
    const actions = [
      { name: '选项一' },
      { name: '选项二' },
      { name: '选项三', subname: '描述信息' },
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
  }, 1000);
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
  background-color: #EFEFEF;
}
.row {
  background-color: white;
  padding-top: 1rem;
  margin-bottom: 1rem;
  padding-left: 1rem;
}
.rowBody {
  background-color: white;
  min-height: 7rem;
}
.title {
    padding-top: 1rem;

  min-height: 3rem;
  text-align: left;
}
.leftCol
{
align-content: left;
text-align: left;
}
</style>