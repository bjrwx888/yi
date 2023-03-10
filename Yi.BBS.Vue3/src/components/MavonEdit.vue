<template>
  <mavon-editor 
  :subfield="subfield" 
  :codeStyle="props.codeStyle" 
  :ishljs="true"
  :style="{minHeight:props.height,maxHeight:'100%'}"
  class="edit"
  @change="change"
  ></mavon-editor>
</template>
<script setup>
// Local Registration
import { mavonEditor } from 'mavon-editor'
import 'mavon-editor/dist/css/index.css'
import { ref } from 'vue';
const props = defineProps(['height','modelValue',"codeStyle"])
const emit=defineEmits(['update:modelValue'])
const subfield = true;
const change=(value ,render)=>
{
  emit('update:modelValue', render)
}

</script>
<style scoped>
.edit
{

  width: 100%;
}
</style>