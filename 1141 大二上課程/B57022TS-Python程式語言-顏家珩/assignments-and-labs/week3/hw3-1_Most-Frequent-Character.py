'''

hw 3-1 Most Frequent Character
從字串 str 中找出出現頻率最高的字元，所有資料中，最高頻的字元被設計成唯一。 str 中可能包含大小寫英文字母、數字、空白、標點符號等。
輸入說明 ：
第一行輸入一整數n(1<=n<=10)，表示有幾筆測資。
接下來n行字串 str ，含各種大小寫與標點符號。
輸出說明 ：
出現頻率最高的字元。 ( 不會有多個答案 )
#input
2
We’re students!
Yes!!! ALL PASS!!!
#output
e
!
'''

n = int(input())
for i in range(n):
    string = input()
    chars = dict()

    for i in string:
        if i in chars:
            chars[i] += 1
        else:
            chars.update({i:1})

    print(max(chars, key=chars.get))