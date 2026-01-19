'''

lab 3-2 Train Station Stack Problem
某鐵路公司在建造火車站時，因土地有限而無法直接讓火車車廂按任意順序通過，
因此設立了一個中介站 C，車廂進入 C 後只能從 C 出來，不能回到 A。火車由 A 方向進入，經由 C 站調度後，從 B 方向離開。
你的任務是判斷，對於 N 節車廂（編號固定為 1~N），能否按照指定順序從 B 方向離開。
進入 A 的順序固定為：1, 2, 3, ..., N
中介站 C 僅能當作 stack（後進先出）使用。
輸入說明
每組測資的第一行為車廂數量 N (N ≤ 1000)
後續每行為一個欲判斷的目標離開序列
每個車廂編號以空白分隔
輸入可能包含多筆測資，直到輸入0結束

#input
3
1 2 3
1 3 2
3 1 2
0
#output
YES
YES
NO
'''


n = int(input())
while True:
    inStation = [ x for x in range(1,n+1) ]
    outStation = list(map(int, input().split()))
    if (len(outStation) == 1 and outStation[0] == 0):
        break
    stack = []
    i = 0
    j = 0
    ok = True

    while j < len(outStation) and ok:
        tar = outStation[j]
        if stack and stack[-1] == outStation[j]:
            stack.pop()
            j += 1
        elif i < n and inStation[i] == tar:
            i += 1
            j += 1
        elif i < n:
            stack.append(inStation[i])
            i += 1
        else:
            ok = False
    if ok and j == len(outStation):
        print("YES")
    else:
        print("NO")