<template>
<div class="bank-body">
    <h2>小心谨慎选择银行机构，确保资金安全</h2>
    <div>
<ExchangeRate/>
    </div>
    <el-divider />
    <div>
        <el-row :gutter="20">
            <el-col :span=8 v-for="i in 10">
                <BankCard ></BankCard>
            </el-col>
            </el-row>
      
    </div>
</div>
</template>
<script setup>
import BankCard from "./components/BankCard.vue"
import ExchangeRate from "./components/ExchangeRate.vue"
</script>
<style scoped lang="scss">
.bank-body
{
    padding: 10px;
}
h2{
text-align: center;

}
</style>