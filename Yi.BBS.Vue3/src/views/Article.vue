<template>
    <div style="width: 90%;min-width: 1200px;">
        <!-- <div style="width: 1200px;"> -->
        <el-row :gutter="20" class="top-div">

            <el-col :span="5">
                <el-row class="art-info-left">
         
                        <el-col :span="24">
                        <InfoCard  header="主题信息" text="展开" hideDivider="true">
                            <template #content>
                                <el-button style="width: 100%;margin-bottom: 0.8rem;">首页</el-button>
                                <el-tree :data="data" @node-click="handleNodeClick" />
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

            <el-col :span="14">

                <el-row class="left-div">
                    <el-col :span="24">
                        <AvatarInfo :size="50" :showWatching="true" :time="'2023-03-08 21:09:02'"></AvatarInfo>

                        <el-divider />
                        <h2>{{discuss.title}}</h2>
                       <ArticleContentInfo :code="discuss.content"></ArticleContentInfo>

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

                    <el-col :span="24" class="comment">
                        文章评论
                    </el-col>
                </el-row>

            </el-col>

            <el-col :span="5">
                <el-row class="right-div">
                    <el-col :span="24">
                        <InfoCard class="art-info-right" header="文章信息" text="更多" hideDivider="true">
                            <template #content>
                                <div>

                                  

                                    <ul class="art-info-ul">
                                        <li>

                                          <RouterLink :to='`/editArt/discuss/update/${route.params.discussId}`'> <el-button type="primary" size="default" >编辑</el-button></RouterLink>
                                        </li>
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
                                        <li v-for="(item,i) in catalogueData" :key="i">
                                            <el-button style="width: 100%;justify-content: left" type="primary" text>{{ `${i+1} ： ${item}` }}</el-button>
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
                </el-row>
            </el-col>
        </el-row>
    </div>
</template>
<script setup>
import { h, ref ,onMounted } from 'vue'
import AvatarInfo from '@/components/AvatarInfo.vue'
import InfoCard from '@/components/InfoCard.vue';
import ArticleContentInfo from  '@/components/ArticleContentInfo.vue'
import { useRoute } from 'vue-router'

import {get as discussGet} from '@/apis/discussApi.js';

//数据定义
const route=useRoute()
const spacer = h(ElDivider, { direction: 'vertical' })
const items = [{ user: "用户1" }, { user: "用户2" }, { user: "用户3" }]
const handleNodeClick = (data) => {
    console.log(data)
}
const data = [
    {
        label: 'HTML',
        children: [
            {
                label: 'Level two 1-1',
                children: [
                    {
                        label: 'Level three 1-1-1',
                    },
                ],
            },
        ],
    },
    {
        label: 'HTML5',
        children: [
            {
                label: 'Level two 2-1',
                children: [
                    {
                        label: 'Level three 2-1-1',
                    },
                ],
            },
            {
                label: 'Level two 2-2',
                children: [
                    {
                        label: 'Level three 2-2-1',
                    },
                ],
            },
        ],
    },
    {
        label: 'XHTML',
        children: [
            {
                label: 'Level two 3-1',
                children: [
                    {
                        label: 'Level three 3-1-1',
                    },
                ],
            },
            {
                label: 'Level two 3-2',
                children: [
                    {
                        label: 'Level three 3-2-1',
                    },
                ],
            },
        ],
    },
    {
        label: 'Xcss',
    },
    {
        label: 'CSS3',
    },
    {
        label: 'Bootstrap 5',
    },
    {
        label: 'Tcp/ip',
    }
]
//主题内容
const discuss=ref({});

//目录数据
const catalogueData=ref([]);

//主题初始化
const loadDiscuss=async()=>{
    discuss.value= await discussGet(route.params.discussId);
    //加载目录
    var reg = /(#{1,6})\s(.*)/g;
    var myArray =discuss.value.content.match(reg);
if(myArray!=null)
{
    catalogueData.value= myArray.map(x=>{ return x.replace(/#/g,"").replace(/\s/g,'')})
}
   
  
}


 onMounted(async()=>{
   await loadDiscuss();

})
</script>
<style scoped >
.comment
{
    height:40rem;
}
.art-info-left .el-col {
    margin-bottom: 1rem;
}

.art-info-ul span {
    margin-left: 1rem;
}

.art-info-ul .el-tag {
    margin-left: 1rem;
}

.art-info-ul {
    padding: 0;
    margin: 0;
}

li {
    list-style: none;
    margin-bottom: 0.5rem;
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
