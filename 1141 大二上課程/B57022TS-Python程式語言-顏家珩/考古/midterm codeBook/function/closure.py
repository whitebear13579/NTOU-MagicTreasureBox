# 這是一個高階函式，它回傳一個新的函式
def create_multiplier(factor):
    """Create a function that multiplies by a specific factor"""
    
    # create_multiplier 要回傳的內部函式
    def multiplier(number):
        return number * factor
    
    return multiplier # 回傳內部函式

# 建立一個「乘以 2」的函式
double = create_multiplier(2)
# 建立一個「乘以 3」的函式
triple = create_multiplier(3)

# 使用新建立的函式
print(double(5)) # 輸出: 10
print(triple(4)) # 輸出: 12