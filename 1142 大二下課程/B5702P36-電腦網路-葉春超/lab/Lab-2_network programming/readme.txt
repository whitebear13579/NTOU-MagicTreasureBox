Computer Network Lab 2
01357101 CS2B 黃翊宏（1人）

作業說明：
這份作業是一個簡易的加密聊天室，client side輸入的訊息首先會經過凱薩加密（偏移量 = +3）後傳送給 server side。
server side 收到密文後，會先進行解密，將明文轉為大寫後，再用相同的偏移量重新加密回傳給 client side。
client side 收到回傳的密文後再解密，最後顯示轉換完成的大寫結果。

執行方式：
請先執行 server.py，再執行 client.py，接著依照提示輸入訊息。
最後觀察 server.py 與 client.py 的輸出訊息即可。

網路傳輸設定：
- 傳輸協定：TCP（SOCK_STREAM）
- 伺服器連接埠：12000
- 用戶端連線主機：localhost
