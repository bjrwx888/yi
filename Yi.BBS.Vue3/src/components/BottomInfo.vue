<template>
<div class="botton-div">
<a><el-icon><UserFilled /></el-icon>站长：橙子</a>
<a><el-icon><Search /></el-icon>YiFramework意框架</a>
<a><el-icon><View /></el-icon>关于本站</a>
<a><el-icon><Message /></el-icon>建议反馈</a>
<p></p>
<a><el-icon><Position /></el-icon>2023 <span style="color: #40a9ff ;">意社区</span> | 赣ICP备xxxxxx号-4</a>
</div>
</template>
<script setup>
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
    margin: 1rem auto;
}
</style>