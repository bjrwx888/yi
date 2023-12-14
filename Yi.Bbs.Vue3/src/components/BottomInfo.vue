<template>
<div class="botton-div">
<a><el-icon><UserFilled /></el-icon>站长：{{configStore.author}}</a>
<a><el-icon><Search /></el-icon>{{configStore.bottom}}</a>
<a><el-icon><View /></el-icon>关于本站</a>
<a><el-icon><Message /></el-icon>建议反馈</a>
<p></p>
<a ><el-icon><Position /></el-icon>{{configStore.icp}}</a>
</div>
</template>
<script setup>
import useConfigStore from "@/stores/config";
const  configStore= useConfigStore();
</script>
<style scoped>
.el-icon
{margin: 0 0.2rem;}
a{
    margin-right: 2rem;
    line-height: 1.8rem;
}
a:hover {
    color: #40a9ff;
    cursor:pointer;
}
.botton-div
{
    background: transparent;
    color: rgba(0,0,0,.45);
    font-size: 14px;
    display: flex;
    flex-wrap: wrap;
    height: auto;
    width: auto;
    justify-content: center;
    margin: 0.5rem auto;
}
</style>