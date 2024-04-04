import { defineStore } from "pinia";

const chatStore = defineStore("chat", {
  state: () => ({
    userList: [],
  }),
//   getters: {
//     userListData: (state) => state.userList,
//   },
  actions: {
    // 设置在线总数
    setUserList(value) {
      this.userList = value;
    },
    addUser(user)
    {

      this.userList.push(user);
    },
    delUser(userId)
    {

      this.userList = this.userList.filter(obj => obj.userId != userId);
    }
  },
});

export default chatStore;
