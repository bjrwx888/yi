<template>
字段:{{ field }}
</template>
<script setup>
import useField from '../hooks/useField'
const {field}=useField();
</script>