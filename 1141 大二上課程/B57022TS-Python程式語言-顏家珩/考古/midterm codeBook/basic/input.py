# 輸入多個變數（字串）
x, y, z = input().split()
# 輸入多個變數（自動轉型整數）
x, y, z = map(int, input().split())
# 輸入一個 list (array) （字串）
arr = input().split()
# 輸入一個 list (array) （自動轉型整數）
arr = list(map(int, input().split()))
# 輸入一個多維 list (array) （字串）
matrix = []
n = 3 # 假設有 n 行
for i in range(n):
    row = input().split()
    matrix.append(row)
# 輸入一個多維 list (array) （自動轉型整數）
matrix = []
n = 3 # 假設有 n 行
for i in range(n):
    row = list(map(int, input().split()))
    matrix.append(row)
# 輸入一串指令（有整數有字串）（以 HW 6-1 為例）
miku = list()
getline = str(input())
cmd = getline.split()

if not cmd:
    continue # opitional: skip this time

oper = cmd[0]

if oper == "stop":
    break

elif oper == "print":
    for i in miku:
        i.output()
else:
    in_band = cmd[1]
    in_year = cmd[2]
    #... 自行處理
# 輸入到 EOF
try:
    while True:
        n = int(input())
        # process n
except EOFError:
    pass




