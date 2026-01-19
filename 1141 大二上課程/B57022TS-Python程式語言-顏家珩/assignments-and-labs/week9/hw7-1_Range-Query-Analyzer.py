'''

hw 7-1 Range Query Analyzer
給定一個整數序列，以及多筆查詢，
每筆查詢會要求計算一個區間 [L, R] 中的統計結果。
查詢指令分為以下三種：
| 指令 | 說明 |
| SUM L R | 計算第 L 到第 R 個元素的總和 |
| MAX L R | 求出第 L 到第 R 個元素的最大值 |
| MIN L R | 求出第 L 到第 R 個元素的最小值 |
請輸出每個查詢的結果。
輸入說明
第一行為整數 N（表示序列長度）
第二行包含 N 個整數，代表序列內容
第三行為整數 Q（查詢次數）
接下來 Q 行為查詢指令，格式如上表所示
輸出說明
對每筆查詢輸出一行結果。
#input
5
2 7 1 8 3
3
SUM 2 5
MAX 1 3
MIN 3 4
#output
19
7
1

'''

n = int(input())
miku = list(map(int, input().split()))
q = int(input())
while q > 0:
    cmd = input()
    oper = cmd.split()
    l = int(oper[1])
    r = int(oper[2])
    if oper[0] == "SUM":
        print(sum(miku[l-1:r]))
    elif oper[0] == "MAX":
        res = -99999
        for i in range(l-1,r):
            res = max(miku[i], res)
        print(res)
    elif oper[0] == "MIN":
        res = 99999
        for i in range(l-1,r):
            res = min(miku[i], res)
        print(res)
    q -= 1