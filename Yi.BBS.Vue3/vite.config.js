import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import AutoImport from 'unplugin-auto-import/vite'
import Components from 'unplugin-vue-components/vite'
import { ElementPlusResolver } from 'unplugin-vue-components/resolvers'

var CopyWebpackPlugin = require('copy-webpack-plugin');
/** @type {import('vite').UserConfig} */
export default defineConfig({
  // envDir: 'env',
  plugins: [
    vue(), 
    AutoImport({
    resolvers: [ElementPlusResolver()],
  }),
  Components({
    resolvers: [ElementPlusResolver()],
  }),
],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server:{
    port:18001,
    open:true,
    proxy:{
      '/api': {
        target: 'http://localhost:19001',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ''),
      },
    }

  }
})
