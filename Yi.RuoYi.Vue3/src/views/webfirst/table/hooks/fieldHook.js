
const useFieldHooks=()=>{
    const field=ref("zd")
    onMounted(() => {
        console.log(`the component is now mounted. field`)
      });
return {field};
}
export default useFieldHooks;