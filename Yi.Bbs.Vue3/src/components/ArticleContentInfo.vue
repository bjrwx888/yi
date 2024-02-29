<style scoped>
::v-deep pre{
    padding: 0;
    margin-bottom: 0;
}
::v-deep .header
{
    background-color: #409eff;
    color: white;
    height: 30px;
    display: flex;
    justify-content: flex-end;
    padding-top: 10px;
}
::v-deep .code{
padding: 0px 10px;

    font-size: 14px;
    line-height: 22px;
    border-radius: 4px;

}
::v-deep .language
{

}
::v-deep .copy
{
margin: 0px 10px;

}
</style>
<template>
    <div>
        <div class="markdown-body" v-html="outputHtml"></div>
    </div>
</template>
  
<script setup>
import { marked } from 'marked';

import hljs from "highlight.js";
//可以设置加载样式切换主题
import '@/assets/atom-one-dark.css'
import '@/assets/github-markdown.css'
import { ref, watch } from 'vue';



const outputHtml = ref("")
const props = defineProps(['code'])
const codeHandler = (code,language) => {
    const codeIndex = parseInt(Date.now() + "") + Math.floor(Math.random() * 10000000);
    // 格式化第一行是右侧language和 “复制” 按钮；

    if (code) {
        try {
            // 使用 highlight.js 对代码进行高亮显示
            const preCode =hljs.highlightAuto(code).value;
            // 将代码包裹在 textarea 中，由于防止textarea渲染出现问题，这里将 "<" 用 "&lt;" 代替，不影响复制功能
            let html = `<pre class='hljs'><div class="header"><span class="language">${language}</span><span class="copy">复制代码</span></div><code class="code">${preCode}</code></pre>`;

            return html;
                      //<textarea style="position: absolute;top: -9999px;left: -9999px;z-index: -9999;" id="copy${codeIndex}">${code.replace(/<\/textarea>/g, "&lt;/textarea>")}</textarea>
        } catch (error) {
            console.log(error);
        }
    }

}

watch(props, (n, o) => {
    marked.setOptions({
        renderer: new marked.Renderer(),
        highlight: function (code,language) {
           return codeHandler(code,language);
           // return hljs.highlightAuto(code).value;
        },
        pedantic: false,
        gfm: true,//允许 Git Hub标准的markdown
        tables: true,//支持表格

        breaks: true,
        sanitize: false,
        smartLists: true,
        smartypants: false,
        xhtml: false,
        smartLists: true,
    }
    );
    //需要注意代码块样式
    outputHtml.value = marked(n.code);
}, { immediate: true, deep: true })

</script>

