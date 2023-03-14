<template>
      <el-tree
                  :data="data"
                  :props="props.defaultProps"
                  @node-click="handleNodeClick"
                  :expand-on-click-node="false"
                  node-key="id"
                >

                <template #default="{ node, data }">
        <span class="custom-tree-node">
          <span>{{data.name}}</span>
          <span>
            
            <a style="color: #409EFF; margin-left: 8px" @click="$emit('create',node, data)" 
  
  > + </a>
            <a style="color: #409EFF; margin-left: 8px" @click="$emit('update',node, data)" 
  
  > 编辑 </a>
            <a style="color: #f56c6c; margin-left: 8px" @click="$emit('remove',node, data)" 
  
            > 删除 </a>
          </span>
        </span>
      </template>
                </el-tree>
</template>
<script setup>

const props = defineProps(['data'])
const emits= defineEmits(["handleNodeClick"])
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
  emits('handleNodeClick',data)
};
</script>
