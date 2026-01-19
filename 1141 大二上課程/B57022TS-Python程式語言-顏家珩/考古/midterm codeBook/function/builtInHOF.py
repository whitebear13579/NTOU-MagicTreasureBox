# map usage
## 將數字平方
numbers = [1, 2, 3, 4, 5]
### 使用 map 和 lambda 將 numbers 中的每個元素都平方
squared = list(map(lambda x: x**2, numbers))
print(squared)
### 輸出: [1, 4, 9, 16, 25]

## 攝氏溫度轉華氏
celsius_temps = [0, 20, 30, 40, 100]
### 套用溫度轉換公式
fahrenheit_temps = list(map(lambda c: c * 9/5 + 32, celsius_temps))
print(fahrenheit_temps) 
### 輸出: [32.0, 68.0, 86.0, 104.0, 212.0]


# filter usage
## 篩選出偶數
numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
### 使用 filter 和 lambda 保留 x % 2 == 0 (偶數) 的元素
even_numbers = list(filter(lambda x: x % 2 == 0, numbers))
print(even_numbers)
#### 輸出: [2, 4, 6, 8, 10]

## 篩選出長度大於 5 的字串
strings = ["apple", "banana", "cherry", "date"]
long_strings = list(filter(lambda s: len(s) > 5, strings))
print(long_strings)
### 輸出: ['banana', 'cherry']

# reduce usage
from functools import reduce
## 將列表中的所有數字加總
numbers = [1, 2, 3, 4, 5]
'''
reduce 會這樣運作：
1 + 2 = 3
3 + 3 = 6
6 + 4 = 10
10 + 5 = 15
'''
total = reduce(lambda x, y: x + y, numbers)
print(total)
### 輸出: 15

## 找出列表中的最大值
numbers = [1, 2, 3, 4, 5]
### 比較 x 和 y，回傳較大者
maximum = reduce(lambda x, y: x if x > y else y, numbers)
print(maximum)

