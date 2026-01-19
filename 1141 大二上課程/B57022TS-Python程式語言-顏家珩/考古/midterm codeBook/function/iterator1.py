# 1. my_list 是一個「可迭代物件」(Iterable)
my_list = [1, 2, 3]
# 2. 使用 iter() 從可迭代物件中取得「迭代器」(Iterator)
my_iterator = iter(my_list)
print(f"這是一個迭代器物件: {my_iterator}")

# 3. 使用 next() 從迭代器中取出下一個值
print(next(my_iterator)) # 輸出: 1
print(next(my_iterator)) # 輸出: 2
print(next(my_iterator)) # 輸出: 3

# 4. 當所有值都取完後，再次呼叫 next() 會引發 StopIteration 錯誤
# print(next(my_iterator)) # 這行會引發 StopIteration