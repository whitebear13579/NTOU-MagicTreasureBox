# 初始化
uwu = str()
uwu = ""
uwu = "Hello"
uwu = 'Hello'
uwu = '''Triple quotes'''
uwu = """Triple quotes"""
uwu = str(123) # 將其他類型轉換為字串: "123"
uwu = "Hello " * 3 # 重複字串: "Hello Hello Hello "

# 三引號字串 (可跨多行)
text = """
This is a multi-line string.
It can span multiple lines.
Useful for long text or docstrings.
"""

# 原始字串 (Raw String) - 忽略跳脫字元
normal = "C:\\Users\\name\\file.txt" # 需要雙反斜線
raw = r"C:\Users\name\file.txt" # 使用 r 前綴，單反斜線即可
regex = r"\d+\.\d+" # 正規表達式常用

# 字串格式化
name = "Alice"
age = 20
score = 95.5

## 1. f-string (Python 3.6+) - 推薦使用
text = f"My name is {name} and I am {age} years old."
text = f"Score: {score:.2f}" # 格式化浮點數: "Score: 95.50"
text = f"{name.upper()}" # 可以包含運算式: "ALICE"
text = f"{2 + 3}" # 運算: "5"

## 2. format() 方法
text = "My name is {} and I am {} years old.".format(name, age)
text = "My name is {0} and I am {1} years old.".format(name, age) # 使用索引
text = "My name is {n} and I am {a} years old.".format(n=name, a=age) # 使用關鍵字
text = "Score: {:.2f}".format(score) # 格式化

## 3. % 格式化 (舊式，不推薦)
text = "My name is %s and I am %d years old." % (name, age)
text = "Score: %.2f" % score

## f-string 進階用法
x = 10
text = f"{x:05d}" # 補零到5位數: "00010"
text = f"{x:>10}" # 右對齊，寬度10: "        10"
text = f"{x:<10}" # 左對齊: "10        "
text = f"{x:^10}" # 置中: "    10    "

# String 可用函數
len("Hello") # 取得長度: 5
max("Hello") # 取得最大字元 (依 ASCII): 'o'
min("Hello") # 取得最小字元: 'H'

# String 可用方法 - 大小寫轉換
text = "Hello World"
text.upper() # 全部大寫: "HELLO WORLD"
text.lower() # 全部小寫: "hello world"
text.capitalize() # 首字母大寫: "Hello world"
text.title() # 每個單字首字母大寫: "Hello World"
text.swapcase() # 大小寫互換: "hELLO wORLD"

# String 可用方法 - 判斷
text = "Hello123"
text.isalpha() # 是否全為字母: False
text.isdigit() # 是否全為數字: False
text.isalnum() # 是否全為字母或數字: True
text.isspace() # 是否全為空白字元: False
text.isupper() # 是否全為大寫: False
text.islower() # 是否全為小寫: False
text.istitle() # 是否為標題格式 (每個單字首字母大寫): False
text.startswith("Hello") # 是否以指定字串開頭: True
text.endswith("123") # 是否以指定字串結尾: True
print("123".isdecimal()) # True
print("½ ".isdecimal()) # False
print("123.45".isdecimal()) # False
print("123".isnumeric()) # True
print("½ ".isnumeric()) # True - Unicode fraction Unicode
print("Ⅳ".isnumeric()) # True - Roman numeral

# String 可用方法 - 搜尋
text = "Hello World Hello"
text.find("World") # 找到回傳索引位置，找不到回傳 -1: 6
text.rfind("Hello") # 從右邊開始找: 12
text.index("World") # 找到回傳索引位置，找不到引發 ValueError: 6
text.rindex("Hello") # 從右邊開始找: 12
text.count("Hello") # 計算出現次數: 2

# String 可用方法 - 修改 (回傳新字串，原字串不變)
text = "  Hello World  "
text.strip() # 移除前後空白: "Hello World"
text.lstrip() # 移除左邊空白: "Hello World  "
text.rstrip() # 移除右邊空白: "  Hello World"
text.strip("Hd ") # 移除指定字元: "ello Worl"

text = "Hello World"
text.replace("World", "Python") # 取代字串: "Hello Python"
text.replace("l", "L", 2) # 取代前兩個: "HeLLo World"

# String 可用方法 - 分割與合併
text = "apple,banana,orange"
fruits = text.split(",") # 分割字串: ['apple', 'banana', 'orange']
text = "Hello World Python"
words = text.split() # 以空白分割: ['Hello', 'World', 'Python']
text = "apple,banana,orange,grape"
parts = text.split(",", 2) # 限制分割次數: ['apple', 'banana', 'orange,grape']

lines = "Line1\nLine2\nLine3"
line_list = lines.splitlines() # 以換行符號分割: ['Line1', 'Line2', 'Line3']

## 合併字串
fruits = ["apple", "banana", "orange"]
text = ",".join(fruits) # 用逗號合併: "apple,banana,orange"
text = " ".join(fruits) # 用空格合併: "apple banana orange"
text = "".join(fruits) # 直接合併: "applebananaorange"

