'''

lab 5-1 Rewriting “The Three Little Pigs”
three_pigs.txt是三隻小豬的故事。
使用with open指令讀取three_pigs.txt，請將故事內容進行改
寫，把文字進行替換。
(with open指令可加上encoding=“utf-8”，避免文字亂碼)
文曲取代對照表如下：
豬->羊
豬小弟->喜羊羊
豬二哥->沸羊羊
豬大哥->懶羊羊
將修改後的內容另存為three_sheep.txt，再於檔案結尾加入
本文由 學號 姓名 所完成 (使用with open的追加模式加入)

'''

my_name = "\n\n本文由 01357101 黃翊宏 所完成\n"

three_pig = ""

with open('./three_pigs.txt', 'r', encoding='utf-8') as in_file:
    three_pig = in_file.read()

three_pig = three_pig.replace("豬大哥","懶羊羊")
three_pig = three_pig.replace("豬二哥","沸羊羊")
three_pig = three_pig.replace("豬小弟","喜羊羊")
three_pig = three_pig.replace("豬","羊")

with open('./three_sheep.txt', 'x', encoding='utf-8') as out_file:
    out_file.write(three_pig)
    out_file.write(my_name)
