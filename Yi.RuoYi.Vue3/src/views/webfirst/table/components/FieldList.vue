<template>
字段:{{ field }}
</template>
<script setup>
import useFieldHook from '../hooks/fieldHook'
const {field}=useFieldHook();
</script>