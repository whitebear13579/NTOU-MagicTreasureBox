import numpy as np
import time
from scipy import linalg

# global matrix
a_matrix = list()
a_np_matrix = None
b_matrix = list()
b_np_matrix = None

# measure time v.
cm_time = 0
sp_time = 0

def generateMatrix():
    global a_matrix, a_np_matrix, b_matrix, b_np_matrix
    tp_a = np.random.rand(200,200)
    tp_b = np.random.rand(200,200)
    a_matrix = tp_a.tolist()
    a_np_matrix = tp_a
    b_matrix = tp_b.tolist()
    b_np_matrix = tp_b
    

def customMatrixMulti():
    global cm_time, a_matrix, b_matrix
    st_time = time.perf_counter()
    result = [[ 0 for i in range(200)] for j in range(200)]
    for i in range(200):
        for j in range(200):
            sum = 0
            for k in range(200):
                sum += a_matrix[i][k] * b_matrix[k][j]
            result[i][j] = sum
    cm_time = time.perf_counter() - st_time
    return result

def sciPyMatrixMulti():
    global sp_time, a_np_matrix, b_np_matrix
    st_time = time.perf_counter()
    result = linalg.blas.dgemm(1.0, a_np_matrix, b_np_matrix)
    sp_time = time.perf_counter() - st_time
    return result

generateMatrix()
a_result = customMatrixMulti()
print(f"自行實作矩陣乘法所花之時間： {cm_time:.6f} 秒")
b_result = sciPyMatrixMulti()
print(f"使用 SciPy BLAS 所花之時間： {sp_time:.6f} 秒")
print(f"兩者結果矩陣是否近似相同： {np.allclose(a_result, b_result)}")
