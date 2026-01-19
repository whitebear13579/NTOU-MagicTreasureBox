# 初始化
uwu = dict()
uwu = {}
uwu = dict([("a", 1), ("b", 2)]) # 從 tuple list 建立
uwu = dict(a=1, b=2, c=3) # 使用關鍵字參數
uwu = {k: v for k, v in [("x", 1), ("y", 2)]} # 字典推導式
uwu = dict.fromkeys(["a", "b", "c"], 0) # 用相同值初始化多個 key: {'a': 0, 'b': 0, 'c': 0}
## Sample Usage
student_scores = {"Alice": 85, "Bob": 92, "Charlie": 78}
ages = {"Alice": 20, "Bob": 21}
mixed_dict = {"name": "Alice", "age": 20, "scores": [85, 92, 78], "is_active": True}

# Dictionary 可用函數
len(uwu) # 取得鍵值對數量
max(uwu) # 取得最大的 key (依字母或數字排序)
min(uwu) # 取得最小的 key
del uwu["a"] # 刪除指定 key 的鍵值對

# Dictionary 可用方法
uwu.clear() # 清空字典
uwu.copy() # 淺複製字典
uwu.get("key", "default") # 取得值，key 不存在時回傳 default (預設為 None)
uwu.items() # 回傳所有鍵值對的視圖 (可用於迭代)
uwu.keys() # 回傳所有 key 的視圖
uwu.values() # 回傳所有 value 的視圖
uwu.pop("key", "default") # 移除並回傳指定 key 的值，不存在時回傳 default
uwu.popitem() # 移除並回傳最後一個鍵值對 (Python 3.7+ 保證順序)
uwu.setdefault("key", "default") # 如果 key 存在則回傳值，否則設定為 default 並回傳
uwu.update({"new_key": "value"}) # 更新字典 (可接受字典、鍵值對列表或關鍵字參數)

# 存取與修改
scores = {"Alice": 85, "Bob": 92}
scores["Alice"] # 取得值 (key 不存在會引發 KeyError)
scores.get("Charlie", 0) # 安全取得值，不存在回傳 0
scores["Charlie"] = 78 # 新增或修改
scores["Alice"] += 5 # 修改現有值

# 檢查 key 是否存在
if "Alice" in scores:
    print("Alice is in the dictionary")
if "David" not in scores:
    print("David is not in the dictionary")

# 迭代 Dictionary
student_scores = {"Alice": 85, "Bob": 92, "Charlie": 78}

## 迭代 keys
for name in student_scores:
    print(name)
for name in student_scores.keys():
    print(name)

## 迭代 values
for score in student_scores.values():
    print(score)

## 迭代 key-value pairs
for name, score in student_scores.items():
    print(f"{name}: {score}")

# 複製 Dictionary
original = {"a": 1, "b": 2}
shallow_copy = original.copy() # 淺複製
shallow_copy2 = dict(original) # 另一種淺複製方式
wrong_copy = original # 只是複製參考
## 完全複製
import copy
nested = {"a": [1, 2, 3], "b": [4, 5, 6]}
deep_copy = copy.deepcopy(nested)

# Dictionary Comprehension
## 建立字典
squares = {x: x**2 for x in range(5)} # {0: 0, 1: 1, 2: 4, 3: 9, 4: 16}
names = ["Alice", "Bob", "Charlie"]
name_lengths = {name: len(name) for name in names} # {'Alice': 5, 'Bob': 3, 'Charlie': 7}

## 轉換或過濾
scores = {"Alice": 85, "Bob": 92, "Charlie": 78, "David": 95}
high_scores = {k: v for k, v in scores.items() if v >= 90} # {'Bob': 92, 'David': 95}
uppercase_keys = {k.upper(): v for k, v in scores.items()} # {'ALICE': 85, 'BOB': 92, ...}

## 反轉鍵值對
original = {"a": 1, "b": 2, "c": 3}
reversed_dict = {v: k for k, v in original.items()} # {1: 'a', 2: 'b', 3: 'c'}

## 合併字典 
dict1 = {"a": 1, "b": 2}
dict2 = {"b": 3, "c": 4}
merged = dict1 | dict2 # {'a': 1, 'b': 3, 'c': 4} (後者覆蓋前者)
dict1 |= dict2 # 原地合併

# 排序 Dictionary
scores = {"Alice": 85, "Bob": 92, "Charlie": 78}

## 依 key 排序
sorted_by_key = dict(sorted(scores.items())) # {'Alice': 85, 'Bob': 92, 'Charlie': 78}

## 依 value 排序
sorted_by_value = dict(sorted(scores.items(), key=lambda x: x[1])) # {'Charlie': 78, 'Alice': 85, 'Bob': 92}
sorted_by_value_desc = dict(sorted(scores.items(), key=lambda x: x[1], reverse=True)) # 降序

