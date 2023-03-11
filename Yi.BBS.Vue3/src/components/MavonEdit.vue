<template>
  <mavon-editor 
  :v-model="text"
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
import { ref,computed,watch } from 'vue';
const props = defineProps(['height','modelValue',"codeStyle"])
const emit=defineEmits(['update:modelValue'])
const subfield = true;

//v-model传值出去
const text = computed({
  get() {
    return props.modelValue
  },
  set(value) {
    emit('update:modelValue', value)
  }
})

const change=(value ,render)=>
{
console.log(value,"value");
  //不用给解析后htm的值，给value即可
  emit('update:modelValue', value)
}

</script>
<style scoped>
.edit
{

  width: 100%;
}
</style>