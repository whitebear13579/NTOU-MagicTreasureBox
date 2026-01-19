# 一般函式
def add(x, y):
    return x + y

# Lambda 函式
add_lambda = lambda x, y: x + y

print(add(5, 3))         # 輸出: 8
print(add_lambda(5, 3))  # 輸出: 8

# 單一參數的範例
square_lambda = lambda x: x**2
print(square_lambda(4))  # 輸出: 16

# 將 lambda 作為參數傳遞 - 用作排序時
students = [
    {'name': 'Alice', 'age': 23, 'grade': 88},
    {'name': 'Bob', 'age': 21, 'grade': 92},
    {'name': 'Carol', 'age': 22, 'grade': 85}
]
# 使用 lambda 指定以 'age' 排序
students_by_age = sorted(students, key=lambda student: student['age'])