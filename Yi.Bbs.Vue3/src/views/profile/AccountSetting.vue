<template>
  <div class="account-box">
    <div class="title">您可以绑定以下第三方账号</div>
    <div class="image-list">
      <div class="item" @click="handleQQLogin">
        <div class="image">
          <img src="@/assets/login_images/qq-setting.png" alt="" />
        </div>
        <div class="text">QQ</div>
      </div>
      <div class="item" @click="handleGiteeLogin">
        <div class="image">
          <img src="@/assets/login_images/gitee-setting.png" alt="" />
        </div>
        <div class="text">Gitee</div>
      </div>
    </div>
    <div class="title">使用以下任意方式都可以登录到您的意社区账号</div>
    <div class="table">
      <yi-table
        :table-data="tableData"
        :options="tableOptions"
        :columns="tableColumn"
      ></yi-table>
    </div>
  </div>
</template>

<script setup>
import { computed, onMounted } from "vue";
import { getOtherAuthInfo } from "@/apis/auth.js";
import YiTable from "@/components/yi-table/index.vue";
import useUserStore from "@/stores/user";

const userInfo = useUserStore();
const tableOptions = computed(() => {
  return {
    showPagination: false,
    border: true,
  };
});

const tableColumn = [
  {
    type: "index",
    minWidth: "50",
    label: "序号",
    align: "center",
  },
  {
    prop: "timeInterval",
    label: "绑定账号信息",
    minWidth: "110",
    align: "center",
  },
  {
    prop: "timeInterval",
    label: "详情",
    minWidth: "110",
    align: "center",
  },
  {
    prop: "timeInterval",
    label: "绑定时间",
    minWidth: "110",
    align: "center",
  },
  {
    prop: "timeInterval",
    label: "状态",
    minWidth: "110",
    align: "center",
  },
  {
    label: "操作",
    align: "center",
    minWidth: "200",
    fixed: "right",
    buttons: [
      {
        name: "绑定",
        type: "text",
        command: "edit",
      },
    ],
  },
];

onMounted(async () => {
  const { data } = await getOtherAuthInfo({ userId: userInfo.id });
});

const handleQQLogin = () => {
  window.open(
    "https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=102087446&redirect_uri=https://ccnetcore.com/auth/qq&state=true&scope=get_user_info",
    undefined,
    "width=500,height=500,left=50,top=50"
  );
};

const handleGiteeLogin = () => {
  window.open(
    "https://gitee.com/oauth/authorize?client_id=949f3519969adc5cfe82c209b71300e8e0868e8536f3d7f59195c8f1e5b72502&redirect_uri=https%3A%2F%2Fccnetcore.com%2Fauth%2Fgitee&response_type=code",
    undefined,
    "width=500,height=500,left=50,top=50"
  );
};
</script>

<style lang="scss" scoped>
.account-box {
  .title {
    font-size: 20px;
    font-weight: bold;
  }
  .image-list {
    margin: 10px;
    display: flex;
    .item {
      cursor: pointer;
      display: flex;
      flex-direction: column;
      align-items: center;
      margin-right: 20px;
      .image {
        width: 30px;
        height: 30px;
        img {
          width: 100%;
          height: 100%;
        }
      }
    }
  }
  .table {
    margin-top: 10px;
    :deep(.yi-table) {
      width: 100%;
      height: 200px;
    }
  }
}
</style>
