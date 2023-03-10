<template>
    <el-row :gutter="20" class="top-div">
        <el-col :span="17">

            <el-row class="left-div">
                <el-col :span="24">
                    <AvatarInfo :size="50" :showWatching="true" :time="'2023-03-08 21:09:02'"></AvatarInfo>

                    <el-divider />
                    <h2>面试题挑战</h2>
                    文章详情


                    <el-divider class="tab-divider" />


                    <el-space :size="10" :spacer="spacer">
                        <el-button icon="Pointer" text>
                            4</el-button>
                        <el-button icon="Star" text>
                            0</el-button>
                        <el-button icon="Share" text>
                            分享</el-button>
                        <el-button icon="Operation" text>
                            操作</el-button>
                    </el-space>

                </el-col>

                <el-col :span="24">
                    文章评论
                </el-col>
            </el-row>

        </el-col>

        <el-col :span="7">
            <el-row class="right-div">
                <el-col :span="24">
                    <InfoCard class="art-info-right" header="文章信息" text="更多" hideDivider="true">
                        <template #content>
                            <div>
                                <ul class="art-info-ul">
                                    <li>
                                        分类： <span>文章</span>
                                    </li>
                                    标签： <el-tag type="success">文章</el-tag>
                                    <el-tag type="info">资源</el-tag>
                                </ul>
                            </div>
                        </template>
                    </InfoCard>
                </el-col>
                <el-col :span="24">
                    <InfoCard class="art-info-right" header="目录" hideDivider="true">
                        <template #content>
                            <div>
                                <ul class="art-info-ul">
                                    <li v-for="i in 6">
                                        <el-button style="width: 100%;
                                          justify-content: left" :key="你好" type="primary" text>{{i}}：第一小结</el-button>
                                    </li>
                                </ul>
                            </div>
                        </template>
                    </InfoCard>
                </el-col>
                <el-col :span="24">
                    <InfoCard :items=items header="推荐好友" text="更多">
                        <template #item="temp">
                            <AvatarInfo />
                        </template>
                    </InfoCard>
                </el-col>
                <el-col :span="24">
                    <InfoCard :items=items header="推荐好友" text="更多">
                        <template #item="temp">
                            <AvatarInfo />
                        </template>
                    </InfoCard>
                </el-col>
                <el-col :span="24">
                    <InfoCard :items=items header="推荐好友" text="更多">
                        <template #item="temp">
                            <AvatarInfo />
                        </template>
                    </InfoCard>
                </el-col>
            </el-row>
        </el-col>
    </el-row>
</template>
<script setup>
import { h, ref } from 'vue'
import AvatarInfo from '@/components/AvatarInfo.vue'
import InfoCard from '../components/InfoCard.vue';
const spacer = h(ElDivider, { direction: 'vertical' })
const items = [{ user: "用户1" }, { user: "用户2" }, { user: "用户3" }]
</script>
<style scoped >
.art-info-ul span {
    margin-left: 1rem;
}

.art-info-ul .el-tag {
    margin-left: 1rem;
}

.art-info-ul {
    padding: 0;
}

li {
    list-style: none;
    margin-bottom: 1rem;
}

.art-info-right {
    height: 100%;
}

.left-div .el-col {
    background-color: #FFFFFF;
    min-height: 12rem;
    margin-bottom: 1rem;
}

.right-div .el-col {
    background-color: #FFFFFF;
    min-height: 10rem;
    margin-bottom: 1rem;
}


.left-top-div .el-col {
    min-height: 2rem;
    background-color: #FAFAFA;
    margin-bottom: 1rem;
    margin-left: 10px;
}

.top-div {
    padding-top: 1rem;
}

.left-top-div {
    font-size: small;
    text-align: center;
    line-height: 2rem;
}

h2 {
    margin-bottom: .5em;
    color: rgba(0, 0, 0, .85);
    font-weight: 600;
    font-size: 30px;
    line-height: 1.35;
}

.left-div .el-col {
    padding: 1.4rem 1.4rem 0.5rem 1.4rem;

}

.el-space {
    display: flex;
    vertical-align: top;
    justify-content: space-evenly;
}

.tab-divider {
    margin-bottom: 0.5rem;
}
</style>
