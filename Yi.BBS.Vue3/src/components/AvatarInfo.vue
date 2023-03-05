<template >

<div class="avatar">
    <el-avatar :size="props.size" />


    <div>
       <div> 大白子</div>
       <div class="remarks"> 2022-03-05 23:33:20</div>
    </div>
</div>
</template>
<script setup>
const props = defineProps(['size'])
</script>
<style scoped>
.avatar
{
    display: flex;
}
.el-avatar
{
    margin-right: 1.2rem;
}
.remarks
{
    padding-top: 0.5rem;
    color: #8C8C8C;
}
</style>