try:
    # 1. 嘗試執行
    result = 10 / 2
except ZeroDivisionError:
    # 2. 如果發生 ZeroDivisionError，執行這裡
    print("Error: Division by zero!")
else:
    # 3. 如果「沒有」發生例外，執行這裡
    print(f"Division successful! Result: {result}")
finally:
    # 4. 「無論如何」都會執行這裡
    print("Division operation completed.")