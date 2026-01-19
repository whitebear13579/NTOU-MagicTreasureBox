import tkinter as tk

root = tk.Tk()
root.title("BMI 計算機")
root.geometry("400x250")

# Result
result_label = tk.Label(root, text="", font=("Noto Sans TC", 14), justify=tk.LEFT)
result_label.grid(
    row=4,
    sticky="w",
    column=0,
    columnspan=2
)

def get_bmi():
    user_height = height_entry.get()
    user_weight = weight_entry.get()
    if not user_height or not user_weight:
        result_label.config(text="請輸入身高和體重資料！")
        return
    else:
        user_height = float(user_height)/100.0
        bmi = float(user_weight)/(user_height*user_height)
        min_weight = 18.5 * (user_height * user_height)
        max_weight = 24.0 * (user_height * user_height)
        result_label.config(text=f"您的 BMI：{bmi:.2f}\n健康體重範圍：{min_weight:.1f} kg ~ {max_weight:.1f} kg")

# Height
tk.Label(root, text="身高 （公分）：", font=("Noto Sans TC", 14, "bold")).grid(
    row=1,
    column=0
)
height_entry = tk.Entry(root, font=("Noto Sans TC", 14))
height_entry.grid(row=1, column=1)

# Weight
tk.Label(root, text="體重 （公斤）：", font=("Noto Sans TC", 14, "bold")).grid(
    row=2,
    column=0
)
weight_entry = tk.Entry(root, font=("Noto Sans TC", 14))
weight_entry.grid(row=2, column=1)


# calculate button  
button_frame = tk.Frame(root)
button_frame.grid(row=3, column=0, columnspan=2)
cal_btn = tk.Button(
    button_frame,
    text="計算 BMI",
    font=("Noto Sans TC", 14),
    command=get_bmi
)
cal_btn.pack(side=tk.LEFT, pady=10)

root.mainloop()