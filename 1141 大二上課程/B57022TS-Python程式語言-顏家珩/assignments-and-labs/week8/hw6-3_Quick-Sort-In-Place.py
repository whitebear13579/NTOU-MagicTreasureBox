'''
hw 6-3 Quick Sort (In-Place)

請撰寫一個 Python 程式，完成 Quick Sort 的 In-Place partition 函式，對整數陣列進行排序。
程式已提供 quick_sort() 函式，請你實作 partition() 函式，使其滿足以下要求：
In-Place 排序：不使用額外陣列，直接修改原陣列
Pivot 選擇：每次選擇陣列最後一個元素作為 pivot
交換輸出：每次交換陣列元素後立即輸出陣列
如果交換位置相同（i == j）則不輸出
最終排序結果由 quick_sort() 遞迴完成
請實作完成partition()

```python
def quick_sort(arr, low, high):
   if low < high:
       pi = partition(arr, low, high)
       quick_sort(arr, low, pi - 1)
       quick_sort(arr, pi + 1, high)

def partition(arr, low, high):
```

輸入說明:
一行整數陣列，以空格分隔
輸出說明:
先輸出原陣列
當有交換發生時，輸出交換後陣列
最後一列應為排序完的陣列
輸出使用print(arr)即可
#input
30 10 40 5 70 15 60 20 50 25
#output
[30, 10, 40, 5, 70, 15, 60, 20, 50, 25]
[10, 30, 40, 5, 70, 15, 60, 20, 50, 25]
[10, 5, 40, 30, 70, 15, 60, 20, 50, 25]
[10, 5, 15, 30, 70, 40, 60, 20, 50, 25]
[10, 5, 15, 20, 70, 40, 60, 30, 50, 25]
[10, 5, 15, 20, 25, 40, 60, 30, 50, 70]
[5, 10, 15, 20, 25, 40, 60, 30, 50, 70]
[5, 10, 15, 20, 25, 40, 30, 60, 50, 70]
[5, 10, 15, 20, 25, 40, 30, 50, 60, 70]
[5, 10, 15, 20, 25, 30, 40, 50, 60, 70]

'''

uwu = list(list())

def partition(arr, low, high):
    pivot = arr[high]
    idx = low
    for i in range(low, high):
        if arr[i] < pivot:
            if i != idx:
                tp = arr[i]
                arr[i] = arr[idx]
                arr[idx] = tp
                uwu.append(arr.copy())
            idx += 1

    if idx != high:
        tp = arr[idx]
        arr[idx] = arr[high]
        arr[high] = tp
        uwu.append(arr.copy())
    return idx

def quick_sort(arr, low, high):
   if low < high:
       pi = partition(arr, low, high)
       quick_sort(arr, low, pi - 1)
       quick_sort(arr, pi + 1, high)


num = list(map(int, input().split()))
uwu.append(num.copy())
quick_sort(num, 0, len(num)-1)
for owo, ouo in enumerate(uwu):
    if owo != 0:
        print("\n\n", end="")
    print(ouo, end="")