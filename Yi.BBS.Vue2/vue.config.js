var CopyWebpackPlugin = require('copy-webpack-plugin');
const path = require('path');
module.exports = {
    //    configureWebpack:{
    //     plugins: [
    //         // ...
    //         new CopyWebpackPlugin([{
    //             from: 'node_modules/mavon-editor/dist/highlightjs',
    //             to: path.resolve(__dirname, './dist/highlightjs'), // 插件将会把文件导出于/dist/highlightjs之下
    //         }, {
    //             from: 'node_modules/mavon-editor/dist/markdown',
    //             to: path.resolve(__dirname, './dist/markdown'), // 插件将会把文件导出于/dist/markdown之下
    //         }, {
    //             from: 'node_modules/mavon-editor/dist/katex', // 插件将会把文件导出
    //             to: path.resolve(__dirname, './dist/katex')
    //         }])
    //         // ...
    //     ],
    // },
    // publicPath: './',
    transpileDependencies: [
        'vuetify'
    ],
    devServer: {
        port: 6789,
        open: true,
        https: false,
        host: "localhost",
        proxy: {
            [process.env.VUE_APP_BASE_API]: {
                target: process.env.VUE_APP_SERVICE_URL,
                changeOrigin: true,
                pathRewrite: {
                    ['^' + process.env.VUE_APP_BASE_API]: ''
                }
            },
        }
    },
    lintOnSave: false, //关闭格式检查
    productionSourceMap: false
}