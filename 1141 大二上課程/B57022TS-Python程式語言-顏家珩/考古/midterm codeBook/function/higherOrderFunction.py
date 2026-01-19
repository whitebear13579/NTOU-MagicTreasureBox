# 先定義幾個簡單的操作函式
def square(x):
    return x**2
def double(x):
    return x * 2

# Higher Order Function
# 接受一個列表和一個「操作函式」
def apply_operation(numbers, operation):
    """Apply an operation to all numbers in a list"""
    result = []
    for num in numbers:
        result.append(operation(num)) # 呼叫傳入的 operation 函式
    return result

numbers = [1, 2, 3, 4, 5]

# 傳入 square 函式
print(apply_operation(numbers, square)) # 輸出: [1, 4, 9, 16, 25]
# 傳入 double 函式
print(apply_operation(numbers, double)) # 輸出: [2, 4, 6, 8, 10]