<template>
    <div>
        <div class="md" v-html="code"></div>
        {{ code }}
        <button @click="code='1234'">你好</button>
    </div>
</template>
  
<script setup>
import { marked } from 'marked';
import hljs from "highlight.js";
import javascript from 'highlight.js/lib/languages/javascript';
import 'highlight.js/styles/monokai-sublime.css';
import { onMounted,ref } from 'vue';
const code =ref( "```javascript\nfunction(){\n\tconsole.log(123)\n}\n```\n"
+"# 你好世界\n"+
"## 是我的"
)

// const props = defineProps(['code'])

onMounted(() => {
    // code.value=props.code;
    marked.setOptions({
        renderer: new marked.Renderer(),
        highlight: function (code) {
            return hljs.highlightAuto(code.value).value;
        },
        pedantic: false,
        gfm: true,
        tables: true,
        breaks: false,
        sanitize: false,
        smartLists: true,
        smartypants: false,
        xhtml: false
    }
    );
    code.value = marked(code.value)
    console.log( code.value," code.value");
})

</script>
