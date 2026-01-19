'''

hw 5-1 Guess Number
題目說明
請設計一個遊戲，首先讓出題者設定一個介於 0 到 50 的整數作為「正確答案」，接著讓答題者開始猜數字，直到猜中數字。
使用 try / except / else / finally 處理所有可能的輸入錯誤，並在程式結束時印出總共猜了幾次（包含猜測時錯誤的輸入）。
輸入說明
第一行輸入一個整數 N，代表出題者設定的正確答案，必須滿足 0 <= N <= 50。 從第二行開始，為使用者的猜測，每次輸入一個數字。
輸出說明
一、出題階段（第一行）

錯誤條件與其輸出內容
非數字->Error: please enter a number
輸入為小數（非整數）->Error: please enter an integer
輸入超出範圍（<0 或 >50）->Error: number out of range
*出題階段若是出現錯誤，則結束遊戲

二、猜測階段（第二行起）
可能狀況與輸出內容
輸入非數字->Error: please enter a number
小數->Error: please enter an integer
超出範圍（<0 或 >50）->Error: number out of range
猜的數字太小->You are too low!
猜的數字太大->You are too high!
猜中->You got it!
無論是出題或猜測階段，遊戲結束前都必須輸出：You guessed n times.
#input
25
apple
30.5
70
10
40
25
#output
Error: please enter a number
Error: please enter an integer
Error: number out of range
You are too low!
You are too high!
You got it!
You guessed 6 times.

'''

def handle_Expect( n ):
    out = ""
    if n == 0:
        out += "Error: please enter a number"
    elif n == 1:
        out += "Error: please enter an integer"
    elif n == 2:
        out += "Error: number out of range"
    elif n == 3:
        out += "You are too low!"
    elif n == 4:
        out += "You are too high!"
    elif n == 5:
        out += "You got it!"
    print(out)

try:
    times = 0
    while True:
        try:
            set_n = input()
            n = float(set_n)

            if n != int(n):
                handle_Expect(1)
                break
                
            n = int(n)

            if n < 0 or n > 50:
                handle_Expect(2)
                break
            
            while True:
                try:
                    guess_tp = input()
                    times = times + 1
                    guess = float(guess_tp)

                    if guess != int(guess):
                        handle_Expect(1)
                        continue

                    guess = int(guess)

                    if guess < 0 or guess > 50:
                        handle_Expect(2)
                        continue

                    if guess < n:
                        handle_Expect(3)
                    elif guess > n:
                        handle_Expect(4)
                    elif guess == n:
                        handle_Expect(5)
                        break

                except ValueError:
                    handle_Expect(0)
                except EOFError:
                    break

        except ValueError:
            handle_Expect(0)
        finally:
            break

finally:
    print(f"You guessed {times} times.")