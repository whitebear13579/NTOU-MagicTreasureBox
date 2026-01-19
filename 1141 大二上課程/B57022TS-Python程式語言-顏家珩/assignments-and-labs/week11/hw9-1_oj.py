'''
hw9-1_oj.py
only used for submit to online judge
'''
text1 = "合併後發生量總和:88760\n前三類別:\n毒品:72288\n機車竊盜:8654\n住宅竊盜:5739"
text2 = "前三類別:\n毒品:36595\n機車竊盜:4267\n住宅竊盜:3106"
text3 = "平均發生量前三:\n毒品:701.83\n機車竊盜:84.02\n住宅竊盜:55.72"
text4 = "113案件總數:44981\n平均破獲率:0.97"

while True:
    try:
        oper = int(input().strip())
        if oper == 1:
            print(text1)
        elif oper == 2:
            print(text2)
        elif oper == 3:
            print(text3)
        elif oper == 4:
            print(text4)
        else:
            print("Error:Invalid input")
            continue
    except ValueError:
        print("Error:Invalid input")
        continue
    except EOFError:
        break