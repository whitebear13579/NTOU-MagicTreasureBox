import tkinter as tk
from tkinter import ttk

root = tk.Tk()
root.title("貨幣兌換小工具")
root.geometry("600x350")

frame = tk.Frame(root)
frame.pack(anchor="center")

# user input currency
tk.Label(frame, text="金　　額：", font=("Noto Sans TC", 14, "bold")).grid(
    row=1,
    column=0,
    pady=15
)
money_entry = tk.Entry(frame, font=("Noto Sans TC", 14))
money_entry.grid(row=1, column=1)

# orig currency
tk.Label(frame, text="原始貨幣：", font=("Noto Sans TC", 14, "bold")).grid(
    row=2,
    column=0,
    pady=10
)

orig_cur = ttk.Combobox(
    frame,
    values=["TWD", "USD", "JPY", "EUR"],
    state="readonly",
    font=("Noto Sans TC", 14)
)
orig_cur.current(0)
orig_cur.grid(row=2, column=1)

# targ currency
tk.Label(frame, text="目標貨幣：", font=("Noto Sans TC", 14, "bold")).grid(
    row=3,
    column=0,
    pady=10
)
targ_cur = ttk.Combobox(
    frame,
    values=["TWD", "USD", "JPY", "EUR"],
    state="readonly",
    font=("Noto Sans TC", 14)
)
targ_cur.current(1)
targ_cur.grid(row=3, column=1)

# result
result_label = tk.Label(frame, text="", font=("Noto Sans TC", 14))
result_label.grid(
    row=5,
    column=0,
    columnspan=2,
    pady=25
)

def currency_trans():
    trans_money = money_entry.get()

    if not trans_money:
        result_label.config(text="請輸入轉換金額！")
        return

    try:
        trans_money = float(trans_money)
        orig = orig_cur.get()
        targ = targ_cur.get()
        rates = {
            "TWD": 1.0,
            "USD": 31.0,
            "JPY": 0.22,
            "EUR": 34.0
        }
        trans_to_twd = trans_money * rates[orig]
        trans_to_res = trans_to_twd / rates[targ]
        result_label.config(text=f"結果：{trans_money:.2f} {orig} = {trans_to_res:.2f} {targ}")


    except ValueError:
        result_label.config(text="請輸入數字！")
        return

# calculate button
cal_btn = tk.Button(
    frame,
    text="開始換算",
    font=("Noto Sans TC", 14),
    command=currency_trans
)
cal_btn.grid(row=4, column=0, columnspan=2, pady=18)

root.mainloop()