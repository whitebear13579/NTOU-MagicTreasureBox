'''

hw 2-1 Triangle
利用if-else 和 迴圈，畫空心等腰三角形 輸入一個三角形的高度整數n，n必須大於1，最後輸出高度為n的空心三角形， 三角形由 * 組成
#input
3
#output
  *
 * *
*****

'''

n = int(input())

if n < 1:
    print("輸入錯誤")
else:
    for i in range(n):
        for sp1 in range(n-i-1):
            print(" ",end="")
        print("*",end="")
        if i != 0 and i != n-1:
            for mid in range((i*2)-1):
                print(" ", end="")
            print("*",end="")
        elif i == n-1:
            for i in range((n*2)-2):
                print("*",end="")
        print()