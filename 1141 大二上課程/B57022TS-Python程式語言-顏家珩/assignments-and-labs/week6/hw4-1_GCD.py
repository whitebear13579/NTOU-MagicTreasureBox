'''

hw 4-1 GCD
題目敘述
已知 N 的值，你必須求 G。
G 的定義則如以下的程式碼：
```
G=0;
for(i=1;i<N;i++)
{
    for(j=i+1;j<=N;j++)
    {
        G+=GCD(i,j);
    }
}
```
GCD()為一個求兩個輸入數字的最大公因數的函數
輸入說明
輸入檔最多有 100 行的輸入。每一行有一個整數N (1<N<501)。N 的定義如題幹。輸入以含有一個 0 的一行作為結束，請不要處理這個 0。
輸出說明
就每行的輸入產生一行輸出。這行含有相對於 N 的 G。
#input
10
100
500
0
#output
67
13015
442011
4
'''

import math

while True:
    n = int(input())
    if ( n == 0 ):
        break
    g = 0
    for i in range(1,n):
        for j in range(i+1,n+1):
            g += math.gcd(i,j)
    print(g)
