# 寫檔
with open('out.txt','w') as f:
    f.write("Hello\n")

# 讀檔
with open('in.txt','r') as f:
    content = f.read()
    print(content)
