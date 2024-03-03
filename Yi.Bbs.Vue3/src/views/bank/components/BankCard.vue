<template>
    <div class="card">
<div class="card-body">
        银行卡-32659854213658541
        <p>当前余额：100钱钱</p>
        <p>状态：存款中</p>
        <p>剩余定期提款时间还剩: 2天12小时</p>
    </div>    
        <div class="div-oper">
            <el-button>存款</el-button><el-button>提款</el-button>
        </div>
    </div>
</template>
<style scoped lang="scss">
.card {
    background-color: green;
    height: 140px;
    margin: 10px 0;
    .card-body
    {
        height: 100px;
    }
    .div-oper {
        background-color: aqua;
        text-align: end;
        height: 40px;
    }
}
</style>