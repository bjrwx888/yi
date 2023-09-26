
const useTable=()=>{
    const table=ref("数据表选择")
    onMounted(() => {
        console.log(`the component is now mounted. table`)
      });
      return {table};
}
export default useTable;