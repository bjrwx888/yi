<template>
    <el-row>
      <el-col
      >
        <el-card :body-style="{ padding: '0px' }" shadow="never">
          <img
            src=""
            class="image"
          />
          <div style="padding: 14px">
            <span>{{props.name}}</span>
            <div class="bottom">
              <time class="remarks">{{ props.introduction }}</time>
              <RouterLink :to="`/discuss/${props.id}`">    <el-button text class="button" type="primary">进入<el-icon><CaretRight /></el-icon></el-button> </RouterLink> 
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </template>
  
  <script  setup>
import { onMounted } from 'vue';

const props = defineProps(['name','introduction','id'])

  </script>
  
  <style>
  .remarks {
    font-size: 12px;
    color: #999;
  }
  
  .bottom {
    margin-top: 13px;
    line-height: 12px;
    display: flex;
    justify-content: space-between;
    align-items: center;
  }
  

  
  .image {
    width: 100%;
    display: block;
  }
  </style>