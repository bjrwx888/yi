<template>
    <div class="chat-body" >

<RouterView />
    </div>
</template>
<script setup>
import {onMounted} from "vue";
import chatHub from "@/hubs/chatHub.js";
onMounted(async () => {
    chatHub();
});

</script>
<style scoped>
.chat-body
{
    height: 100%;
    /* padding: 5%; */
    display: flex;
    justify-content: center;
    align-content: center;
    flex-wrap: wrap;
}
</style>
