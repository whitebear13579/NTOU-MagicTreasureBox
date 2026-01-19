'''

lab 6-2 Quick Sort
請撰寫一個 Python 程式，使用 Quick Sort（快速排序） 演算法，將一串整數從小到大排序。
Quick Sort 是一種常見的 分治法（Divide and Conquer） 排序演算法，其主要步驟如下：
選擇一個「基準值（pivot）」作為劃分依據。
將資料分成三部分：
左邊為所有 小於 pivot 的元素
中間為所有 等於 pivot 的元素
右邊為所有 大於 pivot 的元素
對左、右兩部分遞迴執行相同操作。
將三部分合併後，即為排序結果。
輸入說明:
輸入包含多個整數，以空白分隔。
輸出說明:
輸出排序後的整數序列，以空白分隔。
#input
5 3 8 4 2
#output
2 3 4 5 8

'''

def qsort( arr , l , r ):
    if l >= r:
        return
    i, j = l, r
    pivot = arr[(l+r)//2]
    while i <= j:
        while arr[i] < pivot:
            i += 1
        while arr[j] > pivot:
            j -= 1
        if i <= j:
            tp = arr[i]
            arr[i] = arr[j]
            arr[j] = tp
            i += 1
            j -= 1
            
    qsort( arr, l, j )
    qsort( arr, i, r )
    
num = list(map(int, input().split()))
qsort(num, 0, len(num) - 1 )
print(*num)