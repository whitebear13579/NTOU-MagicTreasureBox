class Demo:
    class_var = "Class Var"

    def __init__(self, v):
        self.instance_var = v

    # 實例方法要 self
    def instance_method(self):
        return f"instance -> {self.instance_var}, class -> {self.class_var}"

    # 類別方法用 cls 操作類別層資料
    @classmethod
    def class_method(cls):
        return f"class -> {cls.class_var}"

    # 靜態方法不能碰 self/cls
    @staticmethod
    def static_method(x):
        return f"static -> {x}"
