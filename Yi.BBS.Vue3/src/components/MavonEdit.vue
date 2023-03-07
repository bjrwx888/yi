<template>
  <mavon-editor
    previewBackground="#ffffff"
    v-model="content"
    ref="md"
    @imgAdd="imgAdd"
    @change="change"
    :ishljs="true"
    codeStyle="atelier-cave-dark"
    :style="{'height': props.height+'px', 'z-index': props.z-index}"
    :externalLink="externalLink"
  />
</template>
  <script setup>
import { mavonEditor } from "mavon-editor";
import "mavon-editor/dist/css/index.css";
import axios from "axios";
import { ref } from "vue";
const props = defineProps(["text", "height",'z-index']);
const content = ref("");
const externalLink = {
  markdown_css: function () {
    // 这是你的markdown css文件路径
    return "/mavon-editor/markdown/github-markdown.min.css";
  },
  hljs_js: function () {
    // 这是你的hljs文件路径
    return "/mavon-editor/highlightjs/highlight.min.js";
  },
  hljs_css: function (css) {
    // 这是你的代码高亮配色文件路径
    return "/mavon-editor/highlightjs/styles/" + css + ".min.css";
  },
  hljs_lang: function (lang) {
    // 这是你的代码高亮语言解析路径
    return "/highlightjs/languages/" + lang + ".min.js";
  },
  katex_css: function () {
    // 这是你的katex配色方案路径路径
    return "/mavon-editor/katex/katex.min.css";
  },
  katex_js: function () {
    // 这是你的katex.js路径
    return "/mavon-editor/katex/katex.min.js";
  },
};

const imgAdd = (pos, file) => {};
const change = (value, render) => {
  this.myhtml2 = value;
};
</script>