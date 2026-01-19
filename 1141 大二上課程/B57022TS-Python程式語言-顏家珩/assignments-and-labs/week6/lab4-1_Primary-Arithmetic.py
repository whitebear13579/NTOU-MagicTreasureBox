'''

lab 4-1 Primary Arithmetic
在小學時我們都做過加法的運算，就是把2個整數靠右對齊然後，由右至左一位一位相加。
如果相加的結果大於等於10就有進位（carry）的情況出現。你的任務就是要判斷2個整數相加時產生了幾次進位的情況。
這將幫助小學老師分析加法題目的難度。
輸入說明
每一列測試資料有2個正整數，長度均小於10位。最後一列有2個0代表輸入結束。
輸出說明
每列測試資料輸出該2數相加時產生多少次進位，請參考Sample Output。注意進位超過1次時operation有加s。
#input
123 456
555 555
123 594
0 0
#output
No carry operation.
3 carry operations.
1 carry operation.

'''

def getAns( num1, num2 ):
    ops = 0
    carry = 0
    num1 = num1[::-1]
    num2 = num2[::-1]

    maxl = max(len(num1), len(num2))
    num1 = num1.ljust(maxl,'0')
    num2 = num2.ljust(maxl,'0')

    for i in range(maxl):
        if (int)(num1[i]) + (int)(num2[i]) + carry >= 10:
            ops += 1
            carry = 1
        else:
            carry = 0
    return ops

try:
    while True:
        num1, num2 = input().split()
        if (num1 == "0" and num2 == "0"):
            break
        ans = getAns(num1, num2)
        print("No carry operation." if ans == 0 else "1 carry operation." if ans == 1 else f"{ans} carry operations.")
except EOFError:
    print("",end="")