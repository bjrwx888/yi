import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
const useUserStore = defineStore('user', 
{
    state: () => ({
      token: '',
      name: '',
      avatar: '',
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
  