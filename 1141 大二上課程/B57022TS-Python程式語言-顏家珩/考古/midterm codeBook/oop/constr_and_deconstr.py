class sampleClass:
    def __init__(self):
        # init 在物件創建時就會執行
        # 除了 self 之外 init 要接多少參數就要傳多少過去
        print("Resource allocated")
    def __del__(self):
        # del 在物件被銷毀前執行
        print("Resource released")

obj = sampleClass()  # 建立物件時會呼叫 __init__
del obj              # 刪除物件時會呼叫 __del__
