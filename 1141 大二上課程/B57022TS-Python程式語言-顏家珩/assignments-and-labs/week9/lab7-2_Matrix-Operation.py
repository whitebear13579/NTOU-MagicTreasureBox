'''

lab 7-2 Matrix Operation
請撰寫一個程式，讀取一個 N×N 的整數矩陣，
以及一組操作指令，對矩陣進行旋轉或翻轉。
共有以下三種操作指令：
| 指令 | 說明 |
|------|------|
| R | 將矩陣順時針旋轉 90 度 |
| H | 水平翻轉（上下顛倒） |
| V | 垂直翻轉（左右顛倒） |
指令可多個連續輸入（例如 RHV 代表依序執行旋轉→水平翻轉→垂直翻轉）。
請輸出所有操作執行完後的矩陣結果。
輸入說明
輸入包含：
第一行為整數 N(矩陣大小)
接下來 N 行，每行有 N 個整數
最後一行為操作指令字串(只包含 R/H/V)直到EOF結束
輸出說明
輸出操作完後的矩陣，每行輸出一列，數字以空白分隔。
#input
3
1 2 3
4 5 6
7 8 9
RHV
#output
3 6 9
2 5 8
1 4 7

'''

def rotate( matrix ):
    matrix_tranpose = [[matrix[j][i] for j in range(len(matrix))] for i in range(len(matrix)) ]
    rotated = [ row[::-1] for row in matrix_tranpose ]
    matrix[:] = rotated

def horizonFlip ( matrix ):
    j = len(matrix) - 1
    for i in range(len(matrix)//2):
        matrix[i], matrix[j] = matrix[j], matrix[i]
        j -= 1

def verticalFlip ( matrix ):
    j = len(matrix) - 1
    for i in range(len(matrix)):
        matrix[i][0] , matrix[i][j] = matrix[i][j], matrix[i][0]

matrix = list()
n = int(input())
for i in range(n):
    row = list(map(int,str(input()).split()))
    matrix.append(row)

try:
    while True:
        cmd = input()
        if not cmd:
            continue

        for i in cmd:
            if i == "R":
                rotate(matrix)
            elif i == "H":
                horizonFlip(matrix)
            elif i == "V":
                verticalFlip(matrix)

except EOFError:
    for i in matrix:
        print(" ".join(map(str, i)))