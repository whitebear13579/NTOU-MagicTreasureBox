# 三元運算
## 也可以不只三元，你可以自己往下加，如果你想讓你的 Code 看起來像義大利麵的話不是不行
## result = TRUE_COND if condition else FALSE_COND

age = 20
isAudlt = True if age >= 18 else False
## 上方寫法等價
if age >= 18:
    isAudlt = True
else:
    isAudlt = False

# range() 函數
range(STOP) # 從 0 到 STOP-1
range(START, STOP) 
# 從 START 到 STOP-1（預設間隔 1）
range(START, STOP, STEP) 
# 從 START 到 STOP-1，間隔 STEP