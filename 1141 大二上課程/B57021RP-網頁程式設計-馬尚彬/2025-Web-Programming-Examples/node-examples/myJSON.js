const http = require('http');
let myObj = {name: 'Stephen Curry', team: "GSW"};

http.createServer(function (req, res) {
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(myObj));
}).listen(8080);