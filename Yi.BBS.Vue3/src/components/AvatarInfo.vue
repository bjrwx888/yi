<template >
    <div class="avatar">
        <div class="avatar-left">
            <el-avatar :size="props.size" :src="props.src ?? '/src/assets/logo.ico'" />

            <div>
                <div class="nick" :class="{mt_1: props.time!='undefined'}"> 大白子</div>
                <div class="remarks" v-if="props.time"> {{props.time}}</div>
                <div class="remarks">  <slot name="bottom" /></div>
            </div>

            <div class="info">
                <el-tag class="ml-2" type="warning">V6</el-tag>
                <el-tag class="ml-2" type="danger">管理员</el-tag>
            </div>
        </div>
        
        <el-button v-if="props.showWatching" type="primary" size="default" icon="Plus">关注</el-button>

    </div>
</template>
<script setup>
import { onMounted } from 'vue';

const props = defineProps(['size', 'src','showWatching','time'])

</script>
<style scoped>
.mt_1
{
  margin-top: 0.5rem;  
}
.nick
{
    font-weight:bold;
}
.info
{
    margin-top: 0.6rem;
    margin-left: 1rem;
}
.info .el-tag
{
    margin-right:1rem;
}
.el-icon {
    color: white;

}

.avatar {
    display: flex;
    justify-content: space-between;
}

.avatar-left {
    display: flex;
    justify-content: flex-start;
    align-items: center;
}

.el-avatar {
    margin-right: 1.2rem;
}

.remarks {
    padding-top: 0.5rem;
    color: #8C8C8C;
}
</style>