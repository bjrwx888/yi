<script setup>
import { reactive } from "vue";
import CodeBox from "./components/CodeBox.vue"
import LableInput from "./components/LableInput.vue"
const form = reactive({
    name: "222"
});
const text1 = "> dotnet tool install -g Yi.Abp.Tool";
const text2 = "> yi-abp new Acme.BookStore -t module -csf";


</script>
<template>
    <div class="start-body">

        <div class="content">
            <div class="content-title"><span>开始</span></div>
            <div class="content-body">
                <div class="content-body-left">

                    <h4>安装 Yi.Abp.Tool</h4>
                    <p>如果之前未安装 Yi.Abp.Tool，请在命令行终端中安装：</p>

                    <CodeBox :text="text1" />
                    <h4>创建你的解决方案</h4>
                    <p>在命令行终端运行以下命令：</p>
                    <CodeBox :text="text2" />

                    <h4>配置</h4>
                    <p>您可以更改下面的解决方案配置。</p>

                    <h5>为项目命名</h5>
                    <LableInput v-model="form.name" />
                    <p>您可以使用不同级别的命名空间；例如，BookStore、Acme.BookStore 或 Acme.Retail.BookStore。</p>
                </div>
                <div class="content-body-right">

                </div>


            </div>
        </div>

    </div>
</template>
<style lang="scss" scoped>
.start-body {
    height: 100%;
    width: 100%;
    background-color: #FCFCFC;

}

.content {
    width: 80%;
    margin: 0 auto;

    &-title {
        background-color: #FCFCFC;
        height: 100px;
        display: flex;
        align-items: center;

        span {
            color: #292d33;
            font-size: 48px;
            font-weight: 700;
        }
    }

    &-body {
        height: 2000px;
        padding: 48px;
        background-color: #fff;
        border-radius: 12px;
        border: 0;
        box-shadow: 0 0 1rem rgba(0, 0, 0, .08);
        display: flex;

        &-left {
            width: 100%;
        }

        &-right {
            width: 0%;
            background-color: #409EFF;
        }



        p {
            margin-top: 0;
            margin-bottom: 1rem;
            font-family: "Poppins";
            font-size: 14px;
            font-weight: 300;
        }
        h4 {
            font-size: 20px;
            font-weight: 500;
            color: #409EFF;
            margin-bottom: .25rem;
            margin-top: 0;

        }
        h5 {
            color: #292d33;
            font-size: 16px !important;
            font-weight: 500 !important;
            margin: 5px;
        }
    }
}
</style>