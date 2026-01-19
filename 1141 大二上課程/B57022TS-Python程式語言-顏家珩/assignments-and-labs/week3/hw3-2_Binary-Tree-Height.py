'''

hw 3-2 Binary Tree Height
給一棵 2 元樹 t ，請你找出這棵樹高度。高度定義為 t 中最長 path 的長度。在下圖中，樹的高度為 4 ，因為最長的 path 為 1 → 3 → 4 → 6 ，長度為 4 。
輸入說明 :
輸入有 2 行。
第 1 行為樹的 pre-order 走訪序（即前序走訪）。這一行為一個字串，包含數個正整數，每個正整數間用空白隔開。
第 2 行為樹的 in-order 走訪序（即中序走訪）。這一行為一個字串，包含數個正整數，每個正整數間用空白隔開。
透過這兩個走訪序，你可以建立出一顆唯一的 2 元樹。
輸出說明 :
找出這棵 2 元樹的高度，並輸出。
#input
2 1 4 3 5
1 2 3 4 5
#output
3

'''


pt = list(map(int, input().split()))
it = list(map(int, input().split()))

def getAns( pct, ict ):
    if not pct or not ict:
        return 0
    
    nowRoot = pct[0]
    nowRoot_idx = ict.index(nowRoot)
    
    lict = ict[:nowRoot_idx]
    rict = ict[nowRoot_idx+1:]

    lpct = pct[1:1 + len(lict)]
    rpct = pct[1+len(lict):]
    return max(getAns(lpct,lict),getAns(rpct,rict)) + 1

print(getAns(pt,it))