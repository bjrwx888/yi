<template>
<van-image
round
:width="width"
:height="height"
:src="url+(src??'0')"
/>
</template>

<script setup lang="ts">
import { type } from 'os';
import { ref } from 'vue'
defineProps<{ src: {type:string,default:'null',required:false},
width:{type:string,default:'3rem',required:false},
height:{type:string,default:'3rem',required:false},

}>()
const url = `${import.meta.env.VITE_APP_BASE_API}/file/`;
</script>
<style scoped>
.col-body
{
text-align: center;

}
.col-body .van-icon
{
    margin-bottom: 0.6rem;
}
p{
font-size: large;
font-weight:bold;
}
</style>
