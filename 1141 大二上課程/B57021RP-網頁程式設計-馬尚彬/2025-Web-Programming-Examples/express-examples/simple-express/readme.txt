
A. 安裝與基本測試：

1. 安裝node.js https://nodejs.org/zh-tw/download/
2. 解壓縮myapp.zip，以命令列(windows於搜尋列輸入cmd)進入到myapp目錄
3. 觀看與大致了解myapp的結構(可參考投影片說明)
4. 在命令列輸入npm install
4. 在命令列輸入npm start
5. 瀏覽器網址輸入http://localhost:3000/index.html (若打http://localhost:3000也會導向此頁)
6. 網頁上點擊每個button看看效果

===

B. 了解與修改前端：

1. 進入public目錄，以VS Code開啟index.html
2. 大致了解index.html引用了三個東西：
   - CSS樣式：stylesheets/style.css
   - jQuery JavaScript函式庫：https://code.jquery.com/jquery-3.6.0.min.js
   - 自己的前端JavaScript：javascripts/myapp.js 
3. 大致了解index.html的四個button分別呼叫getTest()等四個function
4. 進入public\javascripts目錄，以VS Code開啟myapp.js
5. 大致了解getTest()等四個function都是用jQuery的ajax相關API去呼叫後端API：
(可參考投影片說明)
   - getTest()：以http GET呼叫網址為 http://localhost:3000/hello 的後端service
   - postTest()：以http POST呼叫網址為 http://localhost:3000/ 的後端service
   - putTest()：以http PUT呼叫網址為 http://localhost:3000/hello 的後端service
   - deleteTest()：以http DELETE呼叫網址為 http://localhost:3000/hello 的後端service
6. 你可在function內容中加一些變化，如加上window.alert("...")。
7. 瀏覽器重刷畫面後，再點擊相關的button就可以看到前端改變的效果。

===

C. 了解與修改後端：   

1. 進入myapp\routes目錄，以VS Code開啟index.js
2. 大致了解index.js設定了五個路由：
   - 第一個是GET搭配根路徑，會顯示index.html
   - 第二個是GET搭配路徑/hello，會回傳JSON: {"message": "Hello World!"} (express會把JavaScrip物件轉成可以於網路傳遞的JSON字串內容，可留意JSON內的屬性名稱會加上雙引號，字串值也是雙引號)
   - 第三個是POST搭配根路徑，會回傳JSON: {"status": "OK!"}
   - 第四個是PUT搭配路徑/hello，會回傳JSON: {"message": "Hello New World!"}
   - 第五個是DELETE搭配路徑/hello，會回傳JSON: {"status": "Done!"}
3. 你可以嘗試用Postman測試這些服務端點。
4. 你可以嘗試修改回傳的JavaScript物件內容，或改成陣列。
5. 重新執行node.js程式：先Ctrl+C中斷目前程式，再npm start一次。
6. 瀏覽器重刷畫面後，再點擊相關的button就可以看到後端改變的效果。
   