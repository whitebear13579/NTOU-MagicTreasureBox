// demo_rating_api.js
const http = require('http');
const ratings = [];

http.createServer((req, res) => {
    res.setHeader('Content-Type', 'application/json');
    
    if (req.method === 'GET' && req.url === '/ratings') {
        return res.end(JSON.stringify(ratings));
    }

    // 輸入範例{"score":5}
    if (req.method === 'POST' && req.url === '/ratings') {
        let body = '';
        req.on('data', chunk => body += chunk);
        req.on('end', () => {
            const score = JSON.parse(body).score;
            if (!score || score < 1 || score > 5) {
                res.statusCode = 400;
                return res.end(JSON.stringify({ error: '評分須在 1-5 分間' }));
            }
            ratings.push({ score, time: new Date() });
            res.statusCode = 201;
            res.end(JSON.stringify(ratings));
        });
    }
}).listen(3000, () => console.log('運行於 http://localhost:3000'));
