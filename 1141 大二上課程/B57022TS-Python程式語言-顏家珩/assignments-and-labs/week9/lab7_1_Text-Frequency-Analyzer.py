'''

lab 7-1 Text Frequency Analyzer
撰寫一個程式，分析一段英文文字中的單字(word)和字母(letter)
出現頻率。程式需要找出：
出現頻率最高的三個英文單字
出現頻率最高的三個英文字母
分析時需要注意：
不區分大小寫（例如: 'Hello' 和 'hello'視為相同單字)只考慮英文
字母(A-Z, a-z)
忽略所有標點符號、數字和空白字元
若有相同出現次數的單字或字母，選擇字典序較小者
(例如: 'apple' 的字典序小於'banana’)
請建立兩個.py檔，並使用import來呼叫函式
輸入說明:
輸入為txt檔，文字中可能包含:英文字母(大寫和小寫)、數字、
標點符號、空白字元(空格、換行)
輸出說明:
程式會輸出兩行文字:
第一行:前三個最常出現的單字及其次數
第二行:前三個最常出現的字母及其次數
#input
(no input , it from input.txt)
#output
to 16 the 14 tea 12
e 189 r 153 a 146

'''

import lab7_1_func as func
# lab7_1.py
with open("input.txt", "r", encoding='utf-8') as in_file:
    content = in_file.read()

word = func.word_analyzer(content) 
letter = func.letter_analyzer(content)
idx = 0

for key, value in word.items():
    if idx == 3:
        break
    print(f"{key} {value} ", end="")
    idx += 1
print()

idx = 0
for key, value in letter.items():
    if idx == 3:
        break
    print(f"{key} {value} ", end="")
    idx += 1
print()
