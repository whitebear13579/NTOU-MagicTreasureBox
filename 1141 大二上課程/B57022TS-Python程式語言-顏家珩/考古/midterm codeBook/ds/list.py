# Python 的 List 允許將不同型別的資料存於同列表中
grades = [85, 92, 78, 96, 89]
students = ["Alice", "Bob", "Charlie"]
mixed_data = [1, "hello", 3.14, True]

# 初始化
uwu = list() 
uwu = []
uwu = [0] * 5 # [0, 0, 0, 0, 0]
uwu = [1, 2] * 3 # [1, 2, 1, 2, 1, 2]
uwu = [x**2 for x in range(5)] # [0, 1, 4, 9, 16]

# List 可用函數
len(uwu) # 取得長度
sum(uwu) # 計算總和
max(uwu) # 取得最大值
min(uwu) # 取得最小值
del uwu[0] # 刪除指定位置的元素
del uwu[1:3] # 刪除指定index的元素

# List 可用方法
uwu.append(100) # 新增元素
uwu.count(100) # 計算指定元素出現次數
uwu.extend([200, 300]) # 在list的尾部添加多個元素
uwu.index(100) # 回傳指定元素的索引位置 
uwu.insert(0, 50) # 在指定位置插入元素 (index, element)
uwu.pop() # 移除並回傳指定位置的元素 (預設為最後一個，也可以指定 index)
uwu.remove(100) # 移除指定元素 (第一個出現的)
uwu.reverse() # 將列表反轉
uwu.sort() # 將列表排序 (預設由小到大，加入參數 reverse=True 可由大到小)

# 自定義排序方法
def get_grade(student_tuple):
    return student_tuple[1]

students = [("Alice", 85), ("Bob", 92), ("Charlie", 78)]
students.sort(key=get_grade) # 依照成績排序
students.sort(key=lambda x: x[1]) # 使用 lambda （效果同上）


# 複製 List
## 一維 List
original_list = [1, 2, 3, 4, 5]
copy_list = original_list.copy() # 使用 copy 方法複製
copy_list2 = original_list[:] # 使用切片複製
wrong_copy_list3 = original_list # 複製參考而已，改了其中一方會影響另一方
## 多維 List
import copy
original_2d_list = [[1, 2, 3], [4, 5, 6]]
deep_copy_2d = copy.deepcopy(original_2d_list) # 請使用 deepcopy
wrong_copy_2d = original_2d_list.copy() # 只會複製最外層，內層仍是參考

# String to List
sentence = "Hello, world!"
char_list = list(sentence) # 將字串轉換為字元列表

# List Slicing
## [start:stop:step] , start 包含，stop 不包含，step 為步進，預設為 1
numbers = [10, 20, 30, 40, 50, 60]
numbers[0:3] # 取得索引 0 到 2 的元素
numbers[3:] # 取得索引 3 到結尾的元素
numbers[:4] # 取得從開始到索引 3 的元素
numbers[-1] # 取得最後一個元素
numbers[-3:] # 取得最後三個元素
numbers[::2] # 取得所有偶數索引的元素
numbers[::-1] # 反轉列表

# List Comprehension
## 初始化，或是存值
numbers = [i for i in range(1, 6)]
# [1, 2, 3, 4, 5]
pairs = [(x, y) for x in range(3) for y in range(2)]
# [(0, 0), (0, 1), (1, 0), (1, 1), (2, 0), (2, 1)]

## 對元素操作
names = ["alice", "bob","charlie"]
upper_names = [name.upper() for name in names] # ['ALICE', 'BOB', 'CHARLIE']

## 對元素計算
numbers = [1, 2, 3, 4, 5]
squared = [x**2 for x in numbers]
# [1, 4, 9, 16, 25]

## 解包
students = [("Alice", 85), ("Bob", 92)]
names = [name for name, grade in students] # ['Alice', 'Bob']
grades = [grade for name, grade in students] # [85, 92]

## 帶條件篩選 (配合三元運算子)
evens = [x for x in range(20) if x % 2 == 0] # [0, 2, 4, 6, 8, 10, 12, 14, 16, 18]
numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
special = [x for x in numbers if x > 3 and x < 8] # [4, 5, 6, 7]

## 範例：算平均 / 找最高值
stu = [ {"name": "Alice", "scores": [85, 92, 78]}, {"name": "Bob", "scores": [92, 88, 95]} ]
avg = [sum(s["scores"])/len(s["scores"]) for s in stu] # [85.0, 91.66666666666667]
high_performers = [s["name"] for s in stu if sum(s["scores"])/len(s["scores"]) >= 90] # ['Bob']
