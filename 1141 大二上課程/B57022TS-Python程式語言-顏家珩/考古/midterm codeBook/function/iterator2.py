# 1. Range 物件
print("\n--- Range ---")
for i in range(3): # range(3) 是一個可迭代物件
    print(i)
# 輸出: 0, 1, 2

# 2. 字典 (Dictionary) 的 views
print("\n--- 字典 ---")
my_dict = {'a': 1, 'b': 2, 'c': 3}

# .keys() .values() .items() 都是可迭代的
print("Keys:")
for key in my_dict.keys(): # 迭代所有鍵 (key)
    print(key)

print("Values:")
for value in my_dict.values(): # 迭代所有值 (value)
    print(value)

print("Items:")
for item in my_dict.items(): # 迭代所有 (鍵, 值) 對
    print(item)
