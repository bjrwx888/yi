<template>
  <div class="point-box">
    <div class="left">
      <div class="icon"><img :src="userImageSrc" alt="" /></div>
    </div>
    <div class="center">
      <div class="top">
        <el-tag effect="light" :type="userLimit.type">
          {{ userLimit.label }}
        </el-tag>
        <el-tag effect="light" type="success">{{ pointsData.level }}</el-tag>
      </div>
      <div class="bottom">
        {{ pointsData.userName }}
      </div>
    </div>
    <div class="right">
      <div class="follow">
        <el-icon class="el-icon--right"><Plus /></el-icon>
        <div class="text">关注</div>
      </div>
    </div>
  </div>
</template>

<script setup name="PointsRanking">
import { defineProps, computed } from "vue";

const props = defineProps({
  pointsData: {
    type: Array,
    default: () => [],
  },
});

const statusTypeList = [
  {
    label: "正常",
    value: 0,
    type: "success",
  },
  {
    label: "危险",
    value: 1,
    type: "warning",
  },
  {
    label: "已禁止",
    value: 2,
    type: "danger",
  },
];

const getStatusInfo = (type) => {
  return statusTypeList.filter((item) => item.value === type)[0];
};

const userLimit = computed(() => getStatusInfo(props.pointsData.userLimit));
const userImageSrc = computed(
  () => import.meta.env.VITE_APP_BASEAPI + "/file/" + props.pointsData.icon
);
</script>

<style lang="scss">
.point-box {
  width: 100%;
  height: 50px;
  display: flex;
  justify-content: space-around;
  .left {
    flex: 1;
    .icon {
      width: 40px;
      height: 40px;
      img {
        width: 100%;
        height: 100%;
        border-radius: 50%;
      }
    }
  }
  .center {
    flex: 4;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding: 0 10px;
    .top {
      > .el-tag {
        margin-right: 10px;
      }
    }
    .bottom {
      width: 200px;
      color: #252933;
      margin-left: 5px;
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
    }
  }
  .right {
    flex: 2;
    display: flex;
    align-items: center;
    .follow {
      display: flex;
      justify-content: flex-start;
      align-items: center;
      font-size: 16px;
      color: #1171ee;
    }
  }
}
</style>
