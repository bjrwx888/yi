<template>
  <mavon-editor ref='md' v-model="text" :subfield="true" :codeStyle="props.codeStyle" :ishljs="true"
    :style="{ minHeight: props.height, maxHeight: '100%' }" class="edit" @imgAdd="imgAdd">
    <!-- 引用视频链接的自定义按钮 -->
    <template v-slot:left-toolbar-after>
      <!--点击按钮触发的事件是打开表单对话框-->

      <el-dropdown :hide-on-click='false'>
        <el-button aria-hidden="true" class="op-icon fa" title="表情包">
          😊
        </el-button>
        <template #dropdown>
          <el-dropdown-menu >
            <el-dropdown-item>
              <table border="1">
                <tr>
                  <td @click="text+='😊'">😊</td>
                  <td>😊</td>
                  <td>😊</td>
                </tr>
                <tr>
                  <td>😊</td>
                  <td>😊</td>
                  <td>😊</td>
                </tr>
                <tr>
                  <td>😊</td>
                  <td>😊</td>
                  <td>😊</td>
                </tr>
              </table>

            </el-dropdown-item>

          </el-dropdown-menu>
        </template>
      </el-dropdown>





    </template>

  </mavon-editor>
</template>


<script setup>
import { mavonEditor } from 'mavon-editor'
import 'mavon-editor/dist/css/index.css'
import { ref, computed, watch, onMounted } from 'vue';
import { upload } from '@/apis/fileApi'
const md = ref(null);
const props = defineProps(['height', 'modelValue', "codeStyle"])
const emit = defineEmits(['update:modelValue'])

// //v-model传值出去
const text = computed({
  get() {
    return props.modelValue
  },
  set(value) {
    emit('update:modelValue', value)
  }
})

//图片上传
const imgAdd = async (pos, $file) => {
  // 第一步.将图片上传到服务器.
  var formdata = new FormData();
  formdata.append('file', $file);
  const response = await upload(formdata)
  const url = `${import.meta.env.VITE_APP_BASEAPI}/file/${response[0].id}`;
  console.log(url)
  md.value.$img2Url(pos, url);

}
</script>
<style scoped>
.edit {

  width: 100%;
}
</style>