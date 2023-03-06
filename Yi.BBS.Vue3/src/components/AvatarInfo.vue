<template >

<div class="avatar">
    <div class="avatar-left">
        <el-avatar :size="props.size" />
         <div>
       <div> 大白子</div>
       <div class="remarks"> 2022-03-05 23:33:20</div>
    </div>
    </div>

    <el-button type="primary" size="default" icon="Plus" >关注</el-button>

</div>
</template>
<script setup>
const props = defineProps(['size'])
</script>
<style scoped>
.el-icon
{
color: white;

}
.avatar
{
    display: flex;
    justify-content:space-between;
}
.avatar-left
{
    display: flex;
    justify-content:flex-start;
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