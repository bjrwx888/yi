<template>
  <mavon-editor
    previewBackground="#ffffff"
    v-model="myhtml2"
    ref="md"
    @imgAdd="$imgAdd"
    @change="change"
    :ishljs="true"
    codeStyle="atelier-cave-dark"
    style="height: 800px; z-index: 1"
    :externalLink="externalLink"
  />
</template>
<script>
// 该组件中注释掉的代码为局部注册的方式。
import { mavonEditor } from "mavon-editor";
import "mavon-editor/dist/css/index.css";
import axios from "axios";
export default {
  props: ["myhtml2"],
  data: function () {
    return {
      content: this.myhtml2,
      html: "",
      baseurl: "",
      configs: {},
      externalLink: {
        markdown_css: function() {
          // 这是你的markdown css文件路径
          return '/mavon-editor/markdown/github-markdown.min.css';
        },
        hljs_js: function() {
          // 这是你的hljs文件路径
          return '/mavon-editor/highlightjs/highlight.min.js';
        },
        hljs_css: function(css) {
          // 这是你的代码高亮配色文件路径
          return '/mavon-editor/highlightjs/styles/' + css + '.min.css';
        },
        hljs_lang: function(lang) {
          // 这是你的代码高亮语言解析路径
          return '/highlightjs/languages/' + lang + '.min.js';
        },
        katex_css: function() {
          // 这是你的katex配色方案路径路径
          return '/mavon-editor/katex/katex.min.css';
        },
        katex_js: function() {
          // 这是你的katex.js路径
          return '/mavon-editor/katex/katex.min.js';
        },
      },
    };
  },
  components: {
    mavonEditor,
  },
  mounted() {
    //使用初始化
    this.baseurl = process.env.VUE_APP_BASE_API;
  },
  methods: {
    // 将图片上传到服务器，返回地址替换到md中
    $imgAdd(pos, $file) {
      var formdata = new FormData();
      formdata.append("file", $file);
      //将下面上传接口替换为你自己的服务器接口
      axios({
        url: this.baseurl + "/File/OnPostUploadImage",
        method: "post",
        data: formdata,
        headers: { "Content-Type": "multipart/form-data" },
      }).then((resp) => {
        var myurl = resp.data.data[0].url;

        this.$refs.md.$img2Url(pos, myurl.replace("#", this.baseurl));
      });
    },
    change(value, render) {
      // render 为 markdown 解析后的结果
      this.myhtml2 = value;
      this.$emit("giveData", value);
    },
  },
};
</script>