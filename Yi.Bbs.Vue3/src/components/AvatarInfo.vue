<template >
    <div class="avatar">
        <div class="avatar-left">
            <el-avatar :size="props.size" :src="iconUrl" />

            <div v-if="props.isSelf">
                <div class="nick"> {{ userInfo.nick }}</div>
            </div>

            <div v-if="!props.isSelf">

                <div class="nick" :class="{ mt_1: props.time != 'undefined' }"> {{ userInfo.nick }}</div>
                <div class="remarks" v-if="props.time"> {{ props.time }}</div>
                <div class="remarks">
                    <slot name="bottom" />
                </div>
            </div>
            <div class="info" v-if="!props.isSelf">
                <el-tag class="ml-2" type="warning">V8</el-tag>
                <el-tag class="ml-2" type="danger">会员</el-tag>
            </div>
        </div>


        <el-button v-if="props.showWatching" type="primary" size="default" icon="Plus">关注</el-button>

    </div>
</template>
<script setup>
import useUserStore from '@/stores/user'
import { reactive, watch, onMounted, computed, ref } from 'vue';
//userInfo
//{icon,name,role,id},根据判断userInfo是否等于未定义，来觉得是当前登录用户信息，还是其他人信息
const props = defineProps(['size', 'showWatching', 'time', 'userInfo', 'isSelf'])
const userStore = useUserStore();
const userInfo = reactive({
    icon: "",
    nick: "",
    role: [],
    id: ""
});
const iconUrl=ref('/src/assets/logo.ico');
const iconUrlHandler = () => {
    if (userInfo.icon == null || userInfo.icon == undefined || userInfo.icon == '') {

        return '/src/assets/logo.ico';
    }
    return `${import.meta.env.VITE_APP_BASEAPI}/file/${userInfo.icon}`;
}

watch(userStore, (n) => {
    if (props.userInfo == undefined) {
        userInfo.nick = n.name;
    }

})

watch(() => props, (n) => {
    Init();
}, { deep: true })

onMounted(() => {
    Init();
})

const Init = () => {
    //使用传入值
    if (props.userInfo != undefined) {
        userInfo.icon = props.userInfo.icon;
        userInfo.nick = props.userInfo.nick;
        userInfo.role = props.userInfo.role;
        userInfo.id = props.userInfo.id;
           iconUrl.value=iconUrlHandler(userInfo.icon)
    }

    //使用当前登录用户
    else {
     
        userInfo.icon = userStore.icon;
        userInfo.nick = userStore.name;
        userInfo.role = userStore.role;
        userInfo.id = userStore.id;
        iconUrl.value=userInfo.icon;
        
    }
}

</script>
<style scoped>
.mt_1 {
    margin-top: 0.5rem;
}

.nick {
    font-weight: bold;
}

.info {
    margin-top: 0.6rem;
    margin-left: 1rem;
}

.info .el-tag {
    margin-right: 1rem;
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