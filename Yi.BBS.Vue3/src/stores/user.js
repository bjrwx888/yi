import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
export const userStore = defineStore('user', 
{
    state: () => ({
        counter: 0,
      }),
      getters: {
        doubleCount: (state) => state.counter*2,
      },
      actions: {
        increment() {
          this.counter+=10
        }
      },
}
  )
  