import tkinter as tk
from tkinter import ttk

root = tk.Tk()
root.title("記帳小工具")
root.geometry("400x450")

frame = tk.Frame(root)
frame.pack(anchor="center")

total = 0.00

total_price = tk.Label(frame, text=f"總金額：{total:.2f} 元", font=("Noto Sans TC", 12))
total_price.grid(
    row=4,
    column=0,
    columnspan=3,
    sticky="w",
    pady=5,
)

def add_item():
    item_name = item_entry.get()
    item_price = price_entry.get()
    if item_name and item_price:
        global total
        item_price = (float)(item_price)
        listbox.insert(tk.END, f"{item_name} - {item_price:.2f} 元")
        total += item_price
        total_price.config(text=f"總金額：{total:.2f} 元")
        item_entry.delete(0, tk.END)
        price_entry.delete(0, tk.END)

def del_item():
    selected_indices = listbox.curselection()
    global total
    for index in reversed(selected_indices):
        item_text = listbox.get(index)
        item_price = float(item_text.split(" - ")[1].replace(" 元", ""))
        total -= item_price
        listbox.delete(index)
    total_price.config(text=f"總金額：{total:.2f} 元")

def clear_items():
    listbox.delete(0, tk.END)
    global total
    total = 0.00
    total_price.config(text=f"總金額：{total:.2f} 元")

# item entry & add
item = tk.StringVar()
tk.Label(frame, text="品　　項：", font=("Noto Sans TC", 12)).grid(
    row=1,
    column=0,
)

item_entry = tk.Entry(frame, font=("Noto Sans TC", 12))
item_entry.grid(row=1, column=1, columnspan=2)

add_button = tk.Button(
    frame,
    text="新　　增",
    font=("Noto Sans TC", 12),
    command=add_item
)
add_button.grid(row=1, column=3, padx=10, pady=10)

# price & delete
price = tk.StringVar()
tk.Label(frame, text="金　　額：", font=("Noto Sans TC", 12)).grid(
    row=2,
    column=0,
)

price_entry = tk.Entry(frame, font=("Noto Sans TC", 12))
price_entry.grid(row=2, column=1, columnspan=2)

del_button = tk.Button(
    frame,
    text="刪除選取",
    font=("Noto Sans TC", 12),
    command=del_item
)
del_button.grid(row=2, column=3, padx=10, pady=10)

# listbox & clear
listbox = tk.Listbox(frame, width=30, height=10, font=("Noto Sans TC", 12))
listbox.grid(row=3, column=0, columnspan=3, pady=10)

clear_button = tk.Button(
    frame,
    text="清　　空",
    font=("Noto Sans TC", 12),
    command=clear_items
)
clear_button.grid(row=3, column=3, pady=10)

root.mainloop()