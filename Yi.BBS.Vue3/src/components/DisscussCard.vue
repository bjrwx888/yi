<template>
    <el-card class="box-card" shadow="never">

        <div class="card-header">
          <AvatarInfo />
        </div>



        <div class=" item item-title "> <el-link size="100" :underline="false" @click="enterDiscuss">{{props.title}}</el-link></div>
        <div class=" item item-description">{{props.introduction}}</div>
        <div class=" item item-tag"><el-tag v-for="i in 4" :key="i">教程</el-tag></div>
        <div class=" item item-bottom">
            <el-space :size="10" :spacer="spacer">
                <div class="item-description">
                   {{ props.createTime }}
                </div>


                <el-button icon="Pointer" text>
                    点赞</el-button>
                <el-button icon="Star" text>
                    收藏</el-button>

                <el-button icon="ChatDotRound" text>
                    评论</el-button>


            </el-space>
        </div>
    </el-card>
</template>
<script setup>
import { h, ref } from 'vue'
import { useRouter } from 'vue-router'
import AvatarInfo from './AvatarInfo.vue';

const props = defineProps(['title','introduction','createTime'])

const router = useRouter()
const spacer = h(ElDivider, { direction: 'vertical' })
const enterDiscuss = () => {
    router.push("/article")
}
</script>
<style scoped>
.item-bottom .el-icon {
    margin-right: 0.4rem;
}

.card-header {
    display: flex;
    margin-bottom: 1.5rem;
    align-items: center;
}

.item {
    font-size: 14px;
    margin-bottom: 18px;
}

.box-card {
    width: 100%;
    min-height: 15rem;
}

.item-title {
    /* font-size: var(--el-font-size-large); */
}

.item-description {
    font-size: var(--el-font-size-small);
    color: #8C8C8C;
}

.item .el-tag {
    margin-right: 1rem;
}

.ml-2 {
    margin-left: 1.2rem;
}

.el-link {
    font-size: initial;
    font-weight: 700;
}
</style>