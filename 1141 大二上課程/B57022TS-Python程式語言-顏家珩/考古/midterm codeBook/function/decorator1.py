# 步驟 1: 定義裝飾器 (包裝紙)
def my_decorator(func): # func 是被包裝的原始函式 (禮物)
    def wrapper(*args, **kwargs): # wrapper 是包裝後的8新函式
        print(f"Before calling {func.__name__}") # 打開禮物前
        result = func(*args, **kwargs) # 打開禮物 (執行原始函式)
        print(f"After calling {func.__name__}")  # 打開禮物後
        return result
    return wrapper # 回傳包裝好的新函式

# 步驟 2: 套用裝飾器 (包裝動作)
@my_decorator
def greet(name):
    print(f"Hello, {name}!")
    return f"Greeting for {name}"

# 步驟 3: 呼叫 (你呼叫的是「包裝後」的函式)
result = greet("Alice")
print(f"Result: {result}")

# 輸出：
# Before calling greet
# Hello, Alice!
# After calling greet
# Result: Greeting for Alice