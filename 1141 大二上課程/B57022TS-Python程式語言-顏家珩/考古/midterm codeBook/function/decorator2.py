import time
import functools # 用於 @functools.wraps

def timing_decorator(func):
    """Decorator to measure function execution time"""
    
    # @functools.wraps 會保留原始函式的名稱等資訊，是個好習慣
    @functools.wraps(func)
    def wrapper(*args, **kwargs):
        start_time = time.time()       # 執行前: 記錄開始時間
        result = func(*args, **kwargs) # 執行原始函式
        end_time = time.time()         # 執行後: 記錄結束時間
        
        execution_time = end_time - start_time # 計算耗時
        print(f"{func.__name__} executed in {execution_time:.4f} seconds") 
        return result
    return wrapper

# 套用
@timing_decorator
def slow_function():
    """Simulate a slow function"""
    time.sleep(1) # 模擬耗時 1 秒 [cite: 1779]
    return "Done!"

print(slow_function())

# 輸出 (時間會接近 1.0000)：
# slow_function executed in 1.0002 seconds
# Done!