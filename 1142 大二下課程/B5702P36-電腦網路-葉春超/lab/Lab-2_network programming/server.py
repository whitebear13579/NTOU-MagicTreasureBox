import socket

SHIFT = 3
SERVER_PORT = 12000
BUFFER_SIZE = 2048


def caesar_shift(text: str, shift: int) -> str:
     # 字串偏移量計算
    result = []
    for ch in text:
        if "a" <= ch <= "z":
            base = ord("a")
            result.append(chr((ord(ch) - base + shift) % 26 + base))
        elif "A" <= ch <= "Z":
            base = ord("A")
            result.append(chr((ord(ch) - base + shift) % 26 + base))
        else:
            result.append(ch)
    return "".join(result)


def encrypt_caesar(plain_text: str) -> str:
    return caesar_shift(plain_text, SHIFT)


def decrypt_caesar(cipher_text: str) -> str:
    return caesar_shift(cipher_text, -SHIFT)


# 使用 ipv4 建立 TCP socket server，並綁定端口
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind(("", SERVER_PORT))
server_socket.listen(1)
print(f"Server is ready and listening on port {SERVER_PORT}...")

# 等待並接受來自 client 的 request，建立新的 socket 進行通訊
connection_socket, client_address = server_socket.accept()
print(f"Connection accepted from {client_address}")

encrypted_data = connection_socket.recv(BUFFER_SIZE)
if encrypted_data:
    encrypted_text = encrypted_data.decode("utf-8")
    print(f"Encrypted from client: {encrypted_text}")

    # 解密 -> 轉大寫 -> 再加密
    decrypted_text = decrypt_caesar(encrypted_text)
    upper_text = decrypted_text.upper()
    encrypted_reply = encrypt_caesar(upper_text)

    connection_socket.send(encrypted_reply.encode("utf-8"))
    print(f"Encrypted reply sent: {encrypted_reply}")

# release 兩個 socket 連線，關閉 server
connection_socket.close()
server_socket.close()
print("Server closed.")
