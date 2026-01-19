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

qsort(num, 0, len(num) - 1 )
#num is a list