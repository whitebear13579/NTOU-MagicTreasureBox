class Animal:
    def __init__(self, name, age):
        self.name, self.age = name, age
    def make_sound(self): return "Some generic sound"
    def info(self): return f"{self.name} is {self.age} years old"

class Dog(Animal):
    def __init__(self, name, age, breed):
        super().__init__(name, age)  # 呼叫父類建構子
        self.breed = breed
    def make_sound(self): return "Woof! Woof!"  # 覆寫
    def fetch(self): return f"{self.name} fetching!"

class Cat(Animal):
    def __init__(self, name, age, color):
        super().__init__(name, age)  # 呼叫父類建構子
        self.color = color

    def make_sound(self): return "Meow! Meow!"  # 覆寫
    def scratch(self): return f"{self.name} is scratching!"

dog = Dog("Buddy", 3, "Golden Retriever")
cat = Cat("Whiskers", 2, "Gray")
