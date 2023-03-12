<template>
      <el-tree
                  :data="props.data"
                  :props="defaultProps"
                  @node-click="handleNodeClick"
                  :expand-on-click-node="false"
                  node-key="id"
                >

                <template #default="{ node, data }">
        <span class="custom-tree-node">
          <span>{{ node.label }}</span>
          <span>
            <a style="color: #f56c6c; margin-left: 8px" @click="$emit('remove',node, data)" 
  
            > 删除 </a>
          </span>
        </span>
      </template>
                </el-tree>
</template>
<script setup>

const props = defineProps(['data'])
//数据定义
//子文章数据
// const articleData =ref([]);
//树形子文章选项
const defaultProps = {
  children: "children",
  label: "name",
};
// //子文章初始化
// const loadArticleData=async()=>
// {
//     articleData.value=  await  articleall(route.params.discussId);
// }
//点击事件
const handleNodeClick = (data) => {
  console.log(data);
};
</script>
