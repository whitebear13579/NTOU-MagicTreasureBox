import tkinter as tk
from tkinter import ttk

root = tk.Tk()
root.title("生活健康狀況問卷")
root.geometry("400x450")

frame = tk.Frame(root)
frame.pack(anchor="center")

# survey title
tk.Label(frame, text="生活健康狀況問卷", font=("Noto Sans TC", 16, "bold")).grid(
    row=1,
    column=0,
    columnspan=3,
    pady=15
)

# question 1
question1 = tk.BooleanVar(value=True)
tk.Label(frame, text="1.　請問是否有抽菸習慣", font=("Noto Sans TC", 12)).grid(
    row=2,
    column=0,
    pady=10
)

question1_yes = tk.Radiobutton(
    frame,
    text="是",
    variable=question1,
    value=True,
    font=("Noto Sans TC", 12)
)
question1_yes.grid(row=2, column=1,)

question1_no = tk.Radiobutton(
    frame,
    text="否",
    variable=question1,
    value=False,
    font=("Noto Sans TC", 12)
)
question1_no.grid(row=2, column=2)

# question 2
question2 = tk.BooleanVar(value=True)
tk.Label(frame, text="2.　請問是否有飲酒習慣", font=("Noto Sans TC", 12)).grid(
    row=3,
    column=0,
    pady=10
)

question2_yes = tk.Radiobutton(
    frame,
    text="是",
    variable=question2,
    value=True,
    font=("Noto Sans TC", 12)
)
question2_yes.grid(row=3, column=1)

question2_no = tk.Radiobutton(
    frame,
    text="否",
    variable=question2,
    value=False,
    font=("Noto Sans TC", 12)
)
question2_no.grid(row=3, column=2)

# question 3
question3 = tk.BooleanVar(value=True)
tk.Label(frame, text="3.　每天睡眠時間是否超過六小時", font=("Noto Sans TC", 12)).grid(
    row=4,
    column=0,
    pady=10
)

question3_yes = tk.Radiobutton(
    frame,
    text="是",
    variable=question3,
    value=True,
    font=("Noto Sans TC", 12)
)
question3_yes.grid(row=4, column=1)

question3_no = tk.Radiobutton(
    frame,
    text="否",
    variable=question3,
    value=False,
    font=("Noto Sans TC", 12)
)
question3_no.grid(row=4, column=2)


# question 4
question4 = tk.BooleanVar(value=True)
tk.Label(frame, text="4 .　是否有均衡飲食", font=("Noto Sans TC", 12)).grid(
    row=5,
    column=0,
    pady=10
)

question4_yes = tk.Radiobutton(
    frame,
    text="是",
    variable=question4,
    value=True,
    font=("Noto Sans TC", 12)
)
question4_yes.grid(row=5, column=1)

question4_no = tk.Radiobutton(
    frame,
    text="否",
    variable=question4,
    value=False,
    font=("Noto Sans TC", 12)
)
question4_no.grid(row=5, column=2)

# result
result_label = tk.Label(frame, text="", font=("Noto Sans TC", 14))
result_label.grid(
    row=7,
    column=0,
    columnspan=3,
    pady=25
)

def get_survey_result():
    score = 0
    if not question1.get():
        score += 1

    if not question2.get():
        score += 1
    
    if question3.get():
        score += 1  
    
    if question4.get():
        score += 1

    tips = ""
    if score >= 3:
        tips += "健康狀況良好"
    else:
        tips += "健康狀況不好"

    result_label.config(text=f"您的總分為：{score}\n健康狀況：{tips}")



# calculate button
cal_btn = tk.Button(
    frame,
    text="送出問卷並顯示結果",
    font=("Noto Sans TC", 14),
    command=get_survey_result
)
cal_btn.grid(row=6, column=0, columnspan=3, pady=18)

root.mainloop()