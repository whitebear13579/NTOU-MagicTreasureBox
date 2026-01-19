# 將可變物件作為預設參數
# 該物件會在函式定義時被建立一次，之後每次呼叫函式時都會使用同一個物件。
def add_to_list(item, target_list=[]):
    target_list.append(item)
    return target_list
print(add_to_list(1)) # [1]
print(add_to_list(2)) # [1, 2] 
print(add_to_list(3)) # [1, 2, 3]

# 預期結果應該這麼寫
def add_to_list2(item, target_list=None):
    if target_list is None:
        target_list = []
    target_list.append(item)
    return target_list

print(add_to_list2(1)) # [1]
print(add_to_list2(2)) # [2] 
print(add_to_list2(3)) # [3]