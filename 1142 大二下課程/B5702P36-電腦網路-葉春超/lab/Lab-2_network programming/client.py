import socket

SHIFT = 3
SERVER_NAME = "localhost"
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


# 使用 ipv4 建立 TCP socket client，並連接 server
client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
client_socket.connect((SERVER_NAME, SERVER_PORT))
message = input("Input lowercase sentence: ")
encrypted_message = encrypt_caesar(message)
client_socket.send(encrypted_message.encode("utf-8"))

# 這裡接收來自 server 回傳的結果
encrypted_response = client_socket.recv(BUFFER_SIZE).decode("utf-8")
final_response = decrypt_caesar(encrypted_response)

print(f"Encrypted sent to server: {encrypted_message}")
print(f"Encrypted response from server: {encrypted_response}")
print(f"Final uppercase result: {final_response}")

client_socket.close()
