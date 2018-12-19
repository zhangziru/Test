//这是 main.js 是我们项目的JS入口文件

//1.导入 Jquery（在页面中，引入Jquer，直接使用标签引入即可，这里不行）
//方式1：使用require
//方式2：import $ from 'jquery'  //意思：从node_modules中导入 jquery 这个包。然后用 $ 符号，这个变量名 来接收。
//【注意1】方式2 的写法 等价于 Node中的写法： const $ = require('jquery')
//【注意2】import $ from 'jquery' 导入包，是固定语法，是ES6中导入模块的方式。
//【注意3】由于 ES6 的代码，太高级了，浏览器解析不了，所以，import $ from 'jquery' 这行代码会报错。
//【注意4】我们使用 const $ = require('jquery')，node 语法来导入包，也会报错。
import $ from 'jquery'

$(function(){
    $("li:odd").css("backgroundColor","lightblue");
    $("li:even").css("backgroundColor","green")
})

//webpack 可以做什么事情？
//1、webpack 能够处理 JS 文件的互相依赖关系
//2、webpack 能够处理JS的兼容问题，把 高级的、浏览器不是别的语法，转为 低级的，浏览器能正常识别的语法。

//运行的命令格式：webpack 要打包的文件的路径 打包好的输出文件的路径