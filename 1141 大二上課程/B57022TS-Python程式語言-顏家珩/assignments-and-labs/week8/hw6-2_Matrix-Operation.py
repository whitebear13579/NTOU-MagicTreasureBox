'''

hw 6-2 Matrix Operation
請撰寫一個程式，能夠依照輸入的指令對一個「方形矩陣（square matrix）」進行操作
操作方式如下:
scan: 輸入矩陣的內容
rotate right: 將矩陣順時針旋轉 90 度
rotate left: 將矩陣逆時針旋轉 90 度
print: 輸出矩陣內容
stop: 結束程式
若尚未輸入任何矩陣就執行
print → 輸出: No element in matrix can be printed.
rotate left 或 rotate right → 輸出: No element in matrix can be rotated.
所有指令均為小寫字母，且操作均針對最後一次輸入的矩陣
請以物件導向 (Object-Oriented Programming) 的方式撰寫程式 將矩陣的資料與操作封裝在類別中
#input
scan
3
1 2 3
4 5 6
7 8 9
rotate right
print
stop
#output
7 4 1
8 5 2
9 6 3

'''

class matrixOperation:
    def __init__(self):
        self._matrix = None
        self._n = 0

    def scan(self):
        self._matrix = list()
        self._n = int(input())
        for i in range(self._n):
            row = list(map(int,str(input()).split()))
            self._matrix.append(row)
    def rotateL(self):
        # 逆時針旋轉 90 度
        if self._matrix is None:
            print("No element in matrix can be rotated.")
        else:
            matrix_reverse = [ row[::-1] for row in self._matrix ]
            self._matrix = [[matrix_reverse[j][i] for j in range(len(matrix_reverse))] for i in range(len(matrix_reverse)) ]
    def rotateR(self):
        # 順時針旋轉 90 度
        if self._matrix is None:
            print("No element in matrix can be rotated.")
        else:
            matrix_tranpose = [[self._matrix[j][i] for j in range(len(self._matrix))] for i in range(len(self._matrix)) ]
            self._matrix = [ row[::-1] for row in matrix_tranpose ]
    def print(self):
        if self._matrix is None:
            print("No element in matrix can be printed.")
        else:
            for i in range(self._n):
                is_first = True
                for j in range(self._n):
                    if is_first:
                        is_first = False
                    else:
                        print(" ", end="")
                    
                    print(self._matrix[i][j], end="")
                print()
                

yanami_anna = matrixOperation()

try:
    while True:
        cmd = str(input()).strip()
        if not cmd:
            continue

        if cmd == "stop":
            break
        elif cmd == "scan":
            yanami_anna.scan()
        elif cmd == "rotate right":
            yanami_anna.rotateR()
        elif cmd == "rotate left":
            yanami_anna.rotateL()
        elif cmd == "print":
            yanami_anna.print()
except EOFError:
    pass