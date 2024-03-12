<template>
    <div class="bank-body">
        <h2>小心谨慎选择银行机构，确保资金安全</h2>
        <div>
            <ExchangeRate :option="statisOptions" />
        <div class="div-show">
            <p class="p-rate">当前实时利息：<span>110%</span>（可获取投入的百分之110%的本金）</p>
            <el-button type="primary"><el-icon><AddLocation /></el-icon>申领银行卡</el-button>
        </div>
        </div>
        <el-divider />
        <div>
            <el-row :gutter="20">
                <el-col :span=8 v-for="i in 6">
                    <BankCard></BankCard>
                </el-col>
            </el-row>

        </div>
    </div>
</template>
<script setup>
import BankCard from "./components/BankCard.vue"
import ExchangeRate from "./components/ExchangeRateChart.vue"
import {computed, ref} from "vue";
const weekList=ref([]);
const statisOptions = computed(() => {
  return {
    xAxis: {
      data: ['1时', '2时', '3时', '4时', '5时', '6时', '7时','1时', '2时', '3时', '4时', '5时', '6时', '7时','5时', '6时', '7时','1时', '2时', '3时', '4时', '5时', '6时', '7时']
    },
    series: {
      data:[10, 6, 13, 11, 12, 12, 9,10, 11, 13, 11, 8, 14, 9,12, 12, 9,10, 11, 13, 11, 8, 14, 9]
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
.div-show
{
    display: flex;
    justify-content: space-between;
    .p-rate
  {
span{
  font-weight:600;
  font-size: larger;
}

  }
}

</style>