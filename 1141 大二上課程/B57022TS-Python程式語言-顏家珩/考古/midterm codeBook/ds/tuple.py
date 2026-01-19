# 初始化
uwu = tuple()
uwu = ()
uwu = (1,) # 單元素 tuple 需要加逗號
uwu = tuple([1, 2, 3]) # 從 list 建立
uwu = tuple("hello") # 從字串建立: ('h', 'e', 'l', 'l', 'o')
uwu = tuple(range(5)) # (0, 1, 2, 3, 4)
uwu = tuple(x**2 for x in range(5)) # tuple 推導式 (0, 1, 4, 9, 16)
person = ("Alice", 20, "Student") # 可包含不同型態的變數
mixed = (1, "hello", 3.14, [1, 2, 3]) # 可包含 list 等可變物件

# Tuple 可用函數
len(uwu) # 取得長度
max(uwu) # 取得最大值 (元素必須可比較)
min(uwu) # 取得最小值
sum(uwu) # 計算總和 (元素必須是數字)

# Tuple 可用方法 
uwu.count(1) # 計算指定元素出現次數
uwu.index(1) # 回傳指定元素的第一個索引位置 (找不到會 ValueError)

# 存取元素
numbers = (10, 20, 30, 40, 50)
numbers[0] # 第一個元素: 10
numbers[-1] # 最後一個元素: 50
numbers[1:3] # 切片: (20, 30)

# Tuple Unpacking
point = (3, 4)
x, y = point # x=3, y=4

person = ("Alice", 20, "Student")
name, age, role = person # name="Alice", age=20, role="Student"

## 使用 * 收集剩餘元素
numbers = (1, 2, 3, 4, 5)
first, *rest = numbers # first=1, rest=[2, 3, 4, 5]
first, *middle, last = numbers # first=1, middle=[2, 3, 4], last=5
*head, last = numbers # head=[1, 2, 3, 4], last=5

## 交換變數
a, b = 1, 2
a, b = b, a # a=2, b=1

## 忽略某些值
person = ("Alice", 20, "Student", "CS")
name, _, role, _ = person # 使用 _ 表示忽略的值

# Tuple Slicing
## 與 list 相同的切片語法
numbers = (10, 20, 30, 40, 50, 60)
numbers[0:3] # (10, 20, 30)
numbers[3:] # (40, 50, 60)
numbers[:4] # (10, 20, 30, 40)
numbers[-3:] # (40, 50, 60)
numbers[::2] # (10, 30, 50)
numbers[::-1] # (60, 50, 40, 30, 20, 10)

# 迭代 Tuple
fruits = ("apple", "banana", "orange")
for fruit in fruits:
    print(fruit)

## 使用 enumerate 取得索引和值
for i, fruit in enumerate(fruits):
    print(f"{i}: {fruit}")

# 檢查元素是否存在
numbers = (1, 2, 3, 4, 5)
if 3 in numbers:
    print("3 is in the tuple")
if 10 not in numbers:
    print("10 is not in the tuple")

# Tuple 串接和重複
tuple1 = (1, 2, 3)
tuple2 = (4, 5, 6)
combined = tuple1 + tuple2 # (1, 2, 3, 4, 5, 6)
repeated = tuple1 * 3 # (1, 2, 3, 1, 2, 3, 1, 2, 3)

# Named Tuple
from collections import namedtuple

Point = namedtuple('Point', ['x', 'y'])
p = Point(3, 4)
print(p.x, p.y) # 3 4
print(p[0], p[1]) # 也可以用索引: 3 4

Student = namedtuple('Student', ['name', 'age', 'grade'])
student = Student('Alice', 20, 'A')
print(student.name) # Alice
print(student.age) # 20

## namedtuple 的方法
student._asdict() # 轉換為 OrderedDict
student._replace(age=21) # 建立新的 namedtuple，修改指定欄位
Student._fields # 取得所有欄位名稱: ('name', 'age', 'grade')

## 從 dict 建立 namedtuple
data = {'name': 'Bob', 'age': 22, 'grade': 'B'}
student2 = Student(**data)

# Tuple 作為 Dictionary 的 Key
## 因為 tuple 不可變，可以作為 dict 的 key (list 不行)
locations = {
    (0, 0): "Origin",
    (1, 0): "East",
    (0, 1): "North"
}
print(locations[(0, 0)]) # Origin

## 多維座標
grid = {
    (0, 0, 0): "Center",
    (1, 0, 0): "Right",
    (0, 1, 0): "Up"
}

# Tuple 可包含可變物件
## Tuple 本身不可變，但可以包含可變物件
t = (1, 2, [3, 4])
t[0] = 100 # 不能改 tuple 的元素
t[2].append(5) # 可以修改 list 的內容: (1, 2, [3, 4, 5])
t[2][0] = 30 # 可以修改 list 的元素: (1, 2, [30, 4, 5])

## Tuple 不能作為 dict 的 key
d = {(1, 2, [3, 4]): "value"} # 錯誤！TypeError: unhashable type: 'list'

# Sample Applications
## 函數回傳多個值
def get_stats(numbers):
    return min(numbers), max(numbers), sum(numbers)

numbers = [1, 2, 3, 4, 5]
min_val, max_val, total = get_stats(numbers)

## 函數參數解包
def add(a, b, c):
    return a + b + c

numbers = (1, 2, 3)
result = add(*numbers) # 6

## 同時迭代多個序列
names = ("Alice", "Bob", "Charlie")
scores = (85, 92, 78)
for name, score in zip(names, scores):
    print(f"{name}: {score}")

