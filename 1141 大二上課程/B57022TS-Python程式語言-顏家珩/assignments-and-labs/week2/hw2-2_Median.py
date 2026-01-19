'''

hw 2-2 Median
給定一串整數輸入，整數會依序一個一個輸入，請你在每讀入一個整數後，立刻輸出目前所有輸入數字的「中位數」 
利用if-else 和 迴圈，排序數列 中位數: 如果目前共有 n 個數字，且 n 為奇數，則中位數是排序後的「中間那個數字」
如果 n 為偶數，則中位數是排序後「中間兩個數字的平均」，並且採用整數除法 直到輸入EOF結束
#input
1
2
3
4
5
#output
1
1
2
2
3

'''

try:
    num = []
    while True:
        a = int(input())
        num.append(a)
        num.sort()
        numl = len(num)
        median = 0
        if numl%2 == 0:
            median = (num[numl//2] + num[(numl//2) - 1])//2
        else:
            median = num[numl//2]
        print(int(median))
except EOFError:
    print("",end="")