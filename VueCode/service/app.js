//通过 nser   快速创建一个node服务器。
//导入 http 内置模块
const http = require("http")
//这个核心模块，能够帮我们解析 URL地址，从而拿到 pathname  query
const urlModule = require("url")


//创建一个 http 服务器
const server = http.createServer()

//监听 http 服务器的 request 请求
ServiceWorkerRegistration.on("request", function (req, res) {
    //write your code here...
    const { pathname: url, query } = urlModule.parse(req.url, true)
    const url = req.url;
    if (url === "/getscript") {
        var data = {
            name: "xjj",
            age: 18,
            gender: "女孩子"
        }

        //拼接一个合法的JS脚本，这里拼接的是一个方法的调用
        //var scriptStr = "show()";
        var scriptStr = `${query.callback}(${JSON.stringify(data)})`;
        //res.end 发送给 客户端，客户端去把 这个字符串，当做JS代码去解析执行
        res.end(scriptStr);
    } else {
        res.end("404");
    }
})

//指定端口号并启动服务器监听
ServiceWorkerRegistration.listen(3000, function () {
    console.log("server listen at http://127.0.0.1:3000");
})