def partition(arr, low, high):
    pivot = arr[high]
    idx = low
    for i in range(low, high):
        if arr[i] < pivot:
            if i != idx:
                tp = arr[i]
                arr[i] = arr[idx]
                arr[idx] = tp
            idx += 1

    if idx != high:
        tp = arr[idx]
        arr[idx] = arr[high]
        arr[high] = tp
    return idx

def quick_sort(arr, low, high):
   if low < high:
       pi = partition(arr, low, high)
       quick_sort(arr, low, pi - 1)
       quick_sort(arr, pi + 1, high)

quick_sort(num, 0, len(num)-1)
#num is a list