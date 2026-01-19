'''

lab 2-1 Digital Ladder Matrix
利用if-else 和 迴圈，輸入一個矩陣高度n，n不能小於0輸出一個階梯數字矩陣，數字後都有一個空格，讓數字靠右對齊
#input:
5
#output:
1
2 3
4 5 6
7 8 9 10
11 12 13 14 15

'''

n = int(input())

if n <= 0:
    print("輸入錯誤")
else:
    now = 1
    w = len(str((n*(n+1))/2))-2
    for i in range(1,n+1):
        for j in range(i):
            print( (f"{now:>{w}}"), end=" ")
            now = now + 1
        print()