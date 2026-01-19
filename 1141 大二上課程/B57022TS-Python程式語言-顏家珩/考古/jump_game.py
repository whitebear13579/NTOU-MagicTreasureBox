def solution(nums) -> bool:
    n = len(nums)
    maxreach = 0
    last = len(nums) - 1
    for i in range(n):
        if i > maxreach:
            return False
        maxreach = max(maxreach, i + nums[i])
        if maxreach >= last:
            return True
            
    return False
    
arr = list(map(int, input().split(',')))
flag = solution(arr)
if flag:
    print("true")
else:
    print("false")
