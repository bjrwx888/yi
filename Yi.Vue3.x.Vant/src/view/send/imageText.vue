<template>
  <transition name="van-slide-right">
    <div v-show="visible">
      <van-sticky>
        <van-row class="head-row">
          <van-col span="3">
            <router-link to="/recommend">
              <van-icon name="arrow-left" size="1.5rem" />
            </router-link>
          </van-col>
          <van-col span="18"><span>发图文</span></van-col>
          <van-col span="3" @click="send">发布</van-col>
        </van-row>
      </van-sticky>

      <van-cell-group>
        <van-field
          rows="5"
          autosize
          type="textarea"
          v-model="content"
          label-width="0"
          :show-word-limit="true"
          maxlength="500"
          placeholder="每一天，都是为了下一天"
        />
      </van-cell-group>
      <van-row class="body-row">
        <van-col span="10">
          <van-icon name="share-o" size="1.5rem" /><span>发布到去其他</span>
        </van-col>
        <van-col span="4"></van-col>
        <van-col span="10"
          ><span>选择更多人看到</span>
          <van-icon name="arrow" size="1.2rem" />
        </van-col>
      </van-row>

      <van-divider />
      <van-row>
        <van-col class="img-col" span="24">
          <van-uploader :after-read="afterRead" v-model="fileList" multiple />
        </van-col>
      </van-row>
    </div>
  </transition>
</template>
<script setup lang="ts">
import { ref, onMounted, reactive, toRefs } from "vue";
import { ArticleEntity } from "@/type/interface/ArticleEntity.ts";
import fileApi from "@/api/fileApi.ts";
import articleApi from "@/api/articleApi.ts";
import { useRouter } from 'vue-router'
const router = useRouter();
const form = reactive<ArticleEntity>({
  title: "",
  content: "",
  images: [],
  isDeleted: false,
});

const { images, content } = toRefs(form);
const fileList = ref([]);
const visible = ref<boolean>(false);
onMounted(() => {
  visible.value = true;
});
const afterRead = (file: any) => {

  file.status = "uploading";
  file.message = "上传中...";
  var formData = new FormData();
  //一个文件
  if(file.length==undefined)
  {
    formData.append("file", file.file);
  }
else
{
//多个文件
file.forEach((f:any) => {
  formData.append("file", f.file);
});
}
  fileApi.upload("image", formData)
    .then((response: any) => {
      images.value.push(...response.data);
      file.status = "done";
      file.message = "成功";
    })
};

const send = () => {
    articleApi.add(form).then((response:any)=>{
      router.push({ path: '/recommend'});
    })

};
</script>
<style scoped>
.head-row {
  background-color: #f8f8f8;

  padding: 2rem 1rem 1.5rem 1rem;
}

.head-row span {
  font-size: large;
}

.van-field-5-label {
  display: none;
}

.body-row {
  margin-top: 1rem;
}

.preview-cover {
  position: absolute;
  bottom: 0;
  box-sizing: border-box;
  width: 100%;
  padding: 4px;
  color: #fff;
  font-size: 12px;
  text-align: center;
  background: rgba(0, 0, 0, 0.3);
}

.van-uploader {
  margin: 0 1.2rem 0 1.2rem;
}

.img-col {
  text-align: left;
}
</style>