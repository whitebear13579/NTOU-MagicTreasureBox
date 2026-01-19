# 輸出多個變數
a, b, c = 1, 2, 3
print(a, b, c) # 預設以空白分隔
print(a, b, c, sep='#') # 改以 # 分隔
# 格式化輸出
name = "Alice"
age = 30
print(f"Name:{name}, Age:{age}")
# 控制浮點數輸出
pi = 3.141592653589793
print(f"Pi: {pi:.2f}") # 保留小數點後兩位
# 控制輸出不換行
print("Hello Kitty", end=' ') # 以空格結束，不換行
print("Hello World", end="") # 以空字串結束，不換行
# 字串陣列輸出
uwu = [ 'a','b','c','d','e' ]
print(f"uwu: {', '.join(uwu)}")
print(*uwu) # 解包輸出陣列元素，預設以空白分隔
print(*uwu, sep="##") # 解包輸出陣列元素，以 ## 分隔
# 整數陣列格式化輸出
num = [1, 2, 3, 4, 5]
print(*num)
# 多維字串陣列輸出 
matrix_str = [ ['a','b','c'], ['d','e','f'], ['g','h','i'] ]
for row in matrix_str:
    print(*row)
# 多維整數陣列輸出
matrix_int = [ [1,2,3], [4,5,6], [7,8,9] ]
for row in matrix_int:
    print(*row)