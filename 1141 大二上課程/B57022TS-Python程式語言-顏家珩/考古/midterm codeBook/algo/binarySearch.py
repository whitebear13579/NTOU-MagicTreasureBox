def binary_search(arr, tar) -> int:    
    left = 0
    right = len(arr) - 1

    while left <= right:
        mid = (left + right) // 2
        
        if arr[mid] == tar:
            return mid
        elif arr[mid] < tar:
            left = mid + 1
        else:
            right = mid - 1

    return -1