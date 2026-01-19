'''

hw 7-2 Nested String Expander
給定一段壓縮後的字串，請將它完全展開成原始內容。
壓縮字串的規則如下：
數字 k 表示接下來括號 () 中的內容要重複 k 次。
括號內可以包含：
一般字母（a–z、A–Z）
其他巢狀結構（可再包含數字與括號）
數字只會出現在括號前，且所有括號都會正確配對。
請輸出展開後的完整字串。
輸入說明
輸入為一行字串（長度不超過 200 個字元），符合上述壓縮格式。
輸出說明
輸出展開後的結果字串。
#input
3(ab2(c))
#output
abccabccabcc

'''


class stack:
    def __init__(self):
        self.items = []

    def is_empty(self):
        return self.items == []

    def push(self, item):
        self.items.append(item)

    def pop(self):
        if self.is_empty():
            return
        return self.items.pop()

    def top(self):
        if self.is_empty():
            return
        return self.items[-1]

    def size(self):
        return len(self.items)

uwu = stack()
string = input()

now_str = ""
now_num = ""

for i in string:
    if i.isdigit():
        now_num += i
    elif i == "(":
        uwu.push(now_str)
        if int(now_num) > 1:
            uwu.push(int(now_num))
            now_str = ""
            now_num = ""
    elif i == ")":
        rep = uwu.pop()
        last_str = uwu.pop()
        now_str = last_str + now_str * rep
    else:
        now_str += i

print(now_str)