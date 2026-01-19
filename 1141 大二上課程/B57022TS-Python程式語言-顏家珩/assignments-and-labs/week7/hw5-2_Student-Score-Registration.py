'''

hw 5-2 Student Score Registration
題目說明
請撰寫一個程式，登記多筆學生的姓名與分數資料。
每一行包含姓名與分數，並以逗號分隔。
程式需能檢查輸入格式與分數是否合法，並針對每筆資料輸出對應訊息：
若輸入正確合法，印出 OK。
若格式或分數錯誤，印出對應的英文錯誤訊息。
最後，程式需計算所有合法分數的平均值（四捨五入至小數點第 2 位）。
若沒有任何合法資料，則輸出 No valid scores。
輸入說明
第一行輸入一個整數 N，代表資料筆數。
接下來輸入 N 行，每行為：
姓名,分數
姓名為字串（不可包含逗號）。
分數為整數，且需介於 0 ~ 100 之間。
輸出說明
可能的狀況與輸出內容
欄位數不等於 2（例如多逗號或缺欄位）->Error: invalid data format
分數不是整數、或超出範圍->Error: invalid score
格式與分數皆正確->OK
所有資料皆無效->No valid scores
有效資料存在->Average score: xx.xx（取小數點第 2 位）
#input
6
A,95
B,abc
C,90,extra
D,110
E,88
F,100
#output
OK
Error: invalid score
Error: invalid data format
Error: invalid score
OK
OK
Average score: 94.33

'''

def oaouwuouo030qq ( n ):
    out = ""
    if n == 0:
        out += "OK"
    elif n == 1:
        out += "Error: invalid data format"
    elif n == 2:
        out += "Error: invalid score"
    elif n == 3:
        out += "No valid scores"
    print(out)

n = int(input())
many = 0
total_score = 0

for i in range(n):
    data_in = input()
    fileds = data_in.split(',')

    if len(fileds) != 2 or not fileds[0] or not fileds[1]:
        oaouwuouo030qq(1)
        continue
    
    score_str = fileds[1]

    try:
        score = float(score_str)
        if score != int(score):
            oaouwuouo030qq(2)
            continue

        score = int(score)

        if score < 0 or score > 100:
            oaouwuouo030qq(2)
            continue

        many += 1
        total_score += score
        oaouwuouo030qq(0)

    except ValueError:
        oaouwuouo030qq(2)
        continue

if many > 0:
    avg = total_score/many
    print(f"Average score: {avg:.2f}")
else:
    oaouwuouo030qq(3)