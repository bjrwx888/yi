import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
const useUserStore = defineStore('user', 
{
    state: () => ({
      id:'',
      token: '',
      name: '登录用户',
      icon: null,
      roles: ['admin'],
      permissions: ['*:*:*']
      }),
      getters: {
      },
      actions: {
        increment() {
          this.counter+=10
        }
      },
})
export default useUserStore;
  