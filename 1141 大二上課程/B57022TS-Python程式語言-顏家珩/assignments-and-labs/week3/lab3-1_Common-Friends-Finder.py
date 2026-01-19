'''

lab 3-1 Common Friends Finder
在一個社群平台上，每個使用者都有一份好友清單。 
現在給定兩個使用者的好友清單，請你找出他們的 共同好友數量，以及 所有共同好友的編號（依照編號由小到大排序後輸出）。
由於好友數量可能非常龐大，請注意程式的時間效率。建議使用 Python 的 set 結構來處理。

輸入格式
第一行輸入兩個整數 n, m (1 ≤ n, m ≤ 10^5)，分別代表兩個使用者的好友數量。
第二行輸入 n 個整數，代表使用者 A 的好友編號。
第三行輸入 m 個整數，代表使用者 B 的好友編號。
好友編號的範圍為 1 ≤ id ≤ 10^8。

輸出格式
第一行輸出一個整數，代表 共同好友數量。
第二行輸出所有共同好友的編號，依 由小到大排序，數字之間以空白分隔。
若沒有共同好友，第二行不輸出任何內容。

#input
5 6
1 2 3 4 5
3 4 5 6 7 8
#output
3
3 4 5

'''

n, m =  map(int, input().split(" "))
friendA =  set(map(int, input().split()))
friendB =  set(map(int, input().split()))
ans = friendA & friendB
print(len(ans))
print(' '.join(map(str, sorted(ans))))