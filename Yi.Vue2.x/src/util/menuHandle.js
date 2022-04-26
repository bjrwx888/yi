export function setTreeMenu(menuList) {
    if (menuList != null && menuList.length > 0) {

        //结果
        var res;
        //获取最小的parentId
        var minParentId = 0;
        //获取id=最小的parentId的菜单列表
        var menuList1=menuList.filter((item)=>{item.parentId==minParentId}) ;

        menuList1.forEach(element=>{
            res.push(element)
            var children=menuList.filter((item)=>{item.parentId==element.id}) ;
            if (children.length > 0) {
                setTreeChildren(menuList, children,element)
            }
        })
    }
}

function setTreeChildren(menuList, childrenList,model) {
    childrenList.forEach(element => {
        model.Childs.push(element);
        var childrenList2=menuList.filter((item)=>{item.parentId==element.id}) ;
        if (childrenList2.length > 0) {
            setTreeChildren(menuList, childrenList2,element)
        }
    });
}