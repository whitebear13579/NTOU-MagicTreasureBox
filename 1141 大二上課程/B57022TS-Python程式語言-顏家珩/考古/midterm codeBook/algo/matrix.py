# 矩陣轉置
matrix_transpose = [[matrix[j][i] for j in range(len(matrix))] for i in range(len(matrix[0]))]

# 水平翻轉 (上下顛倒)
j = len(matrix) - 1
for i in range(len(matrix)//2):
    matrix[i], matrix[j] = matrix[j], matrix[i]
    j -= 1

# 垂直翻轉 (左右顛倒)
j = len(matrix) - 1
for i in range(len(matrix)):
    matrix[i][0] , matrix[i][j] = matrix[i][j], matrix[i][0]

# 逆時鐘轉 90 度
matrix_reverse = [ row[::-1] for row in matrix ]
matrix = [[matrix_reverse[j][i] for j in range(len(matrix_reverse))] for i in range(len(matrix_reverse)) ]

# 順時鐘轉 90 度
matrix_transpose = [[ matrix[j][i] for j in range(len(matrix))] for i in range(len(matrix[0]))]
matrix = [ row[::-1] for row in matrix_transpose ]