# 初始化
uwu = set()
uwu = {1, 2, 3}
fruits = {"apple", "banana", "orange"}
uwu = set([1, 2, 3, 2, 1]) # 從 list 建立，自動去重: {1, 2, 3}
uwu = set("hello") # 從字串建立: {'h', 'e', 'l', 'o'}
uwu = {x for x in range(5)} # Set 推導式: {0, 1, 2, 3, 4}
mixed = {1, "hello", 3.14, (1, 2)} # 可以混合類型，但不能包含 list 或 dict

# Set 可用函數
len(uwu) # 取得元素數量
max(uwu) # 取得最大值 (元素必須可比較)
min(uwu) # 取得最小值
sum(uwu) # 計算總和 (元素必須是數字)

# Set 可用方法
uwu.add(10) # 新增單個元素
uwu.clear() # 清空集合
uwu.copy() # 淺複製
uwu.discard(10) # 移除元素 (元素不存在也不會報錯)
uwu.remove(10) # 移除元素 (元素不存在會引發 KeyError)
uwu.pop() # 隨機移除並回傳一個元素 (set 無序，所以是"隨機")
uwu.update({6, 7, 8}) # 新增多個元素 (可接受 set, list, tuple 等可迭代物件)
uwu.update([9, 10], {11, 12}) # 可同時更新多個可迭代物件

# 集合運算
set1 = {1, 2, 3, 4}
set2 = {3, 4, 5, 6}

## 聯集 (Union) - 所有元素
union1 = set1 | set2 # {1, 2, 3, 4, 5, 6}
union2 = set1.union(set2) # 同上
set1 |= set2 # 原地更新為聯集

## 交集 (Intersection) - 共同元素
inter1 = set1 & set2 # {3, 4}
inter2 = set1.intersection(set2) # 同上
set1 &= set2 # 原地更新為交集

## 差集 (Difference) - 在 set1 但不在 set2
diff1 = set1 - set2 # {1, 2}
diff2 = set1.difference(set2) # 同上
set1 -= set2 # 原地更新為差集

## 對稱差集 (Symmetric Difference) - 在其中一個但不在兩者
sym_diff1 = set1 ^ set2 # {1, 2, 5, 6}
sym_diff2 = set1.symmetric_difference(set2) # 同上
set1 ^= set2 # 原地更新為對稱差集

# 集合關係判斷
set1 = {1, 2, 3}
set2 = {1, 2, 3, 4, 5}
set3 = {1, 2, 3}

set1.issubset(set2) # set1 是否為 set2 的子集: True
set1 <= set2 # 同上: True
set1 < set2 # 是否為真子集 (不相等的子集): True

set2.issuperset(set1) # set2 是否為 set1 的父集: True
set2 >= set1 # 同上: True
set2 > set1 # 是否為真父集: True

set1.isdisjoint(set2) # 是否沒有交集: False
set1 == set3 # 是否相等: True

# 檢查元素是否存在
numbers = {1, 2, 3, 4, 5}
if 3 in numbers:
    print("3 is in the set")
if 10 not in numbers:
    print("10 is not in the set")

# 迭代 Set (順序不固定)
fruits = {"apple", "banana", "orange"}
for fruit in fruits:
    print(fruit)

# 複製 Set
original = {1, 2, 3}
copy_set = original.copy()
copy_set2 = set(original)
wrong_copy = original # 只是複製參考

# Set Comprehension
## 建立集合
squares = {x**2 for x in range(6)} # {0, 1, 4, 9, 16, 25}
evens = {x for x in range(10) if x % 2 == 0} # {0, 2, 4, 6, 8}

## 從字串提取唯一字符
text = "hello world"
unique_chars = {char for char in text if char != ' '} # {'h', 'e', 'l', 'o', 'w', 'r', 'd'}

## 處理多個集合
lists = [[1, 2], [2, 3], [3, 4]]
all_numbers = {num for lst in lists for num in lst} # {1, 2, 3, 4}

# Frozenset 不可變的 Set（immutable）
## frozenset 可以作為 dict 的 key 或 set 的元素
fs = frozenset([1, 2, 3])
regular_set = {5,6,7,8}
fs2 = frozenset(regular_set)

## frozenset 支援所有不會修改集合的操作
fs1 = frozenset([1, 2, 3])
fs2 = frozenset([3, 4, 5])
union = fs1 | fs2 # frozenset({1, 2, 3, 4, 5})
inter = fs1 & fs2 # frozenset({3})

## 用途：作為 dict 的 key
locations = {
    frozenset(["A", "B"]): "Route 1",
    frozenset(["B", "C"]): "Route 2"
}

# Sample Applications
## 去除重複
numbers = [1, 2, 2, 3, 3, 3, 4, 5, 5]
unique = list(set(numbers)) # [1, 2, 3, 4, 5] (順序可能不同)

## 找出共同元素
list1 = [1, 2, 3, 4]
list2 = [3, 4, 5, 6]
common = list(set(list1) & set(list2)) # [3, 4]

## 找出唯一元素 (只在一個列表中)
unique_to_list1 = list(set(list1) - set(list2)) # [1, 2]
unique_elements = list(set(list1) ^ set(list2)) # [1, 2, 5, 6]


## 移除列表中的特定元素
numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
to_remove = {2, 4, 6, 8}
filtered = [x for x in numbers if x not in to_remove] # [1, 3, 5, 7, 9]

## 檢查是否有重複
def has_duplicates(lst):
    return len(lst) != len(set(lst))

items = [1, 2, 3, 2]
print(has_duplicates(items)) # True
