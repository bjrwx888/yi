<template>
    <div class="card">
<div class="card-body">
        银行卡-32659854213658541
        <p>当前余额：100钱钱</p>
        <p>状态：存款中</p>
        <p>剩余定期提款时间还剩: 2天12小时</p>
    </div>    
        <div class="div-oper">
            <el-button>存款</el-button><el-button type="danger">提款</el-button>
        </div>
    </div>
</template>
<style scoped lang="scss">
.card {
    height: 140px;
    margin: 10px 0;
    border-radius: 15px;
    box-shadow: rgba(0, 0, 0, 0.2) 0px 4px 8px 0px;
    .card-body
    {
        padding: 5px;
        height: 100px;
        background-color: #FDC830;
  color: #FFFFFF;
      border-radius: 15px 15px 0 0;
    }
    .div-oper {
        padding: 5px;
        padding-right: 10px;
        border-radius:0 0 15px 15px ;
        background-color: #FFFFFF;
        text-align: end;
        height: 40px;
    }
}
</style>