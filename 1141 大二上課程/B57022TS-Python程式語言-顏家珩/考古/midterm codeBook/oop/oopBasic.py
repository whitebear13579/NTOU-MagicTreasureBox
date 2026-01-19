class Dog:
    # 類別屬性（所有實例共享）
    species = "Canis familiaris"

    # 建構子：建立實例屬性（各自獨立）
    def __init__(self, name, age):
        self.name = name
        self.age = age

    # 實例方法
    def bark(self):
        return f"{self.name} says Woof!"