# String 可用方法 - 對齊與填充
text = "Hello"
text.center(10) # 置中，寬度10: "  Hello   "
text.center(10, "*") # 用 * 填充: "**Hello***"
text.ljust(10) # 左對齊: "Hello     "
text.ljust(10, "-") # 用 - 填充: "Hello-----"
text.rjust(10) # 右對齊: "     Hello"
text.rjust(10, "*") # 用 * 填充: "*****Hello"
text.zfill(10) # 用 0 填充 (數字專用): "00000Hello"

# 字串索引與切片
text = "Hello World"
text[0] # 第一個字元: 'H'
text[-1] # 最後一個字元: 'd'
text[0:5] # 切片 (索引 0 到 4): "Hello"
text[6:] # 從索引 6 到結尾: "World"
text[:5] # 從開始到索引 4: "Hello"
text[::2] # 每隔一個字元: "HloWrd"
text[::-1] # 反轉字串: "dlroW olleH"
text[6:11] # "World"
# text[0] = 'h' # 錯誤！字串不可變

# 跳脫字元
text = "Hello\nWorld" # 換行
text = "Hello\tWorld" # Tab
text = "He said \"Hello\"" # 雙引號
text = 'It\'s a book' # 單引號
text = "Path: C:\\Users\\name" # 反斜線
text = "Line1\rLine2" # 回車 (Carriage Return)
text = "Unicode: \u0041" # Unicode 字元: "Unicode: A"

# 字串運算
text1 = "Hello"
text2 = "World"
combined = text1 + " " + text2 # 串接: "Hello World"
repeated = text1 * 3 # 重複: "HelloHelloHello"
is_in = "ell" in text1 # 檢查子字串: True
not_in = "xyz" not in text1 # True

# 字串比較
"apple" == "apple" # 相等: True
"apple" != "banana" # 不相等: True
"apple" < "banana" # 字典序比較: True (依 ASCII/Unicode)
"Apple" < "apple" # True (大寫字母 < 小寫字母)

# 字串迭代
text = "Hello"
for char in text:
    print(char) # 逐字元輸出

## 使用 enumerate 取得索引和字元
for i, char in enumerate(text):
    print(f"{i}: {char}")

# String to List
text = "Hello World"
char_list = list(text) # 轉換為字元列表: ['H', 'e', 'l', 'l', 'o', ' ', 'W', 'o', 'r', 'l', 'd']
words = text.split() # 分割為單字列表: ['Hello', 'World']

# List to String
chars = ['H', 'e', 'l', 'l', 'o']
text = "".join(chars) # "Hello"
words = ['Hello', 'World']
text = " ".join(words) # "Hello World"

# 常見應用範例
## 移除特定字元
text = "Hello, World!"
cleaned = text.replace(",", "").replace("!", "") # "Hello World"

## 統計字元出現次數
text = "Hello World"
count = text.count("l") # 3

## 檢查是否包含特定字串
if "World" in text:
    print("Found!")

## 分割 Email
email = "user@example.com"
username, domain = email.split("@") # username="user", domain="example.com"

## 處理路徑
path = "C:/Users/Alice/Documents/file.txt"
parts = path.split("/")
filename = parts[-1] # "file.txt"

## 反轉字串
text = "Hello"
reversed_text = text[::-1] # "olleH"

## 移除標點符號
import string
text = "Hello, World! How are you?"
cleaned = text.translate(str.maketrans("", "", string.punctuation)) # "Hello World How are you"

## 計算每個字元出現次數
text = "hello"
char_count = {}
for char in text:
    char_count[char] = char_count.get(char, 0) + 1
# {'h': 1, 'e': 1, 'l': 2, 'o': 1}

## 使用 Counter (更簡潔)
from collections import Counter
char_count = Counter(text) # Counter({'l': 2, 'h': 1, 'e': 1, 'o': 1})

## 檢查回文 (Palindrome)
def is_palindrome(text):
    text = text.lower().replace(" ", "")
    return text == text[::-1]

print(is_palindrome("A man a plan a canal Panama")) # True

## 移除多餘空白
text = "Hello    World   Python"
cleaned = " ".join(text.split()) # "Hello World Python"

## 首字母縮寫
phrase = "Portable Network Graphics"
acronym = "".join(word[0] for word in phrase.split()) # "PNG"

## 隱藏敏感資訊
credit_card = "1234-5678-9012-3456"
masked = "*" * 12 + credit_card[-4:] # "************3456"

## 字串對齊輸出
items = ["Apple", "Banana", "Orange"]
for item in items:
    print(f"{item:<10} - {len(item):>2} chars")
# Apple      -  5 chars
# Banana     -  6 chars
# Orange     -  6 chars


## 字串正規化 (Normalization)
import unicodedata
text = "Café"
normalized = unicodedata.normalize('NFD', text) # 分解重音符號
ascii_text = normalized.encode('ascii', 'ignore').decode() # "Cafe"

## 產生隨機字串
import random
import string
length = 8
random_string = ''.join(random.choices(string.ascii_letters + string.digits, k=length))
# 例如: "aB3xY9kL"

## 多行字串處理
lines = """Line 1
Line 2
Line 3"""
line_list = [line.strip() for line in lines.split('\n') if line.strip()]
# ['Line 1', 'Line 2', 'Line 3']
