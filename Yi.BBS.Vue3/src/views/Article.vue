<template>
    <el-row :gutter="20" class="top-div">
        <el-col :span="17">

            <el-row class="left-div">
                <el-col :span="24">
                    作者信息
                    <el-divider />
                    <h2>面试题挑战</h2>
                    文章详情
         

                    <el-divider  class="tab-divider" />
                  

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
                </el-col>
                <el-col :span="24">
                </el-col>
                <el-col :span="24">
                </el-col>
                <el-col :span="24">
                </el-col>
                <el-col :span="24">
                </el-col>
            </el-row>
        </el-col>
    </el-row>
</template>
<script setup>
import { h, ref } from 'vue'
const spacer = h(ElDivider, { direction: 'vertical' })
</script>
<style scoped >
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
    display: flex ;
    vertical-align: top ;
    justify-content: space-evenly ;
}
.tab-divider
{
    margin-bottom: 0.5rem;
}
</style>
