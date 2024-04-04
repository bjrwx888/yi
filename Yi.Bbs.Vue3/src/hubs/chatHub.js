import signalR from "@/utils/signalR";
import useChatStore from "@/stores/chat";

const receiveMsg = (connection) => {
    const chatStore = useChatStore();
    //上线用户
    connection.on("liveUser", (user) => {
        chatStore.addUser(user);
    });
    //下线用户
    connection.on("offlineUser", (userId) => {
        chatStore.delUser(userId);
    });
    //接受其他用户消息
    connection.on("receiveMsg", (type, content) => {

    });
    //用户状态-正在输入中，无
    connection.on("userStatus", (type) => {

    });
};
export default () => {
    signalR.start(`chat`, receiveMsg);
}

