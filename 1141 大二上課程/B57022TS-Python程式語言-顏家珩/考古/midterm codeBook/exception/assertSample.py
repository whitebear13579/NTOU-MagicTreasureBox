# 帶有錯誤訊息的 assert
age = 17
assert age >= 18, "Age must be 18 or older"
print("Age check passed!")

'''
Traceback (most recent call last):
  File "/home/main.py", line 2, in <module>
    assert age >= 18, "Age must be 18 or older"
           ^^^^^^^^^
AssertionError: Age must be 18 or older
'''

# 沒有錯誤訊息的 assert
x = 10
assert x > 0  # 條件為 True，靜默通過
assert x < 5  # 條件為 False，引發 AssertionError

'''
Traceback (most recent call last):
  File "/home/main.py", line 3, in <module>
    assert x < 5  # 條件為 False，引發 AssertionError
           ^^^^^
AssertionError
'''