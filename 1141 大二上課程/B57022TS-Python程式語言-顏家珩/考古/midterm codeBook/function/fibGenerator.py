def fibonacci_generator(max_count=None):
    a, b = 0, 1
    # 初始化前 fib 的兩個數字
    count = 0
    
    # 迴圈會一直執行，直到達到 max_count
    while max_count is None or count < max_count:
        yield a # 回傳目前的 Fibonacci 數字，然後暫停函式執行
        a, b = b, a + b # 計算下一個數字
        count += 1


print("--- First 10 Fibonacci numbers: ---")
# for 迴圈會自動處理 next() 呼叫和 StopIteration
for i, fib in enumerate(fibonacci_generator(10)):
    print(f"F({i}) = {fib}")

# 輸出:
# F(0) = 0
# F(1) = 1
# F(2) = 1
# F(3) = 2
# F(4) = 3
# ... (依此類推)