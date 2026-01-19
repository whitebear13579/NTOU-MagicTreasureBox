# 算數運算
a = 10
b = 3
print(a + b) # Addition: 13
print(a - b) # Subtraction: 7
print(a * b) # Multiplication: 30
print(a / b) # Division: 3.333...
print(a // b) # Floor Division: 3
print(a % b) # Modulus: 1
print(a ** b) # Exponentiation: 1000

# 比較運算
a = 10
b = 3
print(a == b) # Equal: False
print(a != b) # Not equal: True
print(a < b) # Less than: True
print(a > b) # Greater than: False
print(a <= b) # Less than or equal: True
print(a >= b) # Greater than or equal: False
print("apple" < "banana") # 字串比較(比較字典序)，True

# 邏輯運算
x = True
y = False
print(x and y) # AND: False
print(x or y) # OR: True
print(not x) # NOT: False

# 位元運算
i = 10 # Binary: 1010
j = 4  # Binary: 0100
print(i & j) # AND: 0 (0000)
print(i | j) # OR: 14 (1110)
print(i ^ j) # XOR: 14 (1110)
print(~i) # NOT: -11
print(i << 1) # Left shift: 20
print(i >> 1) # Right shift: 5