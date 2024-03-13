<template>
  <div class="bank-body">
    <h2>小心谨慎选择银行机构，确保资金安全</h2>
    <div>
      <ExchangeRate :option="statisOptions" />
      <div class="div-show">
        <p class="p-rate">当前实时利息：<span>110%</span>（可获取投入的百分之110%的本金）</p>
        <el-button type="primary" @click="applying()"><el-icon>
            <AddLocation />
          </el-icon>申领银行卡</el-button>
      </div>
    </div>
    <el-divider />
    <div>
      <el-row :gutter="20">
        <el-col :span=8 v-for="item in bankCardList">
          <BankCard></BankCard>
        </el-col>
      </el-row>

    </div>
  </div>
</template>
<script setup>
import BankCard from "./components/BankCard.vue"
import ExchangeRate from "./components/ExchangeRateChart.vue"
import { getBankCardList, applyingBankCard, getInterestList } from '@/apis/bankApi'
import useAuths from '@/hooks/useAuths.js';
import { computed, ref,onMounted } from "vue";

const { isLogin } = useAuths();
const bankCardList = ref([]);

const interestList=ref([]);
const refreshData = async () => {

  if (isLogin) {
  const {data} = await getBankCardList();
  bankCardList.value=data;
  }

   const {data2:data}  = await getInterestList();
  interestList.value =data;
}

onMounted(async () => {
  await refreshData();
})
const applying = async () => {
 // await applyingBankCard();
  //刷新一下
  await refreshData();
}


const statisOptions = computed( () => {

  return {
    xAxis: {
      data: ['1时', '2时', '3时', '4时', '5时', '6时', '7时', '1时', '2时', '3时', '4时', '5时', '6时', '7时', '5时', '6时', '7时', '1时', '2时', '3时', '4时', '5时', '6时', '7时']
    },
    series: {
      data: [10, 6, 13, 11, 12, 12, 9, 10, 11, 13, 11, 8, 14, 9, 12, 12, 9, 10, 11, 13, 11, 8, 14, 9]
    },
  };
});
</script>
<style scoped lang="scss">
.bank-body {
  padding: 20px 30px;
}

h2 {
  text-align: center;

}

.div-show {
  display: flex;
  justify-content: space-between;

  .p-rate {
    span {
      font-weight: 600;
      font-size: larger;
    }

  }
}
</style>