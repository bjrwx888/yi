
const useTable=()=>{
    const table=ref("bg")
    onMounted(() => {
        console.log(`the component is now mounted. table`)
      });
      return {table};
}
export default useTable;