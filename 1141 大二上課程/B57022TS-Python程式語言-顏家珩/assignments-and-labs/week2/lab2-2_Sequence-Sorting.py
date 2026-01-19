'''

lab 2-2 Sequence Sorting
利用if-else 和 迴圈，排序數列 給定一個整數數列，長度為 n (n > 3)，將整數數列由小到大做排序，輸出排序前和排序後的數列，數字後都有一個空格
#input:
5
2 4 1 5 3
#output:
排序前的數列: 2 4 1 5 3
排序後的數列: 1 2 3 4 5

'''
n = int(input())
number = input().split(" ")
print(f"排序前的數列: {' '.join(number)} ")
number = [int(x) for x in number]
number.sort()
number = [str(x) for x in number]
print(f"排序後的數列: {' '.join(number)} ")